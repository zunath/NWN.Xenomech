using XM.UI.Event;

namespace XM.UI.UI.RefreshEvent
{
    public class QuestAcquiredRefreshEvent : IGuiRefreshEvent
    {
        public string QuestId { get; set; }

        public QuestAcquiredRefreshEvent(string questId)
        {
            QuestId = questId;
        }
    }
}
