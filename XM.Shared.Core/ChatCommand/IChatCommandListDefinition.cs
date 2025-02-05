using System.Collections.Generic;

namespace XM.Shared.Core.ChatCommand
{
    public interface IChatCommandListDefinition
    {
        public Dictionary<string, ChatCommandDetail> BuildChatCommands();
    }
}
