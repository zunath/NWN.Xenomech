using System;
using System.Collections.Generic;
using XM.Shared.API.Constants;

namespace XM.Progression.Stat
{
    public class ItemStatCollection: Dictionary<InventorySlotType, ItemStat>
    {
        public ItemStatCollection()
        {
            foreach (var slot in Enum.GetValues<InventorySlotType>())
            {
                this[slot] = new ItemStat();
            }
        }
        public int CalculateHP()
        {
            var totalHP = 0;
            foreach (var (_, item) in this)
            {
                totalHP += item.HP;
            }

            return totalHP;
        }

        public int CalculateEP()
        {
            var totalEP = 0;
            foreach (var (_, item) in this)
            {
                totalEP += item.EP;
            }

            return totalEP;
        }

        public int CalculateHPRegen()
        {
            var totalHPRegen = 0;
            foreach (var (_, item) in this)
            {
                totalHPRegen += item.HPRegen;
            }

            return totalHPRegen;
        }

        public int CalculateEPRegen()
        {
            var totalEPRegen = 0;
            foreach (var (_, item) in this)
            {
                totalEPRegen += item.EPRegen;
            }

            return totalEPRegen;
        }

        public int CalculateRecastReduction()
        {
            var totalRecastReduction = 0;
            foreach (var (_, item) in this)
            {
                totalRecastReduction += item.RecastReduction;
            }

            return totalRecastReduction;
        }

        public int CalculateDefense()
        {
            var defense = 0;
            foreach (var (_, item) in this)
            {
                defense += item.Defense;
            }

            return defense;
        }

        public int CalculateEvasion()
        {
            var evasion = 0;
            foreach (var (_, item) in this)
            {
                evasion += item.Evasion;
            }

            return evasion;
        }

        public int CalculateAccuracy()
        {
            var accuracy = 0;
            foreach (var (_, item) in this)
            {
                accuracy += item.Accuracy;
            }

            return accuracy;
        }

        public int CalculateAttack()
        {
            var attack = 0;
            foreach (var (_, item) in this)
            {
                attack += item.Attack;
            }

            return attack;
        }

        public int CalculateEtherAttack()
        {
            var etherAttack = 0;
            foreach (var (_, item) in this)
            {
                etherAttack += item.EtherAttack;
            }

            return etherAttack;
        }

        public int CalculateTPGain()
        {
            var tpGain = 0;
            foreach (var (_, item) in this)
            {
                tpGain += item.TPGain;
            }

            return tpGain;
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
