# Synthesis Recipes

**Source**: `docs/_incoming/XM Design Bible - Synthesis.tsv`  
**Updated**: 2025-08-24  
**Status**: Component specifications incomplete - requires design work  

## Overview

The Synthesis skill encompasses 13 magical head accessory recipes, ranging from level 1 to 97. This is the most specialized and smallest crafting discipline in the Xenomech system, focusing exclusively on arcane and magical headwear.

### Quality Tiers

- **Normal**: Standard crafted items (13 recipes, 100% coverage)
- **HQ (High Quality)**: Enhanced versions with +1 suffix (11 recipes, 84.6% coverage)  
- **Ultra**: Premium versions (0 recipes, 0% coverage - no Ultra tier)

### Implementation Status

⚠️ **CRITICAL**: All 13 recipes are missing component specifications. The Component 1-8 fields are empty across all recipes, making them non-implementable until the component system is designed and populated.

## Specialization Focus

### Single Category: Head Accessories
- **Total Recipes**: 13 (smallest discipline)
- **Category**: Head slot only (100% specialization)
- **Level Range**: 1-97 (near-full spectrum)
- **Theme**: Magical and arcane headwear

## Complete Recipe List

| Level | Name | HQ Version | Theme | Notes |
|-------|------|------------|-------|-------|
| 1 | Drab Circlet | ✅ Available | Basic | Starter accessory |
| 5 | Aurion Alloy Circlet | ✅ Available | Metalwork | Early crafting |
| 13 | Vanguard Circlet | ✅ Available | Military | Standard progression |
| 18 | Brass Circlet | ✅ Available | Material tier | Mid-tier crafting |
| 26 | Ferrite Circlet | ✅ Available | Advanced metal | Higher tier |
| 31 | Elaborate Hairpin | ❌ Missing | Decorative | **Data gap** |
| 37 | Jeweled Hairpin | ❌ Missing | Luxury | **Data gap** |
| 46 | Mythrite Circlet | ✅ Available | Rare material | High-tier crafting |
| 54 | Toxin Circlet | ✅ Available | Specialized | Unique properties |
| 63 | Ornate Crown | ✅ Available | Royal | Premium item |
| 71 | Titanium Circlet | ✅ Available | Premium metal | End-game material |
| 88 | Sanctified Circlet | ✅ Available | Sacred | Master-tier |
| 97 | Xenomech Crown | ✅ Available | Ultimate | Pinnacle item |

## Data Quality Issues

### Missing HQ Versions (2 recipes)

| Level | Name | Priority | Notes |
|-------|------|----------|-------|
| 31 | Elaborate Hairpin | Medium | Mid-game decorative item |
| 37 | Jeweled Hairpin | Medium | Luxury accessory |

### Ultra Tier Absence
Like Fabrication and Engineering, Synthesis has no Ultra tier items. Given its magical theme, this represents a potential content gap for premium arcane accessories.

## Thematic Analysis

### Material Progression
Synthesis follows the established material hierarchy:
1. **Drab** (Level 1): Basic starter
2. **Aurion Alloy** (Level 5): Early metalwork
3. **Vanguard** (Level 13): Military grade
4. **Brass** (Level 18): Standard crafting
5. **Ferrite** (Level 26): Advanced materials
6. **Mythrite** (Level 46): Rare components
7. **Toxin** (Level 54): Specialized enhancement
8. **Titanium** (Level 71): Premium materials
9. **Sanctified** (Level 88): Sacred/blessed
10. **Xenomech** (Level 97): Ultimate/unique

### Design Themes
- **Circlets**: Primary item type (8/13 recipes)
- **Hairpins**: Decorative accessories (2/13 recipes)
- **Crowns**: Royal/premium items (3/13 recipes)
- **Magical Focus**: Arcane and mystical properties

### Level Distribution Gaps
Unlike other disciplines with even progression, Synthesis has significant gaps:
- **Early (1-25)**: 4 recipes
- **Mid (26-50)**: 4 recipes  
- **Late (51-75)**: 3 recipes
- **End (76-100)**: 2 recipes

## Component System Requirements

### Synthesis-Specific Materials
- **Magical Components**: Crystals, essences, arcane dusts
- **Precious Materials**: Rare metals, gems, enchanted materials
- **Binding Agents**: Mystical threads, ethereal bindings
- **Enhancement Catalysts**: Spell focuses, magical conduits

