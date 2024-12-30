using System;
using System.Collections.Generic;
using Anvil.Services;
using NLog;
using XM.Core.EventManagement.NWNXEvent;

namespace XM.Core.EventManagement
{
    [ServiceBinding(typeof(NWNXEventRegistrationService))]
    internal class NWNXEventRegistrationService
    {
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

        [Inject]
        public IList<INWNXOnModulePreload> OnModulePreloadSubscriptions { get; set; }

        [ScriptHandler(EventScript.NWNXOnModulePreloadScript)]
        public void HandleModulePreload()
        {
            foreach (var handler in OnModulePreloadSubscriptions)
            {
                try
                {
                    handler.OnModulePreload();
                }
                catch (Exception ex)
                {
                    _logger.Error(ex);
                }
            }
        }

    }
}
