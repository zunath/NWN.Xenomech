using System.Collections.Generic;
using Anvil.Services;
using XM.Shared.Core.Conversation;

namespace XM.Chat.UI.Conversation.Actions
{
    /// <summary>
    /// Factory for creating conversation action handlers.
    /// </summary>
    [ServiceBinding(typeof(ConversationActionHandlerFactory))]
    public class ConversationActionHandlerFactory
    {
        private readonly Dictionary<ConversationActionType, IConversationActionHandler> _handlers;

        public ConversationActionHandlerFactory(IScriptDispatcher scriptDispatcher)
        {
            _handlers = new Dictionary<ConversationActionType, IConversationActionHandler>
            {
                { ConversationActionType.ChangePage, new ChangePageActionHandler() },
                { ConversationActionType.EndConversation, new EndConversationActionHandler() },
                { ConversationActionType.OpenShop, new OpenShopActionHandler() },
                { ConversationActionType.Teleport, new TeleportActionHandler() },
                { ConversationActionType.AcceptQuest, new AcceptQuestActionHandler() },
                { ConversationActionType.GiveItem, new GiveItemActionHandler() },
                { ConversationActionType.ExecuteScript, new ExecuteScriptActionHandler(scriptDispatcher) }
            };
        }

        /// <summary>
        /// Gets the appropriate handler for the specified action type.
        /// </summary>
        /// <param name="actionType">The type of action to handle.</param>
        /// <returns>The action handler, or null if no handler exists for the action type.</returns>
        public IConversationActionHandler GetHandler(ConversationActionType actionType)
        {
            return _handlers.TryGetValue(actionType, out var handler) ? handler : null;
        }
    }
} 