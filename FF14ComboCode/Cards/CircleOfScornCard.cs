using BaseLib.Utils;
using FF14Combo.FF14ComboCode.CardPools;
using FF14Combo.FF14ComboCode.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;

namespace FF14Combo.FF14ComboCode.Cards;

[Pool(typeof(KnightCardPool))]
public class CircleOfScornCard() : FF14ComboCard(2, CardType.Attack, CardRarity.Rare, TargetType.AllEnemies)
{
    protected override IEnumerable<DynamicVar> CanonicalVars =>
        [
            new DamageVar(10, ValueProp.Move),
            new PowerVar<WeakPower>(1),
            new PowerVar<BleedingPower>(3)
        ];

    protected override IEnumerable<IHoverTip> ExtraHoverTips =>
        [
            HoverTipFactory.FromPower<WeakPower>(),
            HoverTipFactory.FromPower<BleedingPower>()
        ];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play)
    {
        await DamageCmd.Attack(DynamicVars.Damage.BaseValue)
            .FromCard(this)
            .TargetingAllOpponents(CombatState)
            .Execute(choiceContext);

        await PowerCmd.Apply<WeakPower>(
            CombatState.HittableEnemies,
            DynamicVars.Weak.BaseValue,
            Owner.Creature,
            this);

        await PowerCmd.Apply<BleedingPower>(
            CombatState.HittableEnemies,
            DynamicVars[nameof(BleedingPower)].BaseValue,
            Owner.Creature,
            this);
    }
}
