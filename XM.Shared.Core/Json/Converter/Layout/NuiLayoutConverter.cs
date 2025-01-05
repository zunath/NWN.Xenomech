
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;
using Anvil.API;

namespace XM.Shared.Core.Json.Converter.Layout
{
    public class NuiLayoutConverter : JsonConverter<NuiLayout>
    {
        public override NuiLayout? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            throw new NotSupportedException("Deserialization of NuiLayout is not supported because it's an abstract class.");
        }

        public override void Write(Utf8JsonWriter writer, NuiLayout value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();

            writer.WriteString("type", value.Type);

            // Use reflection to access the protected SerializedChildren property
            PropertyInfo? serializedChildrenProperty = value.GetType()
                .GetProperty("SerializedChildren", BindingFlags.NonPublic | BindingFlags.Instance);

            if (serializedChildrenProperty == null)
            {
                throw new JsonException("The 'SerializedChildren' property is not accessible.");
            }

            var serializedChildren = serializedChildrenProperty.GetValue(value) as IEnumerable<NuiElement>;

            writer.WritePropertyName("children");
            JsonSerializer.Serialize(writer, serializedChildren, options);

            writer.WriteEndObject();
        }
    }


}
