using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading;
using Anvil.Services;
using Newtonsoft.Json;
using NRediSearch;
using NReJSON;
using StackExchange.Redis;
using XM.Configuration;

namespace XM.Data
{
    [ServiceBinding(typeof(DBService))]
    public class DBService : IDisposable, IUpdateable, IInitializable
    {
        private readonly XMSettingsService _settings;
        private ConnectionMultiplexer _multiplexer;
        private readonly Dictionary<Type, string> _keyPrefixByType = new();
        private readonly Dictionary<Type, Client> _searchClientsByType = new();
        private readonly Dictionary<Type, List<string>> _indexedPropertiesByName = new();
        private readonly Dictionary<string, IDBEntity> _cachedEntities = new();

        [Inject]
        public IList<IDBEntity> Entities { get; set; }

        public DBService(XMSettingsService settings)
        {
            NReJSONSerializer.SerializerProxy = new XMJsonSerializer();
            _settings = settings;

            Start();
        }

        public void Init()
        {
            foreach (var entity in Entities)
            {
                var type = entity.GetType();
                // Register the type by itself first.
                _keyPrefixByType[type] = type.Name;

                // Register the search client.
                _searchClientsByType[type] = new Client(type.Name, _multiplexer.GetDatabase());
                ProcessIndex(entity);

                Console.WriteLine($"Registered type '{entity.GetType()}' using key prefix {type.Name}");
            }
        }

        public void Start()
        {
            var options = new ConfigurationOptions
            {
                AbortOnConnectFail = false,
                EndPoints = { _settings.RedisIPAddress }
            };

            _multiplexer = ConnectionMultiplexer.Connect(options);

            Console.WriteLine($"Waiting for database connection. If this takes longer than 10 minutes, there's a problem.");
            while (!_multiplexer.IsConnected)
            {
                // Spin
                Thread.Sleep(100);
            }
            Console.WriteLine($"Database connection established.");
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
            var keyPrefix = _keyPrefixByType[typeof(T)];
            if (_cachedEntities.ContainsKey(id))
            {
                return (T)_cachedEntities[id];
            }
            else
            {
                RedisValue data = _multiplexer.GetDatabase().JsonGet($"{keyPrefix}:{id}").ToString();

                if (string.IsNullOrWhiteSpace(data))
                    return default;

                var entity = JsonConvert.DeserializeObject<T>(data);
                _cachedEntities[id] = entity;

                return entity;
            }
        }

        public void Set<T>(T entity)
            where T : IDBEntity
        {
            var type = typeof(T);
            var data = JsonConvert.SerializeObject(entity);
            var keyPrefix = _keyPrefixByType[type];
            var indexKey = $"Index:{keyPrefix}:{entity.Id}";
            var indexData = new Dictionary<string, RedisValue>();

            foreach (var prop in _indexedPropertiesByName[type])
            {
                var property = type.GetProperty(prop);
                var value = property?.GetValue(entity);

                if (value != null)
                {
                    // Convert enums to numeric values
                    if (property.PropertyType.IsEnum)
                        value = (int)value;

                    if (property.PropertyType == typeof(Guid))
                    {
                        value = RedisTokenHelper.EscapeTokens(((Guid)value).ToString());
                    }

                    if (property.PropertyType == typeof(string))
                    {
                        value = RedisTokenHelper.EscapeTokens((string)value);
                    }

                    indexData[prop] = (dynamic)value;
                }
            }
            _searchClientsByType[type].ReplaceDocument(indexKey, indexData);
            _multiplexer.GetDatabase().JsonSet($"{keyPrefix}:{entity.Id}", data);
            _cachedEntities[entity.Id] = entity;
        }



        /// <summary>
        /// Processes the Redis Search index with the latest changes.
        /// </summary>
        /// <param name="entity"></param>
        private void ProcessIndex(IDBEntity entity)
        {
            var type = entity.GetType();

            // Drop any existing index
            try
            {
                // FT.DROPINDEX is used here in lieu of DropIndex() as it does not cause all documents to be lost.
                _multiplexer.GetDatabase().Execute("FT.DROPINDEX", type.Name);
                Console.WriteLine($"Dropped index for {type}");
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Unknown Index name", StringComparison.InvariantCultureIgnoreCase))
                {
                    Console.WriteLine($"Index does not exist for type {type}.");
                }
                else
                {
                    Console.WriteLine($"Issue dropping index for type {type}. Exception: {ex}");
                }

            }

            // Build the schema based on the IndexedAttribute associated to properties.
            var schema = new Schema();
            var indexedProperties = new List<string>();

            foreach (var prop in type.GetProperties())
            {
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
                        schema.AddNumericField(prop.Name);
                    }
                    else
                    {
                        schema.AddTextField(prop.Name);
                    }

                    indexedProperties.Add(prop.Name);
                }

            }

            // Cache the indexed properties for quick look-up later.
            _indexedPropertiesByName[type] = indexedProperties;

            _searchClientsByType[type].CreateIndex(schema, new Client.ConfiguredIndexOptions());
            Console.WriteLine($"Created index for {type}");
            WaitForReindexing(type);
        }

        private void WaitForReindexing(Type type)
        {
            string indexing;

            Console.WriteLine($"Waiting for Redis to complete indexing of: {type}");
            do
            {
                Thread.Sleep(100);

                try
                {
                    // If there is a lot of data or the machine is slow, this command can time out.
                    // Ignore when this happens and retry the command in 100ms.
                    var info = _searchClientsByType[type].GetInfo();
                    indexing = info["percent_indexed"];
                }
                catch (Exception ex)
                {
                    indexing = "0";
                    Console.WriteLine($"Error during indexing: {ex}");
                }

            } while (indexing != "1");
        }

        /// <summary>
        /// Searches the Redis DB for records matching the query criteria.
        /// </summary>
        /// <typeparam name="T">The type of entity to retrieve.</typeparam>
        /// <param name="query">The query to run.</param>
        /// <returns>An enumerable of entities matching the criteria.</returns>
        public IEnumerable<T> Search<T>(DBQuery<T> query)
            where T : EntityBase
        {
            var result = _searchClientsByType[typeof(T)].Search(query.BuildQuery());

            foreach (var doc in result.Documents)
            {
                // Remove the 'Index:' prefix.
                var recordId = doc.Id.Remove(0, 6);
                yield return _multiplexer.GetDatabase().JsonGet<T>(recordId);
            }
        }

        /// <summary>
        /// Searches the Redis DB for raw JSON records matching the query criteria.
        /// </summary>
        /// <typeparam name="T">The type of entity to retrieve.</typeparam>
        /// <param name="query">The query to run.</param>
        /// <returns>An enumerable of raw json values matching the criteria.</returns>
        public IEnumerable<string> SearchRawJson<T>(DBQuery<T> query)
            where T : EntityBase
        {
            var result = _searchClientsByType[typeof(T)].Search(query.BuildQuery());

            foreach (var doc in result.Documents)
            {
                // Remove the 'Index:' prefix.
                var recordId = doc.Id.Remove(0, 6);
                yield return _multiplexer.GetDatabase().JsonGet(recordId).ToString();
            }
        }
        
        public void Dispose()
        {
            Console.WriteLine($"Disposing DBService");
            _multiplexer.Close();

            _multiplexer.Dispose();
            _multiplexer = null;

            _keyPrefixByType.Clear();
            _searchClientsByType.Clear();
            _indexedPropertiesByName.Clear();
            _cachedEntities.Clear();
            Console.WriteLine($"finished disposing DBService");
        }

        public void Update()
        {
            _cachedEntities.Clear();
        }

    }
}
