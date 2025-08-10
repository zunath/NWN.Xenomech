using Anvil.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XM.Shared.Core.Entity;
using XM.Shared.API.NWNX.ChatPlugin;
using XM.Shared.Core;
using XM.Shared.Core.Data;
using XM.Shared.Core.EventManagement;
using XM.Shared.Core.Localization;

namespace XM.Chat.Communication
{
    [ServiceBinding(typeof(CommunicationService))]
    internal class CommunicationService
    {
        private readonly DBService _db;
        private readonly XMEventService _event;

        private const string DMPossessedCreature = "COMMUNICATION_DM_POSSESSED_CREATURE";
        private static (byte, byte, byte) OOCChatColor { get; } = (64, 64, 64);
        private static (byte, byte, byte) EmoteChatColor { get; } = (0, 255, 0);

        public CommunicationService(
            DBService db,
            XMEventService @event)
        {
            _db = db;
            _event = @event;

            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            _event.Subscribe<NWNXEvent.OnNWNXChat>(ProcessChatMessage);
        }

        private void ProcessChatMessage(uint obj)
        {
            var channel = ChatPlugin.GetChannel();

            // - PlayerTalk, PlayerWhisper, PlayerParty, and PlayerShout are all IC channels. These channels
            //   are subject to emote coloring and language translation. (see below for more info).
            // - PlayerParty is an IC channel with special behaviour. Those outside of the party but within
            //   range may listen in to the party chat. (see below for more information).
            // - PlayerShout sends a holocom message server-wide through the DMTell channel.
            // - PlayerDM echoes back the message received to the sender.

            var inCharacterChat =
                channel == ChatChannelType.PlayerTalk ||
                channel == ChatChannelType.PlayerWhisper ||
                channel == ChatChannelType.PlayerParty ||
                channel == ChatChannelType.PlayerShout;

            var messageToDm = channel == ChatChannelType.PlayerDM;

            var sender = ChatPlugin.GetSender();
            var message = ChatPlugin.GetMessage().Trim();

            // if this is a DMFI chat command, exit as ProcessNativeChatMessage has already handled via mod_chat event.
            if (GetIsDM(sender) && message.Length >= 1 && message.Substring(0, 1) == ".")
            {
                return;
            }

            // Ignore messages on other channels.
            if (!inCharacterChat && !messageToDm) return;

            if (string.IsNullOrWhiteSpace(message))
            {
                // We can't handle empty messages, so skip it.
                return;
            }

            // Echo the message back to the player.
            if (messageToDm)
            {
                var text = LocaleString.SentToDMX.ToLocalizedString(message);
                ChatPlugin.SendMessage(ChatChannelType.ServerMessage, text, sender, sender);
                return;
            }

            ChatPlugin.SkipMessage();

            if (GetIsDead(sender) && !message.StartsWith("/"))
            {
                SendMessageToPC(sender, ColorToken.Red(LocaleString.YouCannotSpeakWhileDead.ToLocalizedString()));
                return;
            }

            var chatComponents = new List<CommunicationComponent>();

            // Quick early out - if we start with "//" or "((", this is an OOC message.
            if (message.Length >= 2 && (message.Substring(0, 2) == "//" || message.Substring(0, 2) == "(("))
            {
                var component = new CommunicationComponent
                {
                    Text = message,
                    IsCustomColor = true,
                    IsOOC = true,
                    IsTranslatable = false
                };
                chatComponents.Add(component);
            }
            // Another early out - if this is a chat command, exit.
            else if (message.Length >= 1 && message.Substring(0, 1) == "/")
            {
                return;
            }
            // Another early out - a completely empty message will just be skipped.
            else if (string.IsNullOrWhiteSpace(message.Trim()))
            {
                return;
            }
            else
            {
                chatComponents = GetEmoteStyle(sender) == EmoteStyle.Regular
                    ? SplitMessageIntoComponents_Regular(message)
                    : SplitMessageIntoComponents_Novel(message);

                // For any components with color, set the emote color.
                foreach (var component in chatComponents)
                {
                    if (component.IsCustomColor)
                    {
                        component.IsEmote = true;
                    }
                }
            }

            if (channel == ChatChannelType.PlayerShout &&
                GetIsPC(sender) &&
                !GetIsDM(sender) &&
                !GetIsDMPossessed(sender))
            {
                SendMessageToPC(sender, LocaleString.PlayersCannotUseThisChatChannel.ToLocalizedString());
                return;
            }


            // Now, depending on the chat channel, we need to build a list of recipients.
            var needsAreaCheck = false;
            var distanceCheck = 0.0f;

            // The sender always wants to see their own message.
            var recipients = new List<uint> { sender };
            var allPlayersAndDMs = new List<uint>();
            var allPlayers = new List<uint>();
            var allDMs = new List<uint>();

            for (var player = GetFirstPC(); GetIsObjectValid(player); player = GetNextPC())
            {
                allPlayersAndDMs.Add(player);

                if (GetIsDM(player) || GetIsDMPossessed(player))
                {
                    allDMs.Add(player);
                }
                else
                {
                    allPlayers.Add(player);
                }
            }

            // This is a server-wide holonet message (that receivers can toggle on or off).
            if (channel == ChatChannelType.PlayerShout)
            {
                recipients.AddRange(allPlayers.Where(player => GetLocalBool(player, "DISPLAY_HOLONET")));
                recipients.AddRange(allDMs);
            }
            // This is the normal party chat, plus everyone within 20 units of the sender.
            else if (channel == ChatChannelType.PlayerParty)
            {
                // Can an NPC use the playerparty channel? I feel this is safe ...

                for (var member = GetFirstFactionMember(sender); GetIsObjectValid(member); member = GetNextFactionMember(sender))
                {
                    if (sender == member) continue;

                    recipients.Add(member);
                }

                recipients.AddRange(allDMs);

                needsAreaCheck = true;
                distanceCheck = 20.0f;
            }
            // Normal talk - 20 units.
            else if (channel == ChatChannelType.PlayerTalk)
            {
                needsAreaCheck = true;
                distanceCheck = 20.0f;
            }
            // Whisper - 4 units.
            else if (channel == ChatChannelType.PlayerWhisper)
            {
                needsAreaCheck = true;
                distanceCheck = 4.0f;
            }

            if (needsAreaCheck)
            {
                foreach (var player in allPlayersAndDMs)
                {
                    var target = player;
                    var possessedNPC = GetLocalObject(player, DMPossessedCreature);
                    if (GetIsObjectValid(possessedNPC))
                    {
                        target = possessedNPC;
                    }

                    var distance = GetDistanceBetween(sender, target);

                    if (GetArea(target) == GetArea(sender) &&
                        distance <= distanceCheck &&
                        !recipients.Contains(target))
                    {
                        recipients.Add(target);
                    }
                }
            }

            // Now we have a list of who is going to actually receive a message, we need to modify
            // the message for each recipient then dispatch them.
            foreach (var receiver in recipients.Distinct())
            {
                var receiverId = PlayerId.Get(receiver);
                var dbReceiverChat = _db.Get<PlayerChat>(receiverId);

                // Generate the final message as perceived by obj.
                var finalMessage = new StringBuilder();

                if (channel == ChatChannelType.PlayerShout)
                {
                    finalMessage.Append("[Global] ");
                }
                else if (channel == ChatChannelType.PlayerParty)
                {
                    finalMessage.Append("[Comms] ");

                    if (GetIsDM(receiver))
                    {
                        // Convenience for DMs - append the party members.
                        finalMessage.Append("{ ");

                        var count = 0;

                        var partyMembers = new List<uint>();
                        for (var member = GetFirstFactionMember(sender); GetIsObjectValid(member); member = GetNextFactionMember(sender))
                        {
                            partyMembers.Add(member);
                        }

                        foreach (var otherPlayer in partyMembers)
                        {
                            var name = GetName(otherPlayer);
                            finalMessage.Append(name.Substring(0, Math.Min(name.Length, 10)));

                            ++count;

                            if (count >= 3)
                            {
                                finalMessage.Append(", ...");
                                break;
                            }
                            else if (count != partyMembers.Count)
                            {
                                finalMessage.Append(",");
                            }
                        }

                        finalMessage.Append(" } ");
                    }
                }

                var originalSender = sender;

                foreach (var component in chatComponents)
                {
                    var text = component.Text;

                    byte r = 255;
                    byte g = 255;
                    byte b = 255;
                    if (component.IsOOC)
                    {
                            if (dbReceiverChat != null)
                        {
                            r = dbReceiverChat.OOC_R;
                            g = dbReceiverChat.OOC_G;
                            b = dbReceiverChat.OOC_B;
                        }
                        else
                        {
                            r = OOCChatColor.Item1;
                            g = OOCChatColor.Item2;
                            b = OOCChatColor.Item3;
                        }
                    }
                    
                    if (component.IsEmote)
                    {
                        byte emoteRed, emoteGreen, emoteBlue;

                        if (dbReceiverChat != null)
                        {
                            emoteRed = dbReceiverChat.Emote_R;
                            emoteGreen = dbReceiverChat.Emote_G;
                            emoteBlue = dbReceiverChat.Emote_B;
                        }
                        else
                        {
                            emoteRed = EmoteChatColor.Item1;
                            emoteGreen = EmoteChatColor.Item2;
                            emoteBlue = EmoteChatColor.Item3;
                        }
                        text = ColorToken.Custom(text, emoteRed, emoteGreen, emoteBlue);
                    }
                    else
                    {
                        text = ColorToken.Custom(text, r, g, b);
                    }

                    finalMessage.Append(text);
                }

                // Dispatch the final message - method depends on the original chat channel.
                // - Shout and party is sent as DMTalk. We do this to get around the restriction that
                //   the PC needs to be in the same area for the normal talk channel.
                //   We could use the native channels for these but the [shout] or [party chat] labels look silly.
                // - Talk and whisper are sent as-is.

                var finalChannel = channel;

                if (channel == ChatChannelType.PlayerShout || channel == ChatChannelType.PlayerParty)
                {
                    finalChannel = ChatChannelType.DMTalk;
                }

                // There are a couple of color overrides we want to use here.
                // - One for holonet (shout).
                // - One for comms (party chat).

                var finalMessageColored = finalMessage.ToString();

                if (channel == ChatChannelType.PlayerShout)
                {
                    finalMessageColored = ColorToken.Custom(finalMessageColored, 0, 180, 255);
                }
                else if (channel == ChatChannelType.PlayerParty)
                {
                    finalMessageColored = ColorToken.Orange(finalMessageColored);
                }

                // set back to original sender, if it was changed by holocom connection
                sender = originalSender;

                ChatPlugin.SendMessage(finalChannel, finalMessageColored, sender, receiver);
            }
        }


