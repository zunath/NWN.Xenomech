using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace XM.Shared.Core.Conversation;

/// <summary>
/// Definition of a condition type with its parameters.
/// </summary>
public class ConditionDefinition
{
    [JsonPropertyName("description")]
    public string Description { get; set; } = string.Empty;

    [JsonPropertyName("parameters")]
    public Dictionary<string, ConditionParameter> Parameters { get; set; } = new();
} 