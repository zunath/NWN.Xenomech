using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace XM.Shared.Core.Json
{
    public class XMBindingListConverter<T> : JsonConverter<XMBindingList<T>>
    {
        private readonly JsonConverter<T> _itemConverter;

        public XMBindingListConverter(JsonSerializerOptions options)
        {
            _itemConverter = (JsonConverter<T>)options.GetConverter(typeof(T));
        }

        public override XMBindingList<T> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType != JsonTokenType.StartArray)
                throw new JsonException();

            var list = new XMBindingList<T>();
            while (reader.Read())
            {
                if (reader.TokenType == JsonTokenType.EndArray)
                    break;

                var item = _itemConverter.Read(ref reader, typeof(T), options);
                list.Add(item);
            }

            return list;
        }

        public override void Write(Utf8JsonWriter writer, XMBindingList<T> value, JsonSerializerOptions options)
        {
            writer.WriteStartArray();

            foreach (var item in value)
            {
                _itemConverter.Write(writer, item, options);
            }

            writer.WriteEndArray();
        }
    }
}
