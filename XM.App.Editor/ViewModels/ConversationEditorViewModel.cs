using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using System.Linq;
using XM.App.Editor.Models;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using StbImageSharp;
using System.Runtime.InteropServices;
using XM.App.Editor.Services;

namespace XM.App.Editor.ViewModels;

public class ConversationEditorViewModel : INotifyPropertyChanged, IDisposable
{
    private readonly ConversationService _conversationService;
    private readonly IConfirmationService _confirmationService;
    private readonly IUserSettingsService _userSettingsService;
    private string? _selectedConversationFile;
    private ConversationData? _currentConversation;
    private ConversationTreeNode? _selectedNode;
    private ConversationPage? _subscribedPageForIdChanges;
    private readonly List<ConversationPage> _allSubscribedPages = new();

    public ObservableCollection<IconOption> AvailableResponseIcons { get; } = new();

    public ConversationEditorViewModel(
        IUserSettingsService userSettingsService,
        ConversationService conversationService,
        IConfirmationService confirmationService)
    {
        _userSettingsService = userSettingsService;
        _conversationService = conversationService;
        _confirmationService = confirmationService;
        // Load icon options
        LoadResponseIcons();

        // Initialize commands
        CreateNewConversationCommand = new RelayCommand(CreateNewConversation);
        DeleteConversationCommand = new RelayCommand(DeleteConversation, CanDeleteConversation);
        SaveConversationCommand = new RelayCommand(SaveConversation, CanSaveConversation);
        AddPageCommand = new RelayCommand(AddPage, CanAddPage);
        AddResponseCommand = new RelayCommand(AddResponse, CanAddResponse);
        DeleteNodeCommand = new RelayCommand(DeleteNode, CanDeleteNode);
        AddNodeCommand = new RelayCommand(AddNode, CanAddNode);
        AddActionCommand = new RelayCommand(AddAction, CanAddAction);
        DeleteActionCommand = new RelayCommand(DeleteAction, CanDeleteAction);
        AddConditionCommand = new RelayCommand(AddCondition, CanAddCondition);
        DeleteConditionCommand = new RelayCommand(DeleteCondition, CanDeleteCondition);
        MoveActionUpCommand = new RelayCommand<ConversationAction>(MoveActionUp, CanMoveActionUp);
        MoveActionDownCommand = new RelayCommand<ConversationAction>(MoveActionDown, CanMoveActionDown);
        AddNextNpcCommand = new RelayCommand(AddNextNpc, CanAddNextNpc);
        DeleteNextNpcCommand = new RelayCommand(DeleteNextNpc, CanDeleteNextNpc);

        // Load conversation files
        LoadConversationFiles();

        // Restore last opened conversation if available
        if (!string.IsNullOrWhiteSpace(_userSettingsService.Current.LastOpenedConversationPath) &&
            ConversationFiles.Contains(_userSettingsService.Current.LastOpenedConversationPath))
        {
            SelectedConversationFile = _userSettingsService.Current.LastOpenedConversationPath;
        }
    }

    public ObservableCollection<string> ConversationFiles { get; } = new();
    public ObservableCollection<ConversationTreeNode> ConversationTree { get; } = new();
    public ObservableCollection<string> AvailablePageIds { get; } = new();

