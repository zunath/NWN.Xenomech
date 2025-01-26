using System;
using System.Collections.Generic;
using System.Linq;

namespace XM.Progression.Skill
{
    internal class SkillCollection: Dictionary<SkillType, int>
    {
        public SkillCollection()
        {
            foreach (var skill in Enum.GetValues(typeof(SkillType)).Cast<SkillType>())
            {
                this[skill] = 0;
            }
        }
    }
}
