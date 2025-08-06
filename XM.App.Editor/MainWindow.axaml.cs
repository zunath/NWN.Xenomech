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
    private bool _moduleExplorerVisible = true;
    private bool _propertiesPanelVisible = true;

    public bool ModuleExplorerVisible
    {
        get => _moduleExplorerVisible;
        set
        {
            _moduleExplorerVisible = value;
            OnPropertyChanged(nameof(ModuleExplorerVisible));
        }
    }

    public bool PropertiesPanelVisible
    {
        get => _propertiesPanelVisible;
        set
        {
            _propertiesPanelVisible = value;
            OnPropertyChanged(nameof(PropertiesPanelVisible));
        }
    }

    // Commands
    public ICommand OpenModuleCommand { get; }
    public ICommand SaveCommand { get; }
    public ICommand SaveAsCommand { get; }
    public ICommand ExitCommand { get; }
    public ICommand UndoCommand { get; }
    public ICommand RedoCommand { get; }
    public ICommand ToggleModuleExplorerCommand { get; }
    public ICommand TogglePropertiesPanelCommand { get; }
    public ICommand AboutCommand { get; }

    public MainWindowViewModel()
    {
        OpenModuleCommand = new RelayCommand(OpenModule);
        SaveCommand = new RelayCommand(Save);
        SaveAsCommand = new RelayCommand(SaveAs);
        ExitCommand = new RelayCommand(Exit);
        UndoCommand = new RelayCommand(Undo);
        RedoCommand = new RelayCommand(Redo);
        ToggleModuleExplorerCommand = new RelayCommand(ToggleModuleExplorer);
        TogglePropertiesPanelCommand = new RelayCommand(TogglePropertiesPanel);
        AboutCommand = new RelayCommand(About);
    }

    public void Initialize()
    {
        // Initialize the editor with default settings
    }

    public void OpenModule()
    {
        // TODO: Implement module opening functionality
    }

    public void Save()
    {
        // TODO: Implement save functionality
    }

    public void SaveAs()
    {
        // TODO: Implement save as functionality
    }

    public void Exit()
    {
        // TODO: Implement exit functionality
    }

    public void Undo()
    {
        // TODO: Implement undo functionality
    }

    public void Redo()
    {
        // TODO: Implement redo functionality
    }

    public void ToggleModuleExplorer()
    {
        ModuleExplorerVisible = !ModuleExplorerVisible;
    }

    public void TogglePropertiesPanel()
    {
        PropertiesPanelVisible = !PropertiesPanelVisible;
    }

    public void About()
    {
        // TODO: Implement about dialog
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