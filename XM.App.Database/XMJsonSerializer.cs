using Newtonsoft.Json;
using NReJSON;
using StackExchange.Redis;

namespace XM.App.Database
{
    internal class XMJsonSerializer : ISerializerProxy
    {
        public TResult Deserialize<TResult>(RedisResult serializedValue)
        {
            return JsonConvert.DeserializeObject<TResult>(serializedValue.ToString());
        }

        public string Serialize<TObjectType>(TObjectType obj)
        {
            return JsonConvert.SerializeObject(obj);
        }
    }
}
