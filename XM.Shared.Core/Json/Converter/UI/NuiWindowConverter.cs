using Anvil.API;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace XM.Shared.Core.Json.Converter.UI
{
    public class NuiWindowConverter : JsonConverter<NuiWindow>
    {
        public override NuiWindow Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType != JsonTokenType.StartObject)
            {
                throw new JsonException("Expected StartObject token.");
            }

            NuiLayout root = null;
            NuiProperty<string> title = null;
            NuiProperty<bool> border = null;
            NuiProperty<bool> closable = null;
            NuiProperty<bool> collapsed = null;
            NuiProperty<NuiRect> geometry = null;
            string id = null;
            NuiProperty<bool> resizable = null;
            NuiProperty<bool> transparent = null;
            var version = 1;
            NuiProperty<bool> acceptsInput = null;

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

                var propertyName = reader.GetString()!;

                reader.Read();

                switch (propertyName)
                {
                    case "root":
                        root = JsonSerializer.Deserialize<NuiLayout>(ref reader, options);
                        break;
                    case "title":
                        title = JsonSerializer.Deserialize<NuiProperty<string>>(ref reader, options);
                        break;
                    case "border":
                        border = JsonSerializer.Deserialize<NuiProperty<bool>>(ref reader, options);
                        break;
                    case "closable":
                        closable = JsonSerializer.Deserialize<NuiProperty<bool>>(ref reader, options);
                        break;
                    case "collapsed":
                        collapsed = JsonSerializer.Deserialize<NuiProperty<bool>>(ref reader, options);
                        break;
                    case "geometry":
                        geometry = JsonSerializer.Deserialize<NuiProperty<NuiRect>>(ref reader, options);
                        break;
                    case "id":
                        id = reader.GetString();
                        break;
                    case "resizable":
                        resizable = JsonSerializer.Deserialize<NuiProperty<bool>>(ref reader, options);
                        break;
                    case "transparent":
                        transparent = JsonSerializer.Deserialize<NuiProperty<bool>>(ref reader, options);
                        break;
                    case "version":
                        version = reader.GetInt32();
                        break;
                    case "accepts_input":
                        acceptsInput = JsonSerializer.Deserialize<NuiProperty<bool>>(ref reader, options);
                        break;
                    default:
                        reader.Skip();
                        break;
                }
            }

            if (root == null || title == null)
            {
                throw new JsonException("Missing required properties 'root' or 'title'.");
            }

            return new NuiWindow(root, title)
            {
                Border = border,
                Closable = closable,
                Collapsed = collapsed,
                Geometry = geometry,
                Id = id,
                Resizable = resizable,
                Transparent = transparent,
                AcceptsInput = acceptsInput
            };
        }

        public override void Write(Utf8JsonWriter writer, NuiWindow value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();

            writer.WritePropertyName("root");
            JsonSerializer.Serialize(writer, value.Root, options);

            writer.WritePropertyName("title");
            JsonSerializer.Serialize(writer, value.Title, options);

            writer.WritePropertyName("border");
            JsonSerializer.Serialize(writer, value.Border, options);

            writer.WritePropertyName("closable");
            JsonSerializer.Serialize(writer, value.Closable, options);

            if (value.Collapsed != null)
            {
                writer.WritePropertyName("collapsed");
                JsonSerializer.Serialize(writer, value.Collapsed, options);
            }

            writer.WritePropertyName("geometry");
            JsonSerializer.Serialize(writer, value.Geometry, options);

            if (!string.IsNullOrEmpty(value.Id))
            {
                writer.WriteString("id", value.Id);
            }

            writer.WritePropertyName("resizable");
            JsonSerializer.Serialize(writer, value.Resizable, options);

            writer.WritePropertyName("transparent");
            JsonSerializer.Serialize(writer, value.Transparent, options);

            writer.WriteNumber("version", value.Version);

            writer.WritePropertyName("accepts_input");
            JsonSerializer.Serialize(writer, value.AcceptsInput, options);

            writer.WriteEndObject();
        }
    }

}
