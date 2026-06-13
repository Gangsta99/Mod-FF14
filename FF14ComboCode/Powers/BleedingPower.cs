using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.ValueProps;

namespace FF14Combo.FF14ComboCode.Powers;

public class BleedingPower : FF14ComboPower
{
    public override PowerType Type => PowerType.Debuff;
    public override PowerStackType StackType => PowerStackType.Counter;

    public override async Task AfterSideTurnStart(CombatSide side, CombatState combatState)
    {
        if (side != Owner.Side || !Owner.IsEnemy || Owner.IsDead)
        {
            return;
        }

        Flash();
        await CreatureCmd.Damage(
            new ThrowingPlayerChoiceContext(),
            Owner,
            Amount,
            ValueProp.Unblockable | ValueProp.Unpowered,
            null,
            null);
    }

    public override async Task AfterTurnEnd(PlayerChoiceContext choiceContext, CombatSide side)
    {
        if (side == Owner.Side && Owner.IsEnemy)
        {
            await PowerCmd.TickDownDuration(this);
        }
    }
}
