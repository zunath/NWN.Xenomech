using System.Collections.Generic;
using Anvil.Services;
using XM.Progression.Ability;
using XM.Progression.Recast;
using XM.Progression.Stat;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Plugin.Combat.AbilityDefinition.Keeper
{
    [ServiceBinding(typeof(IAbilityListDefinition))]
    internal class RadiantBlast: IAbilityListDefinition
    {
        private readonly AbilityBuilder _builder = new();

        private readonly SpellService _spell;

        public RadiantBlast(SpellService spell)
        {
            _spell = spell;
        }

        public Dictionary<FeatType, AbilityDetail> BuildAbilities()
        {
            RadiantBlast1();
            RadiantBlast2();

            return _builder.Build();
        }

        private void Impact(
            uint activator, 
            uint target, 
            int baseDMG)
        {
            var damage = _spell.CalculateSpellDamage(activator, target, baseDMG, ResistType.Light);

            AssignCommand(activator, () => ApplyEffectToObject(DurationType.Instant, EffectDamage(damage, DamageType.Light), target));
            AssignCommand(activator, () => ApplyEffectToObject(DurationType.Temporary, EffectVisualEffect(VisualEffectType.BeamHoly), target, 2f));
        }

        private void RadiantBlast1()
        {
            _builder.Create(FeatType.RadiantBlast1)
                .Name(LocaleString.RadiantBlastI)
                .Description(LocaleString.RadiantBlastIDescription)
                .HasRecastDelay(RecastGroup.RadiantBlast, 30f)
                .DisplaysVisualEffectWhenActivating()
                .HasActivationDelay(2f)
                .UsesAnimation(AnimationType.LoopingConjure1)
                .IsCastedAbility()
                .HasMaxRange(8f)
                .RequirementEP(8)
                .ResonanceCost(1)
                .HasImpactAction((activator, target, location) =>
                {
                    Impact(activator, target, 30);
                });
        }

        private void RadiantBlast2()
        {
            _builder.Create(FeatType.RadiantBlast2)
                .Name(LocaleString.RadiantBlastII)
                .Description(LocaleString.RadiantBlastIIDescription)
                .HasRecastDelay(RecastGroup.RadiantBlast, 4f)
                .DisplaysVisualEffectWhenActivating()
                .HasActivationDelay(2f)
                .UsesAnimation(AnimationType.LoopingConjure1)
                .IsCastedAbility()
                .HasMaxRange(8f)
                .RequirementEP(22)
                .ResonanceCost(2)
                .HasImpactAction((activator, target, location) =>
                {
                    Impact(activator, target, 60);
                });
        }
    }
}
