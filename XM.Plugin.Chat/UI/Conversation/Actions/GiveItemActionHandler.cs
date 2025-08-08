using System;
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
            try
            {
                var itemId = action?.Parameters?.GetValueOrDefault("itemId")?.ToString()?.Trim();
                var quantityRaw = action?.Parameters?.GetValueOrDefault("quantity")?.ToString()?.Trim();

                if (string.IsNullOrWhiteSpace(itemId))
                {
                    SendMessageToPC(player, "ERROR: Missing item identifier.");
                    Console.WriteLine("[GiveItemAction] Missing itemId. Action parameters: {0}", SafeDescribeParams(action?.Parameters));
                    return;
                }

                var itemQuantity = 1;
                if (!string.IsNullOrWhiteSpace(quantityRaw) && int.TryParse(quantityRaw, out var parsedQuantity))
                {
                    if (parsedQuantity > 0)
                    {
                        itemQuantity = parsedQuantity;
                    }
                    else
                    {
                        Console.WriteLine("[GiveItemAction] Non-positive quantity parsed ('{0}') for item '{1}'. Defaulting to 1.", quantityRaw, itemId);
                    }
                }

                var created = CreateItemOnObject(itemId, player, itemQuantity);
                if (!GetIsObjectValid(created))
                {
                    SendMessageToPC(player, $"ERROR: Failed to create item '{itemId}'.");
                    Console.WriteLine("[GiveItemAction] CreateItemOnObject returned invalid for item '{0}' x{1}.", itemId, itemQuantity);
                    return;
                }

                SendMessageToPC(player, $"You received {itemQuantity}x {itemId}.");
            }
            catch (Exception ex)
            {
                try
                {
                    var itemId = action?.Parameters?.GetValueOrDefault("itemId")?.ToString();
                    var quantity = action?.Parameters?.GetValueOrDefault("quantity")?.ToString();
                    Console.WriteLine("[GiveItemAction] Exception for player {0}. itemId='{1}', quantity='{2}'. Error: {3}", player, itemId, quantity, ex);
                }
                catch
                {
                    // Best-effort logging
                }

                SendMessageToPC(player, "ERROR: An unexpected error occurred while giving items.");
            }
        }

        private static string SafeDescribeParams(Dictionary<string, object> parameters)
        {
            if (parameters == null) return "<null>";
            try
            {
                var itemId = parameters.GetValueOrDefault("itemId");
                var quantity = parameters.GetValueOrDefault("quantity");
                return $"itemId='{itemId}', quantity='{quantity}'";
            }
            catch
            {
                return "<unavailable>";
            }
        }
    }
} 