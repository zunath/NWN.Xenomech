using System.Collections.ObjectModel;
using System.Text.Json.Serialization;

namespace XM.App.Editor.Models;

public class ConversationResponse
{
    [JsonPropertyName("text")]
    public string Text { get; set; } = string.Empty;

    [JsonPropertyName("conditions")]
    public ObservableCollection<ConversationCondition> Conditions { get; set; } = new();

    [JsonPropertyName("actions")]
    public ObservableCollection<ConversationAction> Actions { get; set; } = new();

    // Next NPC node in the tree after this response
    [JsonPropertyName("next")]
    public ConversationPage? Next { get; set; }
}


