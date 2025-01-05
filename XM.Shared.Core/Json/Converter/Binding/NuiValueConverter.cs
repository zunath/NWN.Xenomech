using Anvil.API;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace XM.Shared.Core.Json.Converter.Binding
{
    public class NuiValueConverter<T> : JsonConverter<NuiValue<T>>
    {
        public override NuiValue<T> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.Null)
            {
                return null;
            }

            T value = JsonSerializer.Deserialize<T>(ref reader, options);
            return new NuiValue<T>(value!);
        }

        public override void Write(Utf8JsonWriter writer, NuiValue<T> value, JsonSerializerOptions options)
        {
            if (value.Value != null)
            {
                JsonSerializer.Serialize(writer, value.Value, options);
            }
            else
            {
                writer.WriteNullValue();
            }
        }
    }
}
