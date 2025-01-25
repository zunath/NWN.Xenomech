using XM.Shared.Core.Localization;

namespace XM.Progression.Skill.SkillDefinition
{
    public class PistolSkillDefinition: ISkillDefinition
    {
        public SkillType Type => SkillType.Pistol;
        public LocaleString Name => LocaleString.Pistol;
        public string IconResref => "skl_pistol";
    }
}
