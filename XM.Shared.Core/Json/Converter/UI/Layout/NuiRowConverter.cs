using Anvil.API;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace XM.Shared.Core.Json.Converter.UI.Layout
{
    public class NuiRowConverter : JsonConverter<NuiRow>
    {
        public override NuiRow Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType != JsonTokenType.StartObject)
            {
                throw new JsonException("Expected StartObject token.");
            }

            using var jsonDoc = JsonDocument.ParseValue(ref reader);
            var root = jsonDoc.RootElement;

            var nuiRow = new NuiRow();

            foreach (var property in root.EnumerateObject())
            {
                switch (property.Name)
                {
                    case "type":
                        if (property.Value.GetString() != "row")
                        {
                            throw new JsonException("Invalid type for NuiRow.");
                        }
                        break;
                    case "children":
                        nuiRow.Children = JsonSerializer.Deserialize<List<NuiElement>>(property.Value.GetRawText(), options) ?? new List<NuiElement>();
                        break;
                    case "aspect":
                        nuiRow.Aspect = property.Value.GetSingle();
                        break;
                    case "enabled":
                        nuiRow.Enabled = JsonSerializer.Deserialize<NuiProperty<bool>>(property.Value.GetRawText(), options);
                        break;
                    case "foreground_color":
                        nuiRow.ForegroundColor = JsonSerializer.Deserialize<NuiProperty<Color>>(property.Value.GetRawText(), options);
                        break;
                    case "height":
                        nuiRow.Height = property.Value.GetSingle();
                        break;
                    case "id":
                        nuiRow.Id = property.Value.GetString();
                        break;
                    case "margin":
                        nuiRow.Margin = property.Value.GetSingle();
                        break;
                    case "padding":
                        nuiRow.Padding = property.Value.GetSingle();
                        break;
                    case "tooltip":
                        nuiRow.Tooltip = JsonSerializer.Deserialize<NuiProperty<string>>(property.Value.GetRawText(), options);
                        break;
                    case "visible":
                        nuiRow.Visible = JsonSerializer.Deserialize<NuiProperty<bool>>(property.Value.GetRawText(), options);
                        break;
                    case "width":
                        nuiRow.Width = property.Value.GetSingle();
                        break;
                    case "draw_list":
                        nuiRow.DrawList = JsonSerializer.Deserialize<List<NuiDrawListItem>>(property.Value.GetRawText(), options);
                        break;
                    case "draw_list_scissor":
                        nuiRow.Scissor = JsonSerializer.Deserialize<NuiProperty<bool>>(property.Value.GetRawText(), options);
                        break;
                    case "disabled_tooltip":
                        nuiRow.DisabledTooltip = JsonSerializer.Deserialize<NuiProperty<string>>(property.Value.GetRawText(), options);
                        break;
                    case "encouraged":
                        nuiRow.Encouraged = JsonSerializer.Deserialize<NuiProperty<bool>>(property.Value.GetRawText(), options);
                        break;
                }
            }

            return nuiRow;
        }

        public override void Write(Utf8JsonWriter writer, NuiRow value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();

            writer.WriteString("type", value.Type);

            // Serialize common properties from NuiElement
            if (value.Aspect.HasValue)
                writer.WriteNumber("aspect", value.Aspect.Value);
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
            if (value.Height.HasValue)
                writer.WriteNumber("height", value.Height.Value);
            if (!string.IsNullOrEmpty(value.Id))
                writer.WriteString("id", value.Id);
            if (value.Margin.HasValue)
                writer.WriteNumber("margin", value.Margin.Value);
            if (value.Padding.HasValue)
                writer.WriteNumber("padding", value.Padding.Value);
            if (value.Tooltip != null)
            {
                writer.WritePropertyName("tooltip");
                JsonSerializer.Serialize(writer, value.Tooltip, options);
            }
            if (value.Visible != null)
            {
                writer.WritePropertyName("visible");
                JsonSerializer.Serialize(writer, value.Visible, options);
            }
            if (value.Width.HasValue)
                writer.WriteNumber("width", value.Width.Value);
            if (value.DrawList != null && value.DrawList.Count > 0)
            {
                writer.WritePropertyName("draw_list");
                JsonSerializer.Serialize(writer, value.DrawList, options);
            }
            else
            {
                writer.WriteStartArray("draw_list");
                JsonSerializer.Serialize(writer, new List<object>(), options);
                writer.WriteEndArray();
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

            // Serialize children
            writer.WritePropertyName("children");
            JsonSerializer.Serialize(writer, value.Children, options);

            writer.WriteEndObject();
        }
    }


}