### Item-Specific Components
- **Circlets**: Bands, gem settings, arcane inscriptions
- **Hairpins**: Decorative elements, magical focuses
- **Crowns**: Royal materials, ceremonial components

## Unique Position in Crafting System

### Specialization Benefits
- **Deep Focus**: Master magical headwear crafting
- **Niche Expertise**: Unique item category
- **Magical Integration**: Strong ties to spellcasting systems
- **Prestige Items**: Crown crafting for status/roleplay

### System Integration Challenges
- **Limited Scope**: Very narrow specialization
- **Progression Gaps**: Sparse level distribution
- **Material Efficiency**: Small recipe count vs. material variety
- **Economic Viability**: Sustainable market for single item type

## Implementation Roadmap

### Phase 1: Data Completion (Critical)
1. **Component Specification**: Design magical crafting requirements
2. **HQ Gap Filling**: Complete 2 missing HQ versions
3. **Level Distribution**: Consider adding intermediate recipes

### Phase 2: Content Enhancement (High Priority)
1. **Ultra Tier Design**: Premium magical crowns/circlets
2. **Recipe Expansion**: Fill level progression gaps
3. **Magical Properties**: Define arcane enhancement system

### Phase 3: System Integration (Medium Priority)
1. **Spell Integration**: Connect with magic systems
2. **Prestige Crafting**: High-end ceremonial items
3. **Customization**: Magical appearance modifications

## Cross-Discipline Analysis

### Comparison with Other Crafting Skills

| Aspect | Synthesis | Weaponcraft | Engineering | Armorcraft | Fabrication |
|--------|-----------|-------------|-------------|------------|-------------|
| **Scope** | 13 recipes | 205 recipes | 33 recipes | 221 recipes | 105 recipes |
| **Categories** | 1 | 14 | 3 | 5 | 4 |
| **Specialization** | Extreme | Moderate | High | Moderate | Moderate |
| **HQ Coverage** | 84.6% | 87.8% | 72.7% | 93.7% | 66.7% |
| **Ultra Coverage** | 0% | 5.9% | 0% | 6.8% | 0% |
| **Focus** | Magical Head | Melee Combat | Ranged Combat | Defense | Utility |

### System Role
- **Niche Specialist**: Unique magical headwear
- **Complement**: Works alongside other head armor (Armorcraft)
- **Material Sharing**: Uses premium materials efficiently
- **Prestige Crafting**: High-value, specialized items

## Viability Considerations

### Strengths
- **Unique Niche**: Only magical headwear crafting
- **High Quality**: Strong HQ coverage (84.6%)
- **Prestige Factor**: Crown crafting for status
- **Material Efficiency**: Small recipe count allows focused resource use

### Challenges
- **Limited Market**: Single item slot restriction  
- **Progression Gaps**: Sparse level distribution
- **Competition**: Overlaps with Armorcraft head items
- **Economic Sustainability**: Viable player specialization?

### Recommendations
1. **Expand Recipe Count**: Fill level progression gaps
2. **Define Unique Benefits**: Magical properties vs. armor protection
3. **Add Ultra Tier**: Premium arcane accessories
4. **Integration Points**: Connect with magic and prestige systems

## Developer Notes

- All recipes use the `Synthesis` skill designation
- Resref naming follows pattern: `{material/style}_{item_type}[_p1]` for HQ versions
- Unique among disciplines for single-category focus
- Material progression matches other disciplines despite small scope
- "Xenomech Crown" suggests setting-specific ultimate item
- Hairpin items break from circlet/crown pattern
- Significant level gaps suggest intended rarity/specialization
- No quantity variations (all produce single items)

## System Design Questions

1. **Scope Expansion**: Should Synthesis cover all magical accessories?
2. **Differentiation**: How to distinguish from Armorcraft head items?
3. **Economic Model**: Sustainable as player specialization?
4. **Integration**: Connection to spellcasting and magical systems?
5. **Progression**: Fill gaps or maintain rarity design?

## Related Documentation

- [Crafting System Overview](crafting/README.md)
- [Synthesis Component Database](synthesis-components.md) *(to be created)*
- [Magical Systems Integration](../systems/magical-systems.md) *(to be created)*
- [Prestige and Status Items](prestige-items.md) *(to be created)*