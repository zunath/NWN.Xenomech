using System.Collections.Generic;
using Anvil.Services;
using XM.Shared.Core.Authorization;
using XM.Shared.Core.ChatCommand;
using XM.Shared.Core.Configuration;
using XM.Shared.Core.Localization;

namespace XM.Chat.ChatCommand
{
    [ServiceBinding(typeof(IChatCommandListDefinition))]
    internal class SystemChatCommand : IChatCommandListDefinition
    {
        private readonly ChatCommandBuilder _builder = new();

        private readonly AuthorizationService _auth;
        private readonly XMSettingsService _settings;
        private readonly ChatCommandHelpService _help;

        public SystemChatCommand(
            AuthorizationService auth,
            XMSettingsService settings,
            ChatCommandHelpService help)
        {
            _auth = auth;
            _settings = settings;
            _help = help;
        }

        public Dictionary<LocaleString, ChatCommandDetail> BuildChatCommands()
        {
            ListEmotesCommand();
            Help();

            return _builder.Build();
        }


        private void Help()
        {
            _builder.Create(LocaleString.help)
                .Description(LocaleString.DisplaysAllChatCommandsAvailableToYou)
                .Permissions(AuthorizationLevel.All)
                .Action((user, target, location, args) =>
                {
                    var authorization = _auth.GetAuthorizationLevel(user);

                    if (_settings.ServerEnvironment == ServerEnvironmentType.Test ||
                        authorization == AuthorizationLevel.DM)
                    {
                        SendMessageToPC(user, _help.HelpTextDM);
                    }
                    else if (authorization == AuthorizationLevel.Admin)
                    {
                        SendMessageToPC(user, _help.HelpTextAdmin);
                    }
                    else
                    {
                        SendMessageToPC(user, _help.HelpTextPlayer);
                    }
                });
        }

        private void ListEmotesCommand()
        {
            _builder.Create(LocaleString.emotes)
                .Description(LocaleString.DisplaysAllEmotesAvailableToYou)
                .Permissions(AuthorizationLevel.All)
                .Action((user, target, location, args) => { SendMessageToPC(user, _help.HelpTextEmote); });
        }
    }
}
