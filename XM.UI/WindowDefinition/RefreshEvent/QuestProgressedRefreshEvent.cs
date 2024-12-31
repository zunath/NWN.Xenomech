using XM.UI.Event;

namespace XM.UI.WindowDefinition.RefreshEvent
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
