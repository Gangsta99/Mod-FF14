using FF14Combo.FF14ComboCode.Cards;
using HarmonyLib;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Factories;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Runs;

namespace FF14Combo.FF14ComboCode.Patches;

[HarmonyPatch(typeof(CardCreationOptions), nameof(CardCreationOptions.GetPossibleCards))]
public static class RedemptionSwordRewardAvailabilityPatch
{
    public static void Postfix(CardCreationOptions __instance, ref IEnumerable<CardModel> __result)
    {
        if (__instance.Source == CardCreationSource.Encounter &&
            (__instance.RarityOdds == CardRarityOddsType.EliteEncounter ||
             __instance.RarityOdds == CardRarityOddsType.BossEncounter))
        {
            return;
        }

        __result = __result.Where(card => card is not RedemptionSwordCard);
    }
}

[HarmonyPatch(
    typeof(CardFactory),
    nameof(CardFactory.CreateForMerchant),
    [typeof(Player), typeof(IEnumerable<CardModel>), typeof(CardType)])]
public static class RedemptionSwordMerchantTypeAvailabilityPatch
{
    public static void Prefix(ref IEnumerable<CardModel> options)
    {
        options = options.Where(card => card is not RedemptionSwordCard);
    }
}

[HarmonyPatch(
    typeof(CardFactory),
    nameof(CardFactory.CreateForMerchant),
    [typeof(Player), typeof(IEnumerable<CardModel>), typeof(CardRarity)])]
public static class RedemptionSwordMerchantRarityAvailabilityPatch
{
    public static void Prefix(ref IEnumerable<CardModel> options)
    {
        options = options.Where(card => card is not RedemptionSwordCard);
    }
}
