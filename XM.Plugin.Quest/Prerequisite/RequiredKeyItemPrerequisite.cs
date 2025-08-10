﻿using XM.Inventory.KeyItem;

namespace XM.Quest.Prerequisite
{
    internal class RequiredKeyItemPrerequisite : IQuestPrerequisite
    {
        public KeyItemType KeyItemType { get; set; }

        private readonly KeyItemService _keyItem;

        public RequiredKeyItemPrerequisite(KeyItemService keyItem)
        {
            _keyItem = keyItem;
        }

        public bool MeetsPrerequisite(uint player)
        {
            return _keyItem.HasKeyItem(player, KeyItemType);
        }
    }
}
