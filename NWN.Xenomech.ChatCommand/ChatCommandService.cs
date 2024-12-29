using System;
using Anvil.Services;
using NWN.Xenomech.ChatCommand.Command;
using System.Collections.Generic;
using System.Linq;
using Anvil.API;
using Anvil.API.Events;
using NLog;
using NWN.Core;
using NWN.Xenomech.Authorization;
using NWN.Xenomech.Core;
using Animation = NWN.Xenomech.API.Enum.Animation;
using NWN.Core.NWNX;
using NWN.Xenomech.API.Enum;
using ServiceStack.Configuration;

namespace NWN.Xenomech.ChatCommand
{
    [ServiceBinding(typeof(ChatCommandService))]
    public class ChatCommandService: IDisposable
    {
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

        private readonly AuthorizationService _authorization;
        private readonly TargetingService _targeting;
        private readonly Localization _localization;
        private readonly XMSettingsService _settings;
        private readonly CharacterChatCommand _characterChatCommand;

        private static readonly Dictionary<string, ChatCommandDetail> _chatCommands = new();
        private static readonly Dictionary<string, ChatCommandDetail> _emoteCommands = new();
        public static string HelpTextPlayer { get; private set; }
        public static string HelpTextEmote { get; private set; }
        public static string HelpTextDM { get; private set; }
        public static string HelpTextAdmin { get; private set; }
        public static List<Animation> EmoteAnimations { get; } = new();

        public ChatCommandService(
            AuthorizationService authorization,
            TargetingService targeting,
            Localization localization,
            XMSettingsService settings,
            CharacterChatCommand characterChatCommand)
        {
            _authorization = authorization;
            _targeting = targeting;
            _localization = localization;
            _settings = settings;
            _characterChatCommand = characterChatCommand;

            RegisterPluginChatCommands();

            NwModule.Instance.OnChatMessageSend += InstanceOnOnChatMessageSend;
        }

        /// <summary>
        /// Handles validating and processing chat commands sent by players and DMs.
        /// </summary>
        private void InstanceOnOnChatMessageSend(OnChatMessageSend obj)
        {
            var sender = OBJECT_SELF;
            var originalMessage = ChatPlugin.GetMessage().Trim();

            if (!CanHandleChat(sender, originalMessage))
            {
                return;
            }

            var split = originalMessage.Split(' ').ToList();

            // Commands with no arguments won't be split, so if we didn't split anything then add the command to the split list manually.
            if (split.Count <= 0)
                split.Add(originalMessage);

            split[0] = split[0].ToLower();
            var command = split[0].Substring(1, split[0].Length - 1);
            split.RemoveAt(0);

            ChatPlugin.SkipMessage();

            if (!_chatCommands.ContainsKey(command))
            {
                SendInvalidCommandMessage(sender);
                return;
            }

            var chatCommand = _chatCommands[command];

            var args = string.Join(" ", split);

            if (!chatCommand.RequiresTarget)
            {
                ProcessChatCommand(command, sender, OBJECT_INVALID, null, args);
            }
            else
            {
                var error = chatCommand.ValidateArguments?.Invoke(sender, split.ToArray());
                if (!string.IsNullOrWhiteSpace(error))
                {
                    SendMessageToPC(sender, error);
                    return;
                }


                var authorization = _authorization.GetAuthorizationLevel(sender);

                if ((_settings.ServerEnvironment == ServerEnvironmentType.Test && chatCommand.AvailableToAllOnTestEnvironment) ||
                    chatCommand.Authorization.HasFlag(authorization))
                {
                    _targeting.EnterTargetingMode(sender, chatCommand.ValidTargetTypes, _localization.GetString(LocalizationIds.SelectATarget),
                    target =>
                    {
                        var location = GetIsObjectValid(target)
                            ? GetLocation(target)
                            : Location(GetArea(sender), GetTargetingModeSelectedPosition(), 0.0f);
                        ProcessChatCommand(command, sender, target, location, args);
                    });
                }
                else
                {
                    SendInvalidCommandMessage(sender);
                }
            }
        }

