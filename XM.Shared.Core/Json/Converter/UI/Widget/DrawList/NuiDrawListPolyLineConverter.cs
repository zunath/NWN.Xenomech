using System.Collections.Generic;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Anvil.API;

namespace XM.Shared.Core.Json.Converter.UI.Widget.DrawList
{
    public class NuiDrawListPolyLineConverter : JsonConverter<NuiDrawListPolyLine>
    {
        public override NuiDrawListPolyLine Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType != JsonTokenType.StartObject)
            {
                throw new JsonException("Expected StartObject token.");
            }

            using var jsonDoc = JsonDocument.ParseValue(ref reader);
            var root = jsonDoc.RootElement;

            NuiProperty<Color> color = null;
            NuiProperty<bool> fill = null;
            NuiProperty<float> lineThickness = null;
            List<float> points = null;

            foreach (var property in root.EnumerateObject())
            {
                switch (property.Name)
                {
                    case "color":
                        color = JsonSerializer.Deserialize<NuiProperty<Color>>(property.Value.GetRawText(), options);
                        break;
                    case "fill":
                        fill = JsonSerializer.Deserialize<NuiProperty<bool>>(property.Value.GetRawText(), options);
                        break;
                    case "line_thickness":
                        lineThickness = JsonSerializer.Deserialize<NuiProperty<float>>(property.Value.GetRawText(), options);
                        break;
                    case "points":
                        points = JsonSerializer.Deserialize<List<float>>(property.Value.GetRawText(), options);
                        break;
                }
            }

            return new NuiDrawListPolyLine(color, fill, lineThickness, points);
        }

        public override void Write(Utf8JsonWriter writer, NuiDrawListPolyLine value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();

            // Write common properties from the base class
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

            // Write specific properties for NuiDrawListPolyLine
            if (value.Points != null && value.Points.Count > 0)
            {
                writer.WritePropertyName("points");
                JsonSerializer.Serialize(writer, value.Points, options);
            }

            writer.WriteEndObject();
        }
    }
}
