using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Anvil.API;

namespace XM.Shared.Core.Json
{
    public class NuiRectJsonConverter : JsonConverter<NuiRect>
    {
        public override NuiRect Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType != JsonTokenType.StartObject)
            {
                throw new JsonException("Expected StartObject token");
            }

            float x = 0, y = 0, width = 0, height = 0;

            while (reader.Read())
            {
                if (reader.TokenType == JsonTokenType.EndObject)
                {
                    return new NuiRect(x, y, width, height);
                }

                if (reader.TokenType != JsonTokenType.PropertyName)
                {
                    throw new JsonException("Expected PropertyName token");
                }

                var propertyName = reader.GetString();

                reader.Read(); // Move to the value token

                switch (propertyName)
                {
                    case "x":
                        x = reader.GetSingle();
                        break;
                    case "y":
                        y = reader.GetSingle();
                        break;
                    case "w":
                        width = reader.GetSingle();
                        break;
                    case "h":
                        height = reader.GetSingle();
                        break;
                    default:
                        throw new JsonException($"Unexpected property: {propertyName}");
                }
            }

            throw new JsonException("Expected EndObject token");
        }

        public override void Write(Utf8JsonWriter writer, NuiRect value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();
            writer.WriteNumber("x", value.X);
            writer.WriteNumber("y", value.Y);
            writer.WriteNumber("w", value.Width);
            writer.WriteNumber("h", value.Height);
            writer.WriteEndObject();
        }
    }
}
