using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Anvil.Services;
using NLog;
using NRediSearch;
using NReJSON;
using StackExchange.Redis;
using XM.Shared.Core.Configuration;
using XM.Shared.Core.Json;

namespace XM.Shared.Core.Data
{
    [ServiceBinding(typeof(IntegratedDBService))]
    public class IntegratedDBService
    {
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();
        private readonly ConnectionMultiplexer _multiplexer;
        private readonly Dictionary<string, Client> _searchClientsByType = new();
        private readonly Dictionary<Type, List<IndexedProperty>> _indexedPropertiesByType = new();
        private readonly IList<IDBEntity> _entities;
        private bool _initialized = false;
        private readonly SemaphoreSlim _initSemaphore = new(1, 1);

        public IntegratedDBService(
            XMSettingsService settings,
            IList<IDBEntity> entities)
        {
            _entities = entities;
            
            var options = new ConfigurationOptions
            {
                AbortOnConnectFail = false,
                EndPoints = { settings.RedisIPAddress }
            };

            _multiplexer = ConnectionMultiplexer.Connect(options);
            NReJSONSerializer.SerializerProxy = new XMJsonSerializer();
        }

        public async Task InitializeAsync()
        {
            if (_initialized) return;

            await _initSemaphore.WaitAsync();
            try
            {
                if (_initialized) return;

                _logger.Info("Waiting for database connection...");
                while (!_multiplexer.IsConnected)
                {
                    await Task.Delay(100);
                }
                _logger.Info("Database connection established.");

                await LoadEntitiesAsync();
                _initialized = true;
            }
            finally
            {
                _initSemaphore.Release();
            }
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
                    else if (prop.PropertyType.IsEnum)
                    {
                        indexedProperty.Type = IndexedPropertyType.Enum;
                    }
                    else if (prop.PropertyType == typeof(bool))
                    {
                        indexedProperty.Type = IndexedPropertyType.Boolean;
                    }

                    indexedProperties.Add(indexedProperty);
                }
            }

