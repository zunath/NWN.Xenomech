# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Overview

This is a C# server-side solution for Neverwinter Nights: Enhanced Edition using the Xenomech module. The project replaces traditional NWScript with C# code using the NWNX_DotNet plugin and Anvil framework.

## Common Development Commands

### Building and Running
- **Main Development**: Run `XM.App.Runner` project in Visual Studio (set as startup project)
- **Build Solution**: `dotnet build` in root directory
- **Build Editor**: `XM.App.Editor/build.cmd` or `dotnet build XM.App.Editor/XM.App.Editor.csproj`
- **Run Tests**: `dotnet test XM.App.Editor.Tests/` for unit tests with coverage

### Content Management
- **Pack Module**: `Module/PackModule.cmd` - Packages NWN module files
- **Unpack Module**: `Module/UnpackModule.cmd` - Unpacks NWN module for editing
- **Build HAKs**: `Tools/BuildHaks.cmd` - Builds game asset packages
- **Compile Models**: `XM.App.CLI/compileModels.cmd` - Compiles 3D models

### Content Conversion
- **TLK Conversion**: Use `Content/ConvertTlkToJson.cmd` and `Content/ConvertJsonToTlk.cmd` for translating game strings

## Architecture

### Core Projects Structure
- **XM.App.Runner**: Main orchestration application that manages Docker containers and NWN server
- **XM.App.CLI**: Command-line utility for build automation and asset management
- **XM.App.Editor**: Avalonia-based conversation editor application

### Shared Libraries
- **XM.Shared.API**: Core NWScript bindings and engine structure definitions
- **XM.Shared.Core**: Foundation services (chat, authorization, time, messaging)
- **XM.Shared.AI**: Behavior tree-based AI system with targeting and creature management
- **XM.Shared.Inventory**: Item management, caching, and property systems
- **XM.Shared.Progression**: Character advancement and statistical progression
- **XM.Shared.UI**: NUI (NWN User Interface) components and view management

### Plugin System
All plugins inherit from Anvil framework and use dependency injection:
- **XM.Plugin.Administration**: Server moderation and administrative tools
- **XM.Plugin.Area**: Area management and persistent location services
- **XM.Plugin.Chat**: Communication system with typing indicators and roleplay features
- **XM.Plugin.Codex**: In-game documentation and world guide system
- **XM.Plugin.Combat**: Combat mechanics, death system, and spell management
- **XM.Plugin.Craft**: Recipe-based crafting system with categories and quality tiers
- **XM.Plugin.Item**: Item management, appearance editor, and usage systems
- **XM.Plugin.Migration**: Database migration utilities for players and server data
- **XM.Plugin.Quest**: Dynamic quest system with objectives, states, and NPC integration
- **XM.Plugin.Spawn**: Creature spawning with tables, walkmesh services, and queue management

### Docker Infrastructure
The project uses Docker Compose with these services:
- **anvil**: Main NWN server container with NWNX plugins
- **redis**: Caching and session management
- **influxdb**: Metrics collection and performance monitoring
- **grafana**: Data visualization and monitoring dashboards

## Key Development Patterns

### Plugin Development
```csharp
// Plugins use constructor injection for services
public class ExamplePlugin : Plugin
{
    public ExamplePlugin(DBService db, GuiService gui) 
    {
        // Subscribe to NWN events
        // Initialize plugin services
    }
}
```

### Event Handling
- Use `NwnEventType` subscriptions for game integration
- Implement async/await patterns for non-blocking operations
- Handle exceptions gracefully with proper logging

### Database Integration
- Use `DBService` for entity persistence
- Implement migration interfaces for schema changes
- Follow repository pattern for data access

### UI Development
- Use NUI (Neverwinter Nights User Interface) system through `GuiService`
- Implement `IView` and `IViewModel` interfaces for UI components
- Handle window lifecycle events and user interactions

## Technology Stack

- **.NET 8.0**: Target framework for all projects
- **Anvil Framework**: NWN server development framework
- **Docker**: Containerized deployment with Docker Compose
- **Redis**: Caching and session management
- **InfluxDB**: Time-series metrics database
- **Grafana**: Monitoring and visualization
- **xUnit**: Unit testing framework with code coverage
- **Avalonia**: Cross-platform UI framework for editor application

## Configuration

### Environment Variables
Key NWNX configuration in `server/nwserver.env`:
- `NWNX_DOTNET_SKIP=n`: Enables .NET plugin integration
- `NWNX_CORE_LOG_LEVEL=6`: Debug logging level
- Various plugin enable/disable flags

### Project Properties
- **Platform Target**: x64 for all projects
- **Enable Dynamic Loading**: Required for plugin architecture
- **Copy Local Lock File Assemblies**: Ensures dependency deployment

## Testing Strategy

- **Unit Tests**: Located in `XM.App.Editor.Tests/` with 70% coverage threshold
- **Test Command**: `dotnet test` with coverage reports in `TestResults/`
- **Coverage Configuration**: Uses Coverlet for code coverage analysis

## Content Management

### Game Assets
- **Content/**: 3D models, textures, audio, and game data files
- **Module/**: NWN areas, dialogues, items, and game objects
- **Tools/**: Build utilities and asset compilation tools

### Asset Pipeline
1. Create/modify assets in appropriate Content/ subdirectories
2. Use `Tools/BuildHaks.cmd` to package assets into HAK files
3. Use `Module/PackModule.cmd` to create deployable module
4. Deploy through Docker containers

## External Dependencies

### NWNX:EE Unified
- Repository: https://github.com/nwnxee/unified
- Provides core server extension framework
- Enables C#/.NET integration with NWN server

### Design References
- **XM Design Bible**: https://docs.google.com/spreadsheets/d/1CnS5sk6c9cjlRETEuAkkyI5P1gPSskJWct7Qemx-KVs/edit?gid=776582455#gid=776582455
- Contains comprehensive game mechanics specifications
- Reference for character progression, combat systems, and item mechanics

## Game Design Documentation

### Design Documentation Structure
The `docs/design/` directory contains structured game design specifications derived from the XM Design Bible spreadsheet:

- **Character Attributes**: Six core attributes (Might, Perception, Vitality, Willpower, Agility, Social) affecting combat, skills, and progression
- **Growth Grades**: Lettered grade system (A-G) determining stat growth per level for HP, EP, skills, and damage
- **Weapon Skills**: 14 weapon types with unique skill trees, elemental damage, and progression abilities
- **Job System**: 8 character jobs (Beastmaster, Brawler, Elementalist, Hunter, Keeper, Mender, Nightstalker, Techweaver) with distinct roles and abilities
- **Crafting Systems**: Five crafting disciplines (Armorcraft, Engineering, Fabrication, Synthesis, Weaponcraft) with recipes and materials
- **Combat Systems**: Damage calculation formulas, hit rate mechanics, spell damage scaling, and XP progression charts

### Design File Formats
- **Human-readable**: Markdown files in `docs/design/` with structured tables and descriptions
- **Machine-readable**: YAML mirrors in `docs/design/data/` containing full TSV data for programmatic access
- **Source data**: Original TSV exports from XM Design Bible in `docs/_incoming/`

### Key Game Mechanics
- **Tactical Points (TP)**: Resource for weapon skills, gained by combat actions
- **Ether Points (EP)**: Resource for job abilities and magic
- **Resonance Nodes**: Cross-job ability system, gained every 5 levels (max 10)
- **Elemental System**: 10 damage types with resistances and weaknesses
- **Status Effects**: Comprehensive debuff/buff system with duration and stacking rules