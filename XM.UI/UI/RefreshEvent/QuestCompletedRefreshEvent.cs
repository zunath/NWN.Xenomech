using XM.UI.Event;

namespace XM.UI.UI.RefreshEvent
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
