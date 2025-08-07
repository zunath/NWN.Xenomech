using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using XM.App.Editor.Models;
using XM.App.Editor.Services;

namespace XM.App.Editor.ViewModels;

public class ConversationEditorViewModel : INotifyPropertyChanged
{
    private readonly ConversationService _conversationService;
    private readonly IConfirmationService _confirmationService;
    private string? _selectedConversationFile;
    private ConversationData? _currentConversation;
    private ConversationTreeNode? _selectedNode;

    public ConversationEditorViewModel()
    {
        _conversationService = new ConversationService();
        _confirmationService = new ConfirmationService();
        
        // Initialize commands
        CreateNewConversationCommand = new RelayCommand(CreateNewConversation);
        DeleteConversationCommand = new RelayCommand(DeleteConversation, CanDeleteConversation);
        SaveConversationCommand = new RelayCommand(SaveConversation, CanSaveConversation);
        AddPageCommand = new RelayCommand(AddPage, CanAddPage);
        AddResponseCommand = new RelayCommand(AddResponse, CanAddResponse);
        DeleteNodeCommand = new RelayCommand(DeleteNode, CanDeleteNode);
        AddActionCommand = new RelayCommand(AddAction, CanAddAction);
        DeleteActionCommand = new RelayCommand(DeleteAction, CanDeleteAction);
        AddConditionCommand = new RelayCommand(AddCondition, CanAddCondition);
        DeleteConditionCommand = new RelayCommand(DeleteCondition, CanDeleteCondition);
        MoveActionUpCommand = new RelayCommand<ConversationAction>(MoveActionUp, CanMoveActionUp);
        MoveActionDownCommand = new RelayCommand<ConversationAction>(MoveActionDown, CanMoveActionDown);
        
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
            ((RelayCommand)AddActionCommand).RaiseCanExecuteChanged();
            ((RelayCommand)DeleteActionCommand).RaiseCanExecuteChanged();
            ((RelayCommand)AddConditionCommand).RaiseCanExecuteChanged();
            ((RelayCommand)DeleteConditionCommand).RaiseCanExecuteChanged();
        }
    }

    public ConversationPageNode? SelectedPageNode => SelectedNode as ConversationPageNode;
    public ConversationResponseNode? SelectedResponseNode => SelectedNode as ConversationResponseNode;

    private ConversationAction? _selectedAction;
    public ConversationAction? SelectedAction
    {
        get => _selectedAction;
        set
        {
            if (_selectedAction != value)
            {
                _selectedAction = value;
                OnPropertyChanged(nameof(SelectedAction));
                ((RelayCommand)AddActionCommand).RaiseCanExecuteChanged();
                ((RelayCommand)DeleteActionCommand).RaiseCanExecuteChanged();
                ((RelayCommand<ConversationAction>)MoveActionUpCommand).RaiseCanExecuteChanged();
                ((RelayCommand<ConversationAction>)MoveActionDownCommand).RaiseCanExecuteChanged();
            }
        }
    }

    private ConversationCondition? _selectedCondition;
    public ConversationCondition? SelectedCondition
    {
        get => _selectedCondition;
        set
        {
            if (_selectedCondition != value)
            {
                _selectedCondition = value;
                OnPropertyChanged(nameof(SelectedCondition));
                ((RelayCommand)DeleteConditionCommand).RaiseCanExecuteChanged();
            }
        }
    }

    public int PagesCount => CurrentConversation?.Conversation.Pages.Count ?? 0;

    public List<string> ActionTypes { get; } = new()
    {
        "OpenShop",
        "GiveItem", 
        "StartQuest",
        "Custom"
    };

    // Commands
    public ICommand CreateNewConversationCommand { get; }
    public ICommand DeleteConversationCommand { get; }
    public ICommand SaveConversationCommand { get; }
    public ICommand AddPageCommand { get; }
    public ICommand AddResponseCommand { get; }
    public ICommand DeleteNodeCommand { get; }
    public ICommand AddActionCommand { get; }
    public ICommand DeleteActionCommand { get; }
    public ICommand AddConditionCommand { get; }
    public ICommand DeleteConditionCommand { get; }
    public ICommand MoveActionUpCommand { get; }
    public ICommand MoveActionDownCommand { get; }

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

    private async void DeleteConversation()
    {
        if (string.IsNullOrEmpty(SelectedConversationFile))
            return;

        var confirmed = await _confirmationService.ShowConfirmationAsync(
            "Delete Conversation",
            $"Are you sure you want to delete the conversation '{SelectedConversationFile}'? This action cannot be undone."
        );

        if (confirmed && _conversationService.DeleteConversation(SelectedConversationFile))
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

    private async void DeleteNode()
    {
        if (SelectedNode == null || CurrentConversation == null)
            return;

        string nodeName = SelectedNode.Name;
        string nodeType = SelectedNode is ConversationPageNode ? "NPC dialogue" : "player response";

        var confirmed = await _confirmationService.ShowConfirmationAsync(
            "Delete Node",
            $"Are you sure you want to delete the {nodeType} '{nodeName}'? This action cannot be undone."
        );

        if (!confirmed)
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

    public void UpdatePageId(string oldPageId, string newPageId)
    {
        if (CurrentConversation == null || string.IsNullOrEmpty(oldPageId) || string.IsNullOrEmpty(newPageId) || oldPageId == newPageId)
            return;

        if (CurrentConversation.Conversation.Pages.ContainsKey(newPageId))
        {
            // TODO: Show error message - page ID already exists
            return;
        }

        if (CurrentConversation.Conversation.Pages.TryGetValue(oldPageId, out var page))
        {
            CurrentConversation.Conversation.Pages.Remove(oldPageId);
            CurrentConversation.Conversation.Pages[newPageId] = page;
            BuildConversationTree();
        }
    }

    private void AddAction()
    {
        if (SelectedResponseNode == null)
            return;

        var newAction = new ConversationAction
        {
            Type = "OpenShop"
        };

        SelectedResponseNode.Response.Actions.Add(newAction);
        SelectedAction = newAction; // Select the new action
        OnPropertyChanged(nameof(SelectedResponseNode));
        OnPropertyChanged(nameof(SelectedResponseNode.Response));
    }

    private bool CanAddAction()
    {
        return SelectedResponseNode != null;
    }

    private void AddCondition()
    {
        if (SelectedResponseNode == null)
            return;

        var newCondition = new ConversationCondition
        {
            Type = "PlayerLevel",
            Operator = "GreaterThanOrEqual",
            Value = 1
        };

        SelectedResponseNode.Response.Conditions.Add(newCondition);
        SelectedCondition = newCondition;
        OnPropertyChanged(nameof(SelectedResponseNode));
        OnPropertyChanged(nameof(SelectedResponseNode.Response));
    }

    private bool CanAddCondition()
    {
        return SelectedResponseNode != null;
    }

    private async void DeleteAction()
    {
        if (SelectedAction == null || SelectedResponseNode == null)
            return;

        var confirmed = await _confirmationService.ShowConfirmationAsync(
            "Delete Action",
            $"Are you sure you want to delete the {SelectedAction.Type} action? This action cannot be undone."
        );

        if (!confirmed)
            return;

        SelectedResponseNode.Response.Actions.Remove(SelectedAction);
        SelectedAction = null; // Clear selection
        OnPropertyChanged(nameof(SelectedResponseNode));
        OnPropertyChanged(nameof(SelectedResponseNode.Response));
    }

    private bool CanDeleteAction()
    {
        return SelectedAction != null && SelectedResponseNode != null;
    }

    private async void DeleteCondition()
    {
        if (SelectedCondition == null || SelectedResponseNode == null)
            return;

        var confirmed = await _confirmationService.ShowConfirmationAsync(
            "Delete Condition",
            $"Are you sure you want to delete the {SelectedCondition.Type} condition? This action cannot be undone."
        );

        if (!confirmed)
            return;

        SelectedResponseNode.Response.Conditions.Remove(SelectedCondition);
        SelectedCondition = null;
        OnPropertyChanged(nameof(SelectedResponseNode));
        OnPropertyChanged(nameof(SelectedResponseNode.Response));
    }

    private bool CanDeleteCondition()
    {
        return SelectedCondition != null && SelectedResponseNode != null;
    }

    public void MoveActionUp(ConversationAction? actionToMove)
    {
        if (SelectedResponseNode == null || actionToMove == null)
            return;

        var actions = SelectedResponseNode.Response.Actions;
        var currentIndex = actions.IndexOf(actionToMove);
        
        if (currentIndex > 0)
        {
            actions.Move(currentIndex, currentIndex - 1);
            OnPropertyChanged(nameof(SelectedResponseNode));
            OnPropertyChanged(nameof(SelectedResponseNode.Response));
        }
    }

    private bool CanMoveActionUp(ConversationAction? actionToMove)
    {
        if (SelectedResponseNode == null || actionToMove == null)
            return false;

        var actions = SelectedResponseNode.Response.Actions;
        var currentIndex = actions.IndexOf(actionToMove);
        return currentIndex > 0;
    }

    public void MoveActionDown(ConversationAction? actionToMove)
    {
        if (SelectedResponseNode == null || actionToMove == null)
            return;

        var actions = SelectedResponseNode.Response.Actions;
        var currentIndex = actions.IndexOf(actionToMove);
        
        if (currentIndex < actions.Count - 1)
        {
            actions.Move(currentIndex, currentIndex + 1);
            OnPropertyChanged(nameof(SelectedResponseNode));
            OnPropertyChanged(nameof(SelectedResponseNode.Response));
        }
    }

    private bool CanMoveActionDown(ConversationAction? actionToMove)
    {
        if (SelectedResponseNode == null || actionToMove == null)
            return false;

        var actions = SelectedResponseNode.Response.Actions;
        var currentIndex = actions.IndexOf(actionToMove);
        return currentIndex < actions.Count - 1;
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
} 