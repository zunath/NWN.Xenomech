using Anvil.Services;
using XM.Shared.Core.Localization;

namespace XM.Progression.Skill.CraftingSkillDefinition
{
    [ServiceBinding(typeof(ICraftSkillDefinition))]
    internal class WeaponcraftCraftSkillDefinition: ICraftSkillDefinition
    {
        public SkillType Type => SkillType.Weaponcraft;
        public LocaleString Name => LocaleString.Weaponcraft;
        public string IconResref => "ife_weaponcraft";
        public int LevelCap => 100;
    }
}
