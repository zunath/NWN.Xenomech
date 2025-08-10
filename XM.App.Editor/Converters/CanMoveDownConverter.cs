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
        if (value is not ConversationAction action)
            return false;

        if (parameter is not ObservableCollection<ConversationAction> actions || actions is null)
            return false;

        var index = actions.IndexOf(action);
        if (index < 0)
            return false;

        return index < actions.Count - 1;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}


