using XM.Shared.Core.Localization;

namespace XM.Progression.Skill.SkillDefinition
{
    public class RifleSkillDefinition: ISkillDefinition
    {
        public SkillType Type => SkillType.Rifle;
        public LocaleString Name => LocaleString.Rifle;
        public string IconResref => "skl_rifle";
    }
}
