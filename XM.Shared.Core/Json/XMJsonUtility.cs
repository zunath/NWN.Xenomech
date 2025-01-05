using System;
using System.Text.Json;

namespace XM.Shared.Core.Json
{
    public static class XMJsonUtility
    {
        /// <summary>
        /// Deserializes a JSON string.
        /// </summary>
        /// <param name="json">The JSON to deserialize.</param>
        /// <typeparam name="T">The type to deserialize to.</typeparam>
        /// <returns>The deserialized object.</returns>
        public static T Deserialize<T>(string json)
        {
            return JsonSerializer.Deserialize<T>(json);
        }

        /// <summary>
        /// Serializes a value as JSON.
        /// </summary>
        /// <param name="value">The value to serialize.</param>
        /// <typeparam name="T">The type of the value to serialize.</typeparam>
        /// <returns>A JSON string representing the value.</returns>
        public static string Serialize<T>(T value)
        {
            return JsonSerializer.Serialize(value); 
        }

        public static object DeserializeObject(string json, Type type)
        {
            return JsonSerializer.Deserialize(json, type);
        }

    }
}
