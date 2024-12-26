using Anvil.Services;
using NLog;
using NWN.Xenomech.API.Enum;

namespace NWN.Xenomech.Plugin.APITest
{
    [ServiceBinding(typeof(NewPluginService))]
    public class NewPluginService
    {
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();


        [ScriptHandler("bread_test")]
        public void TestMethod()
        {
            var player = GetLastUsedBy();
            var item = GetFirstItemInInventory(player);
            
            SendMessageToPC(GetLastUsedBy(), $"Item = {GetName(item)}");

            AssignCommand(player, () => ActionPlayAnimation(Animation.LoopingSitCross, 1f, 9999f));
            
        }
    }
}
