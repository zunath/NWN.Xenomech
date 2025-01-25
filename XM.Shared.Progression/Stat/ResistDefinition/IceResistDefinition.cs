using XM.Shared.Core.Localization;

namespace XM.Progression.Stat.ResistDefinition
{
    public class IceResistDefinition: IResistDefinition
    {
        public ResistType Type => ResistType.Ice;
        public LocaleString Name => LocaleString.IceResist;
        public string IconResref => "resist_ice";
    }
}
