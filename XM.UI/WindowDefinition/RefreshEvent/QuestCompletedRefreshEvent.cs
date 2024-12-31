using XM.UI.Event;

namespace XM.UI.WindowDefinition.RefreshEvent
{
    public class QuestCompletedRefreshEvent : IGuiRefreshEvent
    {
        public string QuestId { get; set; }

        public QuestCompletedRefreshEvent(string questId)
        {
            QuestId = questId;
        }
    }
}
