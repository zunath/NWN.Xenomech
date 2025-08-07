using System.Collections.ObjectModel;

namespace XM.App.Editor.Models;

public abstract class ConversationTreeNode
{
    public string Name { get; set; } = string.Empty;
    public bool IsExpanded { get; set; } = true;
    public bool IsSelected { get; set; }
    public ObservableCollection<ConversationTreeNode> Children { get; set; } = new();
}

public class ConversationPageNode : ConversationTreeNode
{
    public ConversationPage Page { get; set; } = new();
    public string PageId { get; set; } = string.Empty;
    
    // The page header represents what the NPC says
    public string NpcText => Page.Header;
}

public class ConversationResponseNode : ConversationTreeNode
{
    public ConversationResponse Response { get; set; } = new();
    public int ResponseIndex { get; set; }
    
    // The response text represents what the player can say
    public string PlayerText => Response.Text;
} 