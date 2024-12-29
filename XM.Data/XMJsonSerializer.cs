using System.IO;
using Jil;
using NReJSON;
using StackExchange.Redis;

namespace XM.Data
{
    internal class XMJsonSerializer : ISerializerProxy
    {
        public TResult Deserialize<TResult>(RedisResult serializedValue)
        {
            using (var input = new StringReader(serializedValue.ToString()))
            {
                return JSON.Deserialize<TResult>(input);
            }
        }

        public string Serialize<TObjectType>(TObjectType obj)
        {
            using (var output = new StringWriter())
            {
                JSON.Serialize(obj, output);

                return output.ToString();
            }
        }
    }
}
