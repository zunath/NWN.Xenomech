using XM.Shared.Core.Localization;

namespace XM.Progression.Skill.SkillDefinition
{
    public interface ISkillDefinition
    {
        SkillType Type { get; }
        LocaleString Name { get; }
        string IconResref { get; }
    }
}
