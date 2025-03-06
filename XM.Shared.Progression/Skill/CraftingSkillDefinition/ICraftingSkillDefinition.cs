using XM.Shared.Core.Localization;

namespace XM.Progression.Skill.CraftingSkillDefinition
{
    public interface ICraftingSkillDefinition
    {
        SkillType Type { get; }
        LocaleString Name { get; }
        string IconResref { get; }
    }
}
