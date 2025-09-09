using System;
using XM.Shared.Core.Localization;

namespace XM.Plugin.Mech
{
    internal class MechPartTypeAttribute: Attribute
    {
        public LocaleString Name { get; private set; }
        public bool IsActive { get; private set; }
        public string DefaultImageResref { get; private set; }

        public MechPartTypeAttribute(
            LocaleString name, 
            bool isActive,
            string defaultImageResref)
        {
            Name = name;
            IsActive = isActive;
            DefaultImageResref = defaultImageResref;
        }
    }
}
