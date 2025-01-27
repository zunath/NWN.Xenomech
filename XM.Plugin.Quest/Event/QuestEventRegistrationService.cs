using XM.Shared.Core.EventManagement;

namespace XM.Quest.Event
{
    internal class QuestEventRegistrationService
    {
        private readonly XMEventService _event;

        public QuestEventRegistrationService(XMEventService @event)
        {
            _event = @event;

            RegisterEvents();
        }

        private void RegisterEvents()
        {
            _event.RegisterEvent<QuestEvent.QuestCompletedEvent>(QuestEventScript.OnQuestCompletedScript);
        }

    }
}
