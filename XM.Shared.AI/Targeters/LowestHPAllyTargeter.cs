﻿namespace XM.AI.Targeters
{
    internal class LowestHPAllyTargeter: IAITargeter
    {
        public uint SelectTarget(IAIContext context)
        {
            var stat = context.Services.Stat;
            var lowest = OBJECT_INVALID;
            var lowestPercentage = 100f;

            foreach (var ally in context.GetNearbyFriendlies())
            {
                if (GetIsDead(ally))
                    continue;

                var hpPercentage = (float)stat.GetCurrentHP(ally) / (float)stat.GetMaxHP(ally);
                if (hpPercentage < lowestPercentage)
                {
                    lowestPercentage = hpPercentage;
                    lowest = ally;
                }
            }

            return lowest;
        }
    }
}
