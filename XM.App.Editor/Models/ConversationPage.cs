using System.Text.Json.Serialization;

namespace XM.App.Editor.Models;

public class ConversationPage
{
    [JsonPropertyName("header")]
    public string Header { get; set; } = string.Empty;

    [JsonPropertyName("responses")]
    public List<ConversationResponse> Responses { get; set; } = new();
}


