using BaseLib.Utils;
using FF14Combo.FF14ComboCode.Cards;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.RelicPools;

namespace FF14Combo.FF14ComboCode.Relics;

[Pool(typeof(IroncladRelicPool))]
public class SoulOfThePaladin : FF14ComboRelic
{
    public override RelicRarity Rarity => RelicRarity.Starter;

    protected override IEnumerable<DynamicVar> CanonicalVars => [new CardsVar(2)];

    public override async Task BeforePlayPhaseStartLate(PlayerChoiceContext choiceContext, Player player)
    {
        if (player != Owner || player.Creature.CombatState.RoundNumber > 1)
        {
            return;
        }

        CardPile hand = PileType.Hand.GetPile(player);
        if (hand.Cards.Any(card => card.Keywords.Contains(FF14ComboKeywords.Combo)))
        {
            return;
        }

        Flash();
        await CardPileCmd.Draw(choiceContext, DynamicVars.Cards.BaseValue, player);
    }
}
