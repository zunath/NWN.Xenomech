using Anvil.API;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace XM.Shared.Core.Json.Converter.Widget.DrawList
{
    public class NuiDrawListArcConverter : JsonConverter<NuiDrawListArc>
    {
        public override NuiDrawListArc Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType != JsonTokenType.StartObject)
            {
                throw new JsonException("Expected StartObject token.");
            }

            NuiProperty<Color> color = null;
            NuiProperty<bool> fill = null;
            NuiProperty<float> lineThickness = null;
            NuiProperty<NuiVector> center = null;
            NuiProperty<float> radius = null;
            NuiProperty<float> angleMin = null;
            NuiProperty<float> angleMax = null;

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

            if (color == null || fill == null || lineThickness == null || center == null || radius == null || angleMin == null || angleMax == null)
            {
                throw new JsonException("Missing required properties.");
            }

            return new NuiDrawListArc(color, fill, lineThickness, center, radius, angleMin, angleMax);
        }

        public override void Write(Utf8JsonWriter writer, NuiDrawListArc value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();

            writer.WritePropertyName("color");
            JsonSerializer.Serialize(writer, value.Color, options);

            writer.WritePropertyName("fill");
            JsonSerializer.Serialize(writer, value.Fill, options);

            writer.WritePropertyName("line_thickness");
            JsonSerializer.Serialize(writer, value.LineThickness, options);

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
