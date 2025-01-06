using Anvil.API;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace XM.Shared.Core.Json.Converter.UI.Widget
{
    public class NuiOptionsConverter : JsonConverter<NuiOptions>
    {
        public override NuiOptions Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType != JsonTokenType.StartObject)
            {
                throw new JsonException("Expected StartObject token.");
            }

            NuiDirection? direction = null;
            List<string> optionsList = null;
            NuiProperty<int> selection = null;

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
                        optionsList = JsonSerializer.Deserialize<List<string>>(ref reader, options);
                        break;
                    case "value":
                        selection = JsonSerializer.Deserialize<NuiProperty<int>>(ref reader, options);
                        break;
                    default:
                        reader.Skip();
                        break;
                }
            }

            return new NuiOptions
            {
                Direction = direction ?? NuiDirection.Horizontal,
                Options = optionsList ?? new List<string>(),
                Selection = selection ?? -1
            };
        }

        public override void Write(Utf8JsonWriter writer, NuiOptions value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();

            writer.WriteString("type", value.Type);

            writer.WritePropertyName("direction");
            JsonSerializer.Serialize(writer, value.Direction, options);

            writer.WritePropertyName("elements");
            JsonSerializer.Serialize(writer, value.Options, options);

            writer.WritePropertyName("value");
            JsonSerializer.Serialize(writer, value.Selection, options);

            writer.WriteEndObject();
        }
    }

}