        private List<CommunicationComponent> SplitMessageIntoComponents_Regular(string message)
        {
            var components = new List<CommunicationComponent>();

            var workingOn = WorkingOnEmoteStyle.None;
            var indexStart = 0;
            var length = -1;
            var depth = 0;

            for (var i = 0; i < message.Length; ++i)
            {
                var ch = message[i];

                if (ch == '[')
                {
                    if (workingOn == WorkingOnEmoteStyle.None || workingOn == WorkingOnEmoteStyle.Bracket)
                    {
                        depth += 1;
                        if (depth == 1)
                        {
                            var component = new CommunicationComponent
                            {
                                IsCustomColor = false,
                                IsTranslatable = true,
                                Text = message.Substring(indexStart, i - indexStart)
                            };
                            components.Add(component);

                            indexStart = i + 1;
                            workingOn = WorkingOnEmoteStyle.Bracket;
                        }
                    }
                }
                else if (ch == ']')
                {
                    if (workingOn == WorkingOnEmoteStyle.Bracket)
                    {
                        depth -= 1;
                        if (depth == 0)
                        {
                            length = i - indexStart;
                        }
                    }
                }
                else if (ch == '*')
                {
                    if (workingOn == WorkingOnEmoteStyle.None || workingOn == WorkingOnEmoteStyle.Asterisk)
                    {
                        if (depth == 0)
                        {
                            var component = new CommunicationComponent
                            {
                                IsCustomColor = false,
                                IsTranslatable = true,
                                Text = message.Substring(indexStart, i - indexStart)
                            };
                            components.Add(component);

                            depth = 1;
                            indexStart = i;
                            workingOn = WorkingOnEmoteStyle.Asterisk;
                        }
                        else
                        {
                            depth = 0;
                            length = i - indexStart + 1;
                        }
                    }
                }
                else if (ch == ':')
                {
                    if (workingOn == WorkingOnEmoteStyle.None || workingOn == WorkingOnEmoteStyle.ColonForward)
                    {
                        depth += 1;
                        if (depth == 1)
                        {
                            // Only match this colon if the next symbol is also a colon.
                            // This needs to be done because a single colon can be used in normal chat.
                            if (i + 1 < message.Length && message[i + 1] == ':')
                            {
                                var component = new CommunicationComponent
                                {
                                    IsCustomColor = false,
                                    IsTranslatable = true,
                                    Text = message.Substring(indexStart, i - indexStart)
                                };
                                components.Add(component);

                                indexStart = i;
                                workingOn = WorkingOnEmoteStyle.ColonForward;
                            }
                            else
                            {
                                depth -= 1;
                            }
                        }
                        else if (depth == 2)
                        {
                            workingOn = WorkingOnEmoteStyle.ColonBackward;
                        }
                    }
                    else if (workingOn == WorkingOnEmoteStyle.ColonBackward)
                    {
                        depth -= 1;
                        if (depth == 0)
                        {
                            length = i - indexStart + 1;
                        }
                    }
                }

                if (length != -1)
                {
                    var component = new CommunicationComponent
                    {
                        IsCustomColor = workingOn != WorkingOnEmoteStyle.Bracket || message[indexStart] == '[',
                        IsTranslatable = false,
                        Text = message.Substring(indexStart, length)
                    };
                    components.Add(component);

                    indexStart = i + 1;
                    length = -1;
                    workingOn = WorkingOnEmoteStyle.None;
                }
                else
                {
                    // If this is the last character in the string, we should just display what we've got.
                    if (i == message.Length - 1)
                    {
                        var component = new CommunicationComponent
                        {
                            IsCustomColor = depth != 0,
                            IsTranslatable = depth == 0,
                            Text = message.Substring(indexStart, i - indexStart + 1)
                        };
                        components.Add(component);
                    }
                }
            }

            // Strip any empty components.
            components.RemoveAll(comp => string.IsNullOrEmpty(comp.Text));

            return components;
        }

