using System.Collections.Generic;
using System.Linq;
using Anvil.API;
using Anvil.Services;
using NLog;
using XM.Shared.API.NWNX.ChatPlugin;
using XM.Shared.Core;
using XM.Shared.Core.Authorization;
using XM.Shared.Core.Configuration;
using XM.Shared.Core.EventManagement;
using XM.Shared.Core.Localization;

namespace XM.Chat
{
    [ServiceBinding(typeof(ChatCommandService))]
    internal class ChatCommandService: IInitializable
    {
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

        [Inject]
        public IList<IChatCommandListDefinition> Definitions { get; set; }

        private readonly Dictionary<string, ChatCommandDetail> _chatCommands;
        internal readonly Dictionary<string, ChatCommandDetail> EmoteCommands;

        private readonly XMSettingsService _settings;
        private readonly AuthorizationService _authorization;
        private readonly ChatCommandHelpService _help;
        private readonly XMEventService _event;

        public ChatCommandService(
            XMSettingsService settings, 
            AuthorizationService authorization,
            ChatCommandHelpService help,
            XMEventService @event)
        {
            _chatCommands = new Dictionary<string, ChatCommandDetail>();
            EmoteCommands = new Dictionary<string, ChatCommandDetail>();

            _settings = settings;
            _authorization = authorization;
            _help = help;
            _event = @event;

            HookEvents();
        }

        private void HookEvents()
        {
            _event.Subscribe<NWNXEvent.OnNWNXChat>(OnPlayerChat);
        }


        private void OnPlayerChat(uint objectSelf)
        {
            var sender = OBJECT_SELF;
            var originalMessage = GetPCChatMessage().Trim();

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
                var message = Locale.GetString(LocaleString.InvalidChatCommand);
                SendMessageToPC(sender, ColorToken.Red(message));
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
                    var message = Locale.GetString(LocaleString.SelectTargetForChatCommand);
                    Targeting.EnterTargetingMode(sender, chatCommand.ValidTargetTypes, message,
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
                    SendError(sender);
                }
            }
        }

        public void Init()
        {
            LoadCommands();
            BuildHelpText();

            _logger.Info($"Loaded {_chatCommands.Count} chat commands.");
        }

        private void LoadCommands()
        {
            foreach (var definition in Definitions)
            {
                var commands = definition.BuildChatCommands();
                foreach (var (command, detail) in commands)
                {
                    if (!_chatCommands.TryAdd(command, detail))
                    {
                        _logger.Error($"Command '{command}' is registered multiple times. Only the first registration will take effect.");
                        continue;
                    }

                    if (detail.IsEmote)
                    {
                        EmoteCommands[command] = detail;
                    }
                }
            }
        }

        /// <summary>
        /// Builds text used by the /help command for each authorization level.
        /// This must be called after LoadChatCommands or there will be nothing to process.
        /// </summary>
        private void BuildHelpText()
        {
            var orderedCommands = _chatCommands.OrderBy(o => o.Key);

            foreach (var command in orderedCommands)
            {
                var text = command.Key;
                var definition = command.Value;

                if (definition.Authorization.HasFlag(AuthorizationLevel.Player))
                {
                    if (definition.IsEmote)
                    {
                        _help.HelpTextEmote += ColorToken.Green("/" + text) + ColorToken.White(": " + definition.Description) + "\n";
                    }
                    else
                    {
                        _help.HelpTextPlayer += ColorToken.Green("/" + text) + ColorToken.White(": " + definition.Description) + "\n";
                    }
                }

                if (definition.Authorization.HasFlag(AuthorizationLevel.DM))
                {
                    if (!definition.IsEmote)
                        _help.HelpTextDM += ColorToken.Green("/" + text) + ColorToken.White(": " + definition.Description) + "\n";
                }

                if (definition.Authorization.HasFlag(AuthorizationLevel.Admin))
                {
                    if (!definition.IsEmote)
                        _help.HelpTextAdmin += ColorToken.Green("/" + text) + ColorToken.White(": " + definition.Description) + "\n";
                }
            }
        }

        private void SendError(uint sender)
        {
            var message = Locale.GetString(LocaleString.InvalidChatCommand);
            SendMessageToPC(sender, ColorToken.Red(message));
        }

        /// <summary>
        /// Parse the message and ensure it starts with a slash.
        /// Sender must be a player or DM.
        /// </summary>
        /// <param name="sender">The sender of the message.</param>
        /// <param name="message">The message sent.</param>
        /// <returns>true if this is a chat command, false otherwise</returns>
        private bool CanHandleChat(uint sender, string message)
        {
            var validTarget = GetIsPC(sender) || GetIsDM(sender) || GetIsDMPossessed(sender);
            var validMessage = message.Length >= 2 && message[0] == '/' && message[1] != '/';
            return validTarget && validMessage;
        }

        /// <summary>
        /// Processes and runs the specific chat command entered by the user.
        /// </summary>
        /// <param name="commandName">Name of the command</param>
        /// <param name="sender">The sender object</param>
        /// <param name="target">The target of the command. OBJECT_INVALID if no target is necessary.</param>
        /// <param name="targetLocation">The target location of the command. null if no target is necessary.</param>
        /// <param name="args">User-entered arguments</param>
        private void ProcessChatCommand(
            string commandName, 
            uint sender, 
            uint target,
            Location targetLocation, 
            string args)
        {
            var command = _chatCommands[commandName];
            if (targetLocation == null)
            {
                targetLocation = LOCATION_INVALID;
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
                SendError(sender);
            }
        }

    }
}
