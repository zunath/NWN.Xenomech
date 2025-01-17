using Anvil.Services;
using System.Collections.Generic;
using System;
using XM.Progression.Recast;
using XM.Shared.Core.Data;

namespace XM.Combat.Entity
{
    [ServiceBinding(typeof(IDBEntity))]
    public class PlayerCombat: EntityBase
    {
        public PlayerCombat()
        {
        }

        public PlayerCombat(string id)
        {
            Id = id;
        }


    }
}
