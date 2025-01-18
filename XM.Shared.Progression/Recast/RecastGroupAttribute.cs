using System;
using XM.Shared.Core.Localization;

namespace XM.Progression.Recast
{
    public class RecastGroupAttribute : Attribute
    {
        public LocaleString Name { get; set; }
        public LocaleString ShortName { get; set; }
        public bool IsVisible { get; set; }

        public RecastGroupAttribute(LocaleString name, LocaleString shortName, bool isVisible)
        {
            Name = name;
            ShortName = shortName;
            IsVisible = isVisible;
        }
    }
}
