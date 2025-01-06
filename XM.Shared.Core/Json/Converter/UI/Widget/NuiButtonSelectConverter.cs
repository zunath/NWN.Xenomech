using Anvil.API;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace XM.Shared.Core.Json.Converter.UI.Widget
{
    public class NuiButtonSelectConverter : JsonConverter<NuiButtonSelect>
    {
        public override NuiButtonSelect Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType != JsonTokenType.StartObject)
            {
                throw new JsonException("Expected StartObject token.");
            }

            NuiProperty<string> label = null;
            NuiProperty<bool> selected = null;

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
                    case "label":
                        label = JsonSerializer.Deserialize<NuiProperty<string>>(ref reader, options);
                        break;
                    case "value":
                        selected = JsonSerializer.Deserialize<NuiProperty<bool>>(ref reader, options);
                        break;
                    default:
                        reader.Skip();
                        break;
                }
            }

            if (label == null || selected == null)
            {
                throw new JsonException("Missing required properties 'label' or 'value'.");
            }

            return new NuiButtonSelect(label, selected);
        }

        public override void Write(Utf8JsonWriter writer, NuiButtonSelect value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();

            writer.WriteString("type", value.Type);

            writer.WritePropertyName("label");
            JsonSerializer.Serialize(writer, value.Label, options);

            writer.WritePropertyName("value");
            JsonSerializer.Serialize(writer, value.Selected, options);

            writer.WriteEndObject();
        }
    }

}
