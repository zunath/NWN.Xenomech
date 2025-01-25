using XM.Shared.Core.Localization;

namespace XM.Progression.Skill.SkillDefinition
{
    public class PolearmSkillDefinition: ISkillDefinition
    {
        public SkillType Type => SkillType.Polearm;
        public LocaleString Name => LocaleString.Polearm;
        public string IconResref => "skl_polearm";
    }
}
