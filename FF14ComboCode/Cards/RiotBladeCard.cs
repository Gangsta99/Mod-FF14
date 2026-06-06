using BaseLib.Utils;
<<<<<<< HEAD
using FF14Combo.FF14ComboCode.CardPools;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
=======
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.CardPools;
>>>>>>> 8bcb888286f96e05c566463d647abd737a61aa92
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;

namespace FF14Combo.FF14ComboCode.Cards;

<<<<<<< HEAD
[Pool(typeof(KnightCardPool))]
=======
[Pool(typeof(IroncladCardPool))]
>>>>>>> 8bcb888286f96e05c566463d647abd737a61aa92
public class RiotBladeCard() : FF14ComboCard(1, CardType.Attack, CardRarity.Common, TargetType.AnyEnemy)
{
    public override IEnumerable<CardKeyword> CanonicalKeywords => [FF14ComboKeywords.Combo];
    protected override IEnumerable<DynamicVar> CanonicalVars => [new DamageVar(6, ValueProp.Move)];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play)
    {
        MainFile.Logger.Info("Riot Blade used.");
        await CommonActions.CardAttack(this, play).Execute(choiceContext);
        await CommonActions.ApplySelf<VigorPower>(choiceContext, this, 2, false);
        await ApplyComboKeywordEffect(choiceContext);
    }
}
