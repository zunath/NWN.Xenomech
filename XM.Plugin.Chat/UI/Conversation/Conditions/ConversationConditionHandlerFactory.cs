using System.Collections.Generic;
using Anvil.Services;
using XM.Progression.Stat;
using XM.Shared.Core.Conversation;

namespace XM.Chat.UI.Conversation.Conditions
{
    /// <summary>
    /// Factory for creating conversation condition handlers.
    /// </summary>
    public class ConversationConditionHandlerFactory
    {
        private readonly Dictionary<ConversationConditionType, IConversationConditionHandler> _handlers;
        private readonly StatService _statService;

        public ConversationConditionHandlerFactory(StatService statService)
        {
            _statService = statService;
            _handlers = new Dictionary<ConversationConditionType, IConversationConditionHandler>();
        }

        /// <summary>
        /// Gets the appropriate handler for the specified condition type.
        /// </summary>
        /// <param name="conditionType">The type of condition to handle.</param>
        /// <returns>The condition handler, or null if no handler exists for the condition type.</returns>
        public IConversationConditionHandler GetHandler(ConversationConditionType conditionType)
        {
            if (_handlers.TryGetValue(conditionType, out var handler))
            {
                return handler;
            }

            // Create handler on-demand with dependency injection
            handler = conditionType switch
            {
                ConversationConditionType.PlayerLevel => CreatePlayerLevelHandler(),
                ConversationConditionType.Variable => new VariableConditionHandler(),
                _ => null
            };

            if (handler != null)
            {
                _handlers[conditionType] = handler;
            }

            return handler;
        }

        private PlayerLevelConditionHandler CreatePlayerLevelHandler()
        {
            var handler = new PlayerLevelConditionHandler();
            handler.Stat = _statService;
            return handler;
        }
    }
} 