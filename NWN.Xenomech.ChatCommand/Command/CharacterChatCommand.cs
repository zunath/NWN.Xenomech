using System.Collections.Generic;
using Anvil.Services;
using NWN.Xenomech.Authorization;
using NWN.Xenomech.Data;

namespace NWN.Xenomech.ChatCommand.Command
{
    [ServiceBinding(typeof(CharacterChatCommand))]
    public class CharacterChatCommand: IChatCommandListDefinition
    {
        private readonly ChatCommandBuilder _builder = new();
        private readonly DBService _db;

        public CharacterChatCommand(DBService db)
        {
            _db = db;
        }

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
