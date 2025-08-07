using System;
using System.Globalization;
using Avalonia.Data.Converters;
using XM.App.Editor.Models;

namespace XM.App.Editor.Converters;

public class PageIdConverter : IValueConverter
{
    public static readonly PageIdConverter Instance = new();

    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is ConversationPageNode pageNode)
            return pageNode.PageId;
        return string.Empty;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is string newPageId && parameter is ConversationPageNode)
        {
            return newPageId;
        }
        return string.Empty;
    }
}


