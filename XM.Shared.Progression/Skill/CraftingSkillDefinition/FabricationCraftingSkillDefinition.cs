using XM.Shared.Core.Localization;

namespace XM.Progression.Skill.CraftingSkillDefinition
{
    internal class FabricationCraftingSkillDefinition: ICraftingSkillDefinition
    {
        public SkillType Type => SkillType.Fabrication;
        public LocaleString Name => LocaleString.Fabrication;
        public string IconResref => "ife_fabrication";
    }
}
