using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using XM.Shared.Core.Entity.Stat;

namespace XM.Shared.Core.Json
{
    public class StatGroupJsonConverter : JsonConverter<StatGroup>
    {
        public override StatGroup Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType != JsonTokenType.StartObject)
                throw new JsonException("Expected StartObject for StatGroup");

            var group = new StatGroup();

            while (reader.Read())
            {
                if (reader.TokenType == JsonTokenType.EndObject)
                    break;

                if (reader.TokenType != JsonTokenType.PropertyName)
                    throw new JsonException("Expected PropertyName in StatGroup");

                var propName = reader.GetString();
                reader.Read();

                if (string.Equals(propName, nameof(StatGroup.Stats), StringComparison.Ordinal))
                {
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
                }
                else if (string.Equals(propName, nameof(StatGroup.Resists), StringComparison.Ordinal))
                {
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
                }
                else
                {
                    // Skip unknown properties
                    reader.Skip();
                }
            }

            return group;
        }

        public override void Write(Utf8JsonWriter writer, StatGroup value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();

            writer.WritePropertyName(nameof(StatGroup.Stats));
            writer.WriteStartObject();
            foreach (var (code, statValue) in value.Stats)
            {
                var name = KeyNameRegistry.ToName("StatType", code);
                writer.WriteNumber(name, statValue);
            }
            writer.WriteEndObject();

            writer.WritePropertyName(nameof(StatGroup.Resists));
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


