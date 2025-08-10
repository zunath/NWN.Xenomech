using Anvil.API;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace XM.Shared.Core.Json
{
    public class NuiComboEntryConverter : JsonConverter<NuiComboEntry>
    {
        public override NuiComboEntry Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType != JsonTokenType.StartArray)
                throw new JsonException();

            reader.Read();
            var label = reader.GetString();
            reader.Read();
            var value = reader.GetInt32();
            reader.Read(); // Move past the array end

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
