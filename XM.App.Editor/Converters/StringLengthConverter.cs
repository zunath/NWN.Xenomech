using System;
using System.Globalization;
using Avalonia.Data.Converters;

namespace XM.App.Editor.Converters;

public class StringLengthConverter : IValueConverter
{
    public static readonly StringLengthConverter Instance = new();

    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is string stringValue)
        {
            return $"{stringValue.Length}/16 characters";
        }
        return "0/16 characters";
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}


