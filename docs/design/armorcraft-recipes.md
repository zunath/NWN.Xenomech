# Armorcraft Recipes

**Source**: `docs/_incoming/XM Design Bible - Armorcraft.tsv`  
**Updated**: 2025-08-24  
**Status**: Component specifications incomplete - requires design work  

## Overview

The Armorcraft skill encompasses 221 defensive equipment recipes across 5 armor categories, ranging from level 1 to 100. This is the largest crafting discipline in the Xenomech system, specializing in protective equipment for all body slots.

### Quality Tiers

- **Normal**: Standard crafted items (221 recipes, 100% coverage)
- **HQ (High Quality)**: Enhanced versions with +1 suffix (207 recipes, 93.7% coverage)  
- **Ultra**: Premium versions (15 recipes, 6.8% coverage - highest after Weaponcraft)

### Implementation Status

⚠️ **CRITICAL**: All 221 recipes are missing component specifications. The Component 1-8 fields are empty across all recipes, making them non-implementable until the component system is designed and populated.

## Armor Categories

| Category | Count | Percentage | Level Range | Description |
|----------|-------|------------|-------------|-------------|
| Body | 71 | 32.1% | 2-100 | Torso armor, robes, harnesses |
| Head | 56 | 25.3% | 1-100 | Helmets, caps, headgear |
| Feet | 47 | 21.3% | 4-100 | Boots, shoes, footwear |
| Hands | 32 | 14.5% | 8-100 | Gloves, gauntlets, handwear |
| Shield | 15 | 6.8% | 5-100 | Defensive shields and barriers |

## Level Distribution

| Tier | Level Range | Recipe Count | Percentage | Notes |
|------|-------------|--------------|------------|-------|
| Early Game | 1-25 | 48 | 21.7% | Basic protective gear |
| Mid Game | 26-50 | 65 | 29.4% | Core defensive progression |
| Late Game | 51-75 | 72 | 32.6% | Advanced armor sets |
| End Game | 76-100 | 36 | 16.3% | Master-tier equipment |

## Data Quality Analysis

### Missing HQ Versions (14 recipes)

These recipes lack HQ (+1) variants:

| Level | Name | Category | Priority |
|-------|------|----------|----------|
| 6 | Abyssal Barrier | Shield | High |
| 14 | Shellback Aegis | Shield | Medium |
| 30 | Corroded Tunic | Body | Medium |
| 33 | Warforged Sabatons | Feet | Medium |
| 36 | Corroded Leggings | Feet | Medium |
| 48 | Corroded Breastplate | Body | Medium |
| 55 | Corroded Cap | Head | Medium |
| 57 | Corroded Gauntlets | Hands | Medium |
| 67 | Corroded Harness | Body | Medium |
| 78 | Corroded Mittens | Hands | Low |
| 82 | Corroded Helm | Head | Low |
| 84 | Corroded Shield | Shield | Low |
| 87 | Corroded Greathelm | Head | Low |
| 90 | Corroded Bracers | Hands | Low |

### Ultra Tier Equipment (15 recipes)

Premium armor pieces with special properties:

| Level | Name | Category | Ultra Result | Notes |
|-------|------|----------|--------------|-------|
| 77 | Vanguard Knight's Cuirass | Body | Vanguard Knight's Cuirass +2 | Elite heavy armor |
| 78 | Vanguard Knight's Helm | Head | Vanguard Knight's Helm +2 | Elite helmet |
| 79 | Vanguard Knight's Sabatons | Feet | Vanguard Knight's Sabatons +2 | Elite boots |
| 79 | Vanguard Knight's Gauntlets | Hands | Vanguard Knight's Gauntlets +2 | Elite gloves |
| 80 | Warcaster's Robe | Body | Warcaster's Robe +2 | Elite mage robe |
| 80 | Warcaster's Cap | Head | Warcaster's Cap +2 | Elite mage hat |
| 80 | Warcaster's Slippers | Feet | Warcaster's Slippers +2 | Elite mage footwear |
| 81 | Warcaster's Mitts | Hands | Warcaster's Mitts +2 | Elite mage gloves |
| 81 | Novice Duelist's Vest | Body | Novice Duelist's Vest +2 | Elite light armor |
| 81 | Novice Duelist's Tricorne | Head | Novice Duelist's Tricorne +2 | Elite hat |
| 81 | Novice Duelist's Boots | Feet | Novice Duelist's Boots +2 | Elite boots |
| 81 | Novice Duelist's Gloves | Hands | Novice Duelist's Gloves +2 | Elite gloves |
| 82 | Combat Caster's Cuirass | Body | Combat Caster's Cuirass +2 | Elite battle-mage armor |
| 82 | Combat Caster's Circlet | Head | Combat Caster's Circlet +2 | Elite battle-mage headpiece |
| 82 | Combat Caster's Bracers | Hands | Combat Caster's Bracers +2 | Elite battle-mage gloves |

