using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Anvil.API;

namespace XM.Shared.Core.Json.Converter
{
    public class ColorConverter : JsonConverter<Color>
    {
        public override Color Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType != JsonTokenType.StartObject)
            {
                throw new JsonException("Expected StartObject token.");
            }

            byte red = 0, green = 0, blue = 0, alpha = 255;

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
                    case "r":
                        red = reader.GetByte();
                        break;
                    case "g":
                        green = reader.GetByte();
                        break;
                    case "b":
                        blue = reader.GetByte();
                        break;
                    case "a":
                        alpha = reader.GetByte();
                        break;
                    default:
                        throw new JsonException($"Unexpected property: {propertyName}");
                }
            }

            return new Color(red, green, blue, alpha);
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
