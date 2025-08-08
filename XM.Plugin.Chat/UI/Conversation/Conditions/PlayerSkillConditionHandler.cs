using System;
using System.Text.Json;
using XM.Progression.Skill;
using XM.Shared.Core.Conversation;

namespace XM.Chat.UI.Conversation.Conditions
{
    /// <summary>
    /// Handles Xenomech player skill level conditions.
    /// Supports comparing a specified SkillType to a numeric value.
    /// The condition value should be a string formatted as "<SkillType>:<Level>", e.g. "Longsword:50".
    /// </summary>
    public class PlayerSkillConditionHandler : IConversationConditionHandler
    {
        public SkillService Skill { get; set; }
        public bool EvaluateCondition(ConversationCondition condition, uint player)
        {
            string raw;
            if (condition.Value is JsonElement jsonElement)
            {
                raw = jsonElement.ValueKind == JsonValueKind.String ? jsonElement.GetString() ?? string.Empty : jsonElement.ToString();
            }
            else
            {
                raw = condition.Value.ToString() ?? string.Empty;
            }

            if (string.IsNullOrWhiteSpace(raw))
                return false;

            var parts = raw.Split(':');
            if (parts.Length != 2)
                return false;

            if (!Enum.TryParse<SkillType>(parts[0], true, out var skillType) || skillType == SkillType.Invalid)
                return false;

            if (!int.TryParse(parts[1], out var requiredLevel))
                return false;

            // Fetch current skill level via injected SkillService
            var combatLevel = Skill.GetCombatSkillLevel(player, skillType);
            var currentLevel = combatLevel > 0 ? combatLevel : Skill.GetCraftSkillLevel(player, skillType);

            return condition.Operator switch
            {
                ComparisonOperator.GreaterThanOrEqual => currentLevel >= requiredLevel,
                ComparisonOperator.GreaterThan => currentLevel > requiredLevel,
                ComparisonOperator.LessThanOrEqual => currentLevel <= requiredLevel,
                ComparisonOperator.LessThan => currentLevel < requiredLevel,
                ComparisonOperator.Equal => currentLevel == requiredLevel,
                ComparisonOperator.NotEqual => currentLevel != requiredLevel,
                _ => false
            };
        }
    }
}


