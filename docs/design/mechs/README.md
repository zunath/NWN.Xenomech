# Mech System Documentation

This directory contains comprehensive documentation for the Xenomech mech system, including frames, parts, and combat mechanics.

## Overview

The mech system introduces pilotable mechanical units that enhance player capabilities through modular customization. All job classes can pilot mechs, with the Frame determining the mech's role and capabilities.

## File Structure

### Human-Readable Documentation
- `frames.md` - Complete frame specifications and roles
- `parts.md` - Detailed part descriptions and mechanics  
- `combat.md` - Mech combat system and integration
- `customization.md` - Assembly rules and optimization strategies

### Machine-Readable Data
- `data/frames.yaml` - Frame statistics and requirements
- `data/parts.yaml` - Part specifications and modifiers
- `data/stats.yaml` - Stat definitions and calculations

## Quick Reference

### Frame Types
- **Aegis** (Tank) - High HP/Defense, moderate damage
- **Ravager** (Damage) - High Attack, moderate survivability  
- **Nexus** (Ether) - High Ether Attack, energy specialization
- **Herald** (Support) - Balanced stats, efficiency focus

### Part Categories
- **Head** - Sensors, targeting, command systems
- **Core** - Central processing, power distribution
- **L/R Arms** - Weapon mounts, manipulation systems
- **Legs** - Mobility, stability, positioning
- **Generator** - Power source, fuel management
- **L/R Weapons** - Primary armaments and tools

### Key Mechanics
- **Frame-Level Gating** - Frames require specific character levels (10-50)
- **Percentage Modifiers** - Parts modify base frame stats by percentages
- **Heat Management** - Thermal limits prevent overuse of high-power systems
- **Fuel Economy** - Resource management adds tactical depth