using Anvil.API;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace XM.Shared.Core.Json.Converter.UI.Widget
{
    public class NuiComboConverter : JsonConverter<NuiCombo>
    {
        public override NuiCombo Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType != JsonTokenType.StartObject)
            {
                throw new JsonException("Expected StartObject token.");
            }

            NuiProperty<List<NuiComboEntry>> entries = null;
            NuiProperty<int> selected = null;

            while (reader.Read())
            {
                if (reader.TokenType == JsonTokenType.EndObject)
                {
                    break;
                }

                if (reader.TokenType != JsonTokenType.PropertyName)
                {
                    throw new JsonException("Expected PropertyName token.");
                }

                string propertyName = reader.GetString()!;

                reader.Read();

                switch (propertyName)
                {
                    case "elements":
                        entries = JsonSerializer.Deserialize<NuiProperty<List<NuiComboEntry>>>(ref reader, options);
                        break;
                    case "value":
                        selected = JsonSerializer.Deserialize<NuiProperty<int>>(ref reader, options);
                        break;
                    default:
                        reader.Skip();
                        break;
                }
            }

            if (entries == null || selected == null)
            {
                throw new JsonException("Missing required properties 'elements' or 'value'.");
            }

            return new NuiCombo
            {
                Entries = entries,
                Selected = selected
            };
        }

        public override void Write(Utf8JsonWriter writer, NuiCombo value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();

            writer.WriteString("type", value.Type);

            writer.WritePropertyName("elements");
            JsonSerializer.Serialize(writer, value.Entries, options);

            writer.WritePropertyName("value");
            JsonSerializer.Serialize(writer, value.Selected, options);

            writer.WriteEndObject();
        }
    }

}
