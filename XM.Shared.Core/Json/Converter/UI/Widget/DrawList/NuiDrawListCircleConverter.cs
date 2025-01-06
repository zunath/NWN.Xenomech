using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Anvil.API;

namespace XM.Shared.Core.Json.Converter.UI.Widget.DrawList
{
    public class NuiDrawListCircleConverter : JsonConverter<NuiDrawListCircle>
    {
        public override NuiDrawListCircle Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType != JsonTokenType.StartObject)
            {
                throw new JsonException("Expected StartObject token.");
            }

            NuiProperty<Color>? color = null;
            NuiProperty<bool>? fill = null;
            NuiProperty<float>? lineThickness = null;
            NuiProperty<NuiRect>? rect = null;

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
                    case "fill":
                        fill = JsonSerializer.Deserialize<NuiProperty<bool>>(ref reader, options);
                        break;
                    case "line_thickness":
                        lineThickness = JsonSerializer.Deserialize<NuiProperty<float>>(ref reader, options);
                        break;
                    case "rect":
                        rect = JsonSerializer.Deserialize<NuiProperty<NuiRect>>(ref reader, options);
                        break;
                    default:
                        reader.Skip();
                        break;
                }
            }

            return new NuiDrawListCircle(color!, fill!, lineThickness!, rect!);
        }

        public override void Write(Utf8JsonWriter writer, NuiDrawListCircle value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();

            // Write common properties from NuiDrawListItem
            if (value.Color != null)
            {
                writer.WritePropertyName("color");
                JsonSerializer.Serialize(writer, value.Color, options);
            }

            writer.WritePropertyName("enabled");
            JsonSerializer.Serialize(writer, value.Enabled, options);

            if (value.Fill != null)
            {
                writer.WritePropertyName("fill");
                JsonSerializer.Serialize(writer, value.Fill, options);
            }
            else
            {
                writer.WriteNull("fill");
            }

            if (value.LineThickness != null)
            {
                writer.WritePropertyName("line_thickness");
                JsonSerializer.Serialize(writer, value.LineThickness, options);
            }
            else
            {
                writer.WriteNull("line_thickness");
            }

            writer.WriteNumber("order", (int)value.Order);
            writer.WriteNumber("render", (int)value.Render);

            // Write NuiDrawListCircle-specific property
            writer.WritePropertyName("rect");
            JsonSerializer.Serialize(writer, value.Rect, options);

            writer.WriteEndObject();
        }
    }
}
