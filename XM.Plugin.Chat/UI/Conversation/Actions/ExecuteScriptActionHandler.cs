using System.Collections.Generic;
using XM.Shared.Core.Conversation;

namespace XM.Chat.UI.Conversation.Actions
{
    /// <summary>
    /// Handles the ExecuteScript conversation action.
    /// </summary>
    public class ExecuteScriptActionHandler : IConversationActionHandler
    {
        public void HandleAction(ConversationAction action, uint player, IConversationCallback conversationCallback = null)
        {
            var scriptId = action.Parameters?.GetValueOrDefault("scriptId")?.ToString();
            if (!string.IsNullOrEmpty(scriptId))
            {
                // TODO: Implement script execution logic
                // ExecuteScript is commented out in NWScript.cs, so we'll just send a message for now
                SendMessageToPC(player, $"Script '{scriptId}' would execute here.");
            }
        }
    }
} 