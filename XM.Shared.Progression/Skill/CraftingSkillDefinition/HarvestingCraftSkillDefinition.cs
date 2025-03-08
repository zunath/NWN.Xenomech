using Anvil.Services;
using XM.Shared.Core.Localization;

namespace XM.Progression.Skill.CraftingSkillDefinition
{
    [ServiceBinding(typeof(ICraftSkillDefinition))]
    internal class HarvestingCraftSkillDefinition: ICraftSkillDefinition
    {
        public SkillType Type => SkillType.Harvesting;
        public LocaleString Name => LocaleString.Harvesting;
        public LocaleString DeviceName => LocaleString.Harvester;
        public string IconResref => "ife_harvesting";
        public int LevelCap => 100;
    }
}
