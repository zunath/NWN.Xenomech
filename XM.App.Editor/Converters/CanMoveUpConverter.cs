using System.Collections.ObjectModel;
using System.Globalization;
using Avalonia.Data.Converters;
using XM.App.Editor.Models;

namespace XM.App.Editor.Converters;

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


