using System;
using System.Text.Json;
using Anvil.Services;
using XM.Shared.Core.Conversation;
using XM.Shared.Core.Data;

namespace XM.Chat.UI.Conversation.Conditions
{
    /// <summary>
    /// Handles variable conditions for conversations.
    /// </summary>
    public class VariableConditionHandler : IConversationConditionHandler
    {
        public bool EvaluateCondition(ConversationCondition condition, uint player)
        {
            if (condition.Value == null)
                return false;

            string valueStr;
            
            // Handle JsonElement from JSON deserialization
            if (condition.Value is JsonElement jsonElement)
            {
                if (jsonElement.ValueKind == JsonValueKind.String)
                {
                    valueStr = jsonElement.GetString();
                }
                else
                {
                    valueStr = jsonElement.ToString();
                }
            }
            else
            {
                // Handle direct string values
                valueStr = condition.Value.ToString();
            }

            if (string.IsNullOrEmpty(valueStr))
                return false;

            string variableName;
            string expectedValue = null;

            if (valueStr.Contains(':'))
            {
                var parts = valueStr.Split(':', 2);
                if (parts.Length != 2)
                    return false;

                variableName = parts[0];
                expectedValue = parts[1];
            }
            else
            {
                variableName = valueStr;
            }

            var currentValue = GetLocalString(player, variableName);

            if (expectedValue == null)
            {
                // Just check if variable exists and has a value
                var hasValue = !string.IsNullOrEmpty(currentValue);
                return condition.Operator switch
                {
                    ComparisonOperator.Equal => hasValue,
                    ComparisonOperator.NotEqual => !hasValue,
                    _ => false
                };
            }
            else
            {
                // Compare variable value
                return condition.Operator switch
                {
                    ComparisonOperator.Equal => currentValue == expectedValue,
                    ComparisonOperator.NotEqual => currentValue != expectedValue,
                    ComparisonOperator.Contains => currentValue.Contains(expectedValue),
                    ComparisonOperator.NotContains => !currentValue.Contains(expectedValue),
                    _ => false
                };
            }
        }
    }
} 