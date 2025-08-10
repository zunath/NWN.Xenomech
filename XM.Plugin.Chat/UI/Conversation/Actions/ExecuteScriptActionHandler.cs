using System.Collections.Generic;
using Anvil.Services;
using XM.Shared.Core.Conversation;

namespace XM.Chat.UI.Conversation.Actions
{
    /// <summary>
    /// Handles the ExecuteScript conversation action.
    /// </summary>
    public class ExecuteScriptActionHandler : IConversationActionHandler
    {
        private readonly IScriptDispatcher _scriptDispatcher;

        public ExecuteScriptActionHandler(IScriptDispatcher scriptDispatcher)
        {
            _scriptDispatcher = scriptDispatcher;
        }

        public void HandleAction(ConversationAction action, uint player, IConversationCallback conversationCallback = null)
        {
            var scriptId = action.Parameters?.GetValueOrDefault("scriptId")?.ToString();
            if (string.IsNullOrWhiteSpace(scriptId))
                return;


            _scriptDispatcher.ExecuteScript(scriptId, player);
        }
    }
} 