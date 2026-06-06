using BaseLib.Utils;
using FF14Combo.FF14ComboCode.CardPools;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;

namespace FF14Combo.FF14ComboCode.Cards;

[Pool(typeof(KnightCardPool))]
public class FightOrFightCard() : FF14ComboCard(2, CardType.Power, CardRarity.Uncommon, TargetType.Self)
{
    protected override IEnumerable<IHoverTip> ExtraHoverTips =>
        [HoverTipFactory.FromCard<JustBeatItCard>()];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play)
    {
        await CreatureCmd.TriggerAnim(Owner.Creature, "Cast", Owner.Character.CastAnimDelay);

        var card = CombatState?.CreateCard<JustBeatItCard>(Owner);
        if (card != null)
        {
            await CardPileCmd.AddGeneratedCardToCombat(card, PileType.Hand, addedByPlayer: true);
        }
    }
}
