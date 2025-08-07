# XM.App.Editor

A cross-platform desktop application framework for custom editors within the Xenomech project.

## Overview

XM.App.Editor is a modern desktop application built with Avalonia UI that provides a foundation for creating custom editors for various components within the Xenomech project. It runs on both Windows and Linux operating systems.

## Features

- **Cross-Platform**: Runs on Windows and Linux
- **Clean Framework**: Minimal UI ready for custom editor implementations
- **Modern UI**: Built with Avalonia UI framework
- **Configuration**: JSON-based settings management
- **Extensible**: Easy to add custom editors and functionality

## Conversation Editor

The conversation editor provides a comprehensive interface for managing conversation data with the following features:

### Tree View Structure
- **NPC Dialogue**: Displayed as top-level nodes (🗣️ icon, bold text) showing what the NPC says
- **Player Options**: Displayed as child nodes (👤 icon, normal text) showing what the player can choose
- **Conversation Flow**: Clear visual representation of the dialogue structure

### Node Management
- **NPC**: Create new NPC dialogue nodes with default content
- **Response**: Add player choice options to selected NPC dialogue
- **Delete**: Remove NPC dialogue or player options with confirmation
- **Real-time Updates**: Tree view updates automatically when changes are made

### Node Details Panel
When a conversation is loaded, the right panel shows conversation details and node-specific information:

#### Conversation Details
- **ID**: Edit the unique conversation identifier
- **Name**: Edit the conversation display name
- **Description**: Edit the conversation description (multi-line)
- **Portrait**: Edit the portrait path for the conversation

#### NPC Dialogue Details
- **NPC Text**: Edit what the NPC says to the player
- **Page ID**: Display the unique page identifier

#### Player Option Details
- **Player Text**: Edit what the player can choose to say
- **Conditions List**: View and manage response conditions
  - Condition type, operator, and value display
  - Add, edit, and delete condition buttons
- **Actions List**: View and manage response actions
  - Action type and parameters editing
  - JSON format for complex parameter structures
  - Edit action functionality

### File Management
- **Load Conversations**: Select from existing conversation files
- **Save Changes**: Save modifications to conversation files
- **Create New**: Generate new conversation files with default structure
- **Delete Files**: Remove conversation files with confirmation

### Data Structure
The editor works with conversation data in the following format:
```json
{
  "metadata": {
    "id": "conversation_id",
    "name": "Conversation Name",
    "description": "Description",
    "portrait": "portrait_path"
  },
  "conversation": {
    "defaultPage": "greeting",
    "pages": {
      "page_id": {
        "header": "Page header text",
        "responses": [
          {
            "text": "Response text",
            "conditions": [
              {
                "type": "condition_type",
                "operator": "operator",
                "value": "condition_value"
              }
            ],
            "action": {
              "type": "action_type",
              "parameters": {
                "param1": "value1",
                "param2": "value2"
              }
            }
          }
        ]
      }
    }
  }
}
```

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
# Run the editor application
cd XM.App.Editor
dotnet run
```

### Project Structure

```
XM.App.Editor/
├── Models/                    # Data models
│   ├── ConversationData.cs   # Conversation data structure
│   └── ConversationTreeNode.cs # Tree view node models
├── ViewModels/               # View models for MVVM
│   └── ConversationEditorViewModel.cs
├── Views/                    # UI views
│   ├── ConversationEditorControl.axaml
│   └── ConversationEditorControl.axaml.cs
├── Services/                 # Business logic services
│   └── ConversationService.cs
├── Converters/               # UI data converters
│   └── ConversationConverters.cs
└── README.md                # This file
```

## Usage

1. **Launch the Editor**: Run the application and click the conversation editor button
2. **Load a Conversation**: Select an existing conversation file from the left panel
3. **Navigate the Tree**: Use the tree view to navigate pages and responses
4. **Edit Node Details**: Select a node to edit its properties in the right panel
5. **Manage Conditions**: Add, edit, or delete conditions for responses
6. **Manage Actions**: Edit action types and parameters for responses
7. **Save Changes**: Use the save button to persist your changes

## Future Enhancements

- **Condition Editor**: Advanced condition editing with type-specific controls
- **Action Editor**: Visual action editor with parameter validation
- **Preview Mode**: Test conversations in a preview environment
- **Validation**: Real-time validation of conversation structure
- **Import/Export**: Support for importing from other formats
- **Undo/Redo**: Change history management
- **Search**: Find text across all conversation nodes 