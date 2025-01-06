using Anvil.API;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace XM.Shared.Core.Json.Converter.UI.Widget
{
    public class NuiButtonConverter : JsonConverter<NuiButton>
    {
        public override NuiButton Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType != JsonTokenType.StartObject)
            {
                throw new JsonException("Expected StartObject token.");
            }

            NuiProperty<string> label = null;

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
                    default:
                        // Skip unrecognized properties
                        reader.Skip();
                        break;
                }
            }

            if (label == null)
            {
                throw new JsonException("Missing required property 'label'.");
            }

            return new NuiButton(label);
        }

        public override void Write(Utf8JsonWriter writer, NuiButton value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();

            writer.WriteString("type", value.Type);

            writer.WritePropertyName("label");
            JsonSerializer.Serialize(writer, value.Label, options);

            writer.WriteEndObject();
        }
    }

}
