using System;
using System.Threading;
using Anvil.Services;
using Newtonsoft.Json;
using XM.Progression.UI.PlayerStatusUI;
using XM.Shared.Core.Json;

namespace XM.Progression.Testing
{
    [ServiceBinding(typeof(ViewTesting))]
    [ServiceBinding(typeof(IInitializable))]
    internal class ViewTesting: IInitializable
    {
        public void Init()
        {
            var window = new PlayerStatusView();

            var built = window.Build();

            var newtonsoftJson = JsonConvert.SerializeObject(built.Window);
            var systemTextJson = XMJsonUtility.Serialize(built.Window);


            Console.WriteLine($"{nameof(newtonsoftJson)} = {newtonsoftJson}");
            Console.WriteLine($"{nameof(systemTextJson)} = {systemTextJson}");

        }
    }
}
