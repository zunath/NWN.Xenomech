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
            string? typeHint = null; // "int", "float", or "string"
            string? expectedValue = null;

            var parts = valueStr.Split(':');
            if (parts.Length >= 3 && (parts[0].Equals("int", StringComparison.OrdinalIgnoreCase) || parts[0].Equals("float", StringComparison.OrdinalIgnoreCase) || parts[0].Equals("string", StringComparison.OrdinalIgnoreCase)))
            {
                typeHint = parts[0].ToLowerInvariant();
                variableName = parts[1];
                expectedValue = string.Join(':', parts, 2, parts.Length - 2);
            }
            else if (parts.Length == 2)
            {
                variableName = parts[0];
                expectedValue = parts[1];
            }
            else
            {
                variableName = valueStr;
            }

            // No expected value: treat as string existence check (legacy behavior)
            if (expectedValue == null)
            {
                var currentStr = GetLocalString(player, variableName);
                var hasValue = !string.IsNullOrEmpty(currentStr);
                return condition.Operator switch
                {
                    ComparisonOperator.Equal => hasValue,
                    ComparisonOperator.NotEqual => !hasValue,
                    _ => false
                };
            }

            // With expected value: decide type and compare
            // Prefer explicit type hint, otherwise infer from expectedValue
            if (typeHint == null)
            {
                if (int.TryParse(expectedValue, out _))
                {
                    typeHint = "int";
                }
                else if (float.TryParse(expectedValue, System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out _))
                {
                    typeHint = "float";
                }
                else
                {
                    typeHint = "string";
                }
            }

            switch (typeHint)
            {
                case "int":
                    {
                        if (!int.TryParse(expectedValue, out var expectedInt))
                            return false;
                        var currentInt = GetLocalInt(player, variableName);
                        return condition.Operator switch
                        {
                            ComparisonOperator.Equal => currentInt == expectedInt,
                            ComparisonOperator.NotEqual => currentInt != expectedInt,
                            ComparisonOperator.GreaterThan => currentInt > expectedInt,
                            ComparisonOperator.GreaterThanOrEqual => currentInt >= expectedInt,
                            ComparisonOperator.LessThan => currentInt < expectedInt,
                            ComparisonOperator.LessThanOrEqual => currentInt <= expectedInt,
                            _ => false
                        };
                    }
                case "float":
                    {
                        if (!float.TryParse(expectedValue, System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out var expectedFloat))
                            return false;
                        var currentFloat = GetLocalFloat(player, variableName);
                        // Use a small epsilon for float equality
                        const float Epsilon = 0.0001f;
                        return condition.Operator switch
                        {
                            ComparisonOperator.Equal => Math.Abs(currentFloat - expectedFloat) <= Epsilon,
                            ComparisonOperator.NotEqual => Math.Abs(currentFloat - expectedFloat) > Epsilon,
                            ComparisonOperator.GreaterThan => currentFloat > expectedFloat,
                            ComparisonOperator.GreaterThanOrEqual => currentFloat > expectedFloat || Math.Abs(currentFloat - expectedFloat) <= Epsilon,
                            ComparisonOperator.LessThan => currentFloat < expectedFloat,
                            ComparisonOperator.LessThanOrEqual => currentFloat < expectedFloat || Math.Abs(currentFloat - expectedFloat) <= Epsilon,
                            _ => false
                        };
                    }
                default:
                    {
                        var currentStr = GetLocalString(player, variableName);
                        return condition.Operator switch
                        {
                            ComparisonOperator.Equal => currentStr == expectedValue,
                            ComparisonOperator.NotEqual => currentStr != expectedValue,
                            ComparisonOperator.Contains => !string.IsNullOrEmpty(currentStr) && currentStr.Contains(expectedValue),
                            ComparisonOperator.NotContains => string.IsNullOrEmpty(currentStr) || !currentStr.Contains(expectedValue),
                            _ => false
                        };
                    }
            }
        }
    }
} 