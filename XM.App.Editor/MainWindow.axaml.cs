using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Data.Converters;
using System.ComponentModel;
using System.Windows.Input;
using System.Globalization;
using XM.App.Editor.ViewModels;
using XM.App.Editor.Views;

namespace XM.App.Editor;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        DataContext = new MainWindowViewModel();
    }

    protected override void OnLoaded(RoutedEventArgs e)
    {
        base.OnLoaded(e);
        
        // Initialize the editor
        if (DataContext is MainWindowViewModel viewModel)
        {
            viewModel.Initialize();
            
            // Subscribe to property changes to update UI
            viewModel.PropertyChanged += OnViewModelPropertyChanged;
            
            // Ensure conversation editor is hidden on startup
            if (this.FindControl<ConversationEditorControl>("ConversationEditor") is ConversationEditorControl editor)
            {
                editor.IsVisible = false;
            }
        }
    }

    private void OnViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(MainWindowViewModel.IsConversationEditorVisible))
        {
            if (this.FindControl<ConversationEditorControl>("ConversationEditor") is ConversationEditorControl editor)
            {
                editor.IsVisible = ((MainWindowViewModel)sender!).IsConversationEditorVisible;
            }
        }
    }
}

public class MainWindowViewModel : INotifyPropertyChanged
{
    private bool _isConversationEditorVisible;
    private ConversationEditorViewModel? _conversationEditorViewModel;

    // Commands
    public ICommand ExitCommand { get; }
    public ICommand AboutCommand { get; }
    public ICommand OpenConversationEditorCommand { get; }

    public bool IsConversationEditorVisible
    {
        get => _isConversationEditorVisible;
        set
        {
            _isConversationEditorVisible = value;
            OnPropertyChanged(nameof(IsConversationEditorVisible));
        }
    }

    public ConversationEditorViewModel? ConversationEditorViewModel
    {
        get => _conversationEditorViewModel;
        set
        {
            _conversationEditorViewModel = value;
            OnPropertyChanged(nameof(ConversationEditorViewModel));
        }
    }

    public MainWindowViewModel()
    {
        ExitCommand = new RelayCommand(Exit);
        AboutCommand = new RelayCommand(About);
        OpenConversationEditorCommand = new RelayCommand(OpenConversationEditor);
        
        // Ensure conversation editor is hidden by default
        _isConversationEditorVisible = false;
    }

    public void Initialize()
    {
        // Initialize the editor with default settings
    }

    public void Exit()
    {
        // TODO: Implement exit functionality
    }

    public void About()
    {
        // TODO: Implement about dialog
    }

    public void OpenConversationEditor()
    {
        if (IsConversationEditorVisible)
        {
            // Hide the conversation editor
            IsConversationEditorVisible = false;
            ConversationEditorViewModel = null;
        }
        else
        {
            // Show the conversation editor
            ConversationEditorViewModel = new ViewModels.ConversationEditorViewModel();
            IsConversationEditorVisible = true;
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}

// Simple relay command implementation
public class RelayCommand : ICommand
{
    private readonly Action _execute;
    private readonly Func<bool>? _canExecute;

    public RelayCommand(Action execute, Func<bool>? canExecute = null)
    {
        _execute = execute ?? throw new ArgumentNullException(nameof(execute));
        _canExecute = canExecute;
    }

    public event EventHandler? CanExecuteChanged;

    public void RaiseCanExecuteChanged()
    {
        CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }

    public bool CanExecute(object? parameter) => _canExecute?.Invoke() ?? true;

    public void Execute(object? parameter) => _execute();
}

// Simple converter to show different icons based on conversation editor visibility
public class BooleanToIconConverter : IValueConverter
{
    public static readonly BooleanToIconConverter Instance = new();

    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is bool isVisible)
        {
            return isVisible ? "❌" : "💬";
        }
        return "💬";
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

// Simple converter to show different tooltips based on conversation editor visibility
public class BooleanToTooltipConverter : IValueConverter
{
    public static readonly BooleanToTooltipConverter Instance = new();

    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is bool isVisible)
        {
            return isVisible ? "Hide Conversation Editor" : "Show Conversation Editor";
        }
        return "Conversation Editor";
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

// Simple converter to invert boolean values
public class InverseBooleanConverter : IValueConverter
{
    public static readonly InverseBooleanConverter Instance = new();

    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is bool boolValue)
        {
            return !boolValue;
        }
        return true;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is bool boolValue)
        {
            return !boolValue;
        }
        return false;
    }
} 