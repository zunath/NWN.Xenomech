using Anvil.Services;
using System.Collections.Generic;
using System;

namespace XM.Shared.Core.Entity
{
    [ServiceBinding(typeof(IDBEntity))]
    public class PlayerRecast : EntityBase
    {
        public PlayerRecast()
        {
            RecastTimes = new Dictionary<int, DateTime>();
        }

        public PlayerRecast(string playerId)
        {
            Id = playerId;
            RecastTimes = new Dictionary<int, DateTime>();
        }

        public Dictionary<int, DateTime> RecastTimes { get; set; }
    }
}



