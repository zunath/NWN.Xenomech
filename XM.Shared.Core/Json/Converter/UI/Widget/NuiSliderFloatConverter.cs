using Anvil.API;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace XM.Shared.Core.Json.Converter.UI.Widget
{
    public class NuiSliderFloatConverter : JsonConverter<NuiSliderFloat>
    {
        public override NuiSliderFloat Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType != JsonTokenType.StartObject)
            {
                throw new JsonException("Expected StartObject token.");
            }

            NuiProperty<float> value = null;
            NuiProperty<float> min = null;
            NuiProperty<float> max = null;
            NuiProperty<float> stepSize = null;

            while (reader.Read())
            {
                if (reader.TokenType == JsonTokenType.EndObject)
                {
                    break;
                }

                if (reader.TokenType != JsonTokenType.PropertyName)
                {
                    throw new JsonException("Expected PropertyName token.");
                }

                string propertyName = reader.GetString()!;

                reader.Read();

                switch (propertyName)
                {
                    case "value":
                        value = JsonSerializer.Deserialize<NuiProperty<float>>(ref reader, options);
                        break;
                    case "min":
                        min = JsonSerializer.Deserialize<NuiProperty<float>>(ref reader, options);
                        break;
                    case "max":
                        max = JsonSerializer.Deserialize<NuiProperty<float>>(ref reader, options);
                        break;
                    case "step":
                        stepSize = JsonSerializer.Deserialize<NuiProperty<float>>(ref reader, options);
                        break;
                    default:
                        reader.Skip();
                        break;
                }
            }

            if (value == null || min == null || max == null)
            {
                throw new JsonException("Missing required properties 'value', 'min', or 'max'.");
            }

            return new NuiSliderFloat(value, min, max)
            {
                StepSize = stepSize ?? 0.01f
            };
        }

        public override void Write(Utf8JsonWriter writer, NuiSliderFloat value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();

            writer.WriteString("type", value.Type);

            writer.WritePropertyName("value");
            JsonSerializer.Serialize(writer, value.Value, options);

            writer.WritePropertyName("min");
            JsonSerializer.Serialize(writer, value.Min, options);

            writer.WritePropertyName("max");
            JsonSerializer.Serialize(writer, value.Max, options);

            writer.WritePropertyName("step");
            JsonSerializer.Serialize(writer, value.StepSize, options);

            writer.WriteEndObject();
        }
    }

}
