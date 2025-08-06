using XM.Shared.Core.Conversation;

namespace XM.Chat.UI.Conversation.Conditions
{
    /// <summary>
    /// Interface for conversation condition handlers.
    /// </summary>
    public interface IConversationConditionHandler
    {
        /// <summary>
        /// Evaluates whether the specified condition is met.
        /// </summary>
        /// <param name="condition">The condition to evaluate.</param>
        /// <param name="player">The player ID to evaluate the condition for.</param>
        /// <returns>True if the condition is met, false otherwise.</returns>
        bool EvaluateCondition(ConversationCondition condition, uint player);
    }
} 