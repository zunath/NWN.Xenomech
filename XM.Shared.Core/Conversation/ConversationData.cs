using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace XM.Shared.Core.Conversation;

/// <summary>
/// The main conversation data containing pages, actions, and conditions.
/// </summary>
public class ConversationData
{
    [JsonPropertyName("defaultPage")]
    public string DefaultPage { get; set; } = string.Empty;

    [JsonPropertyName("pages")]
    public Dictionary<string, ConversationPage> Pages { get; set; } = new();

    [JsonPropertyName("actions")]
    public Dictionary<string, ActionDefinition> Actions { get; set; } = new();

    [JsonPropertyName("conditions")]
    public Dictionary<string, ConditionDefinition> Conditions { get; set; } = new();
} 