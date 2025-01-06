using Anvil.API;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace XM.Shared.Core.Json.Converter.UI.Widget
{
    public class NuiChartSlotConverter : JsonConverter<NuiChartSlot>
    {
        public override NuiChartSlot Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType != JsonTokenType.StartObject)
            {
                throw new JsonException("Expected StartObject token.");
            }

            NuiChartType? chartType = null;
            NuiProperty<string> legend = null;
            NuiProperty<Color> color = null;
            NuiProperty<List<float>> data = null;

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
                    case "type":
                        chartType = JsonSerializer.Deserialize<NuiChartType>(ref reader, options);
                        break;
                    case "legend":
                        legend = JsonSerializer.Deserialize<NuiProperty<string>>(ref reader, options);
                        break;
                    case "color":
                        color = JsonSerializer.Deserialize<NuiProperty<Color>>(ref reader, options);
                        break;
                    case "data":
                        data = JsonSerializer.Deserialize<NuiProperty<List<float>>>(ref reader, options);
                        break;
                    default:
                        reader.Skip();
                        break;
                }
            }

            if (chartType == null || legend == null || color == null || data == null)
            {
                throw new JsonException("Missing required properties in NuiChartSlot.");
            }

            return new NuiChartSlot(chartType.Value, legend, color, data);
        }

        public override void Write(Utf8JsonWriter writer, NuiChartSlot value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();

            writer.WritePropertyName("type");
            JsonSerializer.Serialize(writer, value.ChartType, options);

            writer.WritePropertyName("legend");
            JsonSerializer.Serialize(writer, value.Legend, options);

            writer.WritePropertyName("color");
            JsonSerializer.Serialize(writer, value.Color, options);

            writer.WritePropertyName("data");
            JsonSerializer.Serialize(writer, value.Data, options);

            writer.WriteEndObject();
        }
    }

}
