using BaseLib.Utils;
using FF14Combo.FF14ComboCode.CardPools;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.ValueProps;

namespace FF14Combo.FF14ComboCode.Cards;

[Pool(typeof(KnightCardPool))]
public class StrikeKnightCard() : FF14ComboCard(1, CardType.Attack, CardRarity.Basic, TargetType.AnyEnemy)
{
    private static CardModel NativeStrike => ModelDb.Card<StrikeIronclad>();

    public override string CustomPortraitPath => NativeStrike.PortraitPath;
    public override string PortraitPath => NativeStrike.PortraitPath;
    public override string BetaPortraitPath => NativeStrike.BetaPortraitPath;

    protected override HashSet<CardTag> CanonicalTags => [CardTag.Strike];
    protected override IEnumerable<DynamicVar> CanonicalVars => [new DamageVar(6, ValueProp.Move)];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play)
    {
        await DamageCmd.Attack(DynamicVars.Damage.BaseValue)
            .FromCard(this)
            .Targeting(play.Target)
            .WithHitFx("vfx/vfx_attack_slash")
            .Execute(choiceContext);
    }

    protected override void OnUpgrade()
    {
        DynamicVars.Damage.UpgradeValueBy(3);
    }
}
