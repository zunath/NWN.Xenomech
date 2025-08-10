using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using XM.Shared.API.Constants;
using XM.Shared.Core.Entity.Stat;

namespace XM.Shared.Core.Json
{
    public class AbilityStatCollectionJsonConverter : JsonConverter<AbilityStatCollection>
    {
        public override AbilityStatCollection Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType != JsonTokenType.StartObject)
                throw new JsonException("Expected StartObject for AbilityStatCollection");

            var collection = new AbilityStatCollection();
            while (reader.Read())
            {
                if (reader.TokenType == JsonTokenType.EndObject)
                    break;
                if (reader.TokenType != JsonTokenType.PropertyName)
                    throw new JsonException("Expected PropertyName in AbilityStatCollection");

                var key = reader.GetString() ?? string.Empty;
                reader.Read();

                var group = JsonSerializer.Deserialize<StatGroup>(ref reader, options) ?? new StatGroup();

                var code = KeyNameRegistry.ToCode("FeatType", key);
                if (code.HasValue)
                {
                    collection[code.Value] = group;
                }
                else if (int.TryParse(key, out var featCode))
                {
                    collection[featCode] = group;
                }
            }
            return collection;
        }

        public override void Write(Utf8JsonWriter writer, AbilityStatCollection value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();
            foreach (var (featCode, group) in value)
            {
                var name = KeyNameRegistry.ToName("FeatType", featCode);
                writer.WritePropertyName(name);
                JsonSerializer.Serialize(writer, group, options);
            }
            writer.WriteEndObject();
        }
    }
}


