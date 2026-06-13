using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;

namespace FF14Combo.FF14ComboCode.Powers;

public class RedemptionSwordPower : FF14ComboPower
{
    private const string RetaliationDamageKey = "RetaliationDamage";

    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Single;
    public override bool IsInstanced => true;
    public override int DisplayAmount => (int)DynamicVars[RetaliationDamageKey].BaseValue;

    protected override IEnumerable<DynamicVar> CanonicalVars =>
        [new DamageVar(RetaliationDamageKey, 0, ValueProp.Unpowered)];

    public override Task AfterDamageGiven(
        PlayerChoiceContext choiceContext,
        Creature dealer,
        DamageResult result,
        ValueProp props,
        Creature target,
        CardModel cardSource)
    {
        if (!target.IsPlayer || target.Side != Owner.Side || result.UnblockedDamage <= 0)
        {
            return Task.CompletedTask;
        }

        DynamicVars[RetaliationDamageKey].BaseValue += result.UnblockedDamage;
        InvokeDisplayAmountChanged();
        return Task.CompletedTask;
    }

    public override async Task BeforeSideTurnStart(
        PlayerChoiceContext choiceContext,
        CombatSide side,
        CombatState combatState)
    {
        if (side != Owner.Side)
        {
            return;
        }

        var retaliationDamage = DynamicVars[RetaliationDamageKey].BaseValue;
        if (Target?.IsAlive == true && retaliationDamage > 0)
        {
            Flash();
            await CreatureCmd.Damage(
                choiceContext,
                Target,
                retaliationDamage,
                ValueProp.Unpowered,
                Owner,
                null);
        }

        await PowerCmd.Remove(this);
    }

    public override async Task AfterDeath(
        PlayerChoiceContext choiceContext,
        Creature creature,
        bool wasRemovalPrevented,
        float deathAnimLength)
    {
        if (!wasRemovalPrevented && creature == Target)
        {
            await PowerCmd.Remove(this);
        }
    }
}
