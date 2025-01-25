using XM.Shared.Core.Localization;

namespace XM.Progression.Skill.SkillDefinition
{
    public class HandToHandSkillDefinition: ISkillDefinition
    {
        public SkillType Type => SkillType.HandToHand;
        public LocaleString Name => LocaleString.HandToHand;
        public string IconResref => "skl_handtohand";
    }
}
