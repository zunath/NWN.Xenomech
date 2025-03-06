using XM.Shared.Core.Localization;

namespace XM.Progression.Skill.CraftingSkillDefinition
{
    internal class ArmorcraftCraftingSkillDefinition: ICraftingSkillDefinition
    {
        public SkillType Type => SkillType.Armorcraft;
        public LocaleString Name => LocaleString.Armorcraft;
        public string IconResref => "ife_armorcraft";
    }
}
