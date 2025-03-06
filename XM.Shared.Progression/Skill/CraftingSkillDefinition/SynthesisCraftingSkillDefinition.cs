using XM.Shared.Core.Localization;

namespace XM.Progression.Skill.CraftingSkillDefinition
{
    internal class SynthesisCraftingSkillDefinition: ICraftingSkillDefinition
    {
        public SkillType Type => SkillType.Synthesis;
        public LocaleString Name => LocaleString.Synthesis;
        public string IconResref => "ife_synthesis";
    }
}
