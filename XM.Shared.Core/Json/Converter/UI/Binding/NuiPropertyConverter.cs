using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Anvil.API;

namespace XM.Shared.Core.Json.Converter.UI.Binding
{
    /// <summary>
    /// A custom JsonConverter for the abstract class NuiProperty<T>.
    /// </summary>
    /// <typeparam name="T">The underlying type of the property.</typeparam>
    public class NuiPropertyConverter<T> : JsonConverter<NuiProperty<T>>
    {
        public override NuiProperty<T>? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.Null)
            {
                return null; // Gracefully handle null values
            }

            if (reader.TokenType == JsonTokenType.StartObject)
            {
                using JsonDocument jsonDoc = JsonDocument.ParseValue(ref reader);
                var root = jsonDoc.RootElement;

                if (root.TryGetProperty("bind", out JsonElement bindElement))
                {
                    string key = bindElement.GetString() ?? throw new JsonException("Bind key cannot be null.");
                    return NuiProperty<T>.CreateBind(key);
                }
                else if (root.TryGetProperty("value", out JsonElement valueElement))
                {
                    T value = JsonSerializer.Deserialize<T>(valueElement.GetRawText(), options) ??
                              throw new JsonException("Value cannot be null.");
                    return NuiProperty<T>.CreateValue(value);
                }
            }
            else
            {
                T value = JsonSerializer.Deserialize<T>(ref reader, options) ??
                          throw new JsonException("Invalid value for NuiProperty<T>.");
                return NuiProperty<T>.CreateValue(value);
            }

            throw new JsonException("Invalid JSON structure for NuiProperty<T>.");
        }

        public override void Write(Utf8JsonWriter writer, NuiProperty<T> value, JsonSerializerOptions options)
        {
            if (value is NuiBind<T> bind)
            {
                writer.WriteStartObject();
                writer.WriteString("bind", bind.Key);
                writer.WriteEndObject();
            }
            else if (value is NuiValue<T> nuiValue)
            {
                JsonSerializer.Serialize(writer, nuiValue.Value, options);
            }
            else
            {
                throw new JsonException($"Unsupported NuiProperty<T> type: {value.GetType().FullName}");
            }
        }
    }
}