        /// <summary>
        /// Processes and runs the specific chat command entered by the user.
        /// </summary>
        /// <param name="commandName">Name of the command</param>
        /// <param name="sender">The sender object</param>
        /// <param name="target">The target of the command. OBJECT_INVALID if no target is necessary.</param>
        /// <param name="targetLocation">The target location of the command. null if no target is necessary.</param>
        /// <param name="args">User-entered arguments</param>
        private void ProcessChatCommand(string commandName, uint sender, uint target, Location targetLocation, string args)
        {
            var command = _chatCommands[commandName];
            if (targetLocation == null)
            {
                targetLocation = GetLocation(target);
            }

            var authorization = _authorization.GetAuthorizationLevel(sender);

            if ((_settings.ServerEnvironment == ServerEnvironmentType.Test && command.AvailableToAllOnTestEnvironment) ||
                command.Authorization.HasFlag(authorization))
            {
                var argsArr = string.IsNullOrWhiteSpace(args) ? new string[0] : args.Split(' ').ToArray();
                var error = command.ValidateArguments?.Invoke(sender, argsArr);

                if (!string.IsNullOrWhiteSpace(error))
                {
                    SendMessageToPC(sender, error);
                }
                else
                {
                    command.DoAction?.Invoke(sender, target, targetLocation, argsArr);
                }
            }
            else
            {
                SendInvalidCommandMessage(sender);
            }
        }

        private void SendInvalidCommandMessage(uint sender)
        {
            var invalidText = _localization.GetString(LocalizationIds.InvalidChatCommand);
            SendMessageToPC(sender, ColorToken.Red(invalidText));
        }


        /// <summary>
        /// Parse the message and ensure it starts with a slash.
        /// Sender must be a player or DM.
        /// </summary>
        /// <param name="sender">The sender of the message.</param>
        /// <param name="message">The message sent.</param>
        /// <returns>true if this is a chat command, false otherwise</returns>
        private static bool CanHandleChat(uint sender, string message)
        {
            var validTarget = GetIsPC(sender) || GetIsDM(sender) || GetIsDMPossessed(sender);
            var validMessage = message.Length >= 2 && message[0] == '/' && message[1] != '/';
            return validTarget && validMessage;
        }

        private void RegisterPluginChatCommands()
        {
            RegisterChatCommandList(_characterChatCommand);
        }

        public void RegisterChatCommandList(IChatCommandListDefinition chatCommandList)
        {
            var commands = chatCommandList.BuildChatCommands();

            foreach (var (key, value) in commands)
            {
                RegisterSingleCommand(key, value);
            }
        }

        private void RegisterSingleCommand(string command, ChatCommandDetail detail)
        {
            _chatCommands[command] = detail;

            if (detail.IsEmote)
            {
                _emoteCommands[command] = detail;
            }

            UpdateHelpText(command, detail);

            _logger.Info($"Registered chat command: {command}");
        }

        private void UpdateHelpText(string command, ChatCommandDetail detail)
        {
            if (detail.Authorization.HasFlag(AuthorizationLevel.Player))
            {
                if (detail.IsEmote)
                {
                    HelpTextEmote += ColorToken.Green("/" + command) + ColorToken.White(": " + detail.Description) + "\n";
                }
                else
                {
                    HelpTextPlayer += ColorToken.Green("/" + command) + ColorToken.White(": " + detail.Description) + "\n";
                }
            }

            if (detail.Authorization.HasFlag(AuthorizationLevel.DM))
            {
                if (!detail.IsEmote)
                    HelpTextDM += ColorToken.Green("/" + command) + ColorToken.White(": " + detail.Description) + "\n";
            }

            if (detail.Authorization.HasFlag(AuthorizationLevel.Admin))
            {
                if (!detail.IsEmote)
                    HelpTextAdmin += ColorToken.Green("/" + command) + ColorToken.White(": " + detail.Description) + "\n";
            }
        }

        public void Dispose()
        {
            _chatCommands.Clear();
            _emoteCommands.Clear();
        }
    }
}
