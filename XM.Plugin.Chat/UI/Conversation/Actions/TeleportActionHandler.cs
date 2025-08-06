using System.Collections.Generic;
using XM.Shared.Core.Conversation;

namespace XM.Chat.UI.Conversation.Actions
{
    /// <summary>
    /// Handles the Teleport conversation action.
    /// </summary>
    public class TeleportActionHandler : IConversationActionHandler
    {
        public void HandleAction(ConversationAction action, uint player, IConversationCallback conversationCallback = null)
        {
            var location = action.Parameters?.GetValueOrDefault("location")?.ToString();
            if (!string.IsNullOrEmpty(location))
            {
                var waypoint = GetWaypointByTag(location);
                if (GetIsObjectValid(waypoint))
                {
                    var waypointLocation = GetLocation(waypoint);
                    AssignCommand(player, () => ActionJumpToLocation(waypointLocation));
                }
                else
                {
                    SendMessageToPC(player, $"Could not find teleport location '{location}'.");
                }
            }
        }
    }
} 