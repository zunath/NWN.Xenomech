using Anvil.API;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace XM.Shared.Core.Json.Converter.Widget.DrawList
{
    public class NuiDrawListCurveConverter : JsonConverter<NuiDrawListCurve>
    {
        public override NuiDrawListCurve Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType != JsonTokenType.StartObject)
            {
                throw new JsonException("Expected StartObject token.");
            }

            NuiProperty<Color> color = null;
            NuiProperty<float> lineThickness = null;
            NuiProperty<NuiVector> pointA = null;
            NuiProperty<NuiVector> pointB = null;
            NuiProperty<NuiVector> control0 = null;
            NuiProperty<NuiVector> control1 = null;

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
                    case "line_thickness":
                        lineThickness = JsonSerializer.Deserialize<NuiProperty<float>>(ref reader, options);
                        break;
                    case "a":
                        pointA = JsonSerializer.Deserialize<NuiProperty<NuiVector>>(ref reader, options);
                        break;
                    case "b":
                        pointB = JsonSerializer.Deserialize<NuiProperty<NuiVector>>(ref reader, options);
                        break;
                    case "ctrl0":
                        control0 = JsonSerializer.Deserialize<NuiProperty<NuiVector>>(ref reader, options);
                        break;
                    case "ctrl1":
                        control1 = JsonSerializer.Deserialize<NuiProperty<NuiVector>>(ref reader, options);
                        break;
                    default:
                        reader.Skip();
                        break;
                }
            }

            if (color == null || lineThickness == null || pointA == null || pointB == null || control0 == null || control1 == null)
            {
                throw new JsonException("Missing required properties.");
            }

            return new NuiDrawListCurve(color, lineThickness, pointA, pointB, control0, control1);
        }

        public override void Write(Utf8JsonWriter writer, NuiDrawListCurve value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();

            writer.WritePropertyName("color");
            JsonSerializer.Serialize(writer, value.Color, options);

            writer.WritePropertyName("line_thickness");
            JsonSerializer.Serialize(writer, value.LineThickness, options);

            writer.WritePropertyName("a");
            JsonSerializer.Serialize(writer, value.PointA, options);

            writer.WritePropertyName("b");
            JsonSerializer.Serialize(writer, value.PointB, options);

            writer.WritePropertyName("ctrl0");
            JsonSerializer.Serialize(writer, value.Control0, options);

            writer.WritePropertyName("ctrl1");
            JsonSerializer.Serialize(writer, value.Control1, options);

            writer.WriteEndObject();
        }
    }

}
