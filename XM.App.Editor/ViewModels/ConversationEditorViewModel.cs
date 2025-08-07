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
    private ConversationTreeNode? _selectedNode;

    public ConversationEditorViewModel()
    {
        _conversationService = new ConversationService();
        
        // Initialize commands
        CreateNewConversationCommand = new RelayCommand(CreateNewConversation);
        DeleteConversationCommand = new RelayCommand(DeleteConversation, CanDeleteConversation);
        SaveConversationCommand = new RelayCommand(SaveConversation, CanSaveConversation);
        AddPageCommand = new RelayCommand(AddPage, CanAddPage);
        AddResponseCommand = new RelayCommand(AddResponse, CanAddResponse);
        DeleteNodeCommand = new RelayCommand(DeleteNode, CanDeleteNode);
        
        // Load conversation files
        LoadConversationFiles();
    }

    public ObservableCollection<string> ConversationFiles { get; } = new();
    public ObservableCollection<ConversationTreeNode> ConversationTree { get; } = new();

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
            ((RelayCommand)AddPageCommand).RaiseCanExecuteChanged();
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
            BuildConversationTree();
        }
    }

    public ConversationTreeNode? SelectedNode
    {
        get => _selectedNode;
        set
        {
            _selectedNode = value;
            OnPropertyChanged(nameof(SelectedNode));
            OnPropertyChanged(nameof(SelectedPageNode));
            OnPropertyChanged(nameof(SelectedResponseNode));
            ((RelayCommand)AddResponseCommand).RaiseCanExecuteChanged();
            ((RelayCommand)DeleteNodeCommand).RaiseCanExecuteChanged();
        }
    }

    public ConversationPageNode? SelectedPageNode => SelectedNode as ConversationPageNode;
    public ConversationResponseNode? SelectedResponseNode => SelectedNode as ConversationResponseNode;

    public int PagesCount => CurrentConversation?.Conversation.Pages.Count ?? 0;

    // Commands
    public ICommand CreateNewConversationCommand { get; }
    public ICommand DeleteConversationCommand { get; }
    public ICommand SaveConversationCommand { get; }
    public ICommand AddPageCommand { get; }
    public ICommand AddResponseCommand { get; }
    public ICommand DeleteNodeCommand { get; }

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

    private void BuildConversationTree()
    {
        ConversationTree.Clear();
        SelectedNode = null;

        if (CurrentConversation?.Conversation.Pages == null)
            return;

        foreach (var pageEntry in CurrentConversation.Conversation.Pages)
        {
            var pageNode = new ConversationPageNode
            {
                Name = string.IsNullOrEmpty(pageEntry.Value.Header) ? $"NPC: {pageEntry.Key}" : pageEntry.Value.Header,
                PageId = pageEntry.Key,
                Page = pageEntry.Value
            };

            // Add response nodes (player options)
            for (int i = 0; i < pageEntry.Value.Responses.Count; i++)
            {
                var response = pageEntry.Value.Responses[i];
                var responseNode = new ConversationResponseNode
                {
                    Name = string.IsNullOrEmpty(response.Text) ? $"Player Option {i + 1}" : response.Text,
                    Response = response,
                    ResponseIndex = i
                };
                pageNode.Children.Add(responseNode);
            }

            ConversationTree.Add(pageNode);
        }
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

    private void AddPage()
    {
        if (CurrentConversation == null)
            return;

        var pageId = $"page_{DateTime.Now.Ticks}";
        var newPage = new ConversationPage
        {
            Header = "New Page"
        };

        CurrentConversation.Conversation.Pages[pageId] = newPage;
        BuildConversationTree();
    }

    private bool CanAddPage()
    {
        return CurrentConversation != null;
    }

    private void AddResponse()
    {
        if (SelectedPageNode == null)
            return;

        var newResponse = new ConversationResponse
        {
            Text = "New Response"
        };

        SelectedPageNode.Page.Responses.Add(newResponse);
        BuildConversationTree();
    }

    private bool CanAddResponse()
    {
        return SelectedPageNode != null;
    }

    private void DeleteNode()
    {
        if (SelectedNode == null || CurrentConversation == null)
            return;

        if (SelectedNode is ConversationPageNode pageNode)
        {
            CurrentConversation.Conversation.Pages.Remove(pageNode.PageId);
        }
        else if (SelectedNode is ConversationResponseNode responseNode)
        {
            var parentPage = ConversationTree.FirstOrDefault(p => p.Children.Contains(responseNode)) as ConversationPageNode;
            if (parentPage != null)
            {
                parentPage.Page.Responses.RemoveAt(responseNode.ResponseIndex);
            }
        }

        BuildConversationTree();
    }

    private bool CanDeleteNode()
    {
        return SelectedNode != null;
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
} 