using BaseLib.Utils;
using FF14Combo.FF14ComboCode.CardPools;

namespace FF14Combo.FF14ComboCode.Cards;

[Pool(typeof(KnightCardPool))]
public class BladeOfTruthCard() : RequiescatBladeCard<BladeOfValorCard>(1, 8, 5);
