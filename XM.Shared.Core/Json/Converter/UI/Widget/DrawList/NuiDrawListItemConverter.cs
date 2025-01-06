using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;
using Anvil.API;

namespace XM.Shared.Core.Json.Converter.UI.Widget.DrawList
{
    public class NuiDrawListItemConverter : JsonConverter<NuiDrawListItem>
    {
        private static readonly Dictionary<NuiDrawListItemType, Type> TypeMapping = new()
        {
            { NuiDrawListItemType.Arc, typeof(NuiDrawListArc) },
            { NuiDrawListItemType.Circle, typeof(NuiDrawListCircle) },
            { NuiDrawListItemType.Curve, typeof(NuiDrawListCurve) },
            { NuiDrawListItemType.Image, typeof(NuiDrawListImage) },
            { NuiDrawListItemType.Line, typeof(NuiDrawListLine) },
            { NuiDrawListItemType.PolyLine, typeof(NuiDrawListPolyLine) },
            { NuiDrawListItemType.Text, typeof(NuiDrawListText) }
        };

        public override NuiDrawListItem Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType != JsonTokenType.StartObject)
            {
                throw new JsonException("Expected StartObject token.");
            }

            using var jsonDoc = JsonDocument.ParseValue(ref reader);
            var root = jsonDoc.RootElement;

            if (root.TryGetProperty("type", out var typeProperty))
            {
                int typeValue = typeProperty.GetInt32();
                if (Enum.IsDefined(typeof(NuiDrawListItemType), typeValue) && TypeMapping.TryGetValue((NuiDrawListItemType)typeValue, out var concreteType))
                {
                    return (NuiDrawListItem)JsonSerializer.Deserialize(root.GetRawText(), concreteType, options)!;
                }

                throw new JsonException($"Unknown type '{typeValue}' for NuiDrawListItem.");
            }

            throw new JsonException("Missing 'type' property in JSON for NuiDrawListItem.");
        }

        public override void Write(Utf8JsonWriter writer, NuiDrawListItem value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();

            // Write common properties
            WriteCommonProperties(writer, value, options);

            // Write specific properties from the concrete class
            var concreteType = value.GetType();
            foreach (var property in concreteType.GetProperties())
            {
                if (property.DeclaringType == typeof(NuiDrawListItem))
                {
                    continue; // Skip properties already written
                }

                var propertyValue = property.GetValue(value);
                if (propertyValue != null)
                {
                    var propertyName = property.GetCustomAttribute<JsonPropertyNameAttribute>()?.Name ?? property.Name.ToLower();
                    writer.WritePropertyName(propertyName);
                    JsonSerializer.Serialize(writer, propertyValue, propertyValue.GetType(), options);
                }
            }

            writer.WriteEndObject();
        }

        private void WriteCommonProperties(Utf8JsonWriter writer, NuiDrawListItem value, JsonSerializerOptions options)
        {
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
        }
    }
}
