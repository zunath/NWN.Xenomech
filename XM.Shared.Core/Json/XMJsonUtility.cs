using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Anvil.API;

namespace XM.Shared.Core.Json
{
    public static class XMJsonUtility
    {
        private static readonly JsonSerializerOptions _options;

        static XMJsonUtility()
        {
            _options = new JsonSerializerOptions()
            {
                NumberHandling = JsonNumberHandling.AllowNamedFloatingPointLiterals, 
                WriteIndented = true
            };

            _options.Converters.Add(new NuiRectJsonConverter());
            _options.Converters.Add(new ColorJsonConverter());
            _options.Converters.Add(new FloatJsonConverter());
            _options.Converters.Add(new TypeConverter());
            _options.Converters.Add(new NuiComboEntryConverter());
            _options.Converters.Add(new XMBindingListConverter<NuiComboEntry>(new JsonSerializerOptions()
            {
                Converters = { new NuiComboEntryConverter() }
            }));
        }

        /// <summary>
        /// Deserializes a JSON string.
        /// </summary>
        /// <param name="json">The JSON to deserialize.</param>
        /// <typeparam name="T">The type to deserialize to.</typeparam>
        /// <returns>The deserialized object.</returns>
        public static T Deserialize<T>(string json)
        {
            return JsonSerializer.Deserialize<T>(json, _options);
        }

        /// <summary>
        /// Serializes a value as JSON.
        /// </summary>
        /// <param name="value">The value to serialize.</param>
        /// <typeparam name="T">The type of the value to serialize.</typeparam>
        /// <returns>A JSON string representing the value.</returns>
        public static string Serialize<T>(T value)
        {
            return JsonSerializer.Serialize(value, _options); 
        }

        public static object DeserializeObject(string json, Type type)
        {
            return JsonSerializer.Deserialize(json, type, _options);
        }

    }
}
