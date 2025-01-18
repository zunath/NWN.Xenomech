using Anvil.Services;
using System.Collections.Generic;
using System;
using XM.Progression.Recast;
using XM.Shared.Core.Data;

namespace XM.Progression.Entity
{
    [ServiceBinding(typeof(IDBEntity))]
    internal class PlayerRecast: EntityBase
    {
        public PlayerRecast()
        {
            RecastTimes = new Dictionary<RecastGroup, DateTime>();
        }

        public PlayerRecast(string playerId)
        {
            Id = playerId;
            RecastTimes = new Dictionary<RecastGroup, DateTime>();
        }

        public Dictionary<RecastGroup, DateTime> RecastTimes { get; set; }
    }
}
