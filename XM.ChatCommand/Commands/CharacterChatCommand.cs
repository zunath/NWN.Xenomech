using System;
using System.Collections.Generic;
using System.Globalization;
using Anvil.Services;
using XM.Authorization;
using XM.Core;

namespace XM.ChatCommand.Commands
{
    [ServiceBinding(typeof(IChatCommandListDefinition))]
    internal class CharacterChatCommand: IChatCommandListDefinition
    {
        private readonly ChatCommandBuilder _builder = new();

        public Dictionary<string, ChatCommandDetail> BuildChatCommands()
        {
            CDKey();

            return _builder.Build();
        }

        private void CDKey()
        {
            _builder.Create("cdkey")
                .Description("Displays your public CD key.")
                .Permissions(AuthorizationLevel.All)
                .Action((user, target, location, args) =>
                {
                    var cdKey = GetPCPublicCDKey(user);
                    SendMessageToPC(user, "Your public CD Key is: " + cdKey);
                });
        }
    }
}
