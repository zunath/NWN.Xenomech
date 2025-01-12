using XM.Shared.Core.Localization;

namespace XM.Progression.Stat.ResistDefinition
{
    public class DarknessResistDefinition: IResistDefinition
    {
        public LocaleString Name => LocaleString.DarknessResist;
        public string IconResref => "resist_darkness";
    }
}
