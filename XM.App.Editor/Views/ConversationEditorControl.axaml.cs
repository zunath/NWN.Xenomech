using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using XM.App.Editor.ViewModels;
using XM.App.Editor.Models;
using Avalonia;
using System.Linq;
using Avalonia.VisualTree;
using System.ComponentModel;
using System;
using System.Threading.Tasks;
using Avalonia.Input.Platform;
using Avalonia.Threading;
using System.Text.RegularExpressions;

namespace XM.App.Editor.Views;

public partial class ConversationEditorControl : UserControl
{
    public ConversationEditorControl()
    {
        InitializeComponent();
        DataContextChanged += OnDataContextChanged;
        this.AttachedToVisualTree += (_, __) => ExpandRootAndSelected();
    }

    private void OnDataContextChanged(object? sender, EventArgs e)
    {
        // Subscribe to property changes in the view model
        if (DataContext is ConversationEditorViewModel viewModel)
        {
            viewModel.PropertyChanged += OnViewModelPropertyChanged;
        }
    }

    private void OnViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        // Update button states when relevant properties change
        if (e.PropertyName == nameof(ConversationEditorViewModel.SelectedAction) ||
            e.PropertyName == nameof(ConversationEditorViewModel.SelectedResponseNode))
        {
            UpdateButtonStates();
        }

        if (e.PropertyName == nameof(ConversationEditorViewModel.SelectedNode))
        {
            // After selection changes, ensure ancestors are expanded
            ExpandRootAndSelected();
        }
    }

    protected override void OnLoaded(RoutedEventArgs e)
    {
        base.OnLoaded(e);
        UpdateButtonStates();
        ExpandRootAndSelected();
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

    private void OnQuantityTextChanged(object? sender, TextChangedEventArgs e)
    {
        if (sender is TextBox textBox)
        {
            var digitsOnly = Regex.Replace(textBox.Text ?? string.Empty, "[^0-9]", string.Empty);
            if (digitsOnly != (textBox.Text ?? string.Empty))
            {
                var caret = textBox.CaretIndex;
                textBox.Text = digitsOnly;
                textBox.CaretIndex = Math.Min(caret, textBox.Text.Length);
            }
        }
    }

    private void OnMoveActionUp(object? sender, RoutedEventArgs e)
    {
        if (sender is Button button && button.Tag is ConversationAction action && DataContext is ConversationEditorViewModel viewModel)
        {
            // Check if action can be moved up
            var actions = viewModel.SelectedResponseNode?.Response.Actions;
            if (actions != null)
            {
                var index = actions.IndexOf(action);
                if (index > 0)
                {
                    viewModel.MoveActionUp(action);
                    UpdateButtonStates();
                }
            }
        }
    }

    private void OnMoveActionDown(object? sender, RoutedEventArgs e)
    {
        if (sender is Button button && button.Tag is ConversationAction action && DataContext is ConversationEditorViewModel viewModel)
        {
            // Check if action can be moved down
            var actions = viewModel.SelectedResponseNode?.Response.Actions;
            if (actions != null)
            {
                var index = actions.IndexOf(action);
                if (index < actions.Count - 1)
                {
                    viewModel.MoveActionDown(action);
                    UpdateButtonStates();
                }
            }
        }
    }

    private void UpdateButtonStates()
    {
        if (DataContext is ConversationEditorViewModel viewModel && viewModel.SelectedResponseNode?.Response.Actions != null)
        {
            var actions = viewModel.SelectedResponseNode.Response.Actions;
            
            // Find all buttons in the ItemsControl and update their enabled state
            if (ActionsItemsControl != null)
            {
                var containers = ActionsItemsControl.GetVisualDescendants().OfType<Border>().Where(b => b.Name == "ActionBorder").ToList();
                foreach (var container in containers)
                {
                    if (container.DataContext is ConversationAction action)
                    {
                        var index = actions.IndexOf(action);

                        // Update selection visual state via CSS-like class
                        container.Classes.Set("selected", action == viewModel.SelectedAction);
                        
                        // Find and update button states
                        var upButton = container.GetVisualDescendants().OfType<Button>().FirstOrDefault(b => b.Name == "MoveUpButton");
                        var downButton = container.GetVisualDescendants().OfType<Button>().FirstOrDefault(b => b.Name == "MoveDownButton");
                        
                        if (upButton != null)
                        {
                            upButton.IsEnabled = index > 0;
                        }
                        
                        if (downButton != null)
                        {
                            downButton.IsEnabled = index < actions.Count - 1;
                        }
                    }
                }
            }
        }
    }

    private void OnActionItemClicked(object? sender, PointerPressedEventArgs e)
    {
        if (sender is Border border && border.DataContext is ConversationAction action && DataContext is ConversationEditorViewModel viewModel)
        {
            viewModel.SelectedAction = action;
            UpdateButtonStates();
        }
    }

    private void ExpandRootAndSelected()
    {
        if (ConversationTreeView == null)
            return;

        // Expand root item
        var rootContainer = ConversationTreeView.ItemContainerGenerator?.ContainerFromIndex(0) as TreeViewItem;
        if (rootContainer != null)
        {
            rootContainer.IsExpanded = true;
        }

        // Expand ancestors of selected node so it's visible
        if (DataContext is ConversationEditorViewModel vm && vm.SelectedNode is ConversationTreeNode node)
        {
            var selectedContainer = FindContainerForNode(node);
            var current = selectedContainer;
            while (current != null)
            {
                current.IsExpanded = true;
                current = current.GetVisualAncestors().OfType<TreeViewItem>().FirstOrDefault();
            }
        }
    }

    private TreeViewItem? FindContainerForNode(ConversationTreeNode node)
    {
        if (ConversationTreeView == null) return null;
        return ConversationTreeView.GetVisualDescendants().OfType<TreeViewItem>()
            .FirstOrDefault(tvi => ReferenceEquals(tvi.DataContext, node));
    }
}
