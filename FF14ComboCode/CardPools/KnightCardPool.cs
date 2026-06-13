using BaseLib.Abstracts;
using Godot;

namespace FF14Combo.FF14ComboCode.CardPools;

public class KnightCardPool : CustomCardPoolModel
{
    public override string Title => "knight";
    public override string EnergyColorName => "ironclad";
    public override Color ShaderColor => new("87A6AC");
    public override Color DeckEntryCardColor => ShaderColor;
    public override Color EnergyOutlineColor => new("43565B");
    public override bool IsColorless => false;
}
