using System.ComponentModel;
using System.Text.Json.Serialization;

namespace XM.App.Editor.Models;

public class ConversationCondition : INotifyPropertyChanged
{
    private string _type = string.Empty;
    private string _operator = string.Empty;
    private object _value = new();
    private static readonly Dictionary<string, List<string>> TypeToOperators = new()
    {
        { "PlayerLevel", new List<string> { "Equal", "NotEqual", "GreaterThan", "GreaterThanOrEqual", "LessThan", "LessThanOrEqual" } },
        { "Variable", new List<string> { "Equal", "NotEqual", "Contains", "NotContains" } }
    };

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
                // Initialize sensible defaults when type changes
                if (_type == "PlayerLevel")
                {
                    if (string.IsNullOrEmpty(Operator))
                        Operator = "GreaterThanOrEqual";
                    // Ensure numeric value
                    if (_value is not int)
                        _value = 1;
                }
                else if (_type == "Variable")
                {
                    if (string.IsNullOrEmpty(Operator))
                        Operator = "Equal";
                    if (_value is not string)
                        _value = string.Empty;
                }
                // Adjust operator if it's not valid for this type
                if (!AvailableOperators.Contains(Operator))
                {
                    Operator = AvailableOperators.FirstOrDefault() ?? "Equal";
                }
                OnPropertyChanged(nameof(Summary));
                OnPropertyChanged(nameof(AvailableOperators));
            }
        }
    }

    [JsonPropertyName("operator")]
    public string Operator
    {
        get => _operator;
        set
        {
            if (_operator != value)
            {
                _operator = value;
                OnPropertyChanged(nameof(Operator));
                OnPropertyChanged(nameof(Summary));
            }
        }
    }

    [JsonPropertyName("value")]
    public object Value
    {
        get => _value;
        set
        {
            if (!Equals(_value, value))
            {
                _value = value ?? new object();
                OnPropertyChanged(nameof(Value));
                OnPropertyChanged(nameof(NumericValue));
                OnPropertyChanged(nameof(StringValue));
                OnPropertyChanged(nameof(Summary));
            }
        }
    }

    [System.Text.Json.Serialization.JsonIgnore]
    public List<string> AvailableOperators
    {
        get
        {
            if (TypeToOperators.TryGetValue(Type, out var ops))
                return ops;
            // Default operators if type is unknown
            return new List<string> { "Equal", "NotEqual" };
        }
    }

    [System.Text.Json.Serialization.JsonIgnore]
    public string NumericValue
    {
        get => _value is int i ? i.ToString() : (_value?.ToString() ?? string.Empty);
        set
        {
            if (int.TryParse(value, out var parsed))
            {
                Value = parsed; // sets and raises
            }
            else if (string.IsNullOrWhiteSpace(value))
            {
                Value = 0;
            }
        }
    }

    [System.Text.Json.Serialization.JsonIgnore]
    public string StringValue
    {
        get => _value?.ToString() ?? string.Empty;
        set => Value = value ?? string.Empty;
    }

    [System.Text.Json.Serialization.JsonIgnore]
    public string Summary
    {
        get
        {
            var valueText = Type == "PlayerLevel" ? NumericValue : StringValue;
            return string.IsNullOrWhiteSpace(Type)
                ? ""
                : $"{Type} {Operator} {valueText}";
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}


