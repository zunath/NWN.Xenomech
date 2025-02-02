using Anvil.Services;
using XM.Chat.Communication;
using XM.Shared.Core.Data;

namespace XM.Chat.Entity
{
    [ServiceBinding(typeof(IDBEntity))]
    internal class PlayerChat : EntityBase
    {
        public PlayerChat()
        {
            EmoteStyle = EmoteStyle.Regular;
        }

        public PlayerChat(string playerId)
        {
            Id = playerId;
            EmoteStyle = EmoteStyle.Regular;
        }

        public EmoteStyle EmoteStyle { get; set; }
        public ChatColor OOCChatColor { get; set; }
        public ChatColor EmoteChatColor { get; set; }
        public int RPPoints { get; set; }
        public ulong TotalRPExpGained { get; set; }
        public ulong SpamMessageCount { get; set; }
    }
}
