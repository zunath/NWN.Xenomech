using System;
using System.Globalization;
using Avalonia.Data.Converters;
using Avalonia.Media;
using XM.App.Editor.Models;

namespace XM.App.Editor.Converters;

public class NodeFontWeightConverter : IValueConverter
{
    public static readonly NodeFontWeightConverter Instance = new();

    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is ConversationPageNode)
            return FontWeight.Bold;
        if (value is ConversationResponseNode)
            return FontWeight.Normal;
        return FontWeight.Normal;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}


