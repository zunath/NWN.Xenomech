# XM.App.Editor

A cross-platform desktop application for editing Neverwinter Nights: Enhanced Edition modules, specifically designed for the Xenomech project.

## Overview

XM.App.Editor is a modern desktop application built with Avalonia UI that provides a comprehensive editing environment for NWN:EE modules. It runs on both Windows and Linux operating systems.

## Features

- **Cross-Platform**: Runs on Windows and Linux
- **Module Explorer**: Tree-view navigation of module content
- **Property Editor**: Edit properties of selected items
- **Preview Panel**: Visual preview of 3D models and assets
- **Modern UI**: Built with Avalonia UI framework
- **Configuration**: JSON-based settings management

## Architecture

The application follows the MVVM (Model-View-ViewModel) pattern and uses:

- **Avalonia UI**: Cross-platform UI framework
- **Microsoft.Extensions**: Dependency injection and configuration
- **XM.Shared Libraries**: Integration with existing Xenomech shared components

## Development

### Prerequisites

- .NET 8.0 SDK
- Visual Studio 2022 or VS Code
- Git

### Building

```bash
# Build the entire solution
dotnet build Xenomech.sln

# Build just the editor
dotnet build XM.App.Editor/XM.App.Editor.csproj
```

### Running

```bash
# Run the editor
dotnet run --project XM.App.Editor/XM.App.Editor.csproj
```

## Project Structure

```
XM.App.Editor/
├── App.axaml                 # Main application XAML
├── App.axaml.cs              # Application code-behind
├── MainWindow.axaml          # Main window XAML
├── MainWindow.axaml.cs       # Main window code-behind
├── Program.cs                # Application entry point
├── GlobalUsings.cs           # Global using statements
├── appsettings.json          # Application configuration
├── app.manifest              # Windows compatibility manifest
└── XM.App.Editor.csproj     # Project file
```

## Configuration

The application uses `appsettings.json` for configuration:

- **Logging**: Log level settings
- **Editor**: Window dimensions, auto-save settings, theme preferences
- **Module Paths**: Default module locations

## Dependencies

- **Avalonia**: Cross-platform UI framework
- **Microsoft.Extensions**: Configuration and dependency injection
- **XM.Shared.API**: NWN API bindings
- **XM.Shared.Core**: Core functionality
- **XM.Shared.UI**: Shared UI components

## Future Enhancements

- Module file format support
- 3D model preview integration
- Plugin system for custom editors
- Real-time collaboration features
- Advanced search and filtering
- Export/import functionality

## Contributing

This project follows the same development patterns as other Xenomech projects. See the main project README for contribution guidelines. 