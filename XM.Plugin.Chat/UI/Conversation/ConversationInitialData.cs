using XM.Shared.Core.Conversation;

namespace XM.Chat.UI.Conversation
{
    /// <summary>
    /// Initial data passed to the conversation window.
    /// </summary>
    public class ConversationInitialData
    {
        public ConversationDefinition ConversationDefinition { get; set; }
        public string NpcName { get; set; } = string.Empty;
        public string NpcPortrait { get; set; } = string.Empty;
    }
} 