using XM.Shared.Core.Localization;

namespace XM.Progression.Skill.SkillDefinition
{
    public class AxeSkillDefinition: ISkillDefinition
    {
        public SkillType Type => SkillType.Axe;
        public LocaleString Name => LocaleString.Axe;
        public string IconResref => "skl_axe";
    }
}
