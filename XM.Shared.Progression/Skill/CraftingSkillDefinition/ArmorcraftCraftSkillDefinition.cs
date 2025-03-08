using Anvil.Services;
using XM.Shared.Core.Localization;

namespace XM.Progression.Skill.CraftingSkillDefinition
{
    [ServiceBinding(typeof(ICraftSkillDefinition))]
    internal class ArmorcraftCraftSkillDefinition: ICraftSkillDefinition
    {
        public SkillType Type => SkillType.Armorcraft;
        public LocaleString Name => LocaleString.Armorcraft;
        public LocaleString DeviceName => LocaleString.Foundry;
        public string IconResref => "ife_armorcraft";
        public int LevelCap => 100;
    }
}
