using System;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;

namespace XM.App.Editor.Services;

public interface IConfirmationService
{
    Task<bool> ShowConfirmationAsync(string title, string message);
}

public class ConfirmationService : IConfirmationService
{
    public async Task<bool> ShowConfirmationAsync(string title, string message)
    {
        Window? owner = null;
        if (Application.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            owner = desktop.MainWindow;
        }

        var dialog = new XM.App.Editor.Views.ConfirmationDialog(title, message)
        {
            WindowStartupLocation = WindowStartupLocation.CenterOwner
        };

        if (owner != null)
        {
            var result = await dialog.ShowDialog<bool>(owner);
            return result;
        }
        else
        {
            // Fallback if no owner window is available: still return the user's choice
            var result = await dialog.ShowDialog<bool>(null);
            return result;
        }
    }
} 