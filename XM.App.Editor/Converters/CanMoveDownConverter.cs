using System;
using System.Collections.ObjectModel;
using System.Globalization;
using Avalonia.Data.Converters;
using XM.App.Editor.Models;

namespace XM.App.Editor.Converters;

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


