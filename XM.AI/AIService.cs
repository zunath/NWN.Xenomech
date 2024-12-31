using Anvil.Services;
using XM.Core.EventManagement.XMEvent;

namespace XM.AI
{
    [ServiceBinding(typeof(AIService))]
    [ServiceBinding(typeof(ISpawnCreatedEvent))]
    internal class AIService: ISpawnCreatedEvent
    {
        private const string AIFlagsVariable = "AI_FLAGS";

        public void OnSpawnCreated()
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
