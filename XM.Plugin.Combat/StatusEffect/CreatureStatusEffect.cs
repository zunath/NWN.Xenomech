using System.Collections.Generic;
using System.Linq;
using XM.Progression.Stat;

namespace XM.Plugin.Combat.StatusEffect
{
    internal class CreatureStatusEffect
    {
        private readonly HashSet<IStatusEffect> _activeEffects = new();

        public int HPRegen { get; set; }
        public int EPRegen { get; set; }
        public int Defense { get; set; }
        public int Evasion { get; set; }
        public int Accuracy { get; set; }
        public int Attack { get; set; }
        public int EtherAttack { get; set; }
        public Dictionary<ResistType, int> Resists { get; set; }

        public void Add(IStatusEffect statusEffect)
        {
            HPRegen += statusEffect.HPRegen;
            EPRegen += statusEffect.EPRegen;
            Defense += statusEffect.Defense;
            Evasion += statusEffect.Evasion;
            Accuracy += statusEffect.Accuracy;
            Attack += statusEffect.Attack;
            EtherAttack += statusEffect.EtherAttack;

            foreach (var (type, amount) in statusEffect.Resists)
            {
                Resists[type] += amount;
            }

            _activeEffects.Add(statusEffect);
        }

        public void Remove(IStatusEffect statusEffect)
        {
            HPRegen -= statusEffect.HPRegen;
            EPRegen -= statusEffect.EPRegen;
            Defense -= statusEffect.Defense;
            Evasion -= statusEffect.Evasion;
            Accuracy -= statusEffect.Accuracy;
            Attack -= statusEffect.Attack;
            EtherAttack -= statusEffect.EtherAttack;

            foreach (var (type, amount) in statusEffect.Resists)
            {
                Resists[type] -= amount;
            }

            _activeEffects.Remove(statusEffect);
        }

        public HashSet<IStatusEffect> GetAllEffects()
        {
            return _activeEffects.ToHashSet();
        }

        public CreatureStatusEffect()
        {
            Resists = new ResistCollection();
        }
    }
}
