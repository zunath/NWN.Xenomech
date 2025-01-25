using XM.Shared.Core.Localization;

namespace XM.Progression.Skill.SkillDefinition
{
    public class GreatSwordSkillDefinition: ISkillDefinition
    {
        public SkillType Type => SkillType.GreatSword;
        public LocaleString Name => LocaleString.GreatSword;
        public string IconResref => "skl_greatsword";
    }
}
