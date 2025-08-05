using System.Text.Json.Serialization;

namespace XM.Shared.Core.Conversation;

/// <summary>
/// Metadata about the conversation including ID, name, and description.
/// </summary>
public class ConversationMetadata
{
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("description")]
    public string Description { get; set; } = string.Empty;
} 