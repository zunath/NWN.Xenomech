using System.Collections.Generic;
using XM.Shared.Core.Conversation;

namespace XM.Chat.UI.Conversation.Actions
{
    /// <summary>
    /// Handles the AcceptQuest conversation action.
    /// </summary>
    public class AcceptQuestActionHandler : IConversationActionHandler
    {
        public void HandleAction(ConversationAction action, uint player, IConversationCallback conversationCallback = null)
        {
            var questId = action.Parameters?.GetValueOrDefault("questId")?.ToString();
            if (!string.IsNullOrEmpty(questId))
            {
                // TODO: Implement quest acceptance logic
                // Since QuestService is internal, we'll just send a message for now
                SendMessageToPC(player, $"Quest '{questId}' would be accepted here.");
            }
        }
    }
} 