using XM.Shared.Core.Localization;

namespace XM.Progression.Skill.SkillDefinition
{
    public class ShortSwordSkillDefinition: ISkillDefinition
    {
        public SkillType Type => SkillType.ShortSword;
        public LocaleString Name => LocaleString.ShortSword;
        public string IconResref => "skl_shortsword";
    }
}
