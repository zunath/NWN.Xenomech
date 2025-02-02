﻿using System.Collections.Generic;
using Anvil.Services;
using XM.Chat.ChatCommand;
using XM.Shared.Core.Authorization;
using XM.Shared.Core.Configuration;

namespace XM.Chat.ChatCommand.ChatCommandDefinitions
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
            ListEmotesCommand();
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

        private void ListEmotesCommand()
        {
            _builder.Create("emotes")
                .Description("Displays all emotes available to you.")
                .Permissions(AuthorizationLevel.All)
                .Action((user, target, location, args) => { SendMessageToPC(user, _help.HelpTextEmote); });
        }
    }
}
