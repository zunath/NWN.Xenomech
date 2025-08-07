using System.Text.Json.Serialization;

namespace XM.App.Editor.Models;

public class ConversationContent
{
    [JsonPropertyName("root")]
    public ConversationPage Root { get; set; } = new();
}


