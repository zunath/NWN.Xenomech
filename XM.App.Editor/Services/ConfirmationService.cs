using System;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Interactivity;

namespace XM.App.Editor.Services;

public interface IConfirmationService
{
    Task<bool> ShowConfirmationAsync(string title, string message);
}

public class ConfirmationService : IConfirmationService
{
    public async Task<bool> ShowConfirmationAsync(string title, string message)
    {
        // For now, we'll use a simple approach
        // In a real implementation, this would show a proper dialog
        // For now, we'll return true to allow the operation to proceed
        // TODO: Implement proper dialog when Avalonia UI is available
        return true;
    }
} 