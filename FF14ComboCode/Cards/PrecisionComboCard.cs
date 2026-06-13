using BaseLib.Utils;
using FF14Combo.FF14ComboCode.CardPools;
using FF14Combo.FF14ComboCode.Powers;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace FF14Combo.FF14ComboCode.Cards;

[Pool(typeof(KnightCardPool))]
public class PrecisionComboCard() : FF14ComboCard(1, CardType.Power, CardRarity.Common, TargetType.Self)
{
    public override IEnumerable<CardKeyword> CanonicalKeywords =>
        [
            FF14ComboKeywords.SingleTargetCombo,
            FF14ComboKeywords.AreaTargetCombo
        ];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play)
    {
        await CommonActions.ApplySelf<PrecisionComboPower>(choiceContext, this, 1, false);
    }
}
