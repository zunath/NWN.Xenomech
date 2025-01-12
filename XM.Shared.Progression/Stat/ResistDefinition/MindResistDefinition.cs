using XM.Shared.Core.Localization;

namespace XM.Progression.Stat.ResistDefinition
{
    public class MindResistDefinition: IResistDefinition
    {
        public LocaleString Name => LocaleString.MindResist;
        public string IconResref => "resist_mind";
    }
}
