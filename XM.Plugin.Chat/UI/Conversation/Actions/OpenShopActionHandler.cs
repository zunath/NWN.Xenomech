using System;
using System.Collections.Generic;
using System.Linq;
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

            if (!string.IsNullOrEmpty(shopId))
            {
                var store = GetObjectByTag(shopId);

                if(GetIsObjectValid(store))
                    OpenStore(store, player);
                else 
                {
                    var safeShopId = new string((shopId ?? string.Empty).ToCharArray()
                        .Where(ch => char.IsLetterOrDigit(ch) || ch == '_' || ch == '-' || ch == ' ')
                        .ToArray());
                    if (safeShopId.Length > 64) safeShopId = safeShopId.Substring(0, 64);
                    SendMessageToPC(player, $"ERROR: Could not find store: {safeShopId}");
                }
            }
        }
    }
} 