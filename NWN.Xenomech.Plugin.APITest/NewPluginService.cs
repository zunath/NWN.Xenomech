using System;
using System.IO;
using Anvil.Services;
using Jil;
using NLog;
using NWN.Xenomech.Data;

namespace NWN.Xenomech.Plugin.APITest
{
    [ServiceBinding(typeof(NewPluginService))]
    public class NewPluginService: IDisposable
    {
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

        private DBService _db;

        public NewPluginService(DBService db)
        {
            _db = db;

            RegisterTypes();
        }

        private void RegisterTypes()
        {
        }


        [ScriptHandler("bread_test")]
        public void TestMethod()
        {
            var player = GetLastUsedBy();
            var playerId = GetObjectUUID(player);

            SendMessageToPC(player, $"playerId = {playerId}");

            var entity = new TestEntity()
            {
                Id = playerId,
                Name = GetName(player) + " new name"
            };

            _db.Set(entity);


            Console.WriteLine($"GET: {_db.Get<TestEntity>(playerId).Name}");
        }

        public void Dispose()
        {
        }
    }
}
