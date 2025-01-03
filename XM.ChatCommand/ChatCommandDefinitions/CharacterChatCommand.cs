using System;
using System.Collections.Generic;
using System.Globalization;
using Anvil.Services;
using XM.API.NWNX.AdminPlugin;
using XM.Authorization;
using XM.Core;
using XM.Localization;

namespace XM.ChatCommand.ChatCommandDefinitions
{
    [ServiceBinding(typeof(IChatCommandListDefinition))]
    internal class CharacterChatCommand: IChatCommandListDefinition
    {
        private readonly ChatCommandBuilder _builder = new();

        public Dictionary<string, ChatCommandDetail> BuildChatCommands()
        {
            Char();
            DeleteCommand();

            return _builder.Build();
        }

        private void Char()
        {
            _builder.Create("character", "char")
                .Description("Displays your character's info.")
                .Permissions(AuthorizationLevel.All)
                .Validate((user, args) =>
                {
                    if (GetIsDM(user))
                    {
                        return "This command can only be used on PCs.";
                    }

                    return string.Empty;
                })
                .Action((user, target, location, args) =>
                {
                    var playerId = PlayerId.Get(user);
                    var cdKey = GetPCPublicCDKey(user);

                    var message = ColorToken.Green(Locale.GetString(LocaleString.CharacterChatCommandResult,
                        ColorToken.White(cdKey), 
                        ColorToken.White(playerId)));

                    SendMessageToPC(user, message);
                });
        }


        private void DeleteCommand()
        {
            _builder.Create("delete")
                .Description("Permanently deletes your character.")
                .Permissions(AuthorizationLevel.All)
                .Validate((user, args) =>
                {
                    if (!GetIsPC(user) || GetIsDM(user))
                        return "You can only delete a player character.";

                    var cdKey = GetPCPublicCDKey(user);
                    var enteredCDKey = args.Length > 0 ? args[0] : string.Empty;

                    if (cdKey != enteredCDKey)
                    {
                        return "Invalid CD key entered. Please enter the command as follows: \"/delete <CD Key>\". You can retrieve your CD key with the /Char chat command.";
                    }

                    if (GetIsDM(user) || GetIsDMPossessed(user))
                    {
                        return "DM characters cannot use this chat command.";
                    }

                    return string.Empty;
                })
                .Action((user, target, location, args) =>
                {
                    var lastSubmission = GetLocalString(user, "DELETE_CHARACTER_LAST_SUBMISSION");
                    var isFirstSubmission = true;

                    // Check for the last submission, if any.
                    if (!string.IsNullOrWhiteSpace(lastSubmission))
                    {
                        // Found one, parse it.
                        var dateTime = DateTime.ParseExact(lastSubmission, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
                        if (DateTime.UtcNow <= dateTime.AddSeconds(30))
                        {
                            // Player submitted a second request within 30 seconds of the last one. 
                            // This is a confirmation they want to delete.
                            isFirstSubmission = false;
                        }
                    }

                    // Player hasn't submitted or time has elapsed
                    if (isFirstSubmission)
                    {
                        SetLocalString(user, "DELETE_CHARACTER_LAST_SUBMISSION", DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture));
                        FloatingTextStringOnCreature("Please confirm your deletion by entering another \"/delete <CD Key>\" command within 30 seconds.", user, false);
                    }
                    else
                    {
                        var playerName = GetPCPlayerName(user);
                        var characterName = GetName(user);
                        AdminPlugin.DeletePlayerCharacter(user, true, "Your character has been deleted.");
                        AdminPlugin.DeleteTURD(playerName, characterName);
                    }
                });
        }

    }
}
