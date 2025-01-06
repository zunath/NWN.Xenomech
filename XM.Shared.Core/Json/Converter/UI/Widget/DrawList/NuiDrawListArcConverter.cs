using Anvil.API;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace XM.Shared.Core.Json.Converter.UI.Widget.DrawList
{
    public class NuiDrawListArcConverter : JsonConverter<NuiDrawListArc>
    {
        public override NuiDrawListArc Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType != JsonTokenType.StartObject)
            {
                throw new JsonException("Expected StartObject token.");
            }

            NuiProperty<Color>? color = null;
            NuiProperty<bool>? fill = null;
            NuiProperty<float>? lineThickness = null;
            NuiProperty<NuiVector>? center = null;
            NuiProperty<float>? radius = null;
            NuiProperty<float>? angleMin = null;
            NuiProperty<float>? angleMax = null;

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
                    case "c":
                        center = JsonSerializer.Deserialize<NuiProperty<NuiVector>>(ref reader, options);
                        break;
                    case "radius":
                        radius = JsonSerializer.Deserialize<NuiProperty<float>>(ref reader, options);
                        break;
                    case "amin":
                        angleMin = JsonSerializer.Deserialize<NuiProperty<float>>(ref reader, options);
                        break;
                    case "amax":
                        angleMax = JsonSerializer.Deserialize<NuiProperty<float>>(ref reader, options);
                        break;
                    default:
                        reader.Skip();
                        break;
                }
            }

            return new NuiDrawListArc(color!, fill!, lineThickness!, center!, radius!, angleMin!, angleMax!);
        }

        public override void Write(Utf8JsonWriter writer, NuiDrawListArc value, JsonSerializerOptions options)
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

            // Write NuiDrawListArc-specific properties
            writer.WritePropertyName("c");
            JsonSerializer.Serialize(writer, value.Center, options);

            writer.WritePropertyName("radius");
            JsonSerializer.Serialize(writer, value.Radius, options);

            writer.WritePropertyName("amin");
            JsonSerializer.Serialize(writer, value.AngleMin, options);

            writer.WritePropertyName("amax");
            JsonSerializer.Serialize(writer, value.AngleMax, options);

            writer.WriteEndObject();
        }
    }

}
