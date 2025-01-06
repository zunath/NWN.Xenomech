using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Anvil.API;

namespace XM.Shared.Core.Json.Converter.UI.Widget.DrawList
{
    public class NuiDrawListLineConverter : JsonConverter<NuiDrawListLine>
    {
        public override NuiDrawListLine Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
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
            NuiProperty<NuiVector> pointA = null;
            NuiProperty<NuiVector> pointB = null;

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
                    case "a":
                        pointA = JsonSerializer.Deserialize<NuiProperty<NuiVector>>(property.Value.GetRawText(), options);
                        break;
                    case "b":
                        pointB = JsonSerializer.Deserialize<NuiProperty<NuiVector>>(property.Value.GetRawText(), options);
                        break;
                }
            }

            return new NuiDrawListLine(color, fill, lineThickness, pointA, pointB);
        }

        public override void Write(Utf8JsonWriter writer, NuiDrawListLine value, JsonSerializerOptions options)
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

            // Write specific properties for NuiDrawListLine
            if (value.PointA != null)
            {
                writer.WritePropertyName("a");
                JsonSerializer.Serialize(writer, value.PointA, options);
            }

            if (value.PointB != null)
            {
                writer.WritePropertyName("b");
                JsonSerializer.Serialize(writer, value.PointB, options);
            }

            writer.WriteEndObject();
        }
    }
}
