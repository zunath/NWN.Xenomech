using Anvil.API;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace XM.Shared.Core.Json.Converter
{
    public class NuiElementConverter : JsonConverter<NuiElement>
    {
        public override NuiElement Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            throw new NotImplementedException("Deserialization is not required for this scenario.");
        }

        public override void Write(Utf8JsonWriter writer, NuiElement value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();

            // Write aspect ratio
            if (value.Aspect.HasValue)
            {
                writer.WriteNumber("aspect", value.Aspect.Value);
            }

            // Write enabled property
            if (value.Enabled != null)
            {
                writer.WritePropertyName("enabled");
                JsonSerializer.Serialize(writer, value.Enabled, options);
            }

            // Write foreground color
            if (value.ForegroundColor != null)
            {
                writer.WritePropertyName("foreground_color");
                JsonSerializer.Serialize(writer, value.ForegroundColor, options);
            }

            // Write height
            if (value.Height.HasValue)
            {
                writer.WriteNumber("height", value.Height.Value);
            }

            // Write ID
            if (!string.IsNullOrEmpty(value.Id))
            {
                writer.WriteString("id", value.Id);
            }

            // Write margin
            if (value.Margin.HasValue)
            {
                writer.WriteNumber("margin", value.Margin.Value);
            }

            // Write padding
            if (value.Padding.HasValue)
            {
                writer.WriteNumber("padding", value.Padding.Value);
            }

            // Write tooltip
            if (value.Tooltip != null)
            {
                writer.WritePropertyName("tooltip");
                JsonSerializer.Serialize(writer, value.Tooltip, options);
            }

            // Write type
            writer.WriteString("type", value.Type);

            // Write visible property
            if (value.Visible != null)
            {
                writer.WritePropertyName("visible");
                JsonSerializer.Serialize(writer, value.Visible, options);
            }

            // Write width
            if (value.Width.HasValue)
            {
                writer.WriteNumber("width", value.Width.Value);
            }

            // Write draw list
            if (value.DrawList != null && value.DrawList.Count > 0)
            {
                writer.WritePropertyName("draw_list");
                JsonSerializer.Serialize(writer, value.DrawList, options);
            }

            // Write scissor property
            if (value.Scissor != null)
            {
                writer.WritePropertyName("draw_list_scissor");
                JsonSerializer.Serialize(writer, value.Scissor, options);
            }

            // Write disabled tooltip
            if (value.DisabledTooltip != null)
            {
                writer.WritePropertyName("disabled_tooltip");
                JsonSerializer.Serialize(writer, value.DisabledTooltip, options);
            }

            // Write encouraged property
            if (value.Encouraged != null)
            {
                writer.WritePropertyName("encouraged");
                JsonSerializer.Serialize(writer, value.Encouraged, options);
            }

            writer.WriteEndObject();
        }
    }
}
