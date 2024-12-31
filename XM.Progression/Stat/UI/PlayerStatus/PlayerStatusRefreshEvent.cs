using XM.UI.Event;

namespace XM.Progression.Stat.UI.PlayerStatus
{
    internal class PlayerStatusRefreshEvent : IGuiRefreshEvent
    {
        internal enum StatType
        {
            HP = 1,
            EP = 2
        }

        public StatType Type { get; set; }

        public PlayerStatusRefreshEvent(StatType type)
        {
            Type = type;
        }
    }
}
