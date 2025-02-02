using System.Collections.Generic;

namespace XM.Chat.ChatCommand
{
    public interface IChatCommandListDefinition
    {
        public Dictionary<string, ChatCommandDetail> BuildChatCommands();
    }
}
