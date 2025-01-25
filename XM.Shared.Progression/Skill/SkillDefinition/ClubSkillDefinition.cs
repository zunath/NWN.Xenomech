using XM.Shared.Core.Localization;

namespace XM.Progression.Skill.SkillDefinition
{
    public class ClubSkillDefinition: ISkillDefinition
    {
        public SkillType Type => SkillType.Club;
        public LocaleString Name => LocaleString.Club;
        public string IconResref => "skl_club";
    }
}
