using System.Collections.Generic;
using Anvil.Services;
using XM.Progression.Ability;
using XM.Progression.Recast;
using XM.Progression.Stat;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Plugin.Combat.AbilityDefinition.Mender
{
    [ServiceBinding(typeof(IAbilityListDefinition))]
    internal class Tsu: IAbilityListDefinition
    {
        private readonly AbilityBuilder _builder = new();

        private readonly SpellService _spell;

        public Tsu(SpellService spell)
        {
            _spell = spell;
        }

        public Dictionary<FeatType, AbilityDetail> BuildAbilities()
        {
            RadiantBlast1();

            return _builder.Build();
        }

        private void Impact(
            uint activator, 
            uint target, 
            int baseDMG)
        {
            var damage = _spell.CalculateSpellDamage(activator, target, baseDMG, ResistType.Light);

            AssignCommand(activator, () => ApplyEffectToObject(DurationType.Instant, EffectDamage(damage, DamageType.Light), target));
            AssignCommand(activator, () => ApplyEffectToObject(DurationType.Instant, EffectVisualEffect(VisualEffectType.ImpBreach), target));
        }

        private void RadiantBlast1()
        {
            _builder.Create(FeatType.Tsu)
                .Name(LocaleString.Tsu)
                .Description(LocaleString.TsuDescription)
                .HasRecastDelay(RecastGroup.Tsu, 30f)
                .DisplaysVisualEffectWhenActivating()
                .HasActivationDelay(1f)
                .UsesAnimation(AnimationType.LoopingConjure1)
                .IsCastedAbility()
                .HasMaxRange(5f)
                .RequirementEP(100)
                .ResonanceCost(2)
                .HasImpactAction((activator, target, location) =>
                {
                    Impact(activator, target, 150);
                });
        }
    }
}
