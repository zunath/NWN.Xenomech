using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace XM.Shared.Core.Json
{
    public class FloatJsonConverter : JsonConverter<float>
    {
        public override float Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType != JsonTokenType.Number)
            {
                throw new JsonException("Expected a number.");
            }
            return reader.GetSingle();
        }

        public override void Write(Utf8JsonWriter writer, float value, JsonSerializerOptions options)
        {
            writer.WriteRawValue(value.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture));
        }
    }
}