using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace XM.Shared.Core.Conversation;

/// <summary>
/// Represents a response option in a conversation page.
/// </summary>
public class ConversationResponse
{
    [JsonPropertyName("text")]
    public string Text { get; set; } = string.Empty;

    [JsonPropertyName("conditions")]
    public List<ConversationCondition> Conditions { get; set; } = new();

    [JsonPropertyName("action")]
    public ConversationAction Action { get; set; } = new();
} 