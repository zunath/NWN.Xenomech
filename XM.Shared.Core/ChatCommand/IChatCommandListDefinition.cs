using System.Collections.Generic;
using XM.Shared.Core.Localization;

namespace XM.Shared.Core.ChatCommand
{
    public interface IChatCommandListDefinition
    {
        public Dictionary<LocaleString, ChatCommandDetail> BuildChatCommands();
    }
}
