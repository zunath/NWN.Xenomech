using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using XM.Shared.Core.Entity.Stat;

namespace XM.Shared.Core.Json
{
    public class ItemStatGroupJsonConverter : JsonConverter<ItemStatGroup>
    {
        public override ItemStatGroup Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType != JsonTokenType.StartObject)
                throw new JsonException("Expected StartObject for ItemStatGroup");

            var group = new ItemStatGroup();

            while (reader.Read())
            {
                if (reader.TokenType == JsonTokenType.EndObject)
                    break;

                if (reader.TokenType != JsonTokenType.PropertyName)
                    throw new JsonException("Expected PropertyName in ItemStatGroup");

                var propName = reader.GetString();
                reader.Read();

                switch (propName)
                {
                    case nameof(ItemStatGroup.IsEquipped):
                        group.IsEquipped = reader.GetBoolean();
                        break;
                    case nameof(ItemStatGroup.DMG):
                        group.DMG = reader.GetInt32();
                        break;
                    case nameof(ItemStatGroup.Delay):
                        group.Delay = reader.GetInt32();
                        break;
                    case nameof(ItemStatGroup.Condition):
                        group.Condition = (float)reader.GetDouble();
                        break;
                    case nameof(ItemStatGroup.Stats):
                        if (reader.TokenType != JsonTokenType.StartObject)
                            throw new JsonException("Expected StartObject for Stats");
                        while (reader.Read())
                        {
                            if (reader.TokenType == JsonTokenType.EndObject)
                                break;
                            if (reader.TokenType != JsonTokenType.PropertyName)
                                throw new JsonException("Expected PropertyName in Stats");
                            var key = reader.GetString() ?? string.Empty;
                            reader.Read();
                            var value = reader.GetInt32();

                            var mapped = KeyNameRegistry.ToCode("StatType", key);
                            if (mapped.HasValue)
                                group.Stats[mapped.Value] = value;
                            else if (int.TryParse(key, out var statCode))
                                group.Stats[statCode] = value;
                        }
                        break;
                    case nameof(ItemStatGroup.Resists):
                        if (reader.TokenType != JsonTokenType.StartObject)
                            throw new JsonException("Expected StartObject for Resists");
                        while (reader.Read())
                        {
                            if (reader.TokenType == JsonTokenType.EndObject)
                                break;
                            if (reader.TokenType != JsonTokenType.PropertyName)
                                throw new JsonException("Expected PropertyName in Resists");
                            var key = reader.GetString() ?? string.Empty;
                            reader.Read();
                            var value = reader.GetInt32();

                            var mapped = KeyNameRegistry.ToCode("ResistType", key);
                            if (mapped.HasValue)
                                group.Resists[mapped.Value] = value;
                            else if (int.TryParse(key, out var resistCode))
                                group.Resists[resistCode] = value;
                        }
                        break;
                    default:
                        // Unknown; skip
                        reader.Skip();
                        break;
                }
            }

            return group;
        }

        public override void Write(Utf8JsonWriter writer, ItemStatGroup value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();

            writer.WriteBoolean(nameof(ItemStatGroup.IsEquipped), value.IsEquipped);
            writer.WriteNumber(nameof(ItemStatGroup.DMG), value.DMG);
            writer.WriteNumber(nameof(ItemStatGroup.Delay), value.Delay);
            writer.WriteNumber(nameof(ItemStatGroup.Condition), value.Condition);

            writer.WritePropertyName(nameof(ItemStatGroup.Stats));
            writer.WriteStartObject();
            foreach (var (code, statValue) in value.Stats)
            {
                var name = KeyNameRegistry.ToName("StatType", code);
                writer.WriteNumber(name, statValue);
            }
            writer.WriteEndObject();

            writer.WritePropertyName(nameof(ItemStatGroup.Resists));
            writer.WriteStartObject();
            foreach (var (code, resistValue) in value.Resists)
            {
                var name = KeyNameRegistry.ToName("ResistType", code);
                writer.WriteNumber(name, resistValue);
            }
            writer.WriteEndObject();

            writer.WriteEndObject();
        }
    }
}


