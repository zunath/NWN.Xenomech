using XM.Shared.Core.Localization;

namespace XM.Progression.Stat.ResistDefinition
{
    public class LightningResistDefinition: IResistDefinition
    {
        public LocaleString Name => LocaleString.LightningResist;
        public string IconResref => "resist_lightning";
    }
}
