using BaseLib.Utils;
using FF14Combo.FF14ComboCode.CardPools;

namespace FF14Combo.FF14ComboCode.Cards;

[Pool(typeof(KnightCardPool))]
public class BladeOfFaithCard() : RequiescatBladeCard<BladeOfTruthCard>(1, 6, 3);
