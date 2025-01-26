using XM.Shared.Core.Localization;

namespace XM.Progression.Stat.ResistDefinition
{
    public class LightResistDefinition: IResistDefinition
    {
        public ResistType Type => ResistType.Light;
        public LocaleString Name => LocaleString.LightResist;
        public string IconResref => "resist_light";
    }
}
