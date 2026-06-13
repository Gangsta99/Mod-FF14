using BaseLib.Utils;
using FF14Combo.FF14ComboCode.CardPools;

namespace FF14Combo.FF14ComboCode.Cards;

[Pool(typeof(KnightCardPool))]
public class BladeOfValorCard() : RequiescatBladeCard<BladeOfHonorCard>(2, 15, 9);
