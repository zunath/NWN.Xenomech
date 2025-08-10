using System;
using System.Text.Json;
using XM.Progression.Stat;
using XM.Shared.Core.Conversation;

namespace XM.Chat.UI.Conversation.Conditions
{
    /// <summary>
    /// Handles player level conditions for conversations.
    /// </summary>
    public class PlayerLevelConditionHandler : IConversationConditionHandler
    {
        private readonly StatService _statService;

        public PlayerLevelConditionHandler(StatService statService)
        {
            _statService = statService ?? throw new ArgumentNullException(nameof(statService));
        }

        public bool EvaluateCondition(ConversationCondition condition, uint player)
        {
            int requiredLevel;
            
            // condition.Value is always a JsonElement
            var jsonElement = condition.Value;
            if (jsonElement.ValueKind == JsonValueKind.Number)
            {
                requiredLevel = jsonElement.GetInt32();
            }
            else
            {
                return false;
            }

            var playerLevel = _statService.GetLevel(player);

            return condition.Operator switch
            {
                ComparisonOperator.GreaterThanOrEqual => playerLevel >= requiredLevel,
                ComparisonOperator.GreaterThan => playerLevel > requiredLevel,
                ComparisonOperator.LessThanOrEqual => playerLevel <= requiredLevel,
                ComparisonOperator.LessThan => playerLevel < requiredLevel,
                ComparisonOperator.Equal => playerLevel == requiredLevel,
                ComparisonOperator.NotEqual => playerLevel != requiredLevel,
                _ => false
            };
        }
    }
} 