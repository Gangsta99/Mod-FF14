using BaseLib.Abstracts;
using FF14Combo.FF14ComboCode.CardPools;
using FF14Combo.FF14ComboCode.Cards;
using FF14Combo.FF14ComboCode.Extensions;
using FF14Combo.FF14ComboCode.Relics;
using Godot;
using MegaCrit.Sts2.Core.Entities.Characters;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Characters;
using MegaCrit.Sts2.Core.Models.PotionPools;
using MegaCrit.Sts2.Core.Models.RelicPools;
using MegaCrit.Sts2.Core.Nodes.Vfx;

namespace FF14Combo.FF14ComboCode.Characters;

public class Knight : CustomCharacterModel
{
    private static Ironclad Ironclad => ModelDb.Character<Ironclad>();

    public override CharacterGender Gender => Ironclad.Gender;
    public override Color NameColor => Ironclad.NameColor;
    public override int StartingHp => Ironclad.StartingHp;
    public override CardPoolModel CardPool => ModelDb.CardPool<KnightCardPool>();
    public override PotionPoolModel PotionPool => Ironclad.PotionPool;
    public override RelicPoolModel RelicPool => Ironclad.RelicPool;

    public override IEnumerable<CardModel> StartingDeck =>
        Ironclad.StartingDeck.Concat([
            ModelDb.Card<FastBladeCard>(),
            ModelDb.Card<RiotBladeCard>(),
            ModelDb.Card<RageOfHaloneCard>()
        ]);

    public override IReadOnlyList<RelicModel> StartingRelics =>
        [ModelDb.Relic<SoulOfThePaladin>()];

    public override Color EnergyLabelOutlineColor => Ironclad.EnergyLabelOutlineColor;
    public override Color DialogueColor => Ironclad.DialogueColor;
    public override VfxColor SpeechBubbleColor => Ironclad.SpeechBubbleColor;
    public override Color MapDrawingColor => Ironclad.MapDrawingColor;
    public override Color RemoteTargetingLineColor => Ironclad.RemoteTargetingLineColor;
    public override Color RemoteTargetingLineOutline => Ironclad.RemoteTargetingLineOutline;

    public override string CustomVisualPath => SceneHelper.GetScenePath("creature_visuals/ironclad");
    public override string CustomTrailPath => SceneHelper.GetScenePath("vfx/card_trail_ironclad");
    public override string CustomIconTexturePath => "knight.png".CharacterUiPath();
    public override string CustomIconOutlineTexturePath => ImageHelper.GetImagePath("ui/top_panel/character_icon_ironclad_outline.png");
    public override string CustomIconPath => SceneHelper.GetScenePath("ui/character_icons/ironclad_icon");
    public override string CustomEnergyCounterPath => SceneHelper.GetScenePath("combat/energy_counters/ironclad_energy_counter");
    public override string CustomRestSiteAnimPath => SceneHelper.GetScenePath("rest_site/characters/ironclad_rest_site");
    public override string CustomMerchantAnimPath => SceneHelper.GetScenePath("merchant/characters/ironclad_merchant");
    public override string CustomArmPointingTexturePath => ImageHelper.GetImagePath("ui/hands/multiplayer_hand_ironclad_point.png");
    public override string CustomArmRockTexturePath => ImageHelper.GetImagePath("ui/hands/multiplayer_hand_ironclad_rock.png");
    public override string CustomArmPaperTexturePath => ImageHelper.GetImagePath("ui/hands/multiplayer_hand_ironclad_paper.png");
    public override string CustomArmScissorsTexturePath => ImageHelper.GetImagePath("ui/hands/multiplayer_hand_ironclad_scissors.png");
    public override string CustomCharacterSelectBg => $"{MainFile.ResPath}/scenes/character_select/knight_bg.tscn";
    public override string CustomCharacterSelectIconPath => "knight.png".CharacterUiPath();
    public override string CustomCharacterSelectLockedIconPath => "knight.png".CharacterUiPath();
    public override string CustomCharacterSelectTransitionPath => "res://materials/transitions/ironclad_transition_mat.tres";
    public override string CustomMapMarkerPath => ImageHelper.GetImagePath("packed/map/icons/map_marker_ironclad.png");
    public override string CustomAttackSfx => Ironclad.AttackSfx;
    public override string CustomCastSfx => Ironclad.CastSfx;
    public override string CustomDeathSfx => Ironclad.DeathSfx;
    public override string CharacterSelectSfx => Ironclad.CharacterSelectSfx;
    public override string CharacterTransitionSfx => Ironclad.CharacterTransitionSfx;

    public override List<string> GetArchitectAttackVfx()
    {
        return Ironclad.GetArchitectAttackVfx();
    }
}
