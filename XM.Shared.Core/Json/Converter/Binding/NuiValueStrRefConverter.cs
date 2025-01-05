using Anvil.API;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace XM.Shared.Core.Json.Converter.Binding
{
    public class NuiValueStrRefConverter : JsonConverter<NuiValueStrRef>
    {
        public override NuiValueStrRef Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.Null)
            {
                return null;
            }

            StrRef? value = JsonSerializer.Deserialize<StrRef>(ref reader, options);
            return new NuiValueStrRef(value);
        }

        public override void Write(Utf8JsonWriter writer, NuiValueStrRef value, JsonSerializerOptions options)
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
