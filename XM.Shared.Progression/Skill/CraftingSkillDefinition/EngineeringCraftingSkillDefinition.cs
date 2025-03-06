using XM.Shared.Core.Localization;

namespace XM.Progression.Skill.CraftingSkillDefinition
{
    internal class EngineeringCraftingSkillDefinition: ICraftingSkillDefinition
    {
        public SkillType Type => SkillType.Engineering;
        public LocaleString Name => LocaleString.Engineering;
        public string IconResref => "ife_engineering";
    }
}
