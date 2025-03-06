using XM.Shared.Core.Localization;

namespace XM.Progression.Skill.CraftingSkillDefinition
{
    internal class WeaponcraftCraftingSkillDefinition: ICraftingSkillDefinition
    {
        public SkillType Type => SkillType.Weaponcraft;
        public LocaleString Name => LocaleString.Weaponcraft;
        public string IconResref => "ife_weaponcraft";
    }
}
