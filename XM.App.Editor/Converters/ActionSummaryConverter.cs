using System;
using System.Globalization;
using Avalonia.Data.Converters;
using XM.App.Editor.Models;

namespace XM.App.Editor.Converters;

public class ActionSummaryConverter : IValueConverter
{
    public static readonly ActionSummaryConverter Instance = new();

    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is ConversationAction action)
        {
            var type = action.Type ?? string.Empty;
            return type switch
            {
                "ExecuteScript" => $"Script: {action.ScriptId ?? string.Empty}",
                "Teleport" => $"Tag: {action.Tag ?? string.Empty}",
                "ChangePage" => $"Page: {action.PageId ?? string.Empty}",
                "OpenShop" => $"Shop: {action.ShopId ?? string.Empty}",
                "GiveItem" => $"Resref: {action.ItemId ?? string.Empty} x{(action.Quantity ?? "0")}",
                "StartQuest" => $"Quest: {action.QuestId ?? string.Empty}",
                "EndConversation" => "End conversation",
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


