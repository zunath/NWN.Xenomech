using System.Collections.ObjectModel;

namespace XM.App.Editor.Models;

public abstract class ConversationTreeNode
{
    public string Name { get; set; } = string.Empty;
    public string NodePath { get; set; } = string.Empty;
    public bool IsExpanded { get; set; } = true;
    public bool IsSelected { get; set; }
    public ObservableCollection<ConversationTreeNode> Children { get; set; } = new();
}