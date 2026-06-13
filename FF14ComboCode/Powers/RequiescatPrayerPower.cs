using FF14Combo.FF14ComboCode.Cards;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;

namespace FF14Combo.FF14ComboCode.Powers;

public class RequiescatPrayerPower : FF14ComboPower
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Counter;

    public override async Task AfterTurnEnd(PlayerChoiceContext choiceContext, CombatSide side)
    {
        if (side == Owner.Side)
        {
            await PowerCmd.TickDownDuration(this);
        }
    }

    public override async Task AfterRemoved(Creature oldOwner)
    {
        if (oldOwner.Player == null)
        {
            return;
        }

        IReadOnlyList<CardModel> remainingBlades =
            [
                .. PileType.Hand.GetPile(oldOwner.Player).Cards,
                .. PileType.Draw.GetPile(oldOwner.Player).Cards,
                .. PileType.Discard.GetPile(oldOwner.Player).Cards
            ];

        await CardPileCmd.RemoveFromCombat(remainingBlades.Where(card =>
            card is BladeOfFaithCard
                or BladeOfTruthCard
                or BladeOfValorCard
                or BladeOfHonorCard));
    }
}
