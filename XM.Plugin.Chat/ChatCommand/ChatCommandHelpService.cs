using Anvil.Services;

namespace XM.Chat.ChatCommand
{
    [ServiceBinding(typeof(ChatCommandHelpService))]
    internal class ChatCommandHelpService
    {
        public string HelpTextPlayer { get; internal set; }
        public string HelpTextEmote { get; internal set; }
        public string HelpTextDM { get; internal set; }
        public string HelpTextAdmin { get; internal set; }

    }
}
