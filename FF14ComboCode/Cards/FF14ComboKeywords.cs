using BaseLib.Patches.Content;
using MegaCrit.Sts2.Core.Entities.Cards;

namespace FF14Combo.FF14ComboCode.Cards;

public static class FF14ComboKeywords
{
    [CustomEnum]
    [KeywordProperties(AutoKeywordPosition.None)]
    public static CardKeyword Combo;

    [CustomEnum]
    [KeywordProperties(AutoKeywordPosition.None)]
    public static CardKeyword Bleeding;

    [CustomEnum]
    [KeywordProperties(AutoKeywordPosition.None)]
    public static CardKeyword SingleTargetCombo;

    [CustomEnum]
    [KeywordProperties(AutoKeywordPosition.None)]
    public static CardKeyword AreaTargetCombo;
}
