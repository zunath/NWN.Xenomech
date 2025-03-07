using Anvil.Services;
using XM.Shared.Core.Localization;

namespace XM.Progression.Skill.CraftingSkillDefinition
{
    [ServiceBinding(typeof(ICraftSkillDefinition))]
    internal class SynthesisCraftSkillDefinition: ICraftSkillDefinition
    {
        public SkillType Type => SkillType.Synthesis;
        public LocaleString Name => LocaleString.Synthesis;
        public string IconResref => "ife_synthesis";
        public int LevelCap => 100;
    }
}
