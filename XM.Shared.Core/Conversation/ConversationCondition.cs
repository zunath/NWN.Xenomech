using System.Text.Json.Serialization;

namespace XM.Shared.Core.Conversation;

/// <summary>
/// Represents a condition that must be met for a response to be available.
/// </summary>
public class ConversationCondition
{
    [JsonPropertyName("type")]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public ConversationConditionType ConditionType { get; set; } = ConversationConditionType.Invalid;

    [JsonPropertyName("operator")]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public ComparisonOperator Operator { get; set; } = ComparisonOperator.Equal;

    [JsonPropertyName("value")]
    public System.Text.Json.JsonElement Value { get; set; }
} 