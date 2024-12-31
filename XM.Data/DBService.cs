using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using Anvil.Services;
using Newtonsoft.Json;
using NLog;
using NRediSearch;
using NReJSON;
using StackExchange.Redis;
using XM.Configuration;

namespace XM.Data
{
    [ServiceBinding(typeof(DBService))]
    [ServiceBinding(typeof(IUpdateable))]
    public class DBService : 
        IDisposable, 
        IUpdateable, 
        IInitializable
    {
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

        private const string DBLoadedEventScript = "xm_db_loaded";

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
            LoadEntities();
            PublishDBLoadedEvent();
        }

        private void LoadEntities()
        {
            foreach (var entity in Entities)
            {
                var type = entity.GetType();
                // Register the type by itself first.
                _keyPrefixByType[type] = type.Name;

                // Register the search client.
                _searchClientsByType[type] = new Client(type.Name, _multiplexer.GetDatabase());
                ProcessIndex(entity);

                _logger.Info($"Registered type '{entity.GetType()}' using key prefix {type.Name}");
            }
        }

        private void PublishDBLoadedEvent()
        {
            // CLI tools also use this class and don't have access to the NWN context.
            // Perform an environment variable check to ensure we're in the game server context before executing the event.
            if (_settings.IsGameServerContext)
                ExecuteScript(DBLoadedEventScript, OBJECT_SELF);
        }

        public void Start()
        {
            var options = new ConfigurationOptions
            {
                AbortOnConnectFail = false,
                EndPoints = { _settings.RedisIPAddress }
            };

            _multiplexer = ConnectionMultiplexer.Connect(options);

            _logger.Info($"Waiting for database connection. If this takes longer than 10 minutes, there's a problem.");
            while (!_multiplexer.IsConnected)
            {
                // Spin
                Thread.Sleep(100);
            }
            _logger.Info($"Database connection established.");
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
            var combinedKey = $"{keyPrefix}:{id}";
            if (_cachedEntities.ContainsKey(combinedKey))
            {
                return (T)_cachedEntities[combinedKey];
            }
            else
            {
                RedisValue data = _multiplexer.GetDatabase().JsonGet(combinedKey).ToString();

                if (string.IsNullOrWhiteSpace(data))
                    return default;

                var entity = JsonConvert.DeserializeObject<T>(data);
                _cachedEntities[combinedKey] = entity;

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
            _cachedEntities[$"{keyPrefix}:{entity.Id}"] = entity;
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
                _logger.Info($"Dropped index for {type}");
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Unknown Index name", StringComparison.InvariantCultureIgnoreCase))
                {
                    _logger.Info($"Index does not exist for type {type}.");
                }
                else
                {
                    _logger.Info($"Issue dropping index for type {type}. Exception: {ex}");
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
            _logger.Info($"Created index for {type}");
            WaitForReindexing(type);
        }

        private void WaitForReindexing(Type type)
        {
            string indexing;

            _logger.Info($"Waiting for Redis to complete indexing of: {type}");
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
                    _logger.Info($"Error during indexing: {ex}");
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
            _multiplexer.Close();

            _multiplexer.Dispose();
            _multiplexer = null;

            _keyPrefixByType.Clear();
            _searchClientsByType.Clear();
            _indexedPropertiesByName.Clear();
            _cachedEntities.Clear();
        }

        public void Update()
        {
            _cachedEntities.Clear();
        }

    }
}
