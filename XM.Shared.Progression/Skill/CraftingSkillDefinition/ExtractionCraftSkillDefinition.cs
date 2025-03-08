using Anvil.Services;
using XM.Shared.Core.Localization;

namespace XM.Progression.Skill.CraftingSkillDefinition
{
    [ServiceBinding(typeof(ICraftSkillDefinition))]
    internal class ExtractionCraftSkillDefinition: ICraftSkillDefinition
    {
        public SkillType Type => SkillType.Extraction;
        public LocaleString Name => LocaleString.Extraction;
        public LocaleString DeviceName => LocaleString.Extractor;
        public string IconResref => "ife_extraction";
        public int LevelCap => 100;
    }
}
