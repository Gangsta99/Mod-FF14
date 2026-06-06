using BaseLib.Abstracts;
using Godot;

namespace FF14Combo.FF14ComboCode.CardPools;

public class KnightCardPool : CustomCardPoolModel
{
    public override string Title => "knight";
    public override string EnergyColorName => "ironclad";
    public override Color DeckEntryCardColor => new("D62000");
    public override Color EnergyOutlineColor => new("802020");
    public override bool IsColorless => false;
}
