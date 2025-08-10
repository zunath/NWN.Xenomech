using XM.Progression.Stat;
using XM.Shared.Core.Entity.Stat;
using AbilityStatCollection = XM.Shared.Core.Entity.Stat.AbilityStatCollection;
using ItemStatCollection = XM.Shared.Core.Entity.Stat.ItemStatCollection;
using StatGroup = XM.Shared.Core.Entity.Stat.StatGroup;

namespace XM.Shared.Progression.Stat
{
    internal static class CoreStatExtensions
    {
        public static int GetStat(this StatGroup group, StatType type)
        {
            return group.Stats.TryGetValue((int)type, out var value) ? value : 0;
        }

        public static void SetStat(this StatGroup group, StatType type, int value)
        {
            group.Stats[(int)type] = value;
        }

        public static int GetResist(this StatGroup group, ResistType type)
        {
            return group.Resists.TryGetValue((int)type, out var value) ? value : 0;
        }

        public static void SetResist(this StatGroup group, ResistType type, int value)
        {
            group.Resists[(int)type] = value;
        }

        public static int CalculateStat(this ItemStatCollection items, StatType type)
        {
            var total = 0;
            foreach (var (_, item) in items)
            {
                var val = item.Stats.TryGetValue((int)type, out var v) ? v : 0;
                total += (int)(val * item.Condition);
            }
            return total;
        }

        public static int CalculateResist(this ItemStatCollection items, ResistType type)
        {
            var total = 0;
            foreach (var (_, item) in items)
            {
                var val = item.Resists.TryGetValue((int)type, out var v) ? v : 0;
                total += (int)(val * item.Condition);
            }
            return total;
        }

        public static int CalculateStat(this AbilityStatCollection abilities, StatType type)
        {
            var total = 0;
            foreach (var (_, group) in abilities)
            {
                var val = group.Stats.TryGetValue((int)type, out var v) ? v : 0;
                total += val;
            }
            return total;
        }

        public static int CalculateResist(this AbilityStatCollection abilities, ResistType type)
        {
            var total = 0;
            foreach (var (_, group) in abilities)
            {
                var val = group.Resists.TryGetValue((int)type, out var v) ? v : 0;
                total += val;
            }
            return total;
        }
    }
}


