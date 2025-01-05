using Anvil.Services;
using XM.Shared.Core.EventManagement;

namespace XM.AI
{
    [ServiceBinding(typeof(AIService))]
    internal class AIService
    {
        private const string AIFlagsVariable = "AI_FLAGS";

        public AIService(XMEventService @event)
        {
            @event.Subscribe<XMEvent.OnSpawnCreated>(OnSpawnCreated);
        }
        private void OnSpawnCreated()
        {
            var creature = OBJECT_SELF;
            SetAIFlag(creature, AIFlag.ReturnHome);
        }

        private void SetAIFlag(uint creature, AIFlag flags)
        {
            var flagValue = (int)flags;
            SetLocalInt(creature, AIFlagsVariable, flagValue);
        }

    }
}
