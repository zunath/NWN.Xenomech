using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace XM.Shared.Core.Conversation
{
    public class ConversationActionTypeConverter : JsonConverter<ConversationActionType>
    {
        public override ConversationActionType Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var stringValue = reader.GetString();
            return stringValue switch
            {
                "OpenShop" => ConversationActionType.OpenShop,
                "TeleportPlayer" => ConversationActionType.TeleportPlayer,
                "AcceptQuest" => ConversationActionType.AcceptQuest,
                "GiveItem" => ConversationActionType.GiveItem,
                "ChangePage" => ConversationActionType.ChangePage,
                "SetVariable" => ConversationActionType.SetVariable,
                "EndConversation" => ConversationActionType.EndConversation,
                _ => throw new JsonException($"Unknown conversation action type: {stringValue}")
            };
        }

        public override void Write(Utf8JsonWriter writer, ConversationActionType value, JsonSerializerOptions options)
        {
            var stringValue = value switch
            {
                ConversationActionType.OpenShop => "OpenShop",
                ConversationActionType.TeleportPlayer => "TeleportPlayer",
                ConversationActionType.AcceptQuest => "AcceptQuest",
                ConversationActionType.GiveItem => "GiveItem",
                ConversationActionType.ChangePage => "ChangePage",
                ConversationActionType.SetVariable => "SetVariable",
                ConversationActionType.EndConversation => "EndConversation",
                _ => throw new JsonException($"Unknown conversation action type: {value}")
            };
            
            writer.WriteStringValue(stringValue);
        }
    }
} 