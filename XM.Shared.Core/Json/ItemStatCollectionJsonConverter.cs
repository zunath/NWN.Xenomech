using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using XM.Shared.API.Constants;
using XM.Shared.Core.Entity.Stat;

namespace XM.Shared.Core.Json
{
    public class ItemStatCollectionJsonConverter : JsonConverter<ItemStatCollection>
    {
        public override ItemStatCollection Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType != JsonTokenType.StartObject)
                throw new JsonException("Expected StartObject for ItemStatCollection");

            var collection = new ItemStatCollection();
            // Start with defaults from ctor; then overwrite any provided keys
            while (reader.Read())
            {
                if (reader.TokenType == JsonTokenType.EndObject)
                    break;
                if (reader.TokenType != JsonTokenType.PropertyName)
                    throw new JsonException("Expected PropertyName in ItemStatCollection");

                var key = reader.GetString() ?? string.Empty;
                reader.Read();

                ItemStatGroup group;
                if (reader.TokenType == JsonTokenType.StartObject)
                {
                    group = JsonSerializer.Deserialize<ItemStatGroup>(ref reader, options) ?? new ItemStatGroup();
                }
                else
                {
                    throw new JsonException("Expected ItemStatGroup object");
                }

                var code = KeyNameRegistry.ToCode("InventorySlotType", key);
                if (code.HasValue)
                {
                    collection[code.Value] = group;
                }
                else if (int.TryParse(key, out var slotCode))
                {
                    collection[slotCode] = group;
                }
            }

            return collection;
        }

        public override void Write(Utf8JsonWriter writer, ItemStatCollection value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();
            foreach (var (slotCode, group) in value)
            {
                var name = KeyNameRegistry.ToName("InventorySlotType", slotCode);
                writer.WritePropertyName(name);
                JsonSerializer.Serialize(writer, group, options);
            }
            writer.WriteEndObject();
        }
    }
}


