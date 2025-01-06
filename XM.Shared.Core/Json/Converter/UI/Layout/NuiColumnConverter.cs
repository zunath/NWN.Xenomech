using Anvil.API;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace XM.Shared.Core.Json.Converter.UI.Layout
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
            writer.WriteString("type", value.Type);

            writer.WritePropertyName("children");
            JsonSerializer.Serialize(writer, value.Children, options);

            writer.WriteEndObject();
        }
    }

}