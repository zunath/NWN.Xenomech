﻿using XM.Shared.Core.Localization;

namespace XM.Progression.Stat.ResistDefinition
{
    public class DarknessResistDefinition: IResistDefinition
    {
        public ResistType Type => ResistType.Darkness;
        public LocaleString Name => LocaleString.DarknessResist;
        public string IconResref => "resist_darkness";
    }
}
