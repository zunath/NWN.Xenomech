using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using XM.App.Editor.ViewModels;

namespace XM.App.Editor.Views;

public partial class ConversationEditorControl : UserControl
{
    public ConversationEditorControl()
    {
        InitializeComponent();
    }

    private void OnPageIdLostFocus(object sender, RoutedEventArgs e)
    {
        if (sender is TextBox textBox && DataContext is ConversationEditorViewModel viewModel)
        {
            if (viewModel.SelectedPageNode != null)
            {
                var newPageId = textBox.Text;
                var oldPageId = viewModel.SelectedPageNode.PageId;
                
                if (!string.IsNullOrEmpty(newPageId) && newPageId != oldPageId)
                {
                    viewModel.UpdatePageId(oldPageId, newPageId);
                }
            }
        }
    }

    private void OnQuantityKeyDown(object sender, KeyEventArgs e)
    {
        if (sender is TextBox textBox)
        {
            // Allow digits, backspace, delete, tab, enter, and navigation keys
            if (e.Key >= Key.D0 && e.Key <= Key.D9 ||
                e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9 ||
                e.Key == Key.Back || e.Key == Key.Delete ||
                e.Key == Key.Tab || e.Key == Key.Enter ||
                e.Key == Key.Left || e.Key == Key.Right ||
                e.Key == Key.Home || e.Key == Key.End)
            {
                return; // Allow the key
            }
            
            // Block all other keys
            e.Handled = true;
        }
    }
}
