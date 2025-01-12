using System;
using XM.Shared.Core.Localization;

namespace XM.Inventory.KeyItem
{
    public class KeyItemCategoryAttribute: Attribute
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
