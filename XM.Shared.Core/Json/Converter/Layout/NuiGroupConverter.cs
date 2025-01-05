using Anvil.API;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace XM.Shared.Core.Json.Converter.Layout
{
    public class NuiGroupConverter : JsonConverter<NuiGroup>
    {
        public override NuiGroup Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType != JsonTokenType.StartObject)
            {
                throw new JsonException("Expected StartObject token.");
            }

            var group = new NuiGroup();

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
                    case "border":
                        group.Border = reader.GetBoolean();
                        break;
                    case "scrollbars":
                        group.Scrollbars = JsonSerializer.Deserialize<NuiScrollbars>(ref reader, options);
                        break;
                    case "children":
                        var children = JsonSerializer.Deserialize<List<NuiElement>>(ref reader, options);
                        group.Element = children?.Count > 0 ? children[0] : null;
                        break;
                    default:
                        // Skip unrecognized properties
                        reader.Skip();
                        break;
                }
            }

            return group;
        }

        public override void Write(Utf8JsonWriter writer, NuiGroup value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();

            writer.WriteString("type", value.Type);
            writer.WriteBoolean("border", value.Border);
            writer.WritePropertyName("scrollbars");
            JsonSerializer.Serialize(writer, value.Scrollbars, options);

            writer.WritePropertyName("children");
            if (value.Element != null)
            {
                writer.WriteStartArray();
                JsonSerializer.Serialize(writer, value.Element, options);
                writer.WriteEndArray();
            }
            else
            {
                writer.WriteStartArray();
                writer.WriteEndArray();
            }

            writer.WriteEndObject();
        }
    }

}
