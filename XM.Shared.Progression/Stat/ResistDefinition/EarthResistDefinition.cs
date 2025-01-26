using XM.Shared.Core.Localization;

namespace XM.Progression.Stat.ResistDefinition
{
    public class EarthResistDefinition: IResistDefinition
    {
        public ResistType Type => ResistType.Earth;
        public LocaleString Name => LocaleString.EarthResist;
        public string IconResref => "resist_earth";
    }
}
