using Anvil.API;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace XM.Shared.Core.Json.Converter.UI.Widget
{
    public class NuiTextEditConverter : JsonConverter<NuiTextEdit>
    {
        public override NuiTextEdit Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType != JsonTokenType.StartObject)
            {
                throw new JsonException("Expected StartObject token.");
            }

            NuiProperty<string> label = null;
            NuiProperty<string> value = null;
            ushort? maxLength = null;
            bool? multiLine = null;
            NuiProperty<bool> wordWrap = null;

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
                    case "label":
                        label = JsonSerializer.Deserialize<NuiProperty<string>>(ref reader, options);
                        break;
                    case "value":
                        value = JsonSerializer.Deserialize<NuiProperty<string>>(ref reader, options);
                        break;
                    case "max":
                        maxLength = reader.GetUInt16();
                        break;
                    case "multiline":
                        multiLine = reader.GetBoolean();
                        break;
                    case "wordwrap":
                        wordWrap = JsonSerializer.Deserialize<NuiProperty<bool>>(ref reader, options);
                        break;
                    default:
                        reader.Skip();
                        break;
                }
            }

            if (label == null || value == null || maxLength == null || multiLine == null)
            {
                throw new JsonException("Missing required properties.");
            }

            return new NuiTextEdit(label, value, maxLength.Value, multiLine.Value)
            {
                WordWrap = wordWrap ?? true
            };
        }

        public override void Write(Utf8JsonWriter writer, NuiTextEdit value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();

            writer.WriteString("type", value.Type);

            writer.WritePropertyName("label");
            JsonSerializer.Serialize(writer, value.Label, options);

            writer.WritePropertyName("value");
            JsonSerializer.Serialize(writer, value.Value, options);

            writer.WriteNumber("max", value.MaxLength);

            writer.WriteBoolean("multiline", value.MultiLine);

            writer.WritePropertyName("wordwrap");
            JsonSerializer.Serialize(writer, value.WordWrap, options);

            writer.WriteEndObject();
        }
    }

}
