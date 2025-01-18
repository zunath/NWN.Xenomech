using Anvil.Services;
using XM.Shared.Core.EventManagement;

namespace XM.AI.Event
{
    [ServiceBinding(typeof(AIEventRegistrationService))]
    internal class AIEventRegistrationService
    {
        private readonly XMEventService _event;

        public AIEventRegistrationService(XMEventService @event)
        {
            _event = @event;

            RegisterEvents();
        }

        private void RegisterEvents()
        {
            _event.RegisterEvent<AIEvent.OnEnterAggroAOE>(AIEventScript.AggroAOEEnter);
            _event.RegisterEvent<AIEvent.OnExitAggroAOE>(AIEventScript.AggroAOEExit);
        }
    }
}
