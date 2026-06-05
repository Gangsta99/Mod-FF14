using FF14Combo.FF14ComboCode.Cards;
using HarmonyLib;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Characters;

namespace FF14Combo.FF14ComboCode.Patches;

[HarmonyPatch(typeof(Ironclad), nameof(Ironclad.StartingDeck), MethodType.Getter)]
public static class IroncladStartingDeckPatch
{
    public static void Postfix(ref IEnumerable<CardModel> __result)
    {
        var deck = __result.ToList();
        deck.Add(ModelDb.Card<OpenerCard>());
        deck.Add(ModelDb.Card<FastBladeCard>());
        deck.Add(ModelDb.Card<RiotBladeCard>());
        __result = deck;

        MainFile.Logger.Info("Added Opener, Fast Blade, and Riot Blade to Ironclad starting deck.");
    }
}
