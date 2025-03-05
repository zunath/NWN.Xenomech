﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Anvil.Services;
using XM.Chat.Entity;
using XM.Shared.API.Constants;
using XM.Shared.API.NWNX.AdminPlugin;
using XM.Shared.API.NWNX.RenamePlugin;
using XM.Shared.Core;
using XM.Shared.Core.Authorization;
using XM.Shared.Core.ChatCommand;
using XM.Shared.Core.Data;
using XM.Shared.Core.Localization;

namespace XM.Chat.ChatCommand
{
    [ServiceBinding(typeof(IChatCommandListDefinition))]
    internal class CharacterChatCommand : IChatCommandListDefinition
    {
        private readonly ChatCommandBuilder _builder = new();

        private readonly DBService _db;
        public CharacterChatCommand(DBService db)
        {
            _db = db;
        }

        public Dictionary<LocaleString, ChatCommandDetail> BuildChatCommands()
        {
            Char();
            DeleteCommand();
            Name();

            return _builder.Build();
        }


        private void Char()
        {
            _builder.Create(LocaleString.character, LocaleString.@char)
                .Description(LocaleString.DisplaysYourCharacterInfo)
                .Permissions(AuthorizationLevel.All)
                .Validate((user, args) =>
                {
                    if (GetIsDM(user))
                    {
                        return LocaleString.ThisCommandCanOnlyBeUsedOnPCs.ToLocalizedString();
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
            _builder.Create(LocaleString.delete)
                .Description(LocaleString.PermanentlyDeletesYourCharacter)
                .Permissions(AuthorizationLevel.All)
                .Validate((user, args) =>
                {
                    if (!GetIsPC(user) || GetIsDM(user))
                        return LocaleString.YouCanOnlyDeleteAPlayerCharacter.ToLocalizedString();

                    var cdKey = GetPCPublicCDKey(user);
                    var enteredCDKey = args.Length > 0 ? args[0] : string.Empty;

                    if (cdKey != enteredCDKey)
                    {
                        return LocaleString.InvalidCDKeyEnteredDeleteCharacter.ToLocalizedString();
                    }

                    if (GetIsDM(user) || GetIsDMPossessed(user))
                    {
                        return LocaleString.DMCharactersCannotUseThisChatCommand.ToLocalizedString();
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
                        FloatingTextStringOnCreature(LocaleString.PleaseConfirmDeletion.ToLocalizedString(), user, false);
                    }
                    else
                    {
                        var playerName = GetPCPlayerName(user);
                        var characterName = GetName(user);
                        AdminPlugin.DeletePlayerCharacter(user, true, LocaleString.YourCharacterHasBeenDeleted.ToLocalizedString());
                        AdminPlugin.DeleteTURD(playerName, characterName);
                    }
                });
        }

        private void Name()
        {
            _builder.Create(LocaleString.name)
                .Description(LocaleString.AssignsANameToTheTargetedPlayer)
                .Permissions(AuthorizationLevel.All)
                .RequiresTarget(ObjectType.Creature)
                .Validate((user, args) =>
                {
                    if (GetIsDM(user))
                    {
                        return LocaleString.ThisCommandCanOnlyBeUsedOnPCs.ToLocalizedString();
                    }

                    if (args.Length <= 0)
                    {
                        return LocaleString.NameCommandError.ToLocalizedString();
                    }

                    return string.Empty;
                })
                .Action((user, target, location, args) =>
                {
                    if (GetIsDM(target) || !GetIsPC(target))
                    {
                        var error = LocaleString.ThisCommandCanOnlyBeUsedOnPCs.ToLocalizedString();
                        SendMessageToPC(user, error);
                        return;
                    }

                    var playerId = PlayerId.Get(user);
                    var targetPlayerId = PlayerId.Get(target);

                    var name = string.Join(' ', args);
                    var dbPlayerName = _db.Get<PlayerName>(playerId);
                    dbPlayerName.OverrideNames[targetPlayerId] = name;

                    _db.Set(dbPlayerName);
                    RenamePlugin.SetPCNameOverride(target, name, string.Empty, string.Empty, PlayerNameOverrideType.Default, user);
                });
        }
    }
}
