using System;
using System.Text.Json;
using Anvil.Services;
using XM.Progression.Stat;
using XM.Shared.Core.Conversation;

namespace XM.Chat.UI.Conversation.Conditions
{
    /// <summary>
    /// Handles player level conditions for conversations.
    /// </summary>
    public class PlayerLevelConditionHandler : IConversationConditionHandler
    {
        public StatService Stat { get; set; }

        public bool EvaluateCondition(ConversationCondition condition, uint player)
        {
            if (condition.Value == null)
                return false;

            int requiredLevel;
            
            // Handle JsonElement from JSON deserialization
            if (condition.Value is JsonElement jsonElement)
            {
                if (jsonElement.ValueKind == JsonValueKind.Number)
                {
                    requiredLevel = jsonElement.GetInt32();
                }
                else
                {
                    return false;
                }
            }
            else
            {
                // Handle direct integer values
                requiredLevel = Convert.ToInt32(condition.Value);
            }

            var playerLevel = Stat.GetLevel(player);

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