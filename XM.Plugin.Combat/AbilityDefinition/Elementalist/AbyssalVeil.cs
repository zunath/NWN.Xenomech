using System.Collections.Generic;
using Anvil.Services;
using XM.Plugin.Combat.StatusEffectDefinition.Debuff;
using XM.Progression.Ability;
using XM.Progression.Recast;
using XM.Progression.Stat;
using XM.Progression.StatusEffect;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;
using DamageType = XM.Shared.API.Constants.DamageType;

namespace XM.Plugin.Combat.AbilityDefinition.Elementalist
{
    [ServiceBinding(typeof(IAbilityListDefinition))]
    internal class AbyssalVeil: IAbilityListDefinition
    {
        private readonly AbilityBuilder _builder = new();
        private readonly SpellService _spell;
        private readonly StatusEffectService _status;

        public AbyssalVeil(
            StatusEffectService status,
            SpellService spell)
        {
            _spell = spell;
            _status = status;
        }

        public Dictionary<FeatType, AbilityDetail> BuildAbilities()
        {
            AbyssalVeil1();
            AbyssalVeil2();

            return _builder.Build();
        }

        private void Impact<T>(uint activator, uint target, int dmg)
            where T: IStatusEffect
        {
            var damage = _spell.CalculateSpellDamage(activator, target, dmg, ResistType.Darkness);
            damage = _spell.CalculateElementalSealBonus(activator, damage);

            AssignCommand(activator, () =>
            {
                ApplyEffectToObject(DurationType.Instant, EffectDamage(damage, DamageType.Darkness), target);
            });
            AssignCommand(activator, () =>
            {
                ApplyEffectToObject(DurationType.Instant, EffectVisualEffect(VisualEffectType.BeamOdd), target);
            });

            var duration = _spell.CalculateResistedTicks(target, ResistType.Darkness, 20);
            _status.ApplyStatusEffect<T>(activator, target, duration);
        }

        private void AbyssalVeil1()
        {
            _builder.Create(FeatType.AbyssalVeil1)
                .Name(LocaleString.AbyssalVeilI)
                .Description(LocaleString.AbyssalVeilIDescription)
                .HasRecastDelay(RecastGroup.AbyssalVeil, 12f)
                .HasActivationDelay(3f)
                .DisplaysVisualEffectWhenActivating()
                .UsesAnimation(AnimationType.LoopingConjure1)
                .IsCastedAbility()
                .RequirementEP(31)
                .ResonanceCost(1)
                .HasMaxRange(10f)
                .IsHostile()
                .HasImpactAction((activator, target, location) =>
                {
                    Impact<AbyssalVeil1StatusEffect>(activator, target, 38);
                });
        }

        private void AbyssalVeil2()
        {
            _builder.Create(FeatType.AbyssalVeil2)
                .Name(LocaleString.AbyssalVeilII)
                .Description(LocaleString.AbyssalVeilIIDescription)
                .HasRecastDelay(RecastGroup.AbyssalVeil, 12f)
                .HasActivationDelay(3f)
                .DisplaysVisualEffectWhenActivating()
                .UsesAnimation(AnimationType.LoopingConjure1)
                .IsCastedAbility()
                .RequirementEP(62)
                .ResonanceCost(2)
                .HasMaxRange(10f)
                .IsHostile()
                .HasImpactAction((activator, target, location) =>
                {
                    Impact<AbyssalVeil2StatusEffect>(activator, target, 60);
                });
        }
    }
}
