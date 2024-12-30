using System;
using XM.Localization;

namespace XM.Inventory.KeyItem
{
    public class KeyItemAttribute: Attribute
    {
        public LocaleString Name { get; private set; }
        public LocaleString Description { get; private set; }
        public KeyItemCategoryType Category { get; private set; }
        public bool IsActive { get; private set; }

        public KeyItemAttribute(KeyItemCategoryType category, LocaleString name, bool isActive, LocaleString description = LocaleString.Empty)
        {
            Category = category;
            Name = name;
            IsActive = isActive;
            Description = description;
        }
    }
}