            _indexedPropertiesByType[type] = indexedProperties;
        }

        private async Task LoadEntitiesAsync()
        {
            foreach (var entity in _entities)
            {
                var type = entity.GetType();
                BuildIndexedProperties(type);

                await RegisterEntityAsync(type.Name, _indexedPropertiesByType[type]);
                _logger.Info($"Registered type '{entity.GetType()}' using key prefix {type.Name}");
            }

            // Wait for indexing to complete
            _logger.Info("Waiting for reindexing to finish...");
            var indexing = true;
            do
            {
                var status = await GetIndexingStatusAsync();
                if (status)
                {
                    indexing = false;
                }
                else
                {
                    await Task.Delay(100);
                }
            } while (indexing);
            _logger.Info("Reindexing is complete!");
        }

        private async Task RegisterEntityAsync(string type, List<IndexedProperty> indexedProperties)
        {
            _logger.Info($"Registering type: {type}");

            // Register the search client
            _searchClientsByType[type] = new Client(type, _multiplexer.GetDatabase());
            await ProcessIndexAsync(type, indexedProperties);
        }

        private async Task ProcessIndexAsync(string type, List<IndexedProperty> indexedProperties)
        {
            // Drop any existing index
            try
            {
                _multiplexer.GetDatabase().Execute("FT.DROPINDEX", type);
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
                    _logger.Warn($"Issue dropping index for type {type}. Exception: {ex}");
                }
            }

            // Build the schema based on the IndexedAttribute associated to properties
            var schema = new Schema();

            foreach (var prop in indexedProperties)
            {
                if (prop.Type == IndexedPropertyType.Numeric)
                {
                    schema.AddNumericField(prop.Name);
                }
                else
                {
                    schema.AddTextField(prop.Name);
                }
            }

            _searchClientsByType[type].CreateIndex(schema, new Client.ConfiguredIndexOptions());
            _logger.Info($"Created index for {type}");
            await WaitForReindexingAsync(type);
        }

        private async Task WaitForReindexingAsync(string type)
        {
            string indexing;

            _logger.Info($"Waiting for Redis to complete indexing of: {type}");
            do
            {
                await Task.Delay(100);

                try
                {
                    var info = _searchClientsByType[type].GetInfo();
                    indexing = info["percent_indexed"];
                }
                catch (Exception ex)
                {
                    indexing = "0";
                    _logger.Warn($"Error during indexing: {ex}");
                }

            } while (indexing != "1");
        }

        private async Task<bool> GetIndexingStatusAsync()
        {
            // Check if all search clients are ready
            foreach (var client in _searchClientsByType.Values)
            {
                try
                {
                    var info = client.GetInfo();
                    if (info["percent_indexed"] != "1")
                    {
                        return false;
                    }
                }
                catch
                {
                    return false;
                }
            }
            return true;
        }

        public async Task<T> GetAsync<T>(string id) where T : IDBEntity
        {
            await InitializeAsync();

            var combinedKey = $"{typeof(T).Name}:{id}";
            var data = await _multiplexer.GetDatabase().JsonGetAsync(combinedKey);

            if (data.IsNull)
            {
                var newObj = Activator.CreateInstance<T>();
                newObj.Id = id;
                return newObj;
            }

            return XMJsonUtility.Deserialize<T>(data.ToString());
        }

        public async Task SetAsync<T>(T entity) where T : IDBEntity
        {
            await InitializeAsync();

            var type = typeof(T);
            var indexData = new Dictionary<string, RedisValue>();

            foreach (var prop in _indexedPropertiesByType[type])
            {
                var property = type.GetProperty(prop.Name);
                var value = property?.GetValue(entity);

                if (value != null)
                {
                    RedisValue indexValue;
                    switch (prop.Type)
                    {
                        case IndexedPropertyType.Enum:
                            indexValue = Convert.ToInt32(value);
                            break;
                        case IndexedPropertyType.Guid:
                            indexValue = RedisTokenHelper.EscapeTokens(((Guid)value).ToString());
                            break;
                        case IndexedPropertyType.Text:
                            indexValue = RedisTokenHelper.EscapeTokens((string)value);
                            break;
                        case IndexedPropertyType.Numeric:
                            indexValue = Convert.ToInt32(value);
                            break;
                        case IndexedPropertyType.Boolean:
                            indexValue = (bool)value ? 1 : 0;
                            break;
                        default:
                            throw new Exception("Unable to determine property type.");
                    }

                    indexData[prop.Name] = indexValue;
                }
            }

            var jsonEntity = XMJsonUtility.Serialize(entity);
            var indexKey = $"Index:{type.Name}:{entity.Id}";

            await _searchClientsByType[type.Name].ReplaceDocumentAsync(indexKey, indexData);
            await _multiplexer.GetDatabase().JsonSetAsync($"{type.Name}:{entity.Id}", jsonEntity);
        }

        public async Task<IEnumerable<T>> SearchAsync<T>(DBQuery query) where T : IDBEntity
        {
            await InitializeAsync();

            var type = typeof(T);
            var result = await _searchClientsByType[type.Name].SearchAsync(query.BuildQuery(type.Name));

            var entities = new List<T>();
            foreach (var doc in result.Documents)
            {
                var recordId = doc.Id.Remove(0, 6); // Remove the 'Index:' prefix
                var data = await _multiplexer.GetDatabase().JsonGetAsync(recordId);
                if (!data.IsNull)
                {
                    entities.Add(XMJsonUtility.Deserialize<T>(data.ToString()));
                }
            }

            return entities;
        }

        public async Task<int> SearchCountAsync<T>(DBQuery query) where T : IDBEntity
        {
            await InitializeAsync();

            var type = typeof(T);
            var result = await _searchClientsByType[type.Name].SearchAsync(query.BuildQuery(type.Name, true));
            return (int)result.TotalResults;
        }

        public async Task DeleteAsync<T>(string key) where T : IDBEntity
        {
            await InitializeAsync();

            var type = typeof(T);
            var indexKey = $"Index:{type.Name}:{key}";
            
            await _searchClientsByType[type.Name].DeleteDocumentAsync(indexKey);
            await _multiplexer.GetDatabase().JsonDeleteAsync($"{type.Name}:{key}");
        }

        // Synchronous wrappers for backward compatibility
        public T Get<T>(string id) where T : IDBEntity
        {
            return GetAsync<T>(id).GetAwaiter().GetResult();
        }

        public void Set<T>(T entity) where T : IDBEntity
        {
            SetAsync<T>(entity).GetAwaiter().GetResult();
        }

        public IEnumerable<T> Search<T>(DBQuery query) where T : IDBEntity
        {
            return SearchAsync<T>(query).GetAwaiter().GetResult();
        }

        public int SearchCount<T>(DBQuery query) where T : IDBEntity
        {
            return SearchCountAsync<T>(query).GetAwaiter().GetResult();
        }

        public void Delete<T>(string key) where T : IDBEntity
        {
            DeleteAsync<T>(key).GetAwaiter().GetResult();
        }
    }
} 