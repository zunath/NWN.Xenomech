using System;
using System.Collections.Generic;
using System.Linq;

namespace XM.Progression.Stat
{
    public class StatGroup
    {
        public Dictionary<StatType, int> Stats { get; set; }
        public ResistCollection Resists { get; set; }

        public StatGroup()
        {
            Stats = new Dictionary<StatType, int>();
            Resists = new ResistCollection();
            PopulateStats();
        }

        private void PopulateStats()
        {
            foreach (var type in Enum.GetValues(typeof(StatType)).Cast<StatType>())
            {
                Stats[type] = 0;
            }
        }
    }
}
