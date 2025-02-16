using System;
using Anvil.Services;
using XM.Progression.Stat;
using XM.Shared.API.Constants;
using XM.Shared.Core;

namespace XM.Plugin.Combat
{
    [ServiceBinding(typeof(SpellService))]
    internal class SpellService
    {
        private readonly StatService _stat;

        public SpellService(StatService stat)
        {
            _stat = stat;
        }

        private float CalculateResistDamageReduction(uint creature, ResistType resistType)
        {
            var resist = _stat.GetResist(creature, resistType);
            if (resist <= 0)
                return 1f;
            if (resist >= 100)
                return 0.1f;

            return Math.Max(1f - (resist / (resist + 50f)), 0.1f);
        }

        private int CalculateStatDelta(
            uint caster, 
            uint target, 
            AbilityType casterStatType, 
            AbilityType targetStatType)
        {
            var casterWIL = _stat.GetAttribute(caster, casterStatType);
            var targetWIL = _stat.GetAttribute(target, targetStatType);

            return Math.Clamp(casterWIL - targetWIL, -15, 15);
        }

        private int CalculateLevelDelta(uint caster, uint target)
        {
            var casterLevel = _stat.GetLevel(caster);
            var targetLevel = _stat.GetLevel(target);
            return casterLevel - targetLevel;
        }

        private float CalculateEtherAttackDefenseRatio(uint caster, uint target)
        {
            var etherAttack = _stat.GetEtherAttack(caster);
            var etherDefense = _stat.GetDefense(target);
            var numerator = 100f + etherAttack;
            var denominator = 100f + etherDefense;
            if (denominator == 0)
                denominator = 1;

            return numerator / denominator;
        }

        public int CalculateSpellDamage(
            uint caster,
            uint target,
            int baseDamage,
            ResistType resistType,
            AbilityType casterStatType = AbilityType.Willpower,
            AbilityType targetStatType = AbilityType.Willpower)
        {
            var resistReduction = CalculateResistDamageReduction(target, resistType);
            var wilDelta = CalculateStatDelta(caster, target, casterStatType, targetStatType);
            var levelDelta = CalculateLevelDelta(caster, target);
            var etherAttackDefenseDelta = Math.Clamp(CalculateEtherAttackDefenseRatio(caster, target), 0.5f, 2.0f);

            // Spell power scaling from Willpower and Level
            var spellMultiplier = 1.0f + (wilDelta * 0.05f) + (levelDelta * 0.05f);
            spellMultiplier = Math.Clamp(spellMultiplier, 0.5f, 1.75f);

            // Calculate raw damage before applying randomness
            var rawDamage = (int)(baseDamage * spellMultiplier * resistReduction * etherAttackDefenseDelta);
            var damageMax = Math.Max(rawDamage, 1);
            var damageMin = Math.Max((int)(damageMax * 0.75f), 1);

            return XMRandom.Next(damageMin, damageMax);
        }

        public int CalculateResistedTicks(uint creature, ResistType resistType, int baseTicks)
        {
            var resist = _stat.GetResist(creature, resistType);

            if (resist >= 100)
                return 0; // Full immunity
            if (resist <= 0)
                return baseTicks; // No resistance, full effect

            float baseMultiplier;

            if (resist >= 90) // Special handling for extreme resistance (90-99)
            {
                var extremeResistFactor = 1f - ((resist - 90f) / 200f); // Much slower falloff
                baseMultiplier = (1f - (90f / 150f)) * extremeResistFactor; // Prevents full negation
            }
            else
            {
                baseMultiplier = 1f - (resist / 150f);
            }

            // Add controlled randomness
            var varianceFactor = XMRandom.NextFloat(-0.03f, 0.03f); // Smaller range (-3% to +3%)
            var finalMultiplier = Math.Clamp(baseMultiplier + varianceFactor, 0.1f, 1f); // Never below 10% duration

            // Calculate final tick count, ensuring at least 1 tick
            var finalTicks = Math.Max((int)Math.Round(baseTicks * finalMultiplier), 1);

            return finalTicks;
        }
    }
}
