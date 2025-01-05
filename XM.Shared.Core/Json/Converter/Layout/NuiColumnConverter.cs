using Anvil.API;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace XM.Shared.Core.Json.Converter.Layout
{
    public class NuiColumnConverter : JsonConverter<NuiColumn>
    {
        public override NuiColumn Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType != JsonTokenType.StartObject)
            {
                throw new JsonException("Expected StartObject token.");
            }

            var column = new NuiColumn();

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
                    case "aspect":
                        column.Aspect = reader.TokenType == JsonTokenType.Null
                            ? null
                            : reader.GetSingle();
                        break;

                    case "enabled":
                        column.Enabled = JsonSerializer.Deserialize<NuiProperty<bool>>(ref reader, options);
                        break;

                    case "foreground_color":
                        column.ForegroundColor = JsonSerializer.Deserialize<NuiProperty<Color>>(ref reader, options);
                        break;

                    case "height":
                        column.Height = reader.TokenType == JsonTokenType.Null
                            ? null
                            : reader.GetSingle();
                        break;

                    case "id":
                        column.Id = reader.TokenType == JsonTokenType.Null
                            ? null
                            : reader.GetString();
                        break;

                    case "margin":
                        column.Margin = reader.TokenType == JsonTokenType.Null
                            ? null
                            : reader.GetSingle();
                        break;

                    case "padding":
                        column.Padding = reader.TokenType == JsonTokenType.Null
                            ? null
                            : reader.GetSingle();
                        break;

                    case "tooltip":
                        column.Tooltip = JsonSerializer.Deserialize<NuiProperty<string>>(ref reader, options);
                        break;

                    case "type":
                        string? typeValue = reader.GetString();
                        if (typeValue != "col")
                        {
                            throw new JsonException($"Unexpected type value: {typeValue}");
                        }
                        break;

                    case "visible":
                        column.Visible = JsonSerializer.Deserialize<NuiProperty<bool>>(ref reader, options);
                        break;

                    case "width":
                        column.Width = reader.TokenType == JsonTokenType.Null
                            ? null
                            : reader.GetSingle();
                        break;

                    case "draw_list":
                        column.DrawList = JsonSerializer.Deserialize<List<NuiDrawListItem>>(ref reader, options);
                        break;

                    case "draw_list_scissor":
                        column.Scissor = JsonSerializer.Deserialize<NuiProperty<bool>>(ref reader, options);
                        break;

                    case "disabled_tooltip":
                        column.DisabledTooltip = JsonSerializer.Deserialize<NuiProperty<string>>(ref reader, options);
                        break;

                    case "encouraged":
                        column.Encouraged = JsonSerializer.Deserialize<NuiProperty<bool>>(ref reader, options);
                        break;

                    case "children":
                        column.Children = JsonSerializer.Deserialize<List<NuiElement>>(ref reader, options) ?? new List<NuiElement>();
                        break;

                    default:
                        reader.Skip();
                        break;
                }
            }

            return column;
        }

        public override void Write(Utf8JsonWriter writer, NuiColumn value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();

            // Write NuiElement properties
            if (value.Aspect.HasValue) writer.WriteNumber("aspect", value.Aspect.Value);
            if (value.Enabled != null) JsonSerializer.Serialize(writer, value.Enabled, options);
            if (value.ForegroundColor != null) JsonSerializer.Serialize(writer, value.ForegroundColor, options);
            if (value.Height.HasValue) writer.WriteNumber("height", value.Height.Value);
            if (!string.IsNullOrEmpty(value.Id)) writer.WriteString("id", value.Id);
            if (value.Margin.HasValue) writer.WriteNumber("margin", value.Margin.Value);
            if (value.Padding.HasValue) writer.WriteNumber("padding", value.Padding.Value);
            if (value.Tooltip != null) JsonSerializer.Serialize(writer, value.Tooltip, options);
            writer.WriteString("type", value.Type);
            if (value.Visible != null) JsonSerializer.Serialize(writer, value.Visible, options);
            if (value.Width.HasValue) writer.WriteNumber("width", value.Width.Value);
            if (value.DrawList != null && value.DrawList.Count > 0)
            {
                writer.WritePropertyName("draw_list");
                JsonSerializer.Serialize(writer, value.DrawList, options);
            }
            if (value.Scissor != null) JsonSerializer.Serialize(writer, value.Scissor, options);
            if (value.DisabledTooltip != null) JsonSerializer.Serialize(writer, value.DisabledTooltip, options);
            if (value.Encouraged != null) JsonSerializer.Serialize(writer, value.Encouraged, options);

            // Write NuiLayout-specific property
            writer.WritePropertyName("children");
            JsonSerializer.Serialize(writer, value.Children, options);

            writer.WriteEndObject();
        }
    }
}