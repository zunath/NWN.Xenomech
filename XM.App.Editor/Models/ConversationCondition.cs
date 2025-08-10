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
        { "Variable", new List<string> { "Equal", "NotEqual", "Contains", "NotContains" } },
        { "PlayerSkill", new List<string> { "Equal", "NotEqual", "GreaterThan", "GreaterThanOrEqual", "LessThan", "LessThanOrEqual" } }
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
                    // Reset value to default for this type
                    Value = 1;
                }
                else if (_type == "Variable")
                {
                    if (string.IsNullOrEmpty(Operator))
                        Operator = "Equal";
                    // Reset value to default for this type
                    Value = string.Empty;
                }
                else if (_type == "PlayerSkill")
                {
                    if (string.IsNullOrEmpty(Operator))
                        Operator = "GreaterThanOrEqual";
                    // Default as structured fields
                    SkillType = "Longsword";
                    SkillLevelNumeric = "1";
                    Value = $"{SkillType}:{SkillLevelNumeric}";
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
                OnPropertyChanged(nameof(VariableName));
                OnPropertyChanged(nameof(VariableExpectedValue));
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

    // PlayerSkill helpers
    [System.Text.Json.Serialization.JsonIgnore]
    public string SkillType
    {
        get
        {
            var raw = _value?.ToString() ?? string.Empty;
            var parts = raw.Split(':');
            return parts.Length >= 1 ? parts[0] : string.Empty;
        }
        set
        {
            var level = SkillLevelNumeric;
            Value = string.IsNullOrWhiteSpace(value) ? $":{level}" : $"{value}:{level}";
            OnPropertyChanged(nameof(SkillType));
        }
    }

    [System.Text.Json.Serialization.JsonIgnore]
    public string SkillLevelNumeric
    {
        get
        {
            var raw = _value?.ToString() ?? string.Empty;
            var parts = raw.Split(':');
            return parts.Length >= 2 ? parts[1] : string.Empty;
        }
        set
        {
            var type = SkillType;
            // Clamp to non-negative integer in string form
            if (!int.TryParse(value, out var parsed) || parsed < 0) parsed = 0;
            Value = string.IsNullOrWhiteSpace(type) ? $":{parsed}" : $"{type}:{parsed}";
            OnPropertyChanged(nameof(SkillLevelNumeric));
        }
    }

    [System.Text.Json.Serialization.JsonIgnore]
    public string VariableName
    {
        get
        {
            var raw = _value?.ToString() ?? string.Empty;
            if (string.IsNullOrWhiteSpace(raw)) return string.Empty;

            var parts = raw.Split(':');
            if (parts.Length >= 3 && (parts[0].Equals("int", StringComparison.OrdinalIgnoreCase) || parts[0].Equals("float", StringComparison.OrdinalIgnoreCase) || parts[0].Equals("string", StringComparison.OrdinalIgnoreCase)))
            {
                return parts[1];
            }
            if (parts.Length >= 2)
            {
                return parts[0];
            }
            return raw;
        }
        set
        {
            var name = value ?? string.Empty;
            var expected = VariableExpectedValue;
            Value = string.IsNullOrWhiteSpace(expected) ? name : $"{name}:{expected}";
            OnPropertyChanged(nameof(VariableName));
        }
    }

    [System.Text.Json.Serialization.JsonIgnore]
    public string VariableExpectedValue
    {
        get
        {
            var raw = _value?.ToString() ?? string.Empty;
            if (string.IsNullOrWhiteSpace(raw)) return string.Empty;

            var parts = raw.Split(':');
            if (parts.Length >= 3 && (parts[0].Equals("int", StringComparison.OrdinalIgnoreCase) || parts[0].Equals("float", StringComparison.OrdinalIgnoreCase) || parts[0].Equals("string", StringComparison.OrdinalIgnoreCase)))
            {
                return string.Join(':', parts, 2, parts.Length - 2);
            }
            if (parts.Length == 2)
            {
                return parts[1];
            }
            return string.Empty;
        }
        set
        {
            var name = VariableName;
            var expected = value ?? string.Empty;
            Value = string.IsNullOrWhiteSpace(expected) ? name : $"{name}:{expected}";
            OnPropertyChanged(nameof(VariableExpectedValue));
        }
    }

    [System.Text.Json.Serialization.JsonIgnore]
    public string Summary
    {
        get
        {
            if (string.IsNullOrWhiteSpace(Type)) return "";

            if (Type == "PlayerLevel")
            {
                return $"PlayerLevel {Operator} {NumericValue}";
            }

            if (Type == "Variable")
            {
                var name = VariableName;
                var expected = VariableExpectedValue;
                return string.IsNullOrWhiteSpace(expected)
                    ? $"Variable {Operator} {name} (exists)"
                    : $"Variable '{name}' {Operator} {expected}";
            }

            if (Type == "PlayerSkill")
            {
                return $"PlayerSkill {Operator} {StringValue}";
            }

            return $"{Type} {Operator} {StringValue}";
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}


