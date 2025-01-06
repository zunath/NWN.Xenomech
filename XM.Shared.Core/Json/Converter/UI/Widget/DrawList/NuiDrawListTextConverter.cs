using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Anvil.API;

namespace XM.Shared.Core.Json.Converter.UI.Widget.DrawList
{
    public class NuiDrawListTextConverter : JsonConverter<NuiDrawListText>
    {
        public override NuiDrawListText Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType != JsonTokenType.StartObject)
            {
                throw new JsonException("Expected StartObject token.");
            }

            using var jsonDoc = JsonDocument.ParseValue(ref reader);
            var root = jsonDoc.RootElement;

            NuiProperty<Color> color = null;
            NuiProperty<NuiRect> rect = null;
            NuiProperty<string> text = null;

            foreach (var property in root.EnumerateObject())
            {
                switch (property.Name)
                {
                    case "color":
                        color = JsonSerializer.Deserialize<NuiProperty<Color>>(property.Value.GetRawText(), options);
                        break;
                    case "rect":
                        rect = JsonSerializer.Deserialize<NuiProperty<NuiRect>>(property.Value.GetRawText(), options);
                        break;
                    case "text":
                        text = JsonSerializer.Deserialize<NuiProperty<string>>(property.Value.GetRawText(), options);
                        break;
                }
            }

            return new NuiDrawListText(color, rect, text);
        }

        public override void Write(Utf8JsonWriter writer, NuiDrawListText value, JsonSerializerOptions options)
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

            // Write specific properties for NuiDrawListText
            if (value.Rect != null)
            {
                writer.WritePropertyName("rect");
                JsonSerializer.Serialize(writer, value.Rect, options);
            }

            if (value.Text != null)
            {
                writer.WritePropertyName("text");
                JsonSerializer.Serialize(writer, value.Text, options);
            }

            writer.WriteEndObject();
        }
    }
}
