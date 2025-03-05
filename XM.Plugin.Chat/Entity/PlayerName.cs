using System.Collections.Generic;
using Anvil.Services;
using XM.Shared.Core.Data;

namespace XM.Chat.Entity
{
    [ServiceBinding(typeof(IDBEntity))]
    internal class PlayerName: EntityBase
    {
        public Dictionary<string, string> OverrideNames { get; set; }

        public PlayerName()
        {
            OverrideNames = new Dictionary<string, string>();
        }

        public PlayerName(string playerId)
        {
            Id = playerId;
            OverrideNames = new Dictionary<string, string>();
        }
    }
}
