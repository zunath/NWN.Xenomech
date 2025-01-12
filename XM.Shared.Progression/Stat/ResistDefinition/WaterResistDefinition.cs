using XM.Shared.Core.Localization;

namespace XM.Progression.Stat.ResistDefinition
{
    public class WaterResistDefinition: IResistDefinition
    {
        public LocaleString Name => LocaleString.WaterResist;
        public string IconResref => "resist_water";
    }
}
