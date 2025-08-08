using System.Collections.Generic;
using XM.Shared.Core.Conversation;

namespace XM.Chat.UI.Conversation.Actions
{
    /// <summary>
    /// Handles the ChangePage conversation action.
    /// </summary>
    public class ChangePageActionHandler : IConversationActionHandler
    {
        public void HandleAction(ConversationAction action, uint player, IConversationCallback conversationCallback = null)
        {
            var pageId = action.Parameters?.GetValueOrDefault("pageId")?.ToString();
            if (!string.IsNullOrEmpty(pageId))
            {
                conversationCallback?.NavigateToPage(pageId);
            }
        }
    }
} 