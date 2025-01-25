using System;
using XM.Shared.Core.Localization;

namespace XM.Progression.Recast
{
    public class RecastGroupAttribute : Attribute
    {
        public LocaleString Name { get; set; }
        public bool IsVisible { get; set; }

        public RecastGroupAttribute(LocaleString name, bool isVisible)
        {
            Name = name;
            IsVisible = isVisible;
        }
    }
}
