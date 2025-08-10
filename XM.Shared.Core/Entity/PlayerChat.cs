using Anvil.Services;

namespace XM.Shared.Core.Entity
{
    [ServiceBinding(typeof(IDBEntity))]
    public class PlayerChat : EntityBase
    {
        public PlayerChat()
        {
            EmoteStyleCode = 1; // Regular
        }

        public PlayerChat(string playerId)
        {
            Id = playerId;
            EmoteStyleCode = 1;
        }

        // Plugin maps codes <-> EmoteStyle enum
        public int EmoteStyleCode { get; set; }

        // RGB colors stored as bytes
        public byte OOC_R { get; set; }
        public byte OOC_G { get; set; }
        public byte OOC_B { get; set; }

        public byte Emote_R { get; set; }
        public byte Emote_G { get; set; }
        public byte Emote_B { get; set; }

        public int RPPoints { get; set; }
        public ulong TotalRPExpGained { get; set; }
        public ulong SpamMessageCount { get; set; }
    }
}



