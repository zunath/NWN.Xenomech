using Anvil.Services;
using System.Collections.Generic;
using System;
using XM.Inventory.KeyItem;
using XM.Shared.Core.Data;

namespace XM.Inventory.Entity
{
    [ServiceBinding(typeof(IDBEntity))]
    public class PlayerKeyItem: EntityBase
    {
        public Dictionary<KeyItemType, DateTime> KeyItems { get; set; }

        public PlayerKeyItem()
        {
            KeyItems = new Dictionary<KeyItemType, DateTime>();
        }
    }
}
