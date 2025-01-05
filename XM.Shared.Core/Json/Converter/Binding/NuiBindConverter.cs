using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Anvil.API;

namespace XM.Shared.Core.Json.Converter.Binding
{
    public class NuiBindConverter<T> : JsonConverter<NuiBind<T>>
    {
        public override NuiBind<T> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.StartObject)
            {
                JsonDocument jsonDoc = JsonDocument.ParseValue(ref reader);
                if (jsonDoc.RootElement.TryGetProperty("bind", out JsonElement bindElement))
                {
                    string key = bindElement.GetString() ?? throw new JsonException("Key cannot be null.");
                    return new NuiBind<T>(key);
                }
            }

            throw new JsonException("Invalid JSON structure for NuiBind<T>.");
        }

        public override void Write(Utf8JsonWriter writer, NuiBind<T> value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();
            writer.WriteString("bind", value.Key);
            writer.WriteEndObject();
        }
    }
}