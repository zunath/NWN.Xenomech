using System;
using Anvil.Services;
using NLog;
using NWN.Xenomech.Core;
using NWN.Xenomech.Core.Entity;

namespace NWN.Xenomech.Plugin.APITest
{
    [ServiceBinding(typeof(NewPluginService))]
    public class NewPluginService
    {
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

        public NewPluginService()
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
                Name = GetName(player) + " new name 2"
            };

            DB.Instance.Set(entity);
        }

    }
}
