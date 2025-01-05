using Anvil.API;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace XM.Shared.Core.Json.Converter.Widget.DrawList
{
    public class NuiDrawListPolyLineConverter : JsonConverter<NuiDrawListPolyLine>
    {
        public override NuiDrawListPolyLine Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType != JsonTokenType.StartObject)
            {
                throw new JsonException("Expected StartObject token.");
            }

            NuiProperty<Color> color = null;
            NuiProperty<bool> fill = null;
            NuiProperty<float> lineThickness = null;
            List<float> points = null;

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
                    case "points":
                        points = JsonSerializer.Deserialize<List<float>>(ref reader, options);
                        break;
                    default:
                        reader.Skip();
                        break;
                }
            }

            if (color == null || fill == null || lineThickness == null || points == null)
            {
                throw new JsonException("Missing required properties.");
            }

            return new NuiDrawListPolyLine(color, fill, lineThickness, points);
        }

        public override void Write(Utf8JsonWriter writer, NuiDrawListPolyLine value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();

            writer.WritePropertyName("color");
            JsonSerializer.Serialize(writer, value.Color, options);

            writer.WritePropertyName("fill");
            JsonSerializer.Serialize(writer, value.Fill, options);

            writer.WritePropertyName("line_thickness");
            JsonSerializer.Serialize(writer, value.LineThickness, options);

            writer.WritePropertyName("points");
            JsonSerializer.Serialize(writer, value.Points, options);

            writer.WriteEndObject();
        }
    }

}
