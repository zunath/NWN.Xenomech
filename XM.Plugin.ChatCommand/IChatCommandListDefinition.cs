using System.Collections.Generic;

namespace XM.ChatCommand
{
    public interface IChatCommandListDefinition
    {
        public Dictionary<string, ChatCommandDetail> BuildChatCommands();
    }
}
