using System.Collections.Generic;
using Anvil.Services;

namespace XM.Shared.Core.Entity
{
    [ServiceBinding(typeof(IDBEntity))]
    public class PlayerName : EntityBase
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



