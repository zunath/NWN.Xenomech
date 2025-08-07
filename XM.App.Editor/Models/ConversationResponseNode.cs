namespace XM.App.Editor.Models;

public class ConversationResponseNode : ConversationTreeNode
{
    public ConversationResponse Response { get; set; } = new();
    public int ResponseIndex { get; set; }
    
    public string PlayerText => Response.Text;
}


