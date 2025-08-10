using System.Globalization;
using Avalonia.Data.Converters;

namespace XM.App.Editor.Converters;

public class StringLengthConverter : IValueConverter
{
    public static readonly StringLengthConverter Instance = new();
    public int DefaultLimit { get; set; } = 16;

    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        var limit = DefaultLimit;
        if (parameter is string paramStr && int.TryParse(paramStr, out var parsed))
        {
            limit = parsed;
        }

        if (value is string stringValue)
        {
            return $"{stringValue.Length}/{limit} characters";
        }
        return $"0/{limit} characters";
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}


