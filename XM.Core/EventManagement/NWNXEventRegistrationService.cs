using System.Collections.Generic;
using Anvil.Services;
using XM.Core.EventManagement.NWNXEvent;

namespace XM.Core.EventManagement
{
    [ServiceBinding(typeof(NWNXEventRegistrationService))]
    internal class NWNXEventRegistrationService: EventRegistrationServiceBase
    {
        [Inject]
        public IList<INWNXOnModulePreload> OnModulePreloadSubscriptions { get; set; }

        [ScriptHandler(EventScript.NWNXOnModulePreloadScript)]
        public void HandleModulePreload() => HandleEvent(OnModulePreloadSubscriptions, (subscription) => subscription.OnModulePreload());


    }
}
