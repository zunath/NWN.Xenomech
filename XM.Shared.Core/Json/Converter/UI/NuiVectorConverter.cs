using Anvil.API;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace XM.Shared.Core.Json.Converter.UI
{
    public class NuiVectorConverter : JsonConverter<NuiVector>
    {
        public override NuiVector Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType != JsonTokenType.StartObject)
            {
                throw new JsonException("Expected StartObject token.");
            }

            float x = 0, y = 0;

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
                    case "x":
                        x = reader.GetSingle();
                        break;
                    case "y":
                        y = reader.GetSingle();
                        break;
                    default:
                        // Skip unrecognized properties
                        reader.Skip();
                        break;
                }
            }

            return new NuiVector(x, y);
        }

        public override void Write(Utf8JsonWriter writer, NuiVector value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();

            writer.WriteNumber("x", value.X);
            writer.WriteNumber("y", value.Y);

            writer.WriteEndObject();
        }
    }

}
