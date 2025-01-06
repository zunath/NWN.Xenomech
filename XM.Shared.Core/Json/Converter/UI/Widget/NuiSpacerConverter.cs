using Anvil.API;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace XM.Shared.Core.Json.Converter.UI.Widget
{
    public class NuiSpacerConverter : JsonConverter<NuiSpacer>
    {
        public override NuiSpacer Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType != JsonTokenType.StartObject)
            {
                throw new JsonException("Expected StartObject token.");
            }

            while (reader.Read())
            {
                if (reader.TokenType == JsonTokenType.EndObject)
                {
                    break;
                }

                // Skip any unrecognized properties
                if (reader.TokenType == JsonTokenType.PropertyName)
                {
                    reader.Skip();
                }
            }

            return new NuiSpacer();
        }

        public override void Write(Utf8JsonWriter writer, NuiSpacer value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();

            writer.WriteString("type", value.Type);

            writer.WriteEndObject();
        }
    }

}
