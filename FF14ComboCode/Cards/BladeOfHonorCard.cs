using BaseLib.Utils;
using FF14Combo.FF14ComboCode.CardPools;
using FF14Combo.FF14ComboCode.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;

namespace FF14Combo.FF14ComboCode.Cards;

[Pool(typeof(KnightCardPool))]
public class BladeOfHonorCard() : FF14ComboCard(2, CardType.Attack, CardRarity.Token, TargetType.AllEnemies)
{
    public override bool CanBeGeneratedInCombat => false;
    public override bool CanBeGeneratedByModifiers => false;
    public override IEnumerable<CardKeyword> CanonicalKeywords => [CardKeyword.Retain];

    protected override IEnumerable<DynamicVar> CanonicalVars =>
        [new DamageVar(20, ValueProp.Move)];

    protected override IEnumerable<IHoverTip> ExtraHoverTips =>
        [HoverTipFactory.FromPower<RequiescatPrayerPower>()];

    protected override bool IsPlayable =>
        Owner?.Creature.GetPowerAmount<RequiescatPrayerPower>() > 0;

    protected override bool ShouldGlowGoldInternal => IsPlayable;

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play)
    {
        await DamageCmd.Attack(DynamicVars.Damage.BaseValue)
            .FromCard(this)
            .TargetingAllOpponents(CombatState)
            .Execute(choiceContext);

        await PowerCmd.Remove<RequiescatPrayerPower>(Owner.Creature);
    }

    protected override PileType GetResultPileType()
    {
        return PileType.None;
    }
}
