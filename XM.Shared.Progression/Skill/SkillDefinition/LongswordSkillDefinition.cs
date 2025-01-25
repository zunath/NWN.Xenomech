using XM.Shared.Core.Localization;

namespace XM.Progression.Skill.SkillDefinition
{
    public class LongswordSkillDefinition: ISkillDefinition
    {
        public SkillType Type => SkillType.Longsword;
        public LocaleString Name => LocaleString.Longsword;
        public string IconResref => "skl_longsword";
    }
}
