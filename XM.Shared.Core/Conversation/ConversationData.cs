using System.Text.Json.Serialization;

namespace XM.Shared.Core.Conversation;

/// <summary>
/// Conversation content root for the tree-based conversation model.
/// Matches the editor schema where "conversation" contains a single "root" page.
/// </summary>
public class ConversationData
{
    [JsonPropertyName("root")]
    public ConversationPage Root { get; set; } = new();
}