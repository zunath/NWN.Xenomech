using XM.Shared.Core.Localization;

namespace XM.Progression.Skill.SkillDefinition
{
    public class StaffSkillDefinition: ISkillDefinition
    {
        public SkillType Type => SkillType.Staff;
        public LocaleString Name => LocaleString.Staff;
        public string IconResref => "skl_staff";
    }
}