## Thematic Analysis

### Armor Set Families
Armorcraft features organized equipment sets:

- **Aurion Alloy Series**: Early-game metal armor (levels 1-9)
- **Vanguard Series**: Military-grade equipment (levels 25-77) 
- **Titan Scale Series**: Heavy defensive gear (levels 17-67)
- **Corroded Series**: Degraded equipment (levels 30-90) - mostly missing HQ versions
- **Elite Class Sets**: Ultra-tier specialized armor (levels 77-82)

### Protection Categories
- **Light Armor**: Robes, tunics, cloth gear
- **Medium Armor**: Leather, reinforced materials
- **Heavy Armor**: Plate, scale, metal construction
- **Shields**: Defensive barriers and aegis types

## Component System Requirements

### Armorcraft-Specific Materials
- **Base Materials**: Leather, cloth, metals, scales
- **Reinforcement**: Plates, studs, padding, linings
- **Decorative**: Emblems, trim, insignia
- **Magical**: Enchanted threads, protective wards

### Category-Specific Components
- **Body Armor**: Chest plates, backing, articulation
- **Head Protection**: Visors, padding, straps
- **Extremities**: Finger protection, sole reinforcement
- **Shields**: Boss, rim, grip, facing

## Implementation Roadmap

### Phase 1: Data Completion (Critical)
1. **Component Specification**: Design requirements for all 221 recipes
2. **HQ Gap Filling**: Complete 14 missing HQ versions
3. **Quality Validation**: Fix any data inconsistencies

### Phase 2: System Integration (High Priority)
1. **Set Bonuses**: Consider armor set synergies
2. **Protection Values**: Balance defensive ratings
3. **Material Consistency**: Align with other crafting disciplines

### Phase 3: Advanced Features (Medium Priority)
1. **Customization System**: Color schemes, emblems
2. **Durability Mechanics**: Repair and maintenance
3. **Upgrade Paths**: Progressive enhancement systems

## Cross-Discipline Analysis

### Comparison with Other Crafting Skills

| Aspect | Armorcraft | Weaponcraft | Engineering | Notes |
|--------|------------|-------------|-------------|-------|
| **Scope** | 221 recipes | 205 recipes | 33 recipes | Second largest discipline |
| **Categories** | 5 | 14 | 3 | Moderate complexity |
| **HQ Coverage** | 93.7% | 87.8% | 72.7% | Highest HQ completion |
| **Ultra Coverage** | 6.8% | 5.9% | 0% | Second-highest Ultra tier |
| **Focus** | Defense | Offense | Ranged | Complementary roles |

### System Integration Points
- **Material Sharing**: Common base materials with other disciplines
- **Combat Synergy**: Defensive gear complements weapons
- **Level Scaling**: Matches progression of offensive equipment

## Developer Notes

- All recipes use the `Armorcraft` skill designation
- Resref naming follows pattern: `{descriptive_name}[_p1/_p2]` for quality tiers
- Ultra tier concentrated in levels 77-82 for end-game specialization
- "Corroded" series represents degraded/damaged equipment theme
- Set-based organization suggests equipment families and progression paths
- Strong level distribution suggests comprehensive defensive options at all stages

## Related Documentation

- [Crafting System Overview](crafting/README.md)
- [Armorcraft Component Database](armorcraft-components.md) *(to be created)*
- [Defensive Mechanics](../systems/README.md)
- [Equipment Sets and Bonuses](equipment-sets.md) *(to be created)*