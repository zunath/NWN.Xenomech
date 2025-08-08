using System.Collections.Generic;
using Anvil.Services;
using XM.Progression.Stat;
using XM.Progression.Skill;
using XM.Shared.Core.Conversation;

namespace XM.Chat.UI.Conversation.Conditions
{
    /// <summary>
    /// Factory for creating conversation condition handlers.
    /// </summary>
    public class ConversationConditionHandlerFactory
    {
        private readonly Dictionary<ConversationConditionType, IConversationConditionHandler> _handlers;
        private readonly object _syncRoot = new();
        private readonly StatService _statService;
        private readonly SkillService _skillService;

        public ConversationConditionHandlerFactory(StatService statService, SkillService skillService)
        {
            _statService = statService;
            _skillService = skillService;
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

            lock (_syncRoot)
            {
                if (_handlers.TryGetValue(conditionType, out handler))
                {
                    return handler;
                }

                // Create handler on-demand with dependency injection
                handler = conditionType switch
                {
                    ConversationConditionType.PlayerLevel => CreatePlayerLevelHandler(),
                    ConversationConditionType.Variable => new VariableConditionHandler(),
                    ConversationConditionType.PlayerSkill => CreatePlayerSkillHandler(),
                    _ => null
                };

                if (handler != null)
                {
                    _handlers[conditionType] = handler;
                }

                return handler;
            }
        }

        private PlayerLevelConditionHandler CreatePlayerLevelHandler()
        {
            var handler = new PlayerLevelConditionHandler(_statService);
            return handler;
        }

        private PlayerSkillConditionHandler CreatePlayerSkillHandler()
        {
            var handler = new PlayerSkillConditionHandler
            {
                Skill = _skillService
            };
            return handler;
        }
    }
} 