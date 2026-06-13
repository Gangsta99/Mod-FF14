using BaseLib.Utils;
using FF14Combo.FF14ComboCode.CardPools;
using FF14Combo.FF14ComboCode.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.Nodes.CommonUi;
using MegaCrit.Sts2.Core.ValueProps;

namespace FF14Combo.FF14ComboCode.Cards;

[Pool(typeof(KnightCardPool))]
public class RequiescatCard() : FF14ComboCard(3, CardType.Attack, CardRarity.Rare, TargetType.AllEnemies)
{
    protected override IEnumerable<DynamicVar> CanonicalVars =>
        [
            new DamageVar(13, ValueProp.Move),
            new PowerVar<WeakPower>(2),
            new PowerVar<VulnerablePower>(2),
            new PowerVar<RequiescatPrayerPower>(5)
        ];

    protected override IEnumerable<IHoverTip> ExtraHoverTips =>
        [
            HoverTipFactory.FromPower<WeakPower>(),
            HoverTipFactory.FromPower<VulnerablePower>(),
            HoverTipFactory.FromPower<RequiescatPrayerPower>(),
            HoverTipFactory.FromCard<BladeOfFaithCard>()
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

        await PowerCmd.Apply<VulnerablePower>(
            CombatState.HittableEnemies,
            DynamicVars.Vulnerable.BaseValue,
            Owner.Creature,
            this);

        await CommonActions.ApplySelf<RequiescatPrayerPower>(
            choiceContext,
            this,
            DynamicVars[nameof(RequiescatPrayerPower)].BaseValue,
            false);
    }

    public override (PileType, CardPilePosition) ModifyCardPlayResultPileTypeAndPosition(
        CardModel card,
        bool isAutoPlay,
        ResourceInfo resources,
        PileType pileType,
        CardPilePosition position)
    {
        return card == this
            ? (PileType.Hand, CardPilePosition.Top)
            : (pileType, position);
    }

    public override async Task AfterCardChangedPiles(CardModel card, PileType oldPileType, AbstractModel source)
    {
        if (card == this && oldPileType == PileType.Play && Pile?.Type == PileType.Hand)
        {
            await CardCmd.TransformTo<BladeOfFaithCard>(this, CardPreviewStyle.None);
        }
    }
}
