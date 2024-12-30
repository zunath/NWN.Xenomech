using Anvil.Services;
using System.Collections.Generic;
using System;
using XM.Data;
using XM.Inventory.KeyItem;

namespace XM.Inventory.Entity
{
    [ServiceBinding(typeof(IDBEntity))]
    internal class PlayerKeyItem: EntityBase
    {
        public Dictionary<KeyItemType, DateTime> KeyItems { get; set; }

        public PlayerKeyItem()
        {
            KeyItems = new Dictionary<KeyItemType, DateTime>();
        }
    }
}
