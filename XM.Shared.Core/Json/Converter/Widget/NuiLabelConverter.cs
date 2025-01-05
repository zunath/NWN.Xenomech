using Anvil.API;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace XM.Shared.Core.Json.Converter.Widget
{
    public class NuiLabelConverter : JsonConverter<NuiLabel>
    {
        public override NuiLabel Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType != JsonTokenType.StartObject)
            {
                throw new JsonException("Expected StartObject token.");
            }

            NuiProperty<string> label = null;
            NuiProperty<NuiHAlign> horizontalAlign = null;
            NuiProperty<NuiVAlign> verticalAlign = null;

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
                    case "value":
                        label = JsonSerializer.Deserialize<NuiProperty<string>>(ref reader, options);
                        break;
                    case "text_halign":
                        horizontalAlign = JsonSerializer.Deserialize<NuiProperty<NuiHAlign>>(ref reader, options);
                        break;
                    case "text_valign":
                        verticalAlign = JsonSerializer.Deserialize<NuiProperty<NuiVAlign>>(ref reader, options);
                        break;
                    default:
                        reader.Skip();
                        break;
                }
            }

            if (label == null)
            {
                throw new JsonException("Missing required property 'value'.");
            }

            return new NuiLabel(label)
            {
                HorizontalAlign = horizontalAlign ?? NuiHAlign.Left,
                VerticalAlign = verticalAlign ?? NuiVAlign.Top
            };
        }

        public override void Write(Utf8JsonWriter writer, NuiLabel value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();

            writer.WriteString("type", value.Type);

            writer.WritePropertyName("value");
            JsonSerializer.Serialize(writer, value.Label, options);

            writer.WritePropertyName("text_halign");
            JsonSerializer.Serialize(writer, value.HorizontalAlign, options);

            writer.WritePropertyName("text_valign");
            JsonSerializer.Serialize(writer, value.VerticalAlign, options);

            writer.WriteEndObject();
        }
    }

}
