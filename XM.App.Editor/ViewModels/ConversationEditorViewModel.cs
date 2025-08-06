using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using XM.App.Editor.Models;
using XM.App.Editor.Services;

namespace XM.App.Editor.ViewModels;

public class ConversationEditorViewModel : INotifyPropertyChanged
{
    private readonly ConversationService _conversationService;
    private string? _selectedConversationFile;
    private ConversationData? _currentConversation;

    public ConversationEditorViewModel()
    {
        _conversationService = new ConversationService();
        
        // Initialize commands
        CreateNewConversationCommand = new RelayCommand(CreateNewConversation);
        DeleteConversationCommand = new RelayCommand(DeleteConversation, CanDeleteConversation);
        SaveConversationCommand = new RelayCommand(SaveConversation, CanSaveConversation);
        
        // Load conversation files
        LoadConversationFiles();
    }

    public ObservableCollection<string> ConversationFiles { get; } = new();

    public string? SelectedConversationFile
    {
        get => _selectedConversationFile;
        set
        {
            _selectedConversationFile = value;
            OnPropertyChanged(nameof(SelectedConversationFile));
            LoadSelectedConversation();
            ((RelayCommand)DeleteConversationCommand).RaiseCanExecuteChanged();
            ((RelayCommand)SaveConversationCommand).RaiseCanExecuteChanged();
        }
    }

    public ConversationData? CurrentConversation
    {
        get => _currentConversation;
        set
        {
            _currentConversation = value;
            OnPropertyChanged(nameof(CurrentConversation));
            OnPropertyChanged(nameof(PagesCount));
        }
    }

    public int PagesCount => CurrentConversation?.Conversation.Pages.Count ?? 0;

    // Commands
    public ICommand CreateNewConversationCommand { get; }
    public ICommand DeleteConversationCommand { get; }
    public ICommand SaveConversationCommand { get; }

    private void LoadConversationFiles()
    {
        ConversationFiles.Clear();
        var files = _conversationService.GetConversationFiles();
        foreach (var file in files)
        {
            ConversationFiles.Add(file);
        }
    }

    private void LoadSelectedConversation()
    {
        if (string.IsNullOrEmpty(SelectedConversationFile))
        {
            CurrentConversation = null;
            return;
        }

        CurrentConversation = _conversationService.LoadConversation(SelectedConversationFile);
    }

    private void CreateNewConversation()
    {
        // TODO: Show dialog to get conversation details
        var newConversation = _conversationService.CreateNewConversation(
            "new_conversation_" + DateTime.Now.Ticks,
            "New Conversation",
            "A new conversation"
        );

        var fileName = $"new_conversation_{DateTime.Now.Ticks}";
        if (_conversationService.SaveConversation(fileName, newConversation))
        {
            LoadConversationFiles();
            SelectedConversationFile = fileName;
        }
    }

    private void DeleteConversation()
    {
        if (string.IsNullOrEmpty(SelectedConversationFile))
            return;

        // TODO: Add confirmation dialog
        if (_conversationService.DeleteConversation(SelectedConversationFile))
        {
            LoadConversationFiles();
            SelectedConversationFile = null;
        }
    }

    private bool CanDeleteConversation()
    {
        return !string.IsNullOrEmpty(SelectedConversationFile);
    }

    private void SaveConversation()
    {
        if (string.IsNullOrEmpty(SelectedConversationFile) || CurrentConversation == null)
            return;

        _conversationService.SaveConversation(SelectedConversationFile, CurrentConversation);
    }

    private bool CanSaveConversation()
    {
        return !string.IsNullOrEmpty(SelectedConversationFile) && CurrentConversation != null;
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
} 