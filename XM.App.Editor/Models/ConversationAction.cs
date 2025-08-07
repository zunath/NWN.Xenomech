using System.ComponentModel;
using System.Text.Json.Serialization;

namespace XM.App.Editor.Models;

public class ConversationAction : INotifyPropertyChanged
{
    private string _type = string.Empty;
    private Dictionary<string, object> _parameters = new();

    [JsonPropertyName("type")]
    public string Type 
    { 
        get => _type;
        set
        {
            if (_type != value)
            {
                _type = value;
                OnPropertyChanged(nameof(Type));
                // Initialize parameters based on type
                InitializeParametersForType();
            }
        }
    }

    [JsonPropertyName("parameters")]
    public Dictionary<string, object> Parameters 
    { 
        get => _parameters;
        set
        {
            if (_parameters != value)
            {
                _parameters = value;
                OnPropertyChanged(nameof(Parameters));
            }
        }
    }

    // Parameter properties for binding (not serialized)
    [System.Text.Json.Serialization.JsonIgnore]
    public string ShopId
    {
        get => Parameters.TryGetValue("shopId", out var value) ? value?.ToString() ?? "" : "";
        set
        {
            if (Parameters.ContainsKey("shopId"))
                Parameters["shopId"] = value;
            else
                Parameters.Add("shopId", value);
            OnPropertyChanged(nameof(ShopId));
            OnPropertyChanged(nameof(Parameters));
            OnPropertyChanged(nameof(Summary));
        }
    }

    [System.Text.Json.Serialization.JsonIgnore]
    public string ItemId
    {
        get => Parameters.TryGetValue("itemId", out var value) ? value?.ToString() ?? "" : "";
        set
        {
            // Limit to 16 characters
            var finalValue = value?.Length > 16 ? value.Substring(0, 16) : value ?? "";
            
            if (Parameters.ContainsKey("itemId"))
                Parameters["itemId"] = finalValue;
            else
                Parameters.Add("itemId", finalValue);
            OnPropertyChanged(nameof(ItemId));
            OnPropertyChanged(nameof(Parameters));
            OnPropertyChanged(nameof(Summary));
        }
    }

    [System.Text.Json.Serialization.JsonIgnore]
    public string Quantity
    {
        get => Parameters.TryGetValue("quantity", out var value) ? value?.ToString() ?? "0" : "0";
        set
        {
            // If the value is null, empty, or whitespace, default to "0"
            var finalValue = string.IsNullOrWhiteSpace(value) ? "0" : value;
            
            // If the current value is "0" and a new number is entered, replace the "0"
            var currentValue = Parameters.TryGetValue("quantity", out var current) ? current?.ToString() ?? "0" : "0";
            if (currentValue == "0" && finalValue != "0" && !string.IsNullOrWhiteSpace(finalValue))
            {
                // Replace the "0" with the new number
                finalValue = finalValue.TrimStart('0');
                if (string.IsNullOrEmpty(finalValue))
                    finalValue = "0";
            }
            
            if (Parameters.ContainsKey("quantity"))
                Parameters["quantity"] = finalValue;
            else
                Parameters.Add("quantity", finalValue);
            OnPropertyChanged(nameof(Quantity));
            OnPropertyChanged(nameof(Parameters));
            OnPropertyChanged(nameof(Summary));
        }
    }

    [System.Text.Json.Serialization.JsonIgnore]
    public string QuestId
    {
        get => Parameters.TryGetValue("questId", out var value) ? value?.ToString() ?? "" : "";
        set
        {
            if (Parameters.ContainsKey("questId"))
                Parameters["questId"] = value;
            else
                Parameters.Add("questId", value);
            OnPropertyChanged(nameof(QuestId));
            OnPropertyChanged(nameof(Parameters));
            OnPropertyChanged(nameof(Summary));
        }
    }

    // Property specifically for the converter to bind to
    [System.Text.Json.Serialization.JsonIgnore]
    public string Summary
    {
        get
        {
            return Type switch
            {
                "OpenShop" => $"Shop: {ShopId}",
                "GiveItem" => $"Resref: {ItemId} x{Quantity}",
                "StartQuest" => $"Quest: {QuestId}",
                "EndConversation" => "End conversation",
                _ => "Unknown Action"
            };
        }
    }

    private void InitializeParametersForType()
    {
        switch (_type)
        {
            case "OpenShop":
                if (!Parameters.ContainsKey("shopId"))
                    Parameters["shopId"] = "";
                break;
            case "GiveItem":
                if (!Parameters.ContainsKey("itemId"))
                    Parameters["itemId"] = "";
                if (!Parameters.ContainsKey("quantity"))
                    Parameters["quantity"] = "0";
                break;
            case "StartQuest":
                if (!Parameters.ContainsKey("questId"))
                    Parameters["questId"] = "";
                break;
            case "EndConversation":
                // No parameters required. Ensure dictionary is empty.
                if (Parameters.Count > 0)
                    Parameters.Clear();
                break;
        }
        OnPropertyChanged(nameof(Parameters));
        // Trigger property change notifications for all parameter properties
        OnPropertyChanged(nameof(ShopId));
        OnPropertyChanged(nameof(ItemId));
        OnPropertyChanged(nameof(Quantity));
        OnPropertyChanged(nameof(QuestId));
        OnPropertyChanged(nameof(Summary));
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}


