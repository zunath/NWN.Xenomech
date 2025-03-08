using XM.Progression.Skill;

namespace XM.Progression.Craft.UI
{
    internal class CraftPayload
    {
        public SkillType Skill { get; }

        public CraftPayload(SkillType skill)
        {
            Skill = skill;
        }
    }
}
