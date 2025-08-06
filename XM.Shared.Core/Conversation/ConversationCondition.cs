using System.Text.Json.Serialization;

namespace XM.Shared.Core.Conversation;

/// <summary>
/// Represents a condition that must be met for a response to be available.
/// </summary>
public class ConversationCondition
{
    [JsonPropertyName("type")]
    public string Type { get; set; } = string.Empty;

    [JsonIgnore]
    public ConversationConditionType ConditionType
    {
        get => System.Enum.TryParse<ConversationConditionType>(Type, true, out var result) ? result : ConversationConditionType.Invalid;
        set => Type = value.ToString();
    }

    [JsonPropertyName("operator")]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public ComparisonOperator Operator { get; set; } = ComparisonOperator.Equal;

    [JsonPropertyName("value")]
    public object Value { get; set; } = new();
} 