        private List<CommunicationComponent> SplitMessageIntoComponents_Novel(string message)
        {
            var components = new List<CommunicationComponent>();

            var indexStart = 0;
            var workingOnQuotes = false;
            var workingOnBrackets = false;

            for (var i = 0; i < message.Length; ++i)
            {
                var ch = message[i];

                if (ch == '"')
                {
                    if (!workingOnQuotes)
                    {
                        var component = new CommunicationComponent
                        {
                            IsCustomColor = true,
                            IsTranslatable = false,
                            Text = message.Substring(indexStart, i - indexStart)
                        };
                        components.Add(component);

                        workingOnQuotes = true;
                        indexStart = i;
                    }
                    else
                    {
                        var component = new CommunicationComponent
                        {
                            IsCustomColor = false,
                            IsTranslatable = true,
                            Text = message.Substring(indexStart, i - indexStart + 1)
                        };
                        components.Add(component);

                        workingOnQuotes = false;
                        indexStart = i + 1;
                    }
                }
                else if (ch == '[')
                {
                    var translate = workingOnQuotes;

                    var component = new CommunicationComponent
                    {
                        IsCustomColor = !translate,
                        IsTranslatable = translate,
                        Text = message.Substring(indexStart, i - indexStart)
                    };
                    components.Add(component);

                    workingOnBrackets = true;
                    indexStart = i + 1;
                }
                else if (ch == ']')
                {
                    var component = new CommunicationComponent
                    {
                        IsCustomColor = true,
                        IsTranslatable = false,
                        Text = message.Substring(indexStart, i - indexStart)
                    };
                    components.Add(component);

                    workingOnBrackets = false;
                    indexStart = i + 1;
                }
            }

            {
                var translate = workingOnQuotes && !workingOnBrackets;

                var component = new CommunicationComponent
                {
                    IsCustomColor = !translate,
                    IsTranslatable = translate,
                    Text = message.Substring(indexStart, message.Length - indexStart)
                };
                components.Add(component);
            }

            // Strip any empty components.
            components.RemoveAll(comp => string.IsNullOrEmpty(comp.Text));

            return components;
        }

        public EmoteStyle GetEmoteStyle(uint player)
        {
            if (GetIsPC(player) && !GetIsDM(player) && !GetIsDMPossessed(player))
            {
                var playerId = GetObjectUUID(player);
                var dbPlayer = _db.Get<PlayerChat>(playerId);
                
                return (EmoteStyle)dbPlayer.EmoteStyleCode;
            }

            return EmoteStyle.Regular;
        }

        public void SetEmoteStyle(uint player, EmoteStyle style)
        {
            if (GetIsPC(player) && !GetIsDM(player))
            {
                var playerId = GetObjectUUID(player);
                var dbPlayer = _db.Get<PlayerChat>(playerId);
                dbPlayer.EmoteStyleCode = (int)style;
                _db.Set(dbPlayer);
            }
        }
    }
}
