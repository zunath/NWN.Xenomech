using System.Collections.Generic;
using Anvil.Services;
using XM.Core.EventManagement;

namespace XM.Quest.Event
{
    [ServiceBinding(typeof(QuestEventRegistrationService))]
    internal class QuestEventRegistrationService: EventRegistrationServiceBase
    {
        [Inject]
        public IList<IQuestCompleted> OnQuestCompletedSubscriptions { get; set; }

        [ScriptHandler(QuestEventScript.OnQuestCompletedScript)]
        public void HandleQuestCompleted() => HandleEvent(OnQuestCompletedSubscriptions,
            (subscription) => subscription.OnQuestCompleted());
    }
}
