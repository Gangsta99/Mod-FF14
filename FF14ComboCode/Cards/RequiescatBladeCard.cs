using BaseLib.Utils;
using FF14Combo.FF14ComboCode.CardPools;
using FF14Combo.FF14ComboCode.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Nodes.CommonUi;
using MegaCrit.Sts2.Core.ValueProps;

namespace FF14Combo.FF14ComboCode.Cards;

public abstract class RequiescatBladeCard<TNext>(int cost, decimal targetDamage, decimal otherDamage) :
    FF14ComboCard(cost, CardType.Attack, CardRarity.Token, TargetType.AnyEnemy)
    where TNext : CardModel
{
    private const string OtherDamageKey = "OtherDamage";

    public override bool CanBeGeneratedInCombat => false;
    public override bool CanBeGeneratedByModifiers => false;
    public override IEnumerable<CardKeyword> CanonicalKeywords => [CardKeyword.Retain];

    protected override IEnumerable<DynamicVar> CanonicalVars =>
        [
            new DamageVar(targetDamage, ValueProp.Move),
            new DamageVar(OtherDamageKey, otherDamage, ValueProp.Move)
        ];

    protected override IEnumerable<IHoverTip> ExtraHoverTips =>
        [HoverTipFactory.FromPower<RequiescatPrayerPower>()];

    protected override bool IsPlayable =>
        Owner?.Creature.GetPowerAmount<RequiescatPrayerPower>() > 0;

    protected override bool ShouldGlowGoldInternal => IsPlayable;

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play)
    {
        await CommonActions.CardAttack(this, play).Execute(choiceContext);

        IReadOnlyList<Creature> otherEnemies = CombatState.HittableEnemies
            .Where(enemy => enemy != play.Target)
            .ToList();

        if (otherEnemies.Count > 0)
        {
            await CreatureCmd.Damage(
                choiceContext,
                otherEnemies,
                DynamicVars[OtherDamageKey].BaseValue,
                ValueProp.Move,
                Owner.Creature,
                this);
        }
    }

    public override (PileType, CardPilePosition) ModifyCardPlayResultPileTypeAndPosition(
        CardModel card,
        bool isAutoPlay,
        ResourceInfo resources,
        PileType pileType,
        CardPilePosition position)
    {
        return card == this
            ? (PileType.Draw, CardPilePosition.Top)
            : (pileType, position);
    }

    public override async Task AfterCardChangedPiles(CardModel card, PileType oldPileType, AbstractModel source)
    {
        if (card == this && oldPileType == PileType.Play && Pile?.Type == PileType.Draw)
        {
            await CardCmd.TransformTo<TNext>(this, CardPreviewStyle.None);
        }
    }
}
