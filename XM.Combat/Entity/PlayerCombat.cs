using Anvil.Services;
using System.Collections.Generic;
using System;
using XM.Combat.Recast;
using XM.Data;

namespace XM.Combat.Entity
{
    [ServiceBinding(typeof(IDBEntity))]
    internal class PlayerCombat: EntityBase
    {
        public PlayerCombat()
        {
            RecastTimes = new Dictionary<RecastGroup, DateTime>();
        }

        public PlayerCombat(string id)
        {
            Id = id;
        }

        public Dictionary<RecastGroup, DateTime> RecastTimes { get; set; }

    }
}
