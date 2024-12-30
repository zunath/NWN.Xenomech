using System;
using System.Collections.Generic;
using System.Globalization;
using Anvil.Services;
using XM.Authorization;
using XM.Core;

namespace XM.ChatCommand.Commands
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

        public Dictionary<string, ChatCommandDetail> BuildChatCommands()
        {
            Help();

            return _builder.Build();
        }


        private void Help()
        {
            _builder.Create("help")
                .Description("Displays all chat commands available to you.")
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
    }
}
