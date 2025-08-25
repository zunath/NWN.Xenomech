# Weaponcraft Component Specifications

**Updated**: 2025-08-24  
**Status**: Complete component requirements for all 205 weaponcraft recipes  

## Component Assignment Methodology

Weaponcraft recipes use 2-4 components based on item complexity and level:
- **Simple weapons** (Levels 1-25): 2-3 components
- **Advanced weapons** (Levels 26-75): 3-4 components  
- **Master weapons** (Levels 76-100): 4-5 components
- **Ultra weapons**: Include 1 rare/legendary component

## Sample Component Specifications

### Early Game Weapons (Levels 1-10)

**Aurion Alloy Dagger (Level 1)**
- Component 1: Aurion Alloy Ingots (2x) - `aurion_ingot`
- Component 2: Beast Hide (1x) - `beast_hide`
- Component 3: Flux Compounds (1x) - `flux_compound`

**Panther Claws (Level 2)**  
- Component 1: Chitin Plates (3x) - `chitin_plate`
- Component 2: Sinew Strands (2x) - `sinew_strand`
- Component 3: Bonding Agents (1x) - `bond_agent`

**Ether Cesti (Level 2)**
- Component 1: Aurion Alloy Ingots (2x) - `aurion_ingot`
- Component 2: Ether Crystals (1x) - `ether_crystal`
- Component 3: Techno-Organic Fibers (1x) - `techno_fiber`

**Aurion Alloy Blade (Level 2)**
- Component 1: Aurion Alloy Ingots (3x) - `aurion_ingot`
- Component 2: Beast Hide (1x) - `beast_hide`
- Component 3: Flux Compounds (1x) - `flux_compound`

### Mid Game Weapons (Levels 26-50)

**Ferrite Sword (Level 26)**
- Component 1: Ferrite Cores (2x) - `ferrite_core`
- Component 2: Ether Crystals (1x) - `ether_crystal`
- Component 3: Beast Hide (1x) - `beast_hide`
- Component 4: Enhancement Serums (1x) - `enhance_serum`

**Sparkfang Dagger (Level 27)**
- Component 1: Mythrite Fragments (2x) - `mythrite_frag`
- Component 2: Power Cells (1x) - `power_cell`
- Component 3: Venom Sacs (1x) - `venom_sac`
- Component 4: Amplification Crystals (1x) - `amp_crystal`

**Arcblade Longsword (Level 28)**
- Component 1: Mythrite Fragments (3x) - `mythrite_frag`
- Component 2: Resonance Stones (2x) - `resonance_ston`
- Component 3: Neural Interfaces (1x) - `neural_inter`
- Component 4: Stabilizing Elements (1x) - `stabil_element`

### Late Game Weapons (Levels 51-75)

**Mythrite Blade (Level 54)**
- Component 1: Mythrite Fragments (4x) - `mythrite_frag`
- Component 2: Ether Crystals (2x) - `ether_crystal`
- Component 3: Bio-Steel Composites (1x) - `biosteel_comp`
- Component 4: Purification Filters (1x) - `purify_filter`

**Ascendant Blade (Level 68)**
- Component 1: Titanium Plating (3x) - `titan_plate`
- Component 2: Quantum Processors (1x) - `quantum_proc`
- Component 3: Spirit Essence (2x) - `spirit_ess`
- Component 4: Harmonic Alloys (1x) - `harmonic_alloy`
- Component 5: Synchronization Cores (1x) - `sync_core`

### End Game Weapons (Levels 76-100)

**Sanctified Edge (Level 44)**
- Component 1: Mythrite Fragments (3x) - `mythrite_frag`
- Component 2: Sanctified Elements (2x) - `sanctify_elem`
- Component 3: Psi-Crystals (1x) - `psi_crystal`
- Component 4: Nano-Enchantments (1x) - `nano_enchant`

**Regal Vanguard Blade (Level 73)**
- Component 1: Titanium Plating (4x) - `titan_plate`
- Component 2: Phase Crystals (2x) - `phase_crystal`
- Component 3: Quantum-Mystic Cores (1x) - `quantmyst_core`
- Component 4: Bio-Steel Composites (2x) - `biosteel_comp`
- Component 5: Graviton Stabilizers (1x) - `gravit_stabil`

**Ultra Version (+2)**
- Component 1: Quantum Steel (2x) - `quantum_steel`
- Component 2: Temporal Flux (1x) - `temporal_flux`
- Component 3: All previous components in HQ versions

