using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.Json;
using Avalonia.Data.Converters;
using Avalonia.Utilities;
using XM.App.Editor.Models;

namespace XM.App.Editor.Converters;

public class NodeTypeConverter : IValueConverter
{
    public static readonly NodeTypeConverter Instance = new();

    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is ConversationPageNode)
            return "Page";
        if (value is ConversationResponseNode)
            return "Response";
        return "Unknown";
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

public class NodeIconConverter : IValueConverter
{
    public static readonly NodeIconConverter Instance = new();

    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is ConversationPageNode)
            return "🗣️"; // NPC speaking
        if (value is ConversationResponseNode)
            return "👤"; // Player option
        return "❓";
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

public class NodeFontWeightConverter : IValueConverter
{
    public static readonly NodeFontWeightConverter Instance = new();

    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is ConversationPageNode)
            return Avalonia.Media.FontWeight.Bold; // NPC text is bold
        if (value is ConversationResponseNode)
            return Avalonia.Media.FontWeight.Normal; // Player options are normal
        return Avalonia.Media.FontWeight.Normal;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

public class NotNullToVisibilityConverter : IValueConverter
{
    public static readonly NotNullToVisibilityConverter Instance = new();

    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return value != null ? true : false;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

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