    public string? SelectedConversationFile
    {
        get => _selectedConversationFile;
        set
        {
            _selectedConversationFile = value;
            OnPropertyChanged(nameof(SelectedConversationFile));
            LoadSelectedConversation();
            // Persist selection
            _userSettingsService.Current.LastOpenedConversationPath = _selectedConversationFile;
            ((RelayCommand)DeleteConversationCommand).RaiseCanExecuteChanged();
            ((RelayCommand)SaveConversationCommand).RaiseCanExecuteChanged();
            ((RelayCommand)AddPageCommand).RaiseCanExecuteChanged();
            ((RelayCommand)AddNodeCommand).RaiseCanExecuteChanged();
            OnPropertyChanged(nameof(AddNodeButtonText));
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
            OnPropertyChanged(nameof(SelectedResponseIcon));

            // If a different node is selected, clear the selected action so the Action Details panel hides
            if (SelectedAction != null)
            {
                SelectedAction = null;
            }

            // Rewire page Id change subscription
            if (_subscribedPageForIdChanges is INotifyPropertyChanged oldInpc)
            {
                oldInpc.PropertyChanged -= OnSelectedPagePropertyChanged;
            }
            _subscribedPageForIdChanges = null;

            if (SelectedPageNode?.Page is INotifyPropertyChanged newInpc)
            {
                _subscribedPageForIdChanges = SelectedPageNode.Page;
                newInpc.PropertyChanged += OnSelectedPagePropertyChanged;
            }
            ((RelayCommand)AddResponseCommand).RaiseCanExecuteChanged();
            ((RelayCommand)DeleteNodeCommand).RaiseCanExecuteChanged();
            ((RelayCommand)AddNodeCommand).RaiseCanExecuteChanged();
            OnPropertyChanged(nameof(AddNodeButtonText));
            ((RelayCommand)AddActionCommand).RaiseCanExecuteChanged();
            ((RelayCommand)DeleteActionCommand).RaiseCanExecuteChanged();
            ((RelayCommand)AddConditionCommand).RaiseCanExecuteChanged();
            ((RelayCommand)DeleteConditionCommand).RaiseCanExecuteChanged();
            ((RelayCommand)AddNextNpcCommand).RaiseCanExecuteChanged();
            ((RelayCommand)DeleteNextNpcCommand).RaiseCanExecuteChanged();
            RefreshAvailablePageIds();
        }
    }

    public ConversationPageNode? SelectedPageNode => SelectedNode as ConversationPageNode;
    public ConversationResponseNode? SelectedResponseNode => SelectedNode as ConversationResponseNode;

