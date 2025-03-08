using Anvil.Services;
using XM.Shared.Core.Localization;

namespace XM.Progression.Skill.CraftingSkillDefinition
{
    [ServiceBinding(typeof(ICraftSkillDefinition))]
    internal class EngineeringCraftSkillDefinition: ICraftSkillDefinition
    {
        public SkillType Type => SkillType.Engineering;
        public LocaleString Name => LocaleString.Engineering;
        public LocaleString DeviceName => LocaleString.Assembler;
        public string IconResref => "ife_engineering";
        public int LevelCap => 100;
    }
}
