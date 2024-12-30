using System;
using XM.Localization;

namespace XM.Inventory.KeyItem
{
    internal class KeyItemCategoryAttribute: Attribute
    {
        public LocaleString Name { get; private set; }
        public bool IsActive { get; private set; }

        public KeyItemCategoryAttribute(LocaleString name, bool isActive)
        {
            Name = name;
            IsActive = isActive;
        }
    }
}
