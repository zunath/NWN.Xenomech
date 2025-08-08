using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace XM.Shared.Core.Conversation;

/// <summary>
/// Represents an action that occurs when a response is selected.
/// </summary>
public class ConversationAction
{
    [JsonPropertyName("type")]
    public string Type { get; set; } = string.Empty;

    [JsonIgnore]
    public ConversationActionType ActionType
    {
        get => System.Enum.TryParse<ConversationActionType>(Type, true, out var result) ? result : ConversationActionType.Invalid;
        set => Type = value.ToString();
    }

    [JsonPropertyName("parameters")]
    public Dictionary<string, object> Parameters { get; set; } = new();
} 