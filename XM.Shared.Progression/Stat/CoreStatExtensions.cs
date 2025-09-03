using System.Collections.Generic;

namespace XM.Progression.Stat
{
    internal static class CoreStatExtensions
    {
        public static int GetStat(this Shared.Core.Entity.Stat.StatGroup group, StatType type)
        {
            return group.Stats.GetValueOrDefault((int)type, 0);
        }

        public static void SetStat(this Shared.Core.Entity.Stat.StatGroup group, StatType type, int value)
        {
            group.Stats[(int)type] = value;
        }

        public static int GetResist(this Shared.Core.Entity.Stat.StatGroup group, ResistType type)
        {
            return group.Resists.GetValueOrDefault((int)type, 0);
        }

        public static void SetResist(this Shared.Core.Entity.Stat.StatGroup group, ResistType type, int value)
        {
            group.Resists[(int)type] = value;
        }

        public static int CalculateStat(this Shared.Core.Entity.Stat.ItemStatCollection items, StatType type)
        {
            var total = 0;
            foreach (var (_, item) in items)
            {
                var val = item.Stats.GetValueOrDefault((int)type, 0);
                total += (int)(val * item.Condition);
            }
            return total;
        }

        public static int CalculateResist(this Shared.Core.Entity.Stat.ItemStatCollection items, ResistType type)
        {
            var total = 0;
            foreach (var (_, item) in items)
            {
                var val = item.Resists.GetValueOrDefault((int)type, 0);
                total += (int)(val * item.Condition);
            }
            return total;
        }

        public static int CalculateStat(this Shared.Core.Entity.Stat.AbilityStatCollection abilities, StatType type)
        {
            var total = 0;
            foreach (var (_, group) in abilities)
            {
                var val = group.Stats.GetValueOrDefault((int)type, 0);
                total += val;
            }
            return total;
        }

        public static int CalculateResist(this Shared.Core.Entity.Stat.AbilityStatCollection abilities, ResistType type)
        {
            var total = 0;
            foreach (var (_, group) in abilities)
            {
                var val = group.Resists.GetValueOrDefault((int)type, 0);
                total += val;
            }
            return total;
        }
    }
}


