# FF14Combo Asset Requirements

This document defines the image files needed by the mod. Keep filenames exact: the code derives asset paths from C# class names.

## General Rules

- Format: PNG.
- Background: transparent when possible.
- Avoid text inside artwork.
- Keep the main subject readable at small size.
- Use clear silhouettes and high contrast.
- Keep important details away from the card edges.
- Do not rename files unless the matching C# class name changes too.

## Card Portraits

Each card needs two files with the same filename:

```text
FF14Combo/images/card_portraits/<file_name>.png
FF14Combo/images/card_portraits/big/<file_name>.png
```

Recommended sizes:

```text
Small card portrait: 250 x 190 px
Big card portrait:   500 x 380 px
High quality big:    1000 x 760 px
```

Current and planned card filenames:

```text
opener_card.png
continuation_slash_card.png
finisher_card.png
damage_boost_card.png
positioning_card.png
```

The file naming rule is:

```text
OpenerCard -> opener_card.png
ContinuationSlashCard -> continuation_slash_card.png
FinisherCard -> finisher_card.png
DamageBoostCard -> damage_boost_card.png
PositioningCard -> positioning_card.png
```

## Relic Icons

Relic images live here:

```text
FF14Combo/images/relics/
FF14Combo/images/relics/big/
```

Each relic needs a main icon and an outline icon:

```text
crystal_relic.png
crystal_relic_outline.png
advanced_crystal_relic.png
advanced_crystal_relic_outline.png
```

Use the current template files as size references:

```text
FF14Combo/images/relics/relic.png
FF14Combo/images/relics/relic_outline.png
FF14Combo/images/relics/big/relic.png
```

## Power Icons

Power images live here:

```text
FF14Combo/images/powers/
FF14Combo/images/powers/big/
```

Planned power filenames:

```text
vigor_power.png
single_combo_power.png
aoe_combo_power.png
```

Use the current template files as size references:

```text
FF14Combo/images/powers/power.png
FF14Combo/images/powers/big/power.png
```

## Visual Direction

Suggested themes:

```text
Opener: first strike, drawn sword, blue-white blade light
Continuation Slash: flowing follow-up slash, connected blade trails
Finisher: decisive final cut, strong horizontal slash
Damage Boost: red-gold energy gathering around a weapon
Positioning: side-step, flank angle, exposed enemy opening
Crystal: dim crystal pattern
Advanced Crystal: glowing crystal pattern
Vigor: rising attack energy
Single Combo: single-sword combo marker
AOE Combo: circular slash wave or area strike pattern
```

## Current Placeholder Policy

During prototype work, placeholder images are acceptable. Final art should be added after the matching card, relic, or power behavior is stable.
