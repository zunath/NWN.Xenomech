using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;
using Anvil.API;

namespace XM.Shared.Core.Json.Converter.UI
{
    public class NuiElementConverter : JsonConverter<NuiElement>
    {
        private static readonly Dictionary<string, Type> TypeMapping = new()
        {
            { "row", typeof(NuiRow) },
            { "col", typeof(NuiColumn) },
            { "progress", typeof(NuiProgress) },
            { "label", typeof(NuiLabel) },
            { "button", typeof(NuiButton) },
            { "button_image", typeof(NuiButtonImage) },
            { "button_select", typeof(NuiButtonSelect) },
            { "chart", typeof(NuiChart) },
            { "check", typeof(NuiCheck) },
            { "color_picker", typeof(NuiColorPicker) },
            { "combo", typeof(NuiCombo) },
            { "image", typeof(NuiImage) },
            { "options", typeof(NuiOptions) },
            { "slider", typeof(NuiSlider) },
            { "sliderf", typeof(NuiSliderFloat) },
            { "spacer", typeof(NuiSpacer) },
            { "text", typeof(NuiText) },
            { "textedit", typeof(NuiTextEdit) },
            { "tabbar", typeof(NuiToggles) },
            { "draw_list_text", typeof(NuiDrawListText) },
            { "draw_list_line", typeof(NuiDrawListLine) },
            { "draw_list_curve", typeof(NuiDrawListCurve) },
            { "draw_list_polyline", typeof(NuiDrawListPolyLine) },
            { "draw_list_image", typeof(NuiDrawListImage) },
            { "draw_list_arc", typeof(NuiDrawListArc) },
            { "draw_list_circle", typeof(NuiDrawListCircle) }
        };

        public override NuiElement Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType != JsonTokenType.StartObject)
            {
                throw new JsonException("Expected StartObject token.");
            }

            using var jsonDoc = JsonDocument.ParseValue(ref reader);
            var root = jsonDoc.RootElement;

            if (root.TryGetProperty("type", out var typeProperty))
            {
                string typeName = typeProperty.GetString();

                Console.WriteLine($"typeName = {typeName}");

                if (typeName != null && TypeMapping.TryGetValue(typeName, out var concreteType))
                {
                    return (NuiElement)JsonSerializer.Deserialize(root.GetRawText(), concreteType, options);
                }

                throw new JsonException($"Unknown type '{typeName}' for NuiElement.");
            }

            throw new JsonException("Missing 'type' property in JSON for NuiElement.");
        }

        public override void Write(Utf8JsonWriter writer, NuiElement value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();

            // Write common properties
            WriteCommonProperties(writer, value, options);

            // Handle specific layouts
            if (value is NuiLayout layout)
            {
                writer.WritePropertyName("children");
                var serializedChildren = GetSerializedChildren(layout);
                JsonSerializer.Serialize(writer, serializedChildren, options);
            }

            writer.WriteEndObject();
        }

        private void WriteCommonProperties(Utf8JsonWriter writer, NuiElement value, JsonSerializerOptions options)
        {
            if (value.Aspect.HasValue) writer.WriteNumber("aspect", value.Aspect.Value);
            if (value.Enabled != null)
            {
                writer.WritePropertyName("enabled");
                JsonSerializer.Serialize(writer, value.Enabled, options);
            }
            if (value.ForegroundColor != null)
            {
                writer.WritePropertyName("foreground_color");
                JsonSerializer.Serialize(writer, value.ForegroundColor, options);
            }
            if (value.Height.HasValue) writer.WriteNumber("height", value.Height.Value);
            if (!string.IsNullOrEmpty(value.Id)) writer.WriteString("id", value.Id);
            if (value.Margin.HasValue) writer.WriteNumber("margin", value.Margin.Value);
            if (value.Padding.HasValue) writer.WriteNumber("padding", value.Padding.Value);
            if (value.Tooltip != null)
            {
                writer.WritePropertyName("tooltip");
                JsonSerializer.Serialize(writer, value.Tooltip, options);
            }
            writer.WriteString("type", value.Type);
            if (value.Visible != null)
            {
                writer.WritePropertyName("visible");
                JsonSerializer.Serialize(writer, value.Visible, options);
            }
            if (value.Width.HasValue) writer.WriteNumber("width", value.Width.Value);

            if (value.DrawList != null)
            {
                writer.WritePropertyName("draw_list");
                JsonSerializer.Serialize(writer, value.DrawList, options);
            }

            if (value.Scissor != null)
            {
                writer.WritePropertyName("draw_list_scissor");
                JsonSerializer.Serialize(writer, value.Scissor, options);
            }
            if (value.DisabledTooltip != null)
            {
                writer.WritePropertyName("disabled_tooltip");
                JsonSerializer.Serialize(writer, value.DisabledTooltip, options);
            }
            if (value.Encouraged != null)
            {
                writer.WritePropertyName("encouraged");
                JsonSerializer.Serialize(writer, value.Encouraged, options);
            }
        }

        private IEnumerable<NuiElement> GetSerializedChildren(NuiLayout layout)
        {
            var type = layout.GetType();
            var property = type.GetProperty("SerializedChildren", BindingFlags.NonPublic | BindingFlags.Instance);
            if (property == null)
            {
                throw new InvalidOperationException($"The type {type.Name} does not have a SerializedChildren property.");
            }

            var children = (IEnumerable<NuiElement>)property.GetValue(layout) ?? new List<NuiElement>();
            var serializedChildren = new List<NuiElement>();

            foreach (var child in children)
            {
                // Ensure we serialize using the correct derived type
                if (TypeMapping.TryGetValue(child.Type, out var concreteType) && child.GetType() != concreteType)
                {
                    var json = JsonSerializer.Serialize(child, concreteType, options: null);
                    serializedChildren.Add(JsonSerializer.Deserialize<NuiElement>(json)!);
                }
                else
                {
                    serializedChildren.Add(child);
                }
            }

            return serializedChildren;
        }
    }
}
