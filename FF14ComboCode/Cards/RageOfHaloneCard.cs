using BaseLib.Utils;
using FF14Combo.FF14ComboCode.CardPools;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;

namespace FF14Combo.FF14ComboCode.Cards;

[Pool(typeof(KnightCardPool))]
public class RageOfHaloneCard() : FF14ComboCard(1, CardType.Attack, CardRarity.Basic, TargetType.AnyEnemy)
{
    public override IEnumerable<CardKeyword> CanonicalKeywords => [FF14ComboKeywords.Combo];

    protected override IEnumerable<DynamicVar> CanonicalVars =>
        [new DamageVar(8, ValueProp.Move)];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play)
    {
        await CommonActions.CardAttack(this, play).Execute(choiceContext);
        await ApplyComboKeywordEffect(choiceContext);
        await CommonActions.ApplySelf<VigorPower>(choiceContext, this, 4, false);
    }
}
