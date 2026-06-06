using BaseLib.Utils;
using FF14Combo.FF14ComboCode.CardPools;
using FF14Combo.FF14ComboCode.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;

namespace FF14Combo.FF14ComboCode.Cards;

[Pool(typeof(KnightCardPool))]
public class JustBeatItCard() : FF14ComboCard(0, CardType.Skill, CardRarity.Token, TargetType.Self)
{
    private const int ComboCost = 3;
    private const int VigorAmount = 6;

    public override IEnumerable<CardKeyword> CanonicalKeywords => [CardKeyword.Retain];

    protected override IEnumerable<IHoverTip> ExtraHoverTips =>
        [
            HoverTipFactory.FromPower<ComboPower>(),
            HoverTipFactory.FromPower<VigorPower>()
        ];

    protected override bool IsPlayable =>
        Owner?.Creature.GetPowerAmount<ComboPower>() >= ComboCost;

    protected override bool ShouldGlowGoldInternal => IsPlayable;

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play)
    {
        var combo = Owner.Creature.GetPower<ComboPower>();
        if (combo == null || combo.Amount < ComboCost)
        {
            return;
        }

        await PowerCmd.ModifyAmount(combo, -ComboCost, Owner.Creature, this);
        await CommonActions.ApplySelf<VigorPower>(choiceContext, this, VigorAmount, false);
    }
}
