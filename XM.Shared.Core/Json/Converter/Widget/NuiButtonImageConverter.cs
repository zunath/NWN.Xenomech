using Anvil.API;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace XM.Shared.Core.Json.Converter.Widget
{
    public class NuiButtonImageConverter : JsonConverter<NuiButtonImage>
    {
        public override NuiButtonImage Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType != JsonTokenType.StartObject)
            {
                throw new JsonException("Expected StartObject token.");
            }

            NuiProperty<string> resRef = null;

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
                        resRef = JsonSerializer.Deserialize<NuiProperty<string>>(ref reader, options);
                        break;
                    default:
                        // Skip unrecognized properties
                        reader.Skip();
                        break;
                }
            }

            if (resRef == null)
            {
                throw new JsonException("Missing required property 'label'.");
            }

            return new NuiButtonImage(resRef);
        }

        public override void Write(Utf8JsonWriter writer, NuiButtonImage value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();

            writer.WriteString("type", value.Type);

            writer.WritePropertyName("label");
            JsonSerializer.Serialize(writer, value.ResRef, options);

            writer.WriteEndObject();
        }
    }
}