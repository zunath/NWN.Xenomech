using NReJSON;
using StackExchange.Redis;

namespace XM.Shared.Core.Json
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