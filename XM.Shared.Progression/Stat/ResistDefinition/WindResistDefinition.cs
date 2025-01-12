using XM.Shared.Core.Localization;

namespace XM.Progression.Stat.ResistDefinition
{
    public class WindResistDefinition: IResistDefinition
    {
        public LocaleString Name => LocaleString.WindResist;
        public string IconResref => "resist_wind";
    }
}
