using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using Anvil.Services;
using NLog;
using XM.Shared.Core.Configuration;
using XM.Shared.Core.EventManagement;
using XM.Shared.Core.Json;

namespace XM.Shared.Core.Data
{
    [ServiceBinding(typeof(DBService))]
    [ServiceBinding(typeof(IInitializable))]
    public class DBService :
        IInitializable
    {
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();
        private readonly string _socketPath;

        private const string DBLoadedEventScript = "xm_db_loaded";
        private readonly XMSettingsService _settings;
        private readonly Dictionary<Type, List<IndexedProperty>> _indexedPropertiesByType = new();

        [Inject]
        public IList<IDBEntity> Entities { get; set; }

        private readonly XMEventService _event;

        public DBService(XMSettingsService settings, XMEventService @event)
        {
            _settings = settings;
            _event = @event;
            _socketPath = settings.DatabaseSocketPath;
        }

        public void Init()
        {
            LoadEntities();
            PublishDBLoadedEvent();
        }

        private void BuildIndexedProperties(Type type)
        {
            var indexedProperties = new List<IndexedProperty>();

            foreach (var prop in type.GetProperties())
            {
                var indexedProperty = new IndexedProperty();
                indexedProperty.Name = prop.Name;

                var attribute = prop.GetCustomAttribute(typeof(IndexedAttribute));
                if (attribute != null)
                {
                    if (prop.PropertyType == typeof(int) ||
                        prop.PropertyType == typeof(int?) ||
                        prop.PropertyType == typeof(ulong) ||
                        prop.PropertyType == typeof(ulong?) ||
                        prop.PropertyType == typeof(long) ||
                        prop.PropertyType == typeof(long?))
                    {
                        indexedProperty.Type = IndexedPropertyType.Numeric;
                    }
                    else if (prop.PropertyType == typeof(string))
                    {
                        indexedProperty.Type = IndexedPropertyType.Text;
                    }
                    else if (prop.PropertyType == typeof(Guid))
                    {
                        indexedProperty.Type = IndexedPropertyType.Guid;
                    }
                    else if (prop.PropertyType == typeof(Enum))
                    {
                        indexedProperty.Type = IndexedPropertyType.Enum;
                    }

                    indexedProperties.Add(indexedProperty);
                }
            }

            // Cache the indexed properties for quick look-up later.
            _indexedPropertiesByType[type] = indexedProperties;
        }

        private void LoadEntities()
        {
            foreach (var entity in Entities)
            {
                var type = entity.GetType();
                BuildIndexedProperties(type);

                var command = new DBServerCommand
                {
                    CommandType = DBServerCommandType.Register,
                    EntityType = type.Name,
                    IndexedProperties = _indexedPropertiesByType[type]
                };

                //Console.WriteLine($"Sending command: {XMJsonUtility.Serialize(command)}");

                var response = SendCommand(command);
                if (response.CommandType != DBServerCommandType.Ok)
                {
                    throw new Exception($"Failed to register entity: {response.Message}");
                }

                _logger.Info($"Registered type '{entity.GetType()}' using key prefix {type.Name}");
            }
        }

        private void PublishDBLoadedEvent()
        {
            // CLI tools also use this class and don't have access to the NWN context.
            // Perform an environment variable check to ensure we're in the game server context before executing the event.
            if (_settings.IsGameServerContext)
                _event.ExecuteScript(DBLoadedEventScript, OBJECT_SELF);
        }


        /// <summary>
        /// Retrieves a specific object in the database by an arbitrary key.
        /// </summary>
        /// <typeparam name="T">The type of data to retrieve</typeparam>
        /// <param name="id">The arbitrary key the data is stored under</param>
        /// <returns>The object stored in the database under the specified key</returns>
        public T Get<T>(string id)
            where T : IDBEntity
        {
            var command = new DBServerCommand
            {
                CommandType = DBServerCommandType.Get,
                Key = id,
                EntityType = typeof(T).Name
            };

            var response = SendCommand(command);
            if (response.CommandType != DBServerCommandType.Result)
            {
                throw new Exception($"Failed to get entity: {response.Message}");
            }

            if (string.IsNullOrWhiteSpace(response.EntitySingle))
            {
                var newObj = Activator.CreateInstance<T>();
                newObj.Id = id;
                return newObj;
            }

            var entity = XMJsonUtility.Deserialize<T>(response.EntitySingle);
            return entity;
        }


        public void Set<T>(T entity)
            where T : IDBEntity
        {
            var type = typeof(T);
            var indexData = new Dictionary<string, string>();

            foreach (var prop in _indexedPropertiesByType[type])
            {
                var property = type.GetProperty(prop.Name);
                var value = property?.GetValue(entity);

                if (value != null)
                {
                    string indexValue;
                    switch (prop.Type)
                    {
                        case IndexedPropertyType.Enum:
                            indexValue = Convert.ToInt32(value).ToString();
                            break;
                        case IndexedPropertyType.Guid:
                            indexValue = RedisTokenHelper.EscapeTokens(((Guid)value).ToString());
                            break;
                        case IndexedPropertyType.Text:
                            indexValue = RedisTokenHelper.EscapeTokens((string)value);
                            break;
                        case IndexedPropertyType.Numeric:
                            indexValue = Convert.ToInt32(value).ToString();
                            break;
                        default:
                            throw new Exception("Unable to determine property type.");
                    }

                    indexData[prop.Name] = indexValue;
                }
            }

            var jsonEntity = XMJsonUtility.Serialize(entity);
            var command = new DBServerCommand
            {
                CommandType = DBServerCommandType.Set,
                EntityType = type.Name,
                Key = entity.Id,
                EntitySingle = jsonEntity,
                IndexData = indexData
            };

            var response = SendCommand(command);
            if (response.CommandType != DBServerCommandType.Ok)
            {
                throw new Exception($"Failed to set entity: {response.Message}");
            }
        }

        /// <summary>
        /// Searches the Redis DB for records matching the query criteria.
        /// </summary>
        /// <typeparam name="T">The type of entity to retrieve.</typeparam>
        /// <param name="query">The query to run.</param>
        /// <returns>An enumerable of entities matching the criteria.</returns>
        public IEnumerable<T> Search<T>(DBQuery<IDBEntity> query)
            where T : IDBEntity
        {
            var type = typeof(T);
            var command = new DBServerCommand
            {
                CommandType = DBServerCommandType.Search,
                EntityType = type.Name,
                Query = query
            };

            var response = SendCommand(command);
            if (response.CommandType != DBServerCommandType.Result)
            {
                throw new Exception($"Failed to search entities: {response.Message}");
            }

            foreach (var result in response.EntitiesList)
            {
                yield return XMJsonUtility.Deserialize<T>(result);
            }
        }

        private DBServerCommand SendCommand(DBServerCommand command)
        {
            using (var client = new Socket(AddressFamily.Unix, SocketType.Stream, ProtocolType.Unspecified))
            {
                // Connect to the server
                var endPoint = new UnixDomainSocketEndPoint(_socketPath);
                client.Connect(endPoint);

                // Serialize the command to JSON and convert to bytes
                var commandJson = XMJsonUtility.Serialize(command);
                var commandBytes = Encoding.UTF8.GetBytes(commandJson);

                // Send the length of the command first
                var lengthBytes = BitConverter.GetBytes(commandBytes.Length);
                client.Send(lengthBytes);

                // Send the actual command
                client.Send(commandBytes);

                // Receive the length of the response
                var lengthBuffer = new byte[4];
                client.Receive(lengthBuffer);
                var responseLength = BitConverter.ToInt32(lengthBuffer, 0);

                // Receive the response data
                var responseBuffer = new byte[responseLength];
                int totalReceived = 0;
                while (totalReceived < responseLength)
                {
                    totalReceived += client.Receive(
                        responseBuffer,
                        totalReceived,
                        responseLength - totalReceived,
                        SocketFlags.None);
                }

                // Deserialize and return the response
                var responseJson = Encoding.UTF8.GetString(responseBuffer);

                //Console.WriteLine($"SendCommand json response: {responseJson}");
                return XMJsonUtility.Deserialize<DBServerCommand>(responseJson);
            }
        }
    }
}