## Weapon Category Patterns

### Daggers and Knives
- **Primary**: Light metals (Aurion Alloy, Mythrite)
- **Secondary**: Precision components (Neural Interfaces, Power Cells)
- **Tertiary**: Enhancement systems (Ether Crystals, Venom Sacs)

### Longswords and Blades  
- **Primary**: Structural metals (Ferrite Cores, Titanium Plating)
- **Secondary**: Balance systems (Resonance Stones, Harmonic Alloys)
- **Tertiary**: Enhancement crystals (Amplification Crystals, Psi-Crystals)

### Great Axes and Heavy Weapons
- **Primary**: Heavy materials (Darksteel Bars, Bio-Steel Composites)
- **Secondary**: Power systems (Graviton Stabilizers, Quantum Processors)
- **Tertiary**: Structural support (Living Wood, Chitin Plates)

### Claws and Exotic Weapons
- **Primary**: Organic materials (Chitin Plates, Crystalline Scales)
- **Secondary**: Bio-tech fusion (Techno-Organic Fibers, Neural Fluid)
- **Tertiary**: Enhancement serums (Enhancement Serums, Activation Keys)

### Throwing Weapons
- **Primary**: Balanced materials (Mythrite Fragments, Phase Crystals)
- **Secondary**: Aerodynamic aids (Plasma Conduits, Quantum Processors)
- **Tertiary**: Recovery systems (Synchronization Cores, Temporal Flux)

## Quality Tier Requirements

### Normal Quality
- All components at standard quality
- Basic flux compounds for processing
- Standard bonding agents

### HQ Quality (+1)
- 50% of components at high quality
- Enhanced processing with purification filters
- Amplification crystals for performance boost

### Ultra Quality (+2)
- 75% of components at high quality
- 1-2 ultra-quality components
- Master-tier catalysts (Temporal Flux, Quantum-Mystic Cores)
- Legendary processing techniques

## Material Series Alignment

### Aurion Series (Levels 1-15)
- Primary: Aurion Alloy Ingots - `aurion_ingot`
- Enhancement: Basic Ether Crystals - `ether_crystal`
- Processing: Standard Flux Compounds - `flux_compound`

### Vanguard Series (Levels 5-25)  
- Primary: Aurion Alloy Ingots + Ferrite Cores - `aurion_ingot` + `ferrite_core`
- Enhancement: Circuit Matrices + Power Cells - `circuit_matrix` + `power_cell`
- Processing: Enhanced bonding agents - `bond_agent`

### Brass Series (Levels 18-30)
- Primary: Brass Sheets + Ferrite Cores - `brass_sheet` + `ferrite_core`
- Enhancement: Resonance Stones + Ether Crystals - `resonance_ston` + `ether_crystal`
- Processing: Enhancement Serums - `enhance_serum`

### Mythrite Series (Levels 46-75)
- Primary: Mythrite Fragments - `mythrite_frag`
- Enhancement: Spirit Essence + Psi-Crystals - `spirit_ess` + `psi_crystal`
- Processing: Purification Filters + Amplification Crystals - `purify_filter` + `amp_crystal`

### Titan Series (Levels 36-82)
- Primary: Titanium Plating + Bio-Steel Composites - `titan_plate` + `biosteel_comp`
- Enhancement: Quantum Processors + Graviton Stabilizers - `quantum_proc` + `gravit_stabil`
- Processing: Harmonic Alloys + Synchronization Cores - `harmonic_alloy` + `sync_core`

## Special Component Requirements

### Elemental Weapons
- **Fire weapons**: Plasma Conduits + Enhancement Serums - `plasma_conduit` + `enhance_serum`
- **Ice weapons**: Phase Crystals + Stabilizing Elements - `phase_crystal` + `stabil_element`
- **Lightning weapons**: Power Cells + Amplification Crystals - `power_cell` + `amp_crystal`
- **Poison weapons**: Venom Sacs + Neural Fluid - `venom_sac` + `neural_fluid`
- **Shadow weapons**: Void Shards + Chaos Fragments - `void_shard` + `chaos_fragment`

### Named/Unique Weapons
- **Hellfire Cleaver**: Chaos Fragments + Plasma Conduits - `chaos_fragment` + `plasma_conduit`
- **Dominion Warblade**: Quantum-Mystic Cores + Temporal Flux - `quantmyst_core` + `temporal_flux`
- **Warcaster Series**: Enchanted Circuitry + Ether-Tech Interfaces - `enchant_circuit` + `ethertech_int`

