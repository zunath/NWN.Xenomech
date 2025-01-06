using Anvil.API;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace XM.Shared.Core.Json.Converter.UI.Widget
{
    public class NuiSliderConverter : JsonConverter<NuiSlider>
    {
        public override NuiSlider Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType != JsonTokenType.StartObject)
            {
                throw new JsonException("Expected StartObject token.");
            }

            NuiProperty<int> value = null;
            NuiProperty<int> min = null;
            NuiProperty<int> max = null;
            NuiProperty<int> step = null;

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
                        value = JsonSerializer.Deserialize<NuiProperty<int>>(ref reader, options);
                        break;
                    case "min":
                        min = JsonSerializer.Deserialize<NuiProperty<int>>(ref reader, options);
                        break;
                    case "max":
                        max = JsonSerializer.Deserialize<NuiProperty<int>>(ref reader, options);
                        break;
                    case "step":
                        step = JsonSerializer.Deserialize<NuiProperty<int>>(ref reader, options);
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

            return new NuiSlider(value, min, max)
            {
                Step = step ?? 1
            };
        }

        public override void Write(Utf8JsonWriter writer, NuiSlider value, JsonSerializerOptions options)
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
            JsonSerializer.Serialize(writer, value.Step, options);

            writer.WriteEndObject();
        }
    }

}
