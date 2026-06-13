using FF14Combo.FF14ComboCode.Cards;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;

namespace FF14Combo.FF14ComboCode.Powers;

public class PrecisionComboPower : FF14ComboPower
{
    private class Data
    {
        public CardModel PreviousCard;
        public CardModel CardToBoost;
    }

    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Single;

    protected override object InitInternalData()
    {
        return new Data();
    }

    public override Task BeforeCardPlayed(CardPlay cardPlay)
    {
        if (cardPlay.Card.Owner?.Creature != Owner)
        {
            return Task.CompletedTask;
        }

        var data = GetInternalData<Data>();
        data.CardToBoost = IsComboStep(data.PreviousCard, cardPlay.Card)
            ? cardPlay.Card
            : null;

        if (data.CardToBoost != null)
        {
            Flash();
        }

        return Task.CompletedTask;
    }

    public override decimal ModifyDamageMultiplicative(
        Creature target,
        decimal amount,
        ValueProp props,
        Creature dealer,
        CardModel cardSource)
    {
        if (dealer != Owner || !props.IsPoweredAttack() || cardSource == null)
        {
            return 1m;
        }

        return cardSource == GetInternalData<Data>().CardToBoost ? 1.5m : 1m;
    }

    public override Task AfterCardPlayed(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        if (cardPlay.Card.Owner?.Creature != Owner)
        {
            return Task.CompletedTask;
        }

        var data = GetInternalData<Data>();
        data.PreviousCard = cardPlay.Card;
        data.CardToBoost = null;
        return Task.CompletedTask;
    }

    private static bool IsComboStep(CardModel previousCard, CardModel currentCard)
    {
        if (previousCard?.Type != CardType.Attack || currentCard.Type != CardType.Attack)
        {
            return false;
        }

        return (previousCard is FastBladeCard && currentCard is RiotBladeCard)
            || (previousCard is RiotBladeCard && currentCard is RageOfHaloneCard)
            || (previousCard.GetType().Name == "TotalEclipseCard" && currentCard.GetType().Name == "ProminenceCard");
    }
}
