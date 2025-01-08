using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Anvil.API;

namespace XM.Shared.Core.Json
{
    public class ColorJsonConverter : JsonConverter<Color>
    {
        public override Color Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType != JsonTokenType.StartObject)
            {
                throw new JsonException("Expected StartObject token");
            }

            byte r = 0, g = 0, b = 0, a = 255;

            while (reader.Read())
            {
                if (reader.TokenType == JsonTokenType.EndObject)
                {
                    return new Color(r, g, b, a);
                }

                if (reader.TokenType != JsonTokenType.PropertyName)
                {
                    throw new JsonException("Expected PropertyName token");
                }

                var propertyName = reader.GetString();
                reader.Read();

                switch (propertyName)
                {
                    case "r":
                        r = reader.GetByte();
                        break;
                    case "g":
                        g = reader.GetByte();
                        break;
                    case "b":
                        b = reader.GetByte();
                        break;
                    case "a":
                        a = reader.GetByte();
                        break;
                    default:
                        throw new JsonException($"Unknown property: {propertyName}");
                }
            }

            throw new JsonException("Expected EndObject token");
        }

        public override void Write(Utf8JsonWriter writer, Color value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();
            writer.WriteNumber("r", value.Red);
            writer.WriteNumber("g", value.Green);
            writer.WriteNumber("b", value.Blue);
            writer.WriteNumber("a", value.Alpha);
            writer.WriteEndObject();
        }
    }
}
