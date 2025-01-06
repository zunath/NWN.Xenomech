using Anvil.API;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace XM.Shared.Core.Json.Converter.UI.Widget
{
    public class NuiTextConverter : JsonConverter<NuiText>
    {
        public override NuiText Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType != JsonTokenType.StartObject)
            {
                throw new JsonException("Expected StartObject token.");
            }

            NuiProperty<string> text = null;
            bool? border = null;
            NuiScrollbars? scrollbars = null;

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
                        text = JsonSerializer.Deserialize<NuiProperty<string>>(ref reader, options);
                        break;
                    case "border":
                        border = reader.GetBoolean();
                        break;
                    case "scrollbars":
                        scrollbars = JsonSerializer.Deserialize<NuiScrollbars>(ref reader, options);
                        break;
                    default:
                        reader.Skip();
                        break;
                }
            }

            if (text == null)
            {
                throw new JsonException("Missing required property 'value'.");
            }

            return new NuiText(text)
            {
                Border = border ?? true,
                Scrollbars = scrollbars ?? NuiScrollbars.Auto
            };
        }

        public override void Write(Utf8JsonWriter writer, NuiText value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();

            writer.WriteString("type", value.Type);

            writer.WritePropertyName("value");
            JsonSerializer.Serialize(writer, value.Text, options);

            writer.WriteBoolean("border", value.Border);

            writer.WritePropertyName("scrollbars");
            JsonSerializer.Serialize(writer, value.Scrollbars, options);

            writer.WriteEndObject();
        }
    }

}
