using Anvil.Services;
using XM.Shared.Core.Data;

namespace XM.Chat.Roleplay.Entity
{
    [ServiceBinding(typeof(IDBEntity))]
    internal class PlayerRoleplay: EntityBase
    {
        public PlayerRoleplay()
        {
            
        }

        public PlayerRoleplay(string playerId)
        {
            Id = playerId;
        }

        public int RPPoints { get; set; }
        public ulong TotalRPExpGained { get; set; }
        public ulong SpamMessageCount { get; set; }
    }
}
