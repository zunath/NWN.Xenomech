using XM.Shared.Core.Localization;

namespace XM.Progression.Skill.SkillDefinition
{
    public class GreatAxeSkillDefinition: ISkillDefinition
    {
        public SkillType Type => SkillType.GreatAxe;
        public LocaleString Name => LocaleString.GreatAxe;
        public string IconResref => "skl_greataxe";
    }
}
