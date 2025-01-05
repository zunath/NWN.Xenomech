using Anvil.API;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace XM.Shared.Core.Json.Converter.Widget.DrawList
{
    public class NuiDrawListTextConverter : JsonConverter<NuiDrawListText>
    {
        public override NuiDrawListText Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType != JsonTokenType.StartObject)
            {
                throw new JsonException("Expected StartObject token.");
            }

            NuiProperty<Color> color = null;
            NuiProperty<NuiRect> rect = null;
            NuiProperty<string> text = null;

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
                    case "color":
                        color = JsonSerializer.Deserialize<NuiProperty<Color>>(ref reader, options);
                        break;
                    case "rect":
                        rect = JsonSerializer.Deserialize<NuiProperty<NuiRect>>(ref reader, options);
                        break;
                    case "text":
                        text = JsonSerializer.Deserialize<NuiProperty<string>>(ref reader, options);
                        break;
                    default:
                        reader.Skip();
                        break;
                }
            }

            if (color == null || rect == null || text == null)
            {
                throw new JsonException("Missing required properties.");
            }

            return new NuiDrawListText(color, rect, text);
        }

        public override void Write(Utf8JsonWriter writer, NuiDrawListText value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();

            writer.WritePropertyName("color");
            JsonSerializer.Serialize(writer, value.Color, options);

            writer.WritePropertyName("rect");
            JsonSerializer.Serialize(writer, value.Rect, options);

            writer.WritePropertyName("text");
            JsonSerializer.Serialize(writer, value.Text, options);

            writer.WriteEndObject();
        }
    }

}
