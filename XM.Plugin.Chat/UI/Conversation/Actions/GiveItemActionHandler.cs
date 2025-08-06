using System.Collections.Generic;
using XM.Shared.Core.Conversation;

namespace XM.Chat.UI.Conversation.Actions
{
    /// <summary>
    /// Handles the GiveItem conversation action.
    /// </summary>
    public class GiveItemActionHandler : IConversationActionHandler
    {
        public void HandleAction(ConversationAction action, uint player, IConversationCallback conversationCallback = null)
        {
            var itemId = action.Parameters?.GetValueOrDefault("itemId")?.ToString();
            var quantity = action.Parameters?.GetValueOrDefault("quantity")?.ToString();
            
            if (!string.IsNullOrEmpty(itemId))
            {
                var itemQuantity = 1;
                if (!string.IsNullOrEmpty(quantity) && int.TryParse(quantity, out var parsedQuantity))
                {
                    itemQuantity = parsedQuantity;
                }

                CreateItemOnObject(itemId, player, itemQuantity);
                SendMessageToPC(player, $"You received {itemQuantity}x {itemId}.");
            }
        }
    }
} 