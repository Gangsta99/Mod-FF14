using BaseLib.Utils;
using FF14Combo.FF14ComboCode.CardPools;
using FF14Combo.FF14ComboCode.Powers;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;

namespace FF14Combo.FF14ComboCode.Cards;

[Pool(typeof(KnightCardPool))]
public class GoringBladeCard() : FF14ComboCard(2, CardType.Attack, CardRarity.Common, TargetType.AnyEnemy)
{
    public override IEnumerable<CardKeyword> CanonicalKeywords => [FF14ComboKeywords.Bleeding];

    protected override IEnumerable<DynamicVar> CanonicalVars =>
        [
            new DamageVar(8, ValueProp.Move),
            new PowerVar<BleedingPower>(3)
        ];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play)
    {
        await CommonActions.CardAttack(this, play).Execute(choiceContext);
    }
}
