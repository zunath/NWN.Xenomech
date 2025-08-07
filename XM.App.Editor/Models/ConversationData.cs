using System.Text.Json.Serialization;

namespace XM.App.Editor.Models;

public class ConversationData
{
    [JsonPropertyName("metadata")]
    public ConversationMetadata Metadata { get; set; } = new();

    [JsonPropertyName("conversation")]
    public ConversationContent Conversation { get; set; } = new();
}