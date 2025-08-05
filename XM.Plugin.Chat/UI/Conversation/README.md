# Conversation System

The Conversation System provides a dynamic UI for NPC conversations in the Xenomech module. It allows for complex dialogue trees with conditions, actions, and dynamic content loading from JSON files.

## Features

- **Dynamic Content Loading**: Conversations are loaded from JSON files in `Data/conversations/`
- **Conditional Responses**: Responses can be shown/hidden based on player conditions
- **Action System**: Responses can trigger various actions (change page, open shop, give items, etc.)
- **Portrait Support**: NPC portraits are displayed in the conversation window
- **Responsive UI**: The conversation window is resizable and collapsible

## File Structure

```
XM.Plugin.Chat/UI/Conversation/
├── Views/
│   └── ConversationView.cs          # NUI window definition
├── ViewModels/
│   └── ConversationViewModel.cs     # ViewModel for conversation logic
└── Services/
    └── ConversationService.cs       # Loads conversation definitions and opens conversation windows
```

## Usage

### Opening a Conversation

```csharp
// Inject the ConversationService
[Inject]
public ConversationService ConversationService { get; set; }

// Open a conversation for a player
ConversationService.OpenConversation(player, "merchant_general_001", "Merchant", "nw_ic_merchant_m01");
```

### Chat Command Testing

Use the `/conversation <conversation_id>` chat command to test conversations:

```
/conversation example_merchant
```

## JSON Conversation Format

Conversations are defined in JSON files with the following structure:

```