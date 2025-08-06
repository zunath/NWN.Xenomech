using System.Text.Json.Serialization;

namespace XM.App.Editor.Models;

public class ConversationData
{
    [JsonPropertyName("metadata")]
    public ConversationMetadata Metadata { get; set; } = new();

    [JsonPropertyName("conversation")]
    public ConversationContent Conversation { get; set; } = new();
}

public class ConversationMetadata
{
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("description")]
    public string Description { get; set; } = string.Empty;

    [JsonPropertyName("portrait")]
    public string Portrait { get; set; } = string.Empty;
}

public class ConversationContent
{
    [JsonPropertyName("defaultPage")]
    public string DefaultPage { get; set; } = string.Empty;

    [JsonPropertyName("pages")]
    public Dictionary<string, ConversationPage> Pages { get; set; } = new();
}

public class ConversationPage
{
    [JsonPropertyName("header")]
    public string Header { get; set; } = string.Empty;

    [JsonPropertyName("responses")]
    public List<ConversationResponse> Responses { get; set; } = new();
}

public class ConversationResponse
{
    [JsonPropertyName("text")]
    public string Text { get; set; } = string.Empty;

    [JsonPropertyName("conditions")]
    public List<ConversationCondition> Conditions { get; set; } = new();

    [JsonPropertyName("action")]
    public ConversationAction Action { get; set; } = new();
}

public class ConversationCondition
{
    [JsonPropertyName("type")]
    public string Type { get; set; } = string.Empty;

    [JsonPropertyName("operator")]
    public string Operator { get; set; } = string.Empty;

    [JsonPropertyName("value")]
    public object Value { get; set; } = new();
}

public class ConversationAction
{
    [JsonPropertyName("type")]
    public string Type { get; set; } = string.Empty;

    [JsonPropertyName("parameters")]
    public Dictionary<string, object> Parameters { get; set; } = new();
} 