using Anvil.API;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace XM.Shared.Core.Json.Converter.UI.Widget
{
    public class NuiComboEntryConverter : JsonConverter<NuiComboEntry>
    {
        public override NuiComboEntry Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType != JsonTokenType.StartArray)
            {
                throw new JsonException("Expected StartArray token.");
            }

            reader.Read();
            if (reader.TokenType != JsonTokenType.String)
            {
                throw new JsonException("Expected String token for 'label'.");
            }

            string label = reader.GetString()!;

            reader.Read();
            if (reader.TokenType != JsonTokenType.Number)
            {
                throw new JsonException("Expected Number token for 'value'.");
            }

            int value = reader.GetInt32();

            reader.Read();
            if (reader.TokenType != JsonTokenType.EndArray)
            {
                throw new JsonException("Expected EndArray token.");
            }

            return new NuiComboEntry(label, value);
        }

        public override void Write(Utf8JsonWriter writer, NuiComboEntry value, JsonSerializerOptions options)
        {
            writer.WriteStartArray();
            writer.WriteStringValue(value.Label);
            writer.WriteNumberValue(value.Value);
            writer.WriteEndArray();
        }
    }

}
