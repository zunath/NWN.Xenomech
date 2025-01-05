using NReJSON;
using StackExchange.Redis;
using XM.Shared.Core.Json;

namespace XM.App.Database
{
    internal class XMJsonSerializer : ISerializerProxy
    {
        public TResult Deserialize<TResult>(RedisResult serializedValue)
        {
            return XMJsonUtility.Deserialize<TResult>(serializedValue.ToString());
        }

        public string Serialize<TObjectType>(TObjectType obj)
        {
            return XMJsonUtility.Serialize(obj);
        }
    }
}
