using System.Collections.Generic;
using XM.Shared.API.Constants;

namespace XM.Progression.Stat
{
    public class AbilityStatCollection: Dictionary<FeatType, StatGroup>
    {
        public int CalculateStat(StatType type)
        {
            var total = 0;
            foreach (var (_, ability) in this)
            {
                total += ability.Stats[type];
            }

            return total;
        }
        public int CalculateResist(ResistType resistType)
        {
            var resist = 0;

            foreach (var (_, item) in this)
            {
                resist += item.Resists[resistType];
            }

            return resist;
        }
    }
}
