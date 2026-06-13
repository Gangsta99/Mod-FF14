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
public class DefendKnightCard() : FF14ComboCard(1, CardType.Skill, CardRarity.Basic, TargetType.Self)
{
    private static CardModel NativeDefend => ModelDb.Card<DefendIronclad>();

    public override string CustomPortraitPath => NativeDefend.PortraitPath;
    public override string PortraitPath => NativeDefend.PortraitPath;
    public override string BetaPortraitPath => NativeDefend.BetaPortraitPath;
    public override bool GainsBlock => true;

    protected override HashSet<CardTag> CanonicalTags => [CardTag.Defend];
    protected override IEnumerable<DynamicVar> CanonicalVars => [new BlockVar(5, ValueProp.Move)];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play)
    {
        await CreatureCmd.GainBlock(Owner.Creature, DynamicVars.Block, play);
    }

    protected override void OnUpgrade()
    {
        DynamicVars.Block.UpgradeValueBy(3);
    }
}
