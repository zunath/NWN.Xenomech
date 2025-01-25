using XM.Shared.Core.Localization;

namespace XM.Progression.Skill.SkillDefinition
{
    public class DaggerSkillDefinition: ISkillDefinition
    {
        public SkillType Type => SkillType.Dagger;
        public LocaleString Name => LocaleString.Dagger;
        public string IconResref => "skl_dagger";
    }
}
