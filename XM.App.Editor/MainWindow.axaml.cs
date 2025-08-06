using Avalonia.Controls;
using Avalonia.Interactivity;
using System.ComponentModel;
using System.Windows.Input;

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
        }
    }
}

public class MainWindowViewModel : INotifyPropertyChanged
{
    // Commands
    public ICommand ExitCommand { get; }
    public ICommand AboutCommand { get; }
    public ICommand OpenConversationEditorCommand { get; }

    public MainWindowViewModel()
    {
        ExitCommand = new RelayCommand(Exit);
        AboutCommand = new RelayCommand(About);
        OpenConversationEditorCommand = new RelayCommand(OpenConversationEditor);
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
        // TODO: Implement conversation editor opening
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