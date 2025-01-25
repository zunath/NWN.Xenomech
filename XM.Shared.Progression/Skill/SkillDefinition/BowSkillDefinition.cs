using XM.Shared.Core.Localization;

namespace XM.Progression.Skill.SkillDefinition
{
    public class BowSkillDefinition: ISkillDefinition
    {
        public SkillType Type => SkillType.Bow;
        public LocaleString Name => LocaleString.Bow;
        public string IconResref => "skl_bow";
    }
}
