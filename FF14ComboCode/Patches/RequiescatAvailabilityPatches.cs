using FF14Combo.FF14ComboCode.Cards;
using HarmonyLib;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Factories;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Runs;

namespace FF14Combo.FF14ComboCode.Patches;

[HarmonyPatch(typeof(CardCreationOptions), nameof(CardCreationOptions.GetPossibleCards))]
public static class RequiescatRewardAvailabilityPatch
{
    public static void Postfix(CardCreationOptions __instance, Player player, ref IEnumerable<CardModel> __result)
    {
        bool isFirstActBossReward =
            __instance.Source == CardCreationSource.Encounter &&
            __instance.RarityOdds == CardRarityOddsType.BossEncounter &&
            player.RunState.CurrentActIndex == 0 &&
            !PileType.Deck.GetPile(player).Cards.Any(card => card is RequiescatCard);

        if (!isFirstActBossReward)
        {
            __result = __result.Where(card => card is not RequiescatCard);
        }
    }
}

[HarmonyPatch(
    typeof(CardFactory),
    nameof(CardFactory.CreateForMerchant),
    [typeof(Player), typeof(IEnumerable<CardModel>), typeof(CardType)])]
public static class RequiescatMerchantTypeAvailabilityPatch
{
    public static void Prefix(Player player, ref IEnumerable<CardModel> options)
    {
        if (player.RunState.CurrentActIndex != 0 || HasRequiescat(player))
        {
            options = options.Where(card => card is not RequiescatCard);
        }
    }

    private static bool HasRequiescat(Player player)
    {
        return PileType.Deck.GetPile(player).Cards.Any(card => card is RequiescatCard);
    }
}

[HarmonyPatch(
    typeof(CardFactory),
    nameof(CardFactory.CreateForMerchant),
    [typeof(Player), typeof(IEnumerable<CardModel>), typeof(CardRarity)])]
public static class RequiescatMerchantRarityAvailabilityPatch
{
    public static void Prefix(Player player, ref IEnumerable<CardModel> options)
    {
        if (player.RunState.CurrentActIndex != 0 ||
            PileType.Deck.GetPile(player).Cards.Any(card => card is RequiescatCard))
        {
            options = options.Where(card => card is not RequiescatCard);
        }
    }
}
