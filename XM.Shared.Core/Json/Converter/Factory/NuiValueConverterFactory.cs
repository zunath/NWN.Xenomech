using Anvil.API;
using System;
using System.Text.Json.Serialization;
using System.Text.Json;
using XM.Shared.Core.Json.Converter.Binding;

namespace XM.Shared.Core.Json.Converter.Factory
{
    public class NuiValueConverterFactory : JsonConverterFactory
    {
        public override bool CanConvert(Type typeToConvert)
        {
            // Check if the type is NuiValue<T>
            return typeToConvert.IsGenericType && typeToConvert.GetGenericTypeDefinition() == typeof(NuiValue<>);
        }

        public override JsonConverter? CreateConverter(Type typeToConvert, JsonSerializerOptions options)
        {
            // Get the type argument of NuiValue<T>
            Type genericArgument = typeToConvert.GetGenericArguments()[0];

            // Create an instance of NuiValueConverter<T> for the specific T
            Type converterType = typeof(NuiValueConverter<>).MakeGenericType(genericArgument);

            return (JsonConverter?)Activator.CreateInstance(converterType);
        }
    }

}
