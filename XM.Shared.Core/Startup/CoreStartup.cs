using Anvil.Services;
using XM.Shared.Core.Json;

namespace XM.Shared.Core.Startup
{
    [ServiceBinding(typeof(CoreStartup))]
    public sealed class CoreStartup
    {
        public CoreStartup(IKeyNameRegistry registry)
        {
            // Wire the runtime provider once DI is ready
            KeyNameRegistry.SetProvider(registry);
        }
    }
}


