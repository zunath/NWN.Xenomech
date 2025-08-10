using System.Globalization;
using System.Text.Json;
using Avalonia.Data.Converters;

namespace XM.App.Editor.Converters;

public class DictionaryToJsonConverter : IValueConverter
{
    public static readonly DictionaryToJsonConverter Instance = new();

    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is Dictionary<string, object> dict)
        {
            try
            {
                return JsonSerializer.Serialize(dict, new JsonSerializerOptions
                {
                    WriteIndented = true
                });
            }
            catch
            {
                return "{}";
            }
        }
        return "{}";
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is string json)
        {
            try
            {
                return JsonSerializer.Deserialize<Dictionary<string, object>>(json);
            }
            catch
            {
                return new Dictionary<string, object>();
            }
        }
        return new Dictionary<string, object>();
    }
}


