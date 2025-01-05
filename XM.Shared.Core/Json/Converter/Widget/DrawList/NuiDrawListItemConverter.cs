using Anvil.API;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace XM.Shared.Core.Json.Converter.Widget.DrawList
{
    public class NuiDrawListItemConverter : JsonConverter<NuiDrawListItem>
    {
        public override NuiDrawListItem Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType != JsonTokenType.StartObject)
            {
                throw new JsonException("Expected StartObject token.");
            }

            string type = null;

            using var doc = JsonDocument.ParseValue(ref reader);
            var root = doc.RootElement;

            if (root.TryGetProperty("type", out var typeProperty))
            {
                type = typeProperty.GetString();
            }

            if (string.IsNullOrEmpty(type))
            {
                throw new JsonException("Missing 'type' property.");
            }

            // Deserialize to the appropriate subclass based on the type property
            return type switch
            {
                "arc" => JsonSerializer.Deserialize<NuiDrawListArc>(root.GetRawText(), options),
                "circle" => JsonSerializer.Deserialize<NuiDrawListCircle>(root.GetRawText(), options),
                "curve" => JsonSerializer.Deserialize<NuiDrawListCurve>(root.GetRawText(), options),
                "image" => JsonSerializer.Deserialize<NuiDrawListImage>(root.GetRawText(), options),
                _ => throw new JsonException($"Unknown type '{type}' for NuiDrawListItem.")
            };
        }

        public override void Write(Utf8JsonWriter writer, NuiDrawListItem value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();

            writer.WritePropertyName("type");
            JsonSerializer.Serialize(writer, value.Type.ToString().ToLower(), options);

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

            if (value.LineThickness != null)
            {
                writer.WritePropertyName("line_thickness");
                JsonSerializer.Serialize(writer, value.LineThickness, options);
            }

            writer.WritePropertyName("order");
            JsonSerializer.Serialize(writer, value.Order, options);

            writer.WritePropertyName("render");
            JsonSerializer.Serialize(writer, value.Render, options);

            writer.WriteEndObject();
        }
    }

}
