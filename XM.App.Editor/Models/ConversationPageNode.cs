namespace XM.App.Editor.Models;

public class ConversationPageNode : ConversationTreeNode
{
    public ConversationPage Page { get; set; } = new();
    public string PageId { get; set; } = string.Empty;
    
    public string NpcText => Page.Header;
}


