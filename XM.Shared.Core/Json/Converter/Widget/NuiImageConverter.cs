using Anvil.API;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace XM.Shared.Core.Json.Converter.Widget
{
    public class NuiImageConverter : JsonConverter<NuiImage>
    {
        public override NuiImage Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType != JsonTokenType.StartObject)
            {
                throw new JsonException("Expected StartObject token.");
            }

            NuiProperty<string> resRef = null;
            NuiProperty<NuiHAlign> horizontalAlign = null;
            NuiProperty<NuiVAlign> verticalAlign = null;
            NuiProperty<NuiAspect> imageAspect = null;
            NuiProperty<NuiRect> imageRegion = null;

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
                    case "value":
                        resRef = JsonSerializer.Deserialize<NuiProperty<string>>(ref reader, options);
                        break;
                    case "image_halign":
                        horizontalAlign = JsonSerializer.Deserialize<NuiProperty<NuiHAlign>>(ref reader, options);
                        break;
                    case "image_valign":
                        verticalAlign = JsonSerializer.Deserialize<NuiProperty<NuiVAlign>>(ref reader, options);
                        break;
                    case "image_aspect":
                        imageAspect = JsonSerializer.Deserialize<NuiProperty<NuiAspect>>(ref reader, options);
                        break;
                    case "image_region":
                        imageRegion = JsonSerializer.Deserialize<NuiProperty<NuiRect>>(ref reader, options);
                        break;
                    default:
                        reader.Skip();
                        break;
                }
            }

            if (resRef == null)
            {
                throw new JsonException("Missing required property 'value'.");
            }

            return new NuiImage(resRef)
            {
                HorizontalAlign = horizontalAlign ?? NuiHAlign.Left,
                VerticalAlign = verticalAlign ?? NuiVAlign.Top,
                ImageAspect = imageAspect ?? NuiAspect.Exact,
                ImageRegion = imageRegion
            };
        }

        public override void Write(Utf8JsonWriter writer, NuiImage value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();

            writer.WriteString("type", value.Type);

            writer.WritePropertyName("value");
            JsonSerializer.Serialize(writer, value.ResRef, options);

            writer.WritePropertyName("image_halign");
            JsonSerializer.Serialize(writer, value.HorizontalAlign, options);

            writer.WritePropertyName("image_valign");
            JsonSerializer.Serialize(writer, value.VerticalAlign, options);

            writer.WritePropertyName("image_aspect");
            JsonSerializer.Serialize(writer, value.ImageAspect, options);

            if (value.ImageRegion != null)
            {
                writer.WritePropertyName("image_region");
                JsonSerializer.Serialize(writer, value.ImageRegion, options);
            }

            writer.WriteEndObject();
        }
    }

}
