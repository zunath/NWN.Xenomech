using XM.Progression.Skill;

namespace XM.Plugin.Craft.UI
{
    internal class SelectCraftPayload
    {
        public SkillType Skill { get; }

        public SelectCraftPayload(SkillType skill)
        {
            Skill = skill;
        }
    }
}
