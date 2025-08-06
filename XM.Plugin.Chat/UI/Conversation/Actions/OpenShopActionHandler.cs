using System;
using System.Collections.Generic;
using XM.Shared.Core.Conversation;

namespace XM.Chat.UI.Conversation.Actions
{
    /// <summary>
    /// Handles the OpenShop conversation action.
    /// </summary>
    public class OpenShopActionHandler : IConversationActionHandler
    {
        public void HandleAction(ConversationAction action, uint player, IConversationCallback conversationCallback = null)
        {
            var shopId = action.Parameters?.GetValueOrDefault("shopId")?.ToString();
            
            Console.WriteLine($"shopId = {shopId}");

            if (!string.IsNullOrEmpty(shopId))
            {
                var store = GetObjectByTag(shopId);

                if(GetIsObjectValid(store))
                    OpenStore(store, player);
                else 
                    SendMessageToPC(player, $"ERROR: Could not find store: {shopId}");
            }
        }
    }
} 