using System.ComponentModel;
using System.Text.Json.Serialization;

namespace XM.App.Editor.Models;

public class ConversationPage : INotifyPropertyChanged
{
    private string? _id;

    [JsonPropertyName("id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Id
    {
        get => _id;
        set
        {
            var newValue = string.IsNullOrWhiteSpace(value) ? null : value;
            if (_id != newValue)
            {
                _id = newValue;
                OnPropertyChanged(nameof(Id));
            }
        }
    }

    [JsonPropertyName("header")]
    public string Header { get; set; } = string.Empty;

    [JsonPropertyName("responses")]
    public List<ConversationResponse> Responses { get; set; } = new();

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}