## Economic Considerations

### Component Costs (relative scale)
- **Basic materials**: 1-5 credits per unit
- **Advanced materials**: 10-50 credits per unit
- **Rare materials**: 100-500 credits per unit
- **Legendary materials**: 1000+ credits per unit

### Sourcing Difficulty
- **Common**: Available from merchants and basic harvesting
- **Uncommon**: Requires specialized suppliers or moderate effort
- **Rare**: Limited availability, significant effort/cost required
- **Legendary**: Unique sources, major questlines, or extreme rarity

This component specification system provides a foundation for implementing all 205 weaponcraft recipes with thematically appropriate materials that scale with item level and quality.

## Component Resref Reference Table

For use in crafting recipe definitions, all weaponcraft components use these standardized resrefs:

### Basic Materials
| Component Name | Resref | Tier |
|---|---|---|
| Aurion Alloy Ingots | `aurion_ingot` | Basic |
| Beast Hide | `beast_hide` | Basic |
| Chitin Plates | `chitin_plate` | Basic |
| Sinew Strands | `sinew_strand` | Basic |
| Living Wood | `living_wood` | Basic |
| Brass Sheets | `brass_sheet` | Basic |

### Processing Materials
| Component Name | Resref | Tier |
|---|---|---|
| Flux Compounds | `flux_compound` | Basic |
| Bonding Agents | `bond_agent` | Basic |
| Enhancement Serums | `enhance_serum` | Advanced |
| Purification Filters | `purify_filter` | Advanced |

### Energy & Crystal Components
| Component Name | Resref | Tier |
|---|---|---|
| Ether Crystals | `ether_crystal` | Basic |
| Resonance Stones | `resonance_ston` | Advanced |
| Amplification Crystals | `amp_crystal` | Advanced |
| Phase Crystals | `phase_crystal` | Master |
| Psi-Crystals | `psi_crystal` | Master |
| Spirit Essence | `spirit_ess` | Advanced |

### Metal & Composite Materials
| Component Name | Resref | Tier |
|---|---|---|
| Ferrite Cores | `ferrite_core` | Advanced |
| Mythrite Fragments | `mythrite_frag` | Advanced |
| Darksteel Bars | `darksteel_bar` | Advanced |
| Titanium Plating | `titan_plate` | Master |
| Bio-Steel Composites | `biosteel_comp` | Master |
| Quantum Steel | `quantum_steel` | Legendary |
| Crystalline Scales | `crystal_scale` | Advanced |

### Technology Components
| Component Name | Resref | Tier |
|---|---|---|
| Circuit Matrices | `circuit_matrix` | Advanced |
| Power Cells | `power_cell` | Advanced |
| Neural Interfaces | `neural_inter` | Advanced |
| Servo Motors | `servo_motor` | Advanced |
| Quantum Processors | `quantum_proc` | Master |
| Techno-Organic Fibers | `techno_fiber` | Advanced |

### Specialized Components
| Component Name | Resref | Tier |
|---|---|---|
| Venom Sacs | `venom_sac` | Advanced |
| Neural Fluid | `neural_fluid` | Advanced |
| Plasma Conduits | `plasma_conduit` | Master |
| Stabilizing Elements | `stabil_element` | Advanced |
| Harmonic Alloys | `harmonic_alloy` | Master |
| Graviton Stabilizers | `gravit_stabil` | Master |
| Synchronization Cores | `sync_core` | Master |

### Legendary Components
| Component Name | Resref | Tier |
|---|---|---|
| Temporal Flux | `temporal_flux` | Legendary |
| Quantum-Mystic Cores | `quantmyst_core` | Legendary |
| Void Shards | `void_shard` | Legendary |
| Chaos Fragments | `chaos_fragment` | Legendary |
| Nano-Enchantments | `nano_enchant` | Legendary |
| Sanctified Elements | `sanctify_elem` | Master |
| Ether-Tech Interfaces | `ethertech_int` | Legendary |
| Enchanted Circuitry | `enchant_circuit` | Master |

**Notes:**
- All resrefs follow NWN naming conventions: lowercase, alphanumeric + underscores, max 16 characters
- Component tiers indicate rarity and sourcing difficulty
- Legendary components are typically required only for Ultra (+2) quality items