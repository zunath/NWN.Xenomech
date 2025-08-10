using System;
using XM.Shared.Core.Localization;

namespace XM.Plugin.Craft
{
    internal class RecipeCategoryAttribute: Attribute
    {
        public LocaleString Name { get; set; }
        public bool IsActive { get; set; }

        public RecipeCategoryAttribute(LocaleString name, bool isActive)
        {
            Name = name;
            IsActive = isActive;
        }
    }
}
