using Anvil.Services;
using XM.Shared.Core.Localization;

namespace XM.Progression.Skill.CraftingSkillDefinition
{
    [ServiceBinding(typeof(ICraftSkillDefinition))]
    internal class FabricationCraftSkillDefinition: ICraftSkillDefinition
    {
        public SkillType Type => SkillType.Fabrication;
        public LocaleString Name => LocaleString.Fabrication;
        public string IconResref => "ife_fabrication";
        public int LevelCap => 100;
    }
}
