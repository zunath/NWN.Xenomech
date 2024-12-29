using System.Collections.Generic;

namespace NWN.Xenomech.ChatCommand
{
    public interface IChatCommandListDefinition
    {
        public Dictionary<string, ChatCommandDetail> BuildChatCommands();
    }
}
