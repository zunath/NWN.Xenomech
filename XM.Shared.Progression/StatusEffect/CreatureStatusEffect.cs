using System;
using System.Collections.Generic;
using System.Linq;
using XM.Progression.Stat;

namespace XM.Progression.StatusEffect
{
    public class CreatureStatusEffect
    {
        private readonly HashSet<IStatusEffect> _activeEffects = new();

        public StatGroup Stats { get; set; }

        public void Add(IStatusEffect statusEffect)
        {
            foreach (var (type, value) in statusEffect.Stats)
            {
                Stats[type] += value;
            }

            foreach (var (type, value) in statusEffect.Stats.Resists)
            {
                Stats.Resists[type] += value;
            }

            _activeEffects.Add(statusEffect);
        }

        public void Remove(IStatusEffect statusEffect)
        {
            foreach (var (type, value) in statusEffect.Stats)
            {
                Stats[type] -= value;
            }

            foreach (var (type, value) in statusEffect.Stats.Resists)
            {
                Stats.Resists[type] -= value;
            }

            _activeEffects.Remove(statusEffect);
        }

        public HashSet<IStatusEffect> GetAllEffects()
        {
            return _activeEffects.ToHashSet();
        }

        public bool HasEffect(Type effectType)
        {
            return _activeEffects.Any(x => x.GetType() == effectType);
        }

        public CreatureStatusEffect()
        {
            Stats = new StatGroup();
        }
    }
}
