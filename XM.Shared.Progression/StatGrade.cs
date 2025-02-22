using System.Collections.Generic;
using XM.Progression.Skill;

namespace XM.Progression
{
    public class StatGrade
    {
        public GradeType MaxHP { get; set; }
        public GradeType MaxEP { get; set; }
        public GradeType Might { get; set; }
        public GradeType Perception { get; set; }
        public GradeType Vitality { get; set; }
        public GradeType Agility { get; set; }
        public GradeType Willpower { get; set; }
        public GradeType Social { get; set; }
        public GradeType DMG { get; set; }

        public GradeType Evasion { get; set; }

        public Dictionary<SkillType, GradeType> SkillGrades { get; set; }

        public StatGrade()
        {
            SkillGrades = new Dictionary<SkillType, GradeType>();
        }
    }
}
