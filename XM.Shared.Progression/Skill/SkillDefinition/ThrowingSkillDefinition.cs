using XM.Shared.Core.Localization;

namespace XM.Progression.Skill.SkillDefinition
{
    public class ThrowingSkillDefinition: ISkillDefinition
    {
        public SkillType Type => SkillType.Throwing;
        public LocaleString Name => LocaleString.Throwing;
        public string IconResref => "skl_throwing";
    }
}
