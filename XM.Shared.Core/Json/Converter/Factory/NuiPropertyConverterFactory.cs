using Anvil.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Threading.Tasks;
using XM.Shared.Core.Json.Converter.Binding;

namespace XM.Shared.Core.Json.Converter.Factory
{
    public class NuiPropertyConverterFactory : JsonConverterFactory
    {
        public override bool CanConvert(Type typeToConvert)
        {
            // Check if the type is NuiProperty<T>
            return typeToConvert.IsGenericType && typeToConvert.GetGenericTypeDefinition() == typeof(NuiProperty<>);
        }

        public override JsonConverter? CreateConverter(Type typeToConvert, JsonSerializerOptions options)
        {
            // Get the type argument of NuiProperty<T>
            Type genericArgument = typeToConvert.GetGenericArguments()[0];

            // Create an instance of NuiPropertyConverter<T> for the specific T
            Type converterType = typeof(NuiPropertyConverter<>).MakeGenericType(genericArgument);

            return (JsonConverter?)Activator.CreateInstance(converterType);
        }
    }

}
