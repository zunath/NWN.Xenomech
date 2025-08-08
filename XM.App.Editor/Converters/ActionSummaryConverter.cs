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
            return action.Type switch
            {
                    "ChangePage" => $"Page: {action.PageId}",
                "OpenShop" => $"Shop: {action.ShopId}",
                "GiveItem" => $"Resref: {action.ItemId} x{action.Quantity}",
                "StartQuest" => $"Quest: {action.QuestId}",
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