    public IconOption? SelectedResponseIcon
    {
        get
        {
            var iconName = SelectedResponseNode?.Response.Icon ?? string.Empty;
                // Normalize (strip .tga if present)
            if (iconName.EndsWith(".tga", StringComparison.OrdinalIgnoreCase))
                iconName = iconName[..^4];
                // Map stored "Blank" to empty option in UI
                if (string.Equals(iconName, "Blank", StringComparison.OrdinalIgnoreCase))
                    iconName = string.Empty;
            return AvailableResponseIcons.FirstOrDefault(x => string.Equals(x.Name, iconName, StringComparison.OrdinalIgnoreCase));
        }
        set
        {
            if (SelectedResponseNode == null)
                return;
                var newName = value?.Name ?? string.Empty;
                // When empty option is selected, persist as "Blank"
                if (string.IsNullOrEmpty(newName))
                {
                    newName = "Blank";
                }
            if (!string.Equals(SelectedResponseNode.Response.Icon, newName, StringComparison.Ordinal))
            {
                SelectedResponseNode.Response.Icon = newName; // store without .tga
                OnPropertyChanged(nameof(SelectedResponseIcon));
                OnPropertyChanged(nameof(SelectedResponseNode));
                OnPropertyChanged(nameof(SelectedResponseNode.Response));
            }
        }
    }

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
                // Ensure dropdown reflects latest ids when switching actions
                RefreshAvailablePageIds();
                OnPropertyChanged(nameof(SelectedChangePageItem));
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
                // Ensure operator is valid/displayable when a condition is selected
                if (_selectedCondition != null)
                {
                    var ops = _selectedCondition.AvailableOperators;
                    if (string.IsNullOrWhiteSpace(_selectedCondition.Operator) || !ops.Contains(_selectedCondition.Operator))
                    {
                        _selectedCondition.Operator = ops.FirstOrDefault() ?? "Equal";
                    }
                }
                ((RelayCommand)DeleteConditionCommand).RaiseCanExecuteChanged();
            }
        }
    }

    public int PagesCount => CurrentConversation?.Conversation.Root == null ? 0 : 1; // root-based structure

    public List<string> ActionTypes { get; } = new()
    {
        "ChangePage",
        "OpenShop",
        "GiveItem",
        "StartQuest",
        "Teleport",
        "ExecuteScript",
        "EndConversation",
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
    public ICommand AddNextNpcCommand { get; }
    public ICommand DeleteNextNpcCommand { get; }
    public ICommand AddNodeCommand { get; }

    public string AddNodeButtonText
    {
        get
        {
            // No conversation loaded
            if (CurrentConversation == null)
                return "Add";

            // No root yet or nothing selected: offer to add root NPC
            if (CurrentConversation.Conversation.Root == null || SelectedNode == null)
                return "Add NPC";

            // Selected NPC page: add a player response
            if (SelectedNode is ConversationPageNode)
                return "Add Response";

            // Selected player response: add next NPC if not present
            if (SelectedNode is ConversationResponseNode rn)
                return rn.Response.Next == null ? "Add NPC" : "Add";

            return "Add";
        }
    }

    private void LoadConversationFiles()
    {
        ConversationFiles.Clear();
        var files = _conversationService.GetConversationFiles();
        foreach (var file in files)
        {
            ConversationFiles.Add(file);
        }
    }

    private void LoadResponseIcons()
    {
        try
        {
            AvailableResponseIcons.Clear();
            var repoRoot = FindRepoRootForImages(AppContext.BaseDirectory) ?? FindRepoRootForImages(Directory.GetCurrentDirectory());
            if (string.IsNullOrEmpty(repoRoot))
                return;
            var guiPath = Path.Combine(repoRoot!, "Content", "xm_gui");

            // Add an empty option at the top (represents "Blank" in saved JSON)
            var emptyBitmap = new WriteableBitmap(new PixelSize(24, 24), new Vector(96, 96), PixelFormat.Rgba8888, AlphaFormat.Unpremul);
            AvailableResponseIcons.Add(new IconOption { Name = string.Empty, Image = emptyBitmap });
            var names = new[] { "resp_check", "resp_exclaim", "resp_question", "resp_speech", "resp_x" };
            foreach (var name in names)
            {
                var tgaFile = Path.Combine(guiPath, name + ".tga");
                if (!File.Exists(tgaFile))
                    continue;
                var bitmap = LoadBitmapFromTga(tgaFile);
                if (bitmap != null)
                {
                    AvailableResponseIcons.Add(new IconOption { Name = name, Image = bitmap });
                }
            }
        }
        catch
        {
            // Ignore icon load failures; UI will fallback to text
        }
    }

    private static Bitmap? LoadBitmapFromTga(string filePath)
    {
        try
        {
            using var fs = File.OpenRead(filePath);
            var result = ImageResult.FromStream(fs, ColorComponents.RedGreenBlueAlpha);
            var pixelSize = new PixelSize(result.Width, result.Height);
            var wb = new WriteableBitmap(pixelSize, new Vector(96, 96), PixelFormat.Rgba8888, AlphaFormat.Unpremul);
            using var fb = wb.Lock();
            var stride = fb.RowBytes;
            // If stride matches width*4, can copy in one go; else row by row
            if (stride == result.Width * 4)
            {
                Marshal.Copy(result.Data, 0, fb.Address, result.Data.Length);
            }
            else
            {
                for (int y = 0; y < result.Height; y++)
                {
                    var srcOffset = y * result.Width * 4;
                    var dstPtr = fb.Address + y * stride;
                    Marshal.Copy(result.Data, srcOffset, dstPtr, result.Width * 4);
                }
            }
            return wb;
        }
        catch
        {
            return null;
        }
    }

    private static string? FindRepoRootForImages(string startPath)
    {
        try
        {
            var dirInfo = new DirectoryInfo(startPath);
            for (int i = 0; i < 10 && dirInfo != null; i++)
            {
                var sln = Path.Combine(dirInfo.FullName, "Xenomech.sln");
                var contentDir = Path.Combine(dirInfo.FullName, "Content", "xm_gui");
                if (File.Exists(sln) && Directory.Exists(contentDir))
                    return dirInfo.FullName;
                dirInfo = dirInfo.Parent;
            }
        }
        catch { }
        return null;
    }

    private void LoadSelectedConversation()
    {
        if (string.IsNullOrEmpty(SelectedConversationFile))
        {
            CurrentConversation = null;
            return;
        }

        CurrentConversation = _conversationService.LoadConversation(SelectedConversationFile);
        // Normalize any legacy icon values to exclude .tga extension
        if (CurrentConversation?.Conversation.Root != null)
        {
            NormalizeIconsRecursive(CurrentConversation.Conversation.Root);
        }
    }

    private void BuildConversationTree()
    {
        // Preserve selection/expansion state using NodePath keys
        var previousSelectedPath = (SelectedNode as ConversationTreeNode)?.NodePath;
        var expandedPaths = new HashSet<string>(ConversationTree
            .SelectMany(RetrieveExpandedPaths));

        ConversationTree.Clear();
        var previousSelectedNode = SelectedNode; // keep reference for type info
        SelectedNode = null;

        if (CurrentConversation?.Conversation.Root == null)
        {
            // No pages available
            AvailablePageIds.Clear();
            return;
        }

        var rebuiltRoot = BuildTreeRecursive(CurrentConversation.Conversation.Root, "root");
        // Auto-expand root
        rebuiltRoot.IsExpanded = true;
        // Restore expansions
        RestoreExpansion(rebuiltRoot, expandedPaths);
        ConversationTree.Add(rebuiltRoot);

        // Subscribe to Id changes across all pages in the tree
        ResubscribeAllPageIdChanges();

        RefreshAvailablePageIds();

        // Restore selection if possible
        if (!string.IsNullOrEmpty(previousSelectedPath))
        {
            var nodeToSelect = FindByPath(rebuiltRoot, previousSelectedPath);
            if (nodeToSelect == null)
            {
                // If deleted response, fallback to parent page selection
                var lastSlash = previousSelectedPath.LastIndexOf('/')
                                 >= 0 ? previousSelectedPath.LastIndexOf('/') : -1;
                if (lastSlash > 0)
                {
                    var parentPath = previousSelectedPath.Substring(0, lastSlash);
                    nodeToSelect = FindByPath(rebuiltRoot, parentPath);
                }
            }

            if (nodeToSelect != null)
            {
                nodeToSelect.IsSelected = true;
                SelectedNode = nodeToSelect;
            }
        }
        // Update Add button state/label after rebuild
        ((RelayCommand)AddNodeCommand).RaiseCanExecuteChanged();
        OnPropertyChanged(nameof(AddNodeButtonText));
    }

    private ConversationPageNode BuildTreeRecursive(ConversationPage page, string pageId)
    {
        var pageNode = new ConversationPageNode
        {
            Name = string.IsNullOrEmpty(page.Header) ? $"NPC: {pageId}" : page.Header,
            PageId = pageId,
            Page = page,
            NodePath = $"P:{pageId}"
        };

        if (page.Responses != null)
        {
            for (int i = 0; i < page.Responses.Count; i++)
            {
                var response = page.Responses[i];
                var responseNode = new ConversationResponseNode
                {
                    Name = string.IsNullOrEmpty(response.Text) ? $"Player Option {i + 1}" : response.Text,
                    Response = response,
                    ResponseIndex = i,
                    NodePath = $"P:{pageId}/R:{i}"
                };
                pageNode.Children.Add(responseNode);

                if (response.Next != null)
                {
                    var childPageId = $"{pageId}.{i + 1}";
                    var child = BuildTreeRecursive(response.Next, childPageId);
                    responseNode.Children.Add(child);
                }
            }
        }

        return pageNode;
    }

    private void CollectPageIds(ConversationPage page, string pageId, List<string> accumulator)
    {
        // Only include pages that have an explicit Id
        if (!string.IsNullOrWhiteSpace(page.Id))
        {
            accumulator.Add(page.Id!);
        }
        if (page.Responses == null)
            return;
        for (int i = 0; i < page.Responses.Count; i++)
        {
            var resp = page.Responses[i];
            if (resp.Next != null)
            {
                var childId = $"{pageId}.{i + 1}";
                CollectPageIds(resp.Next, childId, accumulator);
            }
        }
    }

    private void RefreshAvailablePageIds()
    {
        AvailablePageIds.Clear();
        if (CurrentConversation?.Conversation.Root == null)
            return;

        var pageIds = new List<string>();
        CollectPageIds(CurrentConversation.Conversation.Root, "root", pageIds);
        foreach (var id in pageIds.Distinct())
            AvailablePageIds.Add(id);

        // Normalize SelectedAction.PageId to an item from the list (case-insensitive)
        if (SelectedAction != null && SelectedAction.Type == "ChangePage" && !string.IsNullOrWhiteSpace(SelectedAction.PageId))
        {
            var match = AvailablePageIds.FirstOrDefault(x => string.Equals(x, SelectedAction.PageId, StringComparison.OrdinalIgnoreCase));
            if (!string.IsNullOrEmpty(match) && !string.Equals(match, SelectedAction.PageId, StringComparison.Ordinal))
            {
                SelectedAction.PageId = match; // ensure exact reference equals an item in ItemsSource
            }
        }

        OnPropertyChanged(nameof(SelectedChangePageItem));
    }

    public string? SelectedChangePageItem
    {
        get
        {
            if (SelectedAction == null || SelectedAction.Type != "ChangePage") return null;
            if (string.IsNullOrWhiteSpace(SelectedAction.PageId)) return null;
            return AvailablePageIds.FirstOrDefault(x => string.Equals(x, SelectedAction.PageId, StringComparison.OrdinalIgnoreCase));
        }
        set
        {
            if (SelectedAction == null || SelectedAction.Type != "ChangePage") return;
            var newValue = value ?? string.Empty;
            if (!string.Equals(SelectedAction.PageId, newValue, StringComparison.Ordinal))
            {
                SelectedAction.PageId = newValue;
                OnPropertyChanged(nameof(SelectedChangePageItem));
            }
        }
    }

    private void ResubscribeAllPageIdChanges()
    {
        // Unsubscribe previous
        foreach (var page in _allSubscribedPages)
        {
            if (page is INotifyPropertyChanged inpc)
            {
                inpc.PropertyChanged -= OnAnyPagePropertyChanged;
            }
        }
        _allSubscribedPages.Clear();

        if (CurrentConversation?.Conversation.Root == null)
            return;

        SubscribePageRecursive(CurrentConversation.Conversation.Root);
    }

    private void SubscribePageRecursive(ConversationPage page)
    {
        if (page is INotifyPropertyChanged inpc)
        {
            inpc.PropertyChanged += OnAnyPagePropertyChanged;
            _allSubscribedPages.Add(page);
        }

        if (page.Responses == null)
            return;

        foreach (var response in page.Responses)
        {
            if (response.Next != null)
            {
                SubscribePageRecursive(response.Next);
            }
        }
    }

    private void OnAnyPagePropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(ConversationPage.Id))
        {
            RefreshAvailablePageIds();
        }
    }

    private void OnSelectedPagePropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(ConversationPage.Id))
        {
            RefreshAvailablePageIds();
        }
    }

    private IEnumerable<string> RetrieveExpandedPaths(ConversationTreeNode node)
    {
        var result = new List<string>();
        if (node.IsExpanded && !string.IsNullOrEmpty(node.NodePath))
            result.Add(node.NodePath);

        foreach (var child in node.Children)
            result.AddRange(RetrieveExpandedPaths(child));

        return result;
    }

    private void RestoreExpansion(ConversationTreeNode node, HashSet<string> expandedPaths)
    {
        if (!string.IsNullOrEmpty(node.NodePath))
            node.IsExpanded = expandedPaths.Contains(node.NodePath) || node.NodePath == "P:root";
        foreach (var child in node.Children)
            RestoreExpansion(child, expandedPaths);
    }

    private ConversationTreeNode? FindByPath(ConversationTreeNode node, string targetPath)
    {
        if (node.NodePath == targetPath)
            return node;
        foreach (var child in node.Children)
        {
            var found = FindByPath(child, targetPath);
            if (found != null) return found;
        }
        return null;
    }

    private void CreateNewConversation()
    {
        // TODO: Show dialog to get conversation details
        var ticks = DateTime.Now.Ticks;
        var newConversation = _conversationService.CreateNewConversation(
            "new_conversation_" + ticks,
            "New Conversation",
            "A new conversation"
        );

        var fileName = $"new_conversation_{ticks}";
        if (_conversationService.SaveConversation(fileName, newConversation))
        {
            LoadConversationFiles();
            SelectedConversationFile = fileName;
        }
    }

    private async void DeleteConversation()
    {
        try
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
        catch (Exception ex)
        {
            // Show a user-friendly error and log for diagnostics
            await _confirmationService.ShowConfirmationAsync(
                "Error",
                $"Failed to delete the conversation.\nDetails: {ex.Message}"
            );
            System.Diagnostics.Debug.WriteLine($"[ConversationEditor] Error deleting conversation '{SelectedConversationFile}': {ex}");
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

        // Ensure icons are saved without .tga extension
        if (CurrentConversation.Conversation.Root != null)
        {
            NormalizeIconsRecursive(CurrentConversation.Conversation.Root);
        }
        _conversationService.SaveConversation(SelectedConversationFile, CurrentConversation);
    }

    private bool CanSaveConversation()
    {
        return !string.IsNullOrEmpty(SelectedConversationFile) && CurrentConversation != null;
    }

    private static void NormalizeIconsRecursive(ConversationPage page)
    {
        if (page.Responses == null)
            return;
        for (int i = 0; i < page.Responses.Count; i++)
        {
            var resp = page.Responses[i];
            if (!string.IsNullOrWhiteSpace(resp.Icon) && resp.Icon.EndsWith(".tga", StringComparison.OrdinalIgnoreCase))
            {
                resp.Icon = resp.Icon[..^4];
            }
            if (resp.Next != null)
            {
                NormalizeIconsRecursive(resp.Next);
            }
        }
    }

    private void AddPage()
    {
        if (CurrentConversation == null)
            return;

        var newPage = new ConversationPage { Header = "New Page" };
        CurrentConversation.Conversation.Root = newPage;
        BuildConversationTree();
        RefreshAvailablePageIds();
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

        if (SelectedPageNode.Page.Responses == null)
        {
            SelectedPageNode.Page.Responses = new List<ConversationResponse>();
        }
        SelectedPageNode.Page.Responses.Add(newResponse);
        BuildConversationTree();
        RefreshAvailablePageIds();
    }

    private bool CanAddResponse()
    {
        return SelectedPageNode != null;
    }

    private void AddNode()
    {
        if (!CanAddNode())
            return;

        // If there's no root yet or nothing selected, create root NPC page
        if (CurrentConversation != null && (CurrentConversation.Conversation.Root == null || SelectedNode == null))
        {
            AddPage();
            return;
        }

        // If an NPC page is selected, add a player response
        if (SelectedPageNode != null)
        {
            AddResponse();
            return;
        }

        // If a player response without a next NPC is selected, add next NPC
        if (SelectedResponseNode != null && SelectedResponseNode.Response.Next == null)
        {
            AddNextNpc();
            return;
        }
    }

    private bool CanAddNode()
    {
        if (CurrentConversation == null)
            return false;

        if (CurrentConversation.Conversation.Root == null || SelectedNode == null)
            return true; // allow creating root

        if (SelectedPageNode != null)
            return true; // can add response

        if (SelectedResponseNode != null && SelectedResponseNode.Response.Next == null)
            return true; // can add next NPC

        return false;
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
            // If deleting root, clear root
            if (pageNode.Page == CurrentConversation.Conversation.Root)
            {
                CurrentConversation.Conversation.Root = null;
            }
            else
            {
                // Find the parent response whose Next references this page and remove it
                var parentResponseNode = ConversationTree
                    .SelectMany(p => GetAllResponseNodes(p))
                    .FirstOrDefault(rn => rn.Response.Next == pageNode.Page);

                if (parentResponseNode != null)
                {
                    parentResponseNode.Response.Next = null;
                }
            }
        }
        else if (SelectedNode is ConversationResponseNode responseNode)
        {
            // Find parent page node that owns this response
            var parentPageNode = ConversationTree
                .SelectMany(p => GetAllPageNodes(p))
                .FirstOrDefault(pn => pn.Page.Responses.Contains(responseNode.Response));

            if (parentPageNode != null)
            {
                parentPageNode.Page.Responses.RemoveAt(responseNode.ResponseIndex);
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
        // With hierarchical model, page IDs are implicit; no-op retained for binding compatibility
        return;
    }

    private IEnumerable<ConversationPageNode> GetAllPageNodes(ConversationTreeNode node)
    {
        if (node is ConversationPageNode pn)
            yield return pn;
        foreach (var child in node.Children)
            foreach (var pnChild in GetAllPageNodes(child))
                yield return pnChild;
    }

    private IEnumerable<ConversationResponseNode> GetAllResponseNodes(ConversationTreeNode node)
    {
        if (node is ConversationResponseNode rn)
            yield return rn;
        foreach (var child in node.Children)
            foreach (var rnChild in GetAllResponseNodes(child))
                yield return rnChild;
    }

    private void AddAction()
    {
        if (SelectedResponseNode == null)
            return;

        var newAction = new ConversationAction
        {
            Type = "OpenShop"
        };

        if (SelectedResponseNode.Response.Actions == null)
        {
            SelectedResponseNode.Response.Actions = new ObservableCollection<ConversationAction>();
        }
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

        if (SelectedResponseNode.Response.Conditions == null)
        {
            SelectedResponseNode.Response.Conditions = new ObservableCollection<ConversationCondition>();
        }
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

    private void AddNextNpc()
    {
        if (SelectedResponseNode == null)
            return;

        if (SelectedResponseNode.Response.Next == null)
        {
            SelectedResponseNode.Response.Next = new ConversationPage
            {
                Header = "NPC continues...",
                Responses = new List<ConversationResponse>()
                {
                    new ConversationResponse { Text = "Back" }
                }
            };
            BuildConversationTree();
            RefreshAvailablePageIds();
        }
    }

    private bool CanAddNextNpc()
    {
        return SelectedResponseNode != null && SelectedResponseNode.Response.Next == null;
    }

    private void DeleteNextNpc()
    {
        if (SelectedResponseNode == null || SelectedResponseNode.Response.Next == null)
            return;

        SelectedResponseNode.Response.Next = null;
        BuildConversationTree();
        RefreshAvailablePageIds();
    }

    private bool CanDeleteNextNpc()
    {
        return SelectedResponseNode != null && SelectedResponseNode.Response.Next != null;
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public void Dispose()
    {
        if (_subscribedPageForIdChanges is INotifyPropertyChanged oldInpc)
        {
            oldInpc.PropertyChanged -= OnSelectedPagePropertyChanged;
        }
        _subscribedPageForIdChanges = null;

        foreach (var page in _allSubscribedPages)
        {
            if (page is INotifyPropertyChanged inpc)
            {
                inpc.PropertyChanged -= OnAnyPagePropertyChanged;
            }
        }
        _allSubscribedPages.Clear();

        // Dispose any loaded icon bitmaps
        foreach (var icon in AvailableResponseIcons)
        {
            icon.Dispose();
        }
        AvailableResponseIcons.Clear();
    }
}