using System.Text.Json.Serialization;

namespace XM.Shared.Core.Conversation;

/// <summary>
/// Represents a complete conversation definition with metadata, pages, actions, and conditions.
/// </summary>
public class ConversationDefinition
{
    [JsonPropertyName("metadata")]
    public ConversationMetadata Metadata { get; set; } = new();

    [JsonPropertyName("conversation")]
    public ConversationData Conversation { get; set; } = new();
} 