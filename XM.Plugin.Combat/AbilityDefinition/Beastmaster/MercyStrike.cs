using System.Collections.Generic;
using Anvil.Services;
using XM.Progression.Ability;
using XM.Progression.Recast;
using XM.Progression.Stat;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Plugin.Combat.AbilityDefinition.Beastmaster
{
    [ServiceBinding(typeof(IAbilityListDefinition))]
    internal class MercyStrike: IAbilityListDefinition
    {
        private readonly AbilityBuilder _builder = new();
        private readonly SpellService _spell;

        public MercyStrike(
            SpellService spell)
        {
            _spell = spell;
        }

        private void Impact(uint activator, List<uint> targets, int dmg)
        {
            ApplyEffectToObject(DurationType.Instant, EffectVisualEffect(VisualEffectType.ImpDeathWard), activator);

            foreach (var target in targets)
            {
                if (target == activator)
                    continue;

                var damage = _spell.CalculateSpellDamage(activator, target, dmg, ResistType.Earth, AbilityType.Might, AbilityType.Might);
                AssignCommand(activator, () =>
                {
                    ApplyEffectToObject(DurationType.Instant, EffectDamage(damage, DamageType.Darkness), target);
                });
            }
        }

        public Dictionary<FeatType, AbilityDetail> BuildAbilities()
        {
            MercyStrike1();
            MercyStrike2();

            return _builder.Build();
        }

        private void MercyStrike1()
        {
            _builder.Create(FeatType.MercyStrike1)
                .Name(LocaleString.MercyStrikeI)
                .Description(LocaleString.MercyStrikeIDescription)
                .Classification(AbilityCategoryType.Offensive)
                .TargetingType(AbilityTargetingType.SelfTargetsEnemy)
                .HasRecastDelay(RecastGroup.MercyStrike, 30f)
                .HasActivationDelay(2f)
                .DisplaysVisualEffectWhenActivating()
                .IsCastedAbility()
                .RequirementEP(18)
                .ResonanceCost(1)
                .TelegraphSize(4f, 4f)
                .HasTelegraphSphereAction((activator, targets, targetLocation) =>
                {
                    Impact(activator, targets, 24);
                });
        }

        private void MercyStrike2()
        {
            _builder.Create(FeatType.MercyStrike2)
                .Name(LocaleString.MercyStrikeII)
                .Description(LocaleString.MercyStrikeIIDescription)
                .Classification(AbilityCategoryType.Offensive)
                .TargetingType(AbilityTargetingType.SelfTargetsEnemy)
                .HasRecastDelay(RecastGroup.MercyStrike, 30f)
                .HasActivationDelay(2f)
                .DisplaysVisualEffectWhenActivating()
                .IsCastedAbility()
                .RequirementEP(34)
                .ResonanceCost(2)
                .TelegraphSize(4f, 4f)
                .HasTelegraphSphereAction((activator, targets, targetLocation) =>
                {
                    Impact(activator, targets, 44);
                });
        }
    }
}
