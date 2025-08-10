using XM.Shared.Core.Conversation;

namespace XM.Chat.UI.Conversation.Actions
{
    /// <summary>
    /// Interface for conversation action handlers.
    /// </summary>
    public interface IConversationActionHandler
    {
        /// <summary>
        /// Handles the specified conversation action.
        /// </summary>
        /// <param name="action">The action to handle.</param>
        /// <param name="player">The player ID performing the action.</param>
        /// <param name="conversationCallback">Optional callback for conversation-specific actions.</param>
        void HandleAction(ConversationAction action, uint player, IConversationCallback conversationCallback = null);
    }

    /// <summary>
    /// Interface for conversation callbacks that allow action handlers to interact with the conversation system.
    /// </summary>
    public interface IConversationCallback
    {
        /// <summary>
        /// Navigates to a specific page in the conversation.
        /// </summary>
        /// <param name="pageId">The ID of the page to navigate to.</param>
        void NavigateToPage(string pageId);

        /// <summary>
        /// Closes the conversation window.
        /// </summary>
        void CloseConversation();
    }
} 