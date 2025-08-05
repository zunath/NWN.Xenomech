using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace XM.Shared.Core.Conversation;

/// <summary>
/// Definition of a parameter for a condition.
/// </summary>
public class ConditionParameter
{
    [JsonPropertyName("type")]
    public string Type { get; set; } = string.Empty;

    [JsonPropertyName("required")]
    public bool Required { get; set; }

    [JsonPropertyName("default")]
    public object? Default { get; set; }

    [JsonPropertyName("enum")]
    public List<string>? Enum { get; set; }
} 