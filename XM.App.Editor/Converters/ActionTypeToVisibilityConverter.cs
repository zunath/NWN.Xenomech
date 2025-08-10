using System.Globalization;
using Avalonia.Data.Converters;

namespace XM.App.Editor.Converters;

public class ActionTypeToVisibilityConverter : IValueConverter
{
    public static readonly ActionTypeToVisibilityConverter Instance = new();

    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is string actionType && parameter is string expectedType)
        {
            return actionType == expectedType;
        }
        return false;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}


