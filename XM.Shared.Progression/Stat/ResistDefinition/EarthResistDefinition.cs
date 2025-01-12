using XM.Shared.Core.Localization;

namespace XM.Progression.Stat.ResistDefinition
{
    public class EarthResistDefinition: IResistDefinition
    {
        public LocaleString Name => LocaleString.EarthResist;
        public string IconResref => "resist_earth";
    }
}
