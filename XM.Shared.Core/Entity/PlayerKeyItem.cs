using Anvil.Services;
using System.Collections.Generic;
using System;

namespace XM.Shared.Core.Entity
{
    [ServiceBinding(typeof(IDBEntity))]
    public class PlayerKeyItem: EntityBase
    {
        public Dictionary<int, DateTime> KeyItems { get; set; }

        public PlayerKeyItem()
        {
            KeyItems = new Dictionary<int, DateTime>();
        }

        public PlayerKeyItem(string playerId)
        {
            Id = playerId;
            KeyItems = new Dictionary<int, DateTime>();
        }
    }
}



