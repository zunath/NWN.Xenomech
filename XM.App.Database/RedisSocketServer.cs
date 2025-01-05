using System.Dynamic;
using System.Net.Sockets;
using System.Text;
using NRediSearch;
using NReJSON;
using StackExchange.Redis;
using XM.Shared.Core.Data;
using XM.Shared.Core.Extension;
using XM.Shared.Core.Json;

namespace XM.App.Database
{
    public class RedisSocketServer
    {
        private ConnectionMultiplexer _multiplexer;
        private const string SocketPath = "/tmp/xm-sockets/xm-redis.sock";
        private readonly string _ipAddress;

        private readonly Dictionary<string, Client> _searchClientsByType = new();

        public RedisSocketServer(string ipAddress)
        {
            _ipAddress = ipAddress;
            NReJSONSerializer.SerializerProxy = new XMJsonSerializer();
        }

        public void Start()
        {
            ConnectToRedis();
            CreateDirectory();
            StartSocketServer();
        }

        private void CreateDirectory()
        {
            var socketDirectory = Path.GetDirectoryName(SocketPath);
            if (socketDirectory != null && !Directory.Exists(socketDirectory))
            {
                Directory.CreateDirectory(socketDirectory);

                // Set directory permissions to 777 (read/write/execute for all)
                SetDirectoryPermissions(socketDirectory, "0777");
            }
        }

        private void SetDirectoryPermissions(string path, string permissions)
        {
            var chmodProcess = new System.Diagnostics.Process
            {
                StartInfo = new System.Diagnostics.ProcessStartInfo
                {
                    FileName = "chmod",
                    Arguments = $"{permissions} {path}",
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                }
            };

            chmodProcess.Start();
            chmodProcess.WaitForExit();

            if (chmodProcess.ExitCode != 0)
            {
                var error = chmodProcess.StandardError.ReadToEnd();
                throw new Exception($"Failed to set permissions on {path}. Error: {error}");
            }
        }


        private void StartSocketServer()
        {
            var endPoint = new UnixDomainSocketEndPoint(SocketPath);
            using (var server = new Socket(AddressFamily.Unix, SocketType.Stream, ProtocolType.Unspecified))
            {
                if (File.Exists(SocketPath))
                {
                    Console.WriteLine($"Deleting stale socket path: {SocketPath}");
                    File.Delete(SocketPath);
                }

                server.Bind(endPoint);
                server.Listen(5);

                Console.WriteLine($"Server listening on {SocketPath}");

                while (true)
                {
                    using var client = server.Accept();
                    var lengthBuffer = new byte[4];
                    client.Receive(lengthBuffer);
                    var payloadLength = BitConverter.ToInt32(lengthBuffer, 0);

                    var buffer = new byte[payloadLength];
                    int totalReceived = 0;
                    while (totalReceived < payloadLength)
                    {
                        totalReceived += client.Receive(buffer, totalReceived, payloadLength - totalReceived,
                            SocketFlags.None);
                    }

                    var json = Encoding.UTF8.GetString(buffer);

                    //Console.WriteLine($"json = {json}");

                    var command = XMJsonUtility.Deserialize<DBServerCommand>(json);
                    var response = HandleCommand(command);

                    var responseJson = XMJsonUtility.Serialize(response);

                    //Console.WriteLine($"responseJson = {responseJson}");

                    var responseBytes = Encoding.UTF8.GetBytes(responseJson);
                    var responseLengthBytes = BitConverter.GetBytes(responseBytes.Length);
                    client.Send(responseLengthBytes);

                    // Send the actual response data
                    client.Send(responseBytes);
                }
            }
        }

        private void ConnectToRedis()
        {
            var options = new ConfigurationOptions
            {
                AbortOnConnectFail = false,
                EndPoints = { _ipAddress }
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

        private DBServerCommand HandleCommand(DBServerCommand command)
        {
            try
            {
                switch (command.CommandType)
                {
                    case DBServerCommandType.Register:
                        return HandleRegister(command);
                    case DBServerCommandType.Get:
                        return HandleGet(command);
                    case DBServerCommandType.Set:
                        return HandleSet(command);
                    case DBServerCommandType.Search:
                        return HandleSearch(command);
                    default:
                        return new DBServerCommand { CommandType = DBServerCommandType.Error, Message = "Unknown command" };
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{nameof(HandleCommand)} exception: {ex.ToMessageAndCompleteStacktrace()}");
                return new DBServerCommand { CommandType = DBServerCommandType.Error, Message = ex.Message };
            }
        }

        private DBServerCommand HandleRegister(DBServerCommand command)
        {
            RegisterEntity(command.EntityType, command.IndexedProperties);
            return new DBServerCommand { CommandType = DBServerCommandType.Ok };
        }

        private DBServerCommand HandleGet(DBServerCommand command)
        {
            var json = Get(command.Key, command.EntityType);
            return new DBServerCommand
            {
                CommandType = DBServerCommandType.Result,
                EntitySingle = json
            };
        }

        private DBServerCommand HandleSet(DBServerCommand command)
        {
            Set(command.Key, command.EntitySingle, command.EntityType, command.IndexData);
            return new DBServerCommand { CommandType = DBServerCommandType.Ok };
        }

        private DBServerCommand HandleSearch(DBServerCommand command)
        {
            var results = Search(command.EntityType, command.Query);
            return new DBServerCommand
            {
                CommandType = DBServerCommandType.Result,
                EntitiesList = results.ToList()
            };
        }

        /// <summary>
        /// Processes the Redis Search index with the latest changes.
        /// </summary>
        private void ProcessIndex(string type, List<IndexedProperty> indexedProperties)
        {
            // Drop any existing index
            try
            {
                // FT.DROPINDEX is used here in lieu of DropIndex() as it does not cause all documents to be lost.
                _multiplexer.GetDatabase().Execute("FT.DROPINDEX", type);
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
            Console.WriteLine($"Created index for {type}");
            WaitForReindexing(type);
        }

        private void WaitForReindexing(string type)
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


        private void RegisterEntity(string type, List<IndexedProperty> indexedProperties)
        {
            Console.WriteLine($"Registering type: {type}");

            // Register the search client.
            _searchClientsByType[type] = new Client(type, _multiplexer.GetDatabase());
            ProcessIndex(type, indexedProperties);
        }


        private IEnumerable<string> Search(string type, DBQuery<IDBEntity> query)
        {
            var result = _searchClientsByType[type].Search(query.BuildQuery());

            foreach (var doc in result.Documents)
            {
                // Remove the 'Index:' prefix.
                var recordId = doc.Id.Remove(0, 6);
                yield return (string)_multiplexer.GetDatabase()
                    .JsonGet(recordId);
            }
        }

        private string Get(string id, string type)
        {
            var combinedKey = $"{type}:{id}";

            RedisValue data = _multiplexer.GetDatabase().JsonGet(combinedKey).ToString();

            if (string.IsNullOrWhiteSpace(data))
                return default;

            return data;
        }

        private void Set(string key, string entity, string type, Dictionary<string, string> indexData)
        {
            var data = XMJsonUtility.Serialize(entity);
            var indexKey = $"Index:{type}:{key}";

            var redisData = new Dictionary<string, RedisValue>();
            foreach (var index in indexData)
            {
                Console.WriteLine($"Processing index: Key = {index.Key}, Value = {index.Value}, Type = {index.Value?.GetType()}");
                redisData[index.Key] = RedisValue.Unbox(index.Value);
            }

            _searchClientsByType[type].ReplaceDocument(indexKey, redisData);
            _multiplexer.GetDatabase().JsonSet($"{type}:{key}", data);
        }
    }
}