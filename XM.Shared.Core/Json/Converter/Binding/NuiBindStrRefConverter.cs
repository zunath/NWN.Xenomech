using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Anvil.API;

namespace XM.Shared.Core.Json.Converter.Binding
{
    public class NuiBindStrRefConverter : JsonConverter<NuiBindStrRef>
    {
        public override NuiBindStrRef Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.StartObject)
            {
                JsonDocument jsonDoc = JsonDocument.ParseValue(ref reader);

                if (jsonDoc.RootElement.TryGetProperty("bind", out JsonElement bindElement))
                {
                    string key = bindElement.GetString() ?? throw new JsonException("Key cannot be null.");
                    return new NuiBindStrRef(key);
                }
            }

            throw new JsonException("Invalid JSON structure for NuiBindStrRef.");
        }

        public override void Write(Utf8JsonWriter writer, NuiBindStrRef value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();
            writer.WriteString("bind", value.Key);
            writer.WriteEndObject();
        }
    }
}