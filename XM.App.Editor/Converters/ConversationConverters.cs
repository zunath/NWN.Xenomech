using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        if (value is string newPageId && parameter is ConversationPageNode pageNode)
        {
            // This would need to be handled in the ViewModel
            return newPageId;
        }
        return string.Empty;
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

public class StringEqualsConverter : IValueConverter
{
    public static readonly StringEqualsConverter Instance = new();

    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is string stringValue && parameter is string expectedValue)
        {
            return stringValue == expectedValue;
        }
        return false;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

public class ActionSummaryConverter : IValueConverter
{
    public static readonly ActionSummaryConverter Instance = new();

    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is ConversationAction action)
        {
            return action.Type switch
            {
                "OpenShop" => $"Shop: {action.ShopId}",
                "GiveItem" => $"Resref: {action.ItemId} x{action.Quantity}",
                "StartQuest" => $"Quest: {action.QuestId}",
                _ => "Unknown Action"
            };
        }
        return "Unknown Action";
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

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

public class CanMoveUpConverter : IValueConverter
{
    public static readonly CanMoveUpConverter Instance = new();

    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is ConversationAction action && parameter is ObservableCollection<ConversationAction> actions)
        {
            var index = actions.IndexOf(action);
            return index > 0;
        }
        return false;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

public class CanMoveDownConverter : IValueConverter
{
    public static readonly CanMoveDownConverter Instance = new();

    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is ConversationAction action && parameter is ObservableCollection<ConversationAction> actions)
        {
            var index = actions.IndexOf(action);
            return index < actions.Count - 1;
        }
        return false;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}



