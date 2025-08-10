using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using XM.Shared.Core.Primitives;

namespace XM.Shared.Core.Json
{
    public class SmartEnumJsonConverter<TEnum> : JsonConverter<TEnum> where TEnum : SmartEnum<TEnum>
    {
        public override TEnum Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.String)
            {
                var name = reader.GetString() ?? string.Empty;
                return SmartEnum<TEnum>.FromName(name, ignoreCase: true);
            }
            if (reader.TokenType == JsonTokenType.Number)
            {
                var value = reader.GetInt32();
                return SmartEnum<TEnum>.FromValue(value);
            }
            throw new JsonException($"Unexpected token {reader.TokenType} for {typeof(TEnum).Name}");
        }

        public override void Write(Utf8JsonWriter writer, TEnum value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.Name);
        }
    }
}


