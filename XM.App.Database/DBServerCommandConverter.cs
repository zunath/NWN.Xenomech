//using Newtonsoft.Json;
//using Newtonsoft.Json.Linq;
//using StackExchange.Redis;
//using XM.Data.Shared;

//namespace XM.App.Database
//{
//    public class DBServerCommandConverter : JsonConverter
//    {
//        public override bool CanConvert(Type objectType)
//        {
//            // We only want to convert DBServerCommand objects.
//            return objectType == typeof(DBServerCommand);
//        }

//        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
//        {
//            // Parse the raw JSON into a JObject so we can manually read each property.
//            JObject jObj = JObject.Load(reader);

//            // Create the command to return.
//            var command = new DBServerCommand
//            {
//                CommandType = jObj["CommandType"]?.ToObject<DBServerCommandType>(serializer) ?? DBServerCommandType.Error,
//                EntityType = jObj["EntityType"]?.ToString(),
//                Key = jObj["Key"]?.ToString(),
//                EntitySingle = jObj["EntitySingle"]?.ToString(),
//                Message = jObj["Message"]?.ToString(),
//            };

//            // EntitiesList -> List<string>
//            if (jObj["EntitiesList"] is JArray entitiesListArray)
//            {
//                command.EntitiesList = entitiesListArray.ToObject<List<string>>(serializer);
//            }

//            // IndexedProperties -> List<IndexedProperty>
//            if (jObj["IndexedProperties"] is JArray indexedPropsArray)
//            {
//                command.IndexedProperties = indexedPropsArray.ToObject<List<IndexedProperty>>(serializer);
//            }

//            // IndexData -> Dictionary<string, RedisValue>
//            // We treat each value as a string in JSON. If you need numeric or other data,
//            // you can parse it accordingly.
//            if (jObj["IndexData"] is JObject jIndexData)
//            {
//                var dict = new Dictionary<string, RedisValue>();

//                foreach (var kvp in jIndexData)
//                {
//                    // Convert the JToken to a string, store in RedisValue
//                    dict[kvp.Key] = (RedisValue)kvp.Value?.ToString();
//                }

//                command.IndexData = dict;
//            }

//            // If DBQuery<IDBEntity> can be deserialized directly, do that
//            if (jObj["Query"] != null)
//            {
//                command.Query = jObj["Query"].ToObject<DBQuery<IDBEntity>>(serializer);
//            }

//            return command;
//        }

//        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
//        {
//            var command = (DBServerCommand)value;

//            writer.WriteStartObject();

//            // CommandType
//            writer.WritePropertyName(nameof(DBServerCommand.CommandType));
//            serializer.Serialize(writer, command.CommandType);

//            // EntityType
//            writer.WritePropertyName(nameof(DBServerCommand.EntityType));
//            writer.WriteValue(command.EntityType);

//            // Key
//            writer.WritePropertyName(nameof(DBServerCommand.Key));
//            writer.WriteValue(command.Key);

//            // EntitySingle
//            writer.WritePropertyName(nameof(DBServerCommand.EntitySingle));
//            writer.WriteValue(command.EntitySingle);

//            // EntitiesList
//            writer.WritePropertyName(nameof(DBServerCommand.EntitiesList));
//            if (command.EntitiesList != null)
//            {
//                serializer.Serialize(writer, command.EntitiesList);
//            }
//            else
//            {
//                writer.WriteNull();
//            }

//            // Message
//            writer.WritePropertyName(nameof(DBServerCommand.Message));
//            writer.WriteValue(command.Message);

//            // IndexedProperties
//            writer.WritePropertyName(nameof(DBServerCommand.IndexedProperties));
//            if (command.IndexedProperties != null)
//            {
//                serializer.Serialize(writer, command.IndexedProperties);
//            }
//            else
//            {
//                writer.WriteNull();
//            }

//            // IndexData
//            writer.WritePropertyName(nameof(DBServerCommand.IndexData));
//            if (command.IndexData != null)
//            {
//                writer.WriteStartObject();
//                foreach (var kvp in command.IndexData)
//                {
//                    // Key is a string, value is a RedisValue -> convert to string
//                    writer.WritePropertyName(kvp.Key);
//                    writer.WriteValue(kvp.Value.ToString());
//                }
//                writer.WriteEndObject();
//            }
//            else
//            {
//                writer.WriteNull();
//            }

//            // Query
//            writer.WritePropertyName(nameof(DBServerCommand.Query));
//            serializer.Serialize(writer, command.Query);

//            writer.WriteEndObject();
//        }
//    }
//}
