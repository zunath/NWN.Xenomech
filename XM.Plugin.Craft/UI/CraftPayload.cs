using XM.Progression.Skill;

namespace XM.Plugin.Craft.UI
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
