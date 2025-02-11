using System;
using System.Collections.Generic;
using System.Linq;

namespace XM.Progression.Stat
{
    public class StatGroup: Dictionary<StatType, int>
    {
        public ResistCollection Resists { get; set; }

        public StatGroup()
        {
            Resists = new ResistCollection();
            PopulateStats();
        }

        private void PopulateStats()
        {
            foreach (var type in Enum.GetValues(typeof(StatType)).Cast<StatType>())
            {
                this[type] = 0;
            }
        }
    }
}
