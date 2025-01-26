using XM.Shared.Core.Localization;

namespace XM.Progression.Stat.ResistDefinition
{
    public class FireResistDefinition: IResistDefinition
    {
        public ResistType Type => ResistType.Fire;
        public LocaleString Name => LocaleString.FireResist;
        public string IconResref => "resist_fire";
    }
}
