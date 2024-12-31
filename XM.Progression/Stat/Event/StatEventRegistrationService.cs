using Anvil.Services;
using System.Collections.Generic;
using XM.Core.EventManagement;

namespace XM.Progression.Stat.Event
{
    [ServiceBinding(typeof(StatEventRegistrationService))]
    internal class StatEventRegistrationService : EventRegistrationServiceBase
    {
        [Inject]
        public IList<IPlayerHPAdjustedEvent> OnPlayerHPAdjustedSubscriptions { get; set; }

        [ScriptHandler(ProgressionEventScript.OnPlayerHPAdjustedScript)]
        public void HandleOnPlayerHPAdjusted() => HandleEvent(OnPlayerHPAdjustedSubscriptions,
            (subscription) => subscription.OnPlayerHPAdjusted());

        [Inject]
        public IList<IPlayerEPAdjustedEvent> OnPlayerEPAdjustedSubscriptions { get; set; }

        [ScriptHandler(ProgressionEventScript.OnPlayerEPAdjustedScript)]
        public void HandleOnPlayerEPAdjusted() => HandleEvent(OnPlayerEPAdjustedSubscriptions,
            (subscription) => subscription.OnPlayerEPAdjusted());
    }
}
