using XM.Shared.Core.Localization;

namespace XM.Progression.Skill.CraftingSkillDefinition
{
    public interface ICraftSkillDefinition
    {
        SkillType Type { get; }
        LocaleString Name { get; }
        LocaleString DeviceName { get; }
        string IconResref { get; }
        int LevelCap { get; }
    }
}
