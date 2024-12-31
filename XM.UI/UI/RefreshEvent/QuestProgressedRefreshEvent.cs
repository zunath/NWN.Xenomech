using XM.UI.Event;

namespace XM.UI.UI.RefreshEvent
{
    public class QuestProgressedRefreshEvent : IGuiRefreshEvent
    {
        public string QuestId { get; set; }

        public QuestProgressedRefreshEvent(string questId)
        {
            QuestId = questId;
        }
    }
}
