using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Anvil.API;

namespace XM.Shared.Core.Json.Converter.UI.Widget.DrawList
{
    public class NuiDrawListImageConverter : JsonConverter<NuiDrawListImage>
    {
        public override NuiDrawListImage Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType != JsonTokenType.StartObject)
            {
                throw new JsonException("Expected StartObject token.");
            }

            NuiProperty<string>? resRef = null;
            NuiProperty<NuiRect>? rect = null;
            NuiProperty<NuiAspect>? aspect = null;
            NuiProperty<NuiHAlign>? horizontalAlign = null;
            NuiProperty<NuiVAlign>? verticalAlign = null;
            NuiProperty<NuiRect>? imageRegion = null;

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
                    case "image":
                        resRef = JsonSerializer.Deserialize<NuiProperty<string>>(ref reader, options);
                        break;
                    case "rect":
                        rect = JsonSerializer.Deserialize<NuiProperty<NuiRect>>(ref reader, options);
                        break;
                    case "image_aspect":
                        aspect = JsonSerializer.Deserialize<NuiProperty<NuiAspect>>(ref reader, options);
                        break;
                    case "image_halign":
                        horizontalAlign = JsonSerializer.Deserialize<NuiProperty<NuiHAlign>>(ref reader, options);
                        break;
                    case "image_valign":
                        verticalAlign = JsonSerializer.Deserialize<NuiProperty<NuiVAlign>>(ref reader, options);
                        break;
                    case "image_region":
                        imageRegion = JsonSerializer.Deserialize<NuiProperty<NuiRect>>(ref reader, options);
                        break;
                    default:
                        reader.Skip();
                        break;
                }
            }

            if (resRef == null || rect == null)
            {
                throw new JsonException("Missing required properties 'image' or 'rect'.");
            }

            var image = new NuiDrawListImage(resRef, rect)
            {
                Aspect = aspect ?? NuiAspect.Exact,
                HorizontalAlign = horizontalAlign ?? NuiHAlign.Left,
                VerticalAlign = verticalAlign ?? NuiVAlign.Top,
                ImageRegion = imageRegion,
            };

            return image;
        }

        public override void Write(Utf8JsonWriter writer, NuiDrawListImage value, JsonSerializerOptions options)
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

            if (value.LineThickness != null)
            {
                writer.WritePropertyName("line_thickness");
                JsonSerializer.Serialize(writer, value.LineThickness, options);
            }

            writer.WriteNumber("order", (int)value.Order);
            writer.WriteNumber("render", (int)value.Render);

            // Write NuiDrawListImage-specific properties
            writer.WritePropertyName("image");
            JsonSerializer.Serialize(writer, value.ResRef, options);

            writer.WritePropertyName("rect");
            JsonSerializer.Serialize(writer, value.Rect, options);

            writer.WritePropertyName("image_aspect");
            JsonSerializer.Serialize(writer, value.Aspect, options);

            writer.WritePropertyName("image_halign");
            JsonSerializer.Serialize(writer, value.HorizontalAlign, options);

            writer.WritePropertyName("image_valign");
            JsonSerializer.Serialize(writer, value.VerticalAlign, options);

            if (value.ImageRegion != null)
            {
                writer.WritePropertyName("image_region");
                JsonSerializer.Serialize(writer, value.ImageRegion, options);
            }

            writer.WriteEndObject();
        }
    }
}
