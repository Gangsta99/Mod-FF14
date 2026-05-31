using BaseLib.Utils;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models.CardPools;

namespace FF14Combo.FF14ComboCode.Cards;

[Pool(typeof(IroncladCardPool))]
public class FF14TestCard() : FF14ComboCard(1, CardType.Skill, CardRarity.Common, TargetType.Self)
{
    protected override Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play)
    {
        MainFile.Logger.Info("FF14 Test Card played.");
        return Task.CompletedTask;
    }
}
