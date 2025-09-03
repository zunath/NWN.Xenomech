---
title: Companions and Cybernetic Beasts
updated: 2025-09-03
category: design
---

### Overview

The Beastmaster job can summon cybernetic creatures, bioengineered entities, and mechanical constructs to aid in combat. These technological companions range from basic drones at early levels to reality-warping cosmic entities at endgame.

### Beast Categories

**Basic Tech Units (Levels 1-10)**
- Scout Drone, Data Rat, Recon Probe - Reconnaissance and utility systems
- Combat Mech, Cyber Cat, Security Bot - Entry-level combat platforms
- Web Crawler, Hunter Drone, Pack Alpha, Bio Tank - Specialized tactical units

**Advanced Systems (Levels 11-25)**
- Mining Mech, Plasma Beetle, Cryo Hound - Industrial and elemental warfare units
- Echo Wing, Shadow Unit, Titan Frame - Stealth and heavy combat platforms
- Storm Hawk, Frost Walker, Venom Synth - Environmental warfare systems
- Steel Guardian, Flame Walker, Thunder Lizard - Specialized combat units
- Holo Spider, Wind Rider, Siege Frame - Advanced technology platforms

**Quantum Technology (Levels 26-35)**
- Phase Hunter, Ion Wolf, Battle Wyrm - Quantum-enhanced combat units
- Phoenix Core, Apex Mech - Self-repairing and adaptive systems
- Stellar Hound, Null Spider, Storm Reaper - Space-age technology
- Magma Titan, Cryo Dragon - Environmental extremes units

**Experimental Tech (Levels 36-45)**
- Photon Stag, Void Stalker, Omega Frame - Light/dark energy manipulation
- Quantum Fox, Leviathan Mech - Reality-manipulating constructs
- Void Serpent, Archangel Unit, Alpha Prime - Godlike AI constructs
- Titan Beast, Wraith Engine - Ancient/spectral technology

**Cosmic Technology (Levels 46-50)**
- Cosmic Entity, Nightmare Rider, World Engine - Planet-scale systems
- Chrono Wyrm, Genesis Machine - Time/reality manipulation entities

### Summoning System

Each beast requires a specific **Broth** item to summon:
- Broth items are technological concoctions that interface with the summoning system
- Each broth has a unique ResRef following the pattern `broth_[descriptor]`
- Examples: Circuit Broth (`broth_circuit`), Quantum Broth (`broth_quantum`), Creation Broth (`broth_create`)

### Sources

- Original template: `docs/_incoming/XM Design Bible - Beasts.tsv`
- Complete beast data: `beast_data.csv` (50 levels of progression)
- Machine-readable YAML: `docs/design/data/beasts.yaml`

### Schema

Updated schema fields: `levelReq`, `name`, `resref`, `scale`, `portraitId`, `soundId`, `item`, `hpGrade`, `epGrade`, attributes (letter grades), `dmg`, `delay`, `evasion`, `abilities` (1-4).

### Design Notes

- All beasts follow sci-fi themes rather than fantasy
- Progression from simple drones to cosmic-level entities
- Abilities reflect advanced technology: scanning, hacking, energy manipulation, quantum effects
- Names and descriptions evoke cyberpunk/space-age aesthetics
- ResRefs comply with NWN 16-character alphanumeric+underscore limitations


