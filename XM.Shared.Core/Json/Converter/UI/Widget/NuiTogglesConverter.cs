using Anvil.API;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace XM.Shared.Core.Json.Converter.UI.Widget
{
    public class NuiTogglesConverter : JsonConverter<NuiToggles>
    {
        public override NuiToggles Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType != JsonTokenType.StartObject)
            {
                throw new JsonException("Expected StartObject token.");
            }

            NuiDirection? direction = null;
            List<string> elements = null;

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
                    case "direction":
                        direction = JsonSerializer.Deserialize<NuiDirection>(ref reader, options);
                        break;
                    case "elements":
                        elements = JsonSerializer.Deserialize<List<string>>(ref reader, options);
                        break;
                    default:
                        reader.Skip();
                        break;
                }
            }

            if (direction == null || elements == null)
            {
                throw new JsonException("Missing required properties 'direction' or 'elements'.");
            }

            return new NuiToggles(direction.Value, elements);
        }

        public override void Write(Utf8JsonWriter writer, NuiToggles value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();

            writer.WriteString("type", value.Type);

            writer.WritePropertyName("direction");
            JsonSerializer.Serialize(writer, value.Direction, options);

            writer.WritePropertyName("elements");
            JsonSerializer.Serialize(writer, value.Elements, options);

            writer.WriteEndObject();
        }
    }

}
