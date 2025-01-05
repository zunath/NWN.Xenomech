using Anvil.API;
using System;
using System.Text.Json.Serialization;
using System.Text.Json;
using XM.Shared.Core.Json.Converter.Binding;

namespace XM.Shared.Core.Json.Converter.Factory
{
    public class NuiBindConverterFactory : JsonConverterFactory
    {
        public override bool CanConvert(Type typeToConvert)
        {
            // Check if the type is NuiBind<T>
            return typeToConvert.IsGenericType && typeToConvert.GetGenericTypeDefinition() == typeof(NuiBind<>);
        }

        public override JsonConverter? CreateConverter(Type typeToConvert, JsonSerializerOptions options)
        {
            // Get the type argument of NuiBind<T>
            Type genericArgument = typeToConvert.GetGenericArguments()[0];

            // Create an instance of NuiBindConverter<T> for the specific T
            Type converterType = typeof(NuiBindConverter<>).MakeGenericType(genericArgument);

            return (JsonConverter?)Activator.CreateInstance(converterType);
        }
    }

}
