using Anvil.Services;
using System.Globalization;
using System;
using XM.Progression.Job;
using XM.Shared.API.Constants;
using XM.Shared.API.NWNX.ChatPlugin;
using XM.Shared.Core;
using XM.Shared.Core.Data;
using XM.Shared.Core.EventManagement;
using XM.Chat.Entity;

namespace XM.Chat.Roleplay
{
    [ServiceBinding(typeof(RoleplayXPService))]
    internal class RoleplayXPService
    {
        private const string TrackerVariableName = "RP_SYSTEM_TICKS";
        private const string RPTimestampVariable = "RP_SYSTEM_LAST_MESSAGE_TIMESTAMP";

        private readonly DBService _db;
        private readonly XMEventService _event;
        private readonly JobService _job;

        public RoleplayXPService(
            DBService db,
            XMEventService @event,
            JobService job)
        {
            _db = db;
            _event = @event;
            _job = job;

            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            _event.Subscribe<PlayerEvent.OnHeartbeat>(DistributeRoleplayXP);
            _event.Subscribe<NWNXEvent.OnNWNXChat>(ProcessRPMessage);
        }

        private void ProcessRPMessage(uint module)
        {
            var channel = ChatPlugin.GetChannel();
            var player = ChatPlugin.GetSender();
            if (!GetIsPC(player) || GetIsDM(player) || GetIsDMPossessed(player)) return;

            var message = ChatPlugin.GetMessage().Trim();
            var now = DateTime.UtcNow;

            var isInCharacterChat =
                channel == ChatChannelType.PlayerTalk ||
                channel == ChatChannelType.PlayerWhisper ||
                channel == ChatChannelType.PlayerParty ||
                channel == ChatChannelType.PlayerShout;

            // Don't care about other chat channels.
            if (!isInCharacterChat) return;

            // Is the message too short?
            if (message.Length <= 3) return;

            var playerId = PlayerId.Get(player);
            var dbPlayerRoleplay = _db.Get<PlayerChat>(playerId);

            // Is this an OOC message?
            var startingText = message.Substring(0, 2);
            if (startingText == "//" || startingText == "((")
            {
                dbPlayerRoleplay.RPPoints -= 5;
                if (dbPlayerRoleplay.RPPoints < 0)
                    dbPlayerRoleplay.RPPoints = 0;
                _db.Set(dbPlayerRoleplay);
                return;
            }

            // Spam prevention
            var timestampString = GetLocalString(player, RPTimestampVariable);
            SetLocalString(player, RPTimestampVariable, now.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture));

            // If there was a timestamp then we'll check for spam and prevent it from counting towards
            // the RP XP points.
            if (!string.IsNullOrWhiteSpace(timestampString))
            {
                var lastSend = DateTime.ParseExact(timestampString, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
                if (now <= lastSend.AddSeconds(1))
                {
                    dbPlayerRoleplay.SpamMessageCount++;
                    _db.Set(dbPlayerRoleplay);
                    return;
                }
            }

            // Check if players are close enough for the channel in which the message was sent.
            if (!CanReceiveRPPoint(player, channel)) return;

            dbPlayerRoleplay.RPPoints++;
            _db.Set(dbPlayerRoleplay);
        }

        private void DistributeRoleplayXP(uint player)
        {
            var ticks = GetLocalInt(player, TrackerVariableName) + 1;

            // Is it time to process RP points?
            if (ticks >= 300) // 300 ticks * 6 seconds per HB = 1800 seconds = 30 minutes
            {
                ProcessPlayerRoleplayXP(player);
                ticks = 0;
            }

            SetLocalInt(player, TrackerVariableName, ticks);
        }

        private void ProcessPlayerRoleplayXP(uint player)
        {
            if (!GetIsPC(player) || GetIsDM(player)) return;

            var playerId = PlayerId.Get(player);
            var dbPlayerRoleplay = _db.Get<PlayerChat>(playerId);

            if (dbPlayerRoleplay.RPPoints >= 50)
            {
                var socialModifier = GetAbilityModifier(AbilityType.Social, player);
                const int BaseXP = 1500;
                var delta = dbPlayerRoleplay.RPPoints - 50;
                var bonusXP = delta * 25;
                var xp = BaseXP + bonusXP + socialModifier * (BaseXP / 4);

                dbPlayerRoleplay.RPPoints = 0;
                dbPlayerRoleplay.TotalRPExpGained += (ulong)xp;
                _db.Set(dbPlayerRoleplay);

                _job.GiveXP(player, xp);
            }
        }

        private bool CanReceiveRPPoint(uint player, ChatChannelType channel)
        {
            var playerId = PlayerId.Get(player);

            // Party - Must be in a party with another PC.
            if (channel == ChatChannelType.PlayerParty)
            {
                for (var member = GetFirstFactionMember(player); GetIsObjectValid(member); member = GetNextFactionMember(player))
                {
                    if (GetObjectUUID(member) == playerId) continue;
                    return true;
                }

                return false;
            }

            for (var currentPlayer = GetFirstPC(); GetIsObjectValid(currentPlayer); currentPlayer = GetNextPC())
            {
                float distance;
                if (channel == ChatChannelType.PlayerTalk)
                {
                    distance = 20.0f;
                }
                else if (channel == ChatChannelType.PlayerWhisper)
                {
                    distance = 4.0f;
                }
                else break;

                if (GetDistanceBetween(player, currentPlayer) <= distance)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
