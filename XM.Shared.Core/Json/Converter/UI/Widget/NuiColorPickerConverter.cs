using Anvil.API;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace XM.Shared.Core.Json.Converter.UI.Widget
{
    public class NuiColorPickerConverter : JsonConverter<NuiColorPicker>
    {
        public override NuiColorPicker Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType != JsonTokenType.StartObject)
            {
                throw new JsonException("Expected StartObject token.");
            }

            NuiProperty<Color> color = null;

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
                        color = JsonSerializer.Deserialize<NuiProperty<Color>>(ref reader, options);
                        break;
                    default:
                        reader.Skip();
                        break;
                }
            }

            if (color == null)
            {
                throw new JsonException("Missing required property 'value'.");
            }

            return new NuiColorPicker(color);
        }

        public override void Write(Utf8JsonWriter writer, NuiColorPicker value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();

            writer.WriteString("type", value.Type);

            writer.WritePropertyName("value");
            JsonSerializer.Serialize(writer, value.Color, options);

            writer.WriteEndObject();
        }
    }

}
