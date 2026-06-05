using BaseLib.Abstracts;
using BaseLib.Extensions;
using BaseLib.Utils;
using FF14Combo.FF14ComboCode.Extensions;
using FF14Combo.FF14ComboCode.Powers;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace FF14Combo.FF14ComboCode.Cards;

public abstract class FF14ComboCard(int cost, CardType type, CardRarity rarity, TargetType target) :
    CustomCardModel(cost, type, rarity, target)
{
    //Image size:
    //Normal art: 1000x760 (Using 500x380 should also work, it will simply be scaled.)
    //Full art: 606x852
    public override string CustomPortraitPath => $"{Id.Entry.RemovePrefix().ToLowerInvariant()}.png".BigCardImagePath();

    //Smaller variants of card images for efficiency:
    //Smaller variant of fullart: 250x350
    //Smaller variant of normalart: 250x190

    //Uses card_portraits/card_name.png as image path. These should be smaller images.
    public override string PortraitPath => $"{Id.Entry.RemovePrefix().ToLowerInvariant()}.png".CardImagePath();
    public override string BetaPortraitPath => $"beta/{Id.Entry.RemovePrefix().ToLowerInvariant()}.png".CardImagePath();

    protected async Task ApplyComboKeywordEffect(PlayerChoiceContext choiceContext)
    {
        if (Owner?.Creature != null && Keywords.Contains(FF14ComboKeywords.Combo))
        {
            await CommonActions.ApplySelf<ComboPower>(choiceContext, this, 1, false);
        }
    }
}
