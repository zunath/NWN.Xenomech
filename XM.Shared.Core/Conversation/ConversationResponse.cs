using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace XM.Shared.Core.Conversation;

/// <summary>
/// Represents a response option in a conversation page.
/// </summary>
public class ConversationResponse
{
    [JsonPropertyName("icon")]
    public string Icon { get; set; } = string.Empty;

    [JsonPropertyName("text")]
    public string Text { get; set; } = string.Empty;

    [JsonPropertyName("conditions")]
    public List<ConversationCondition> Conditions { get; set; } = new();

    [JsonPropertyName("actions")]
    public List<ConversationAction> Actions { get; set; } = new();

    // Next NPC page to navigate to after this response; null means stay/close depending on actions
    [JsonPropertyName("next")]
    public ConversationPage? Next { get; set; }
} 