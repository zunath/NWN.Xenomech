using Anvil.Services;

namespace XM.Shared.Core.ChatCommand
{
    [ServiceBinding(typeof(ChatCommandHelpService))]
    public class ChatCommandHelpService
    {
        public string HelpTextPlayer { get; internal set; }
        public string HelpTextEmote { get; internal set; }
        public string HelpTextDM { get; internal set; }
        public string HelpTextAdmin { get; internal set; }

    }
}
