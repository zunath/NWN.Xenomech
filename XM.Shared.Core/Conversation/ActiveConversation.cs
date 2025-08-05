using System.Collections.Generic;

namespace XM.Shared.Core.Conversation
{
    public class ActiveConversation
    {
        public string PlayerId { get; set; }
        public string ConversationId { get; set; }
        public string CurrentPage { get; set; }
        public uint Target { get; set; }
        public Dictionary<string, object> Variables { get; set; }
        public int CurrentResponseCount { get; set; }
    }
} 