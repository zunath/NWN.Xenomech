using System.Collections.Generic;
using Anvil.Services;
using XM.Plugin.Combat.StatusEffectDefinition.Debuff;
using XM.Progression.Ability;
using XM.Progression.Recast;
using XM.Progression.Stat;
using XM.Progression.StatusEffect;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Plugin.Combat.AbilityDefinition.Techweaver
{
    [ServiceBinding(typeof(IAbilityListDefinition))]
    internal class Rupture: IAbilityListDefinition
    {
        private readonly AbilityBuilder _builder = new();

        private readonly StatusEffectService _status;
        private readonly SpellService _spell;

        public Rupture(
            StatusEffectService status,
            SpellService spell)
        {
            _status = status;
            _spell = spell;
        }

        public Dictionary<FeatType, AbilityDetail> BuildAbilities()
        {
            RuptureAbility();

            return _builder.Build();
        }

        private void RuptureAbility()
        {
            _builder.Create(FeatType.Rupture)
                .Name(LocaleString.Rupture)
                .Description(LocaleString.RuptureDescription)
                .Classification(AbilityCategoryType.Offensive)
                .HasRecastDelay(RecastGroup.Rupture, 18f)
                .HasActivationDelay(4f)
                .UsesAnimation(AnimationType.LoopingConjure1)
                .DisplaysVisualEffectWhenActivating()
                .RequirementEP(32)
                .ResonanceCost(1)
                .HasImpactAction((activator, target, location) =>
                {
                    var duration = _spell.CalculateResistedTicks(target, ResistType.Mind, 40);
                    _status.ApplyStatusEffect<RuptureStatusEffect>(activator, target, duration);
                });
        }
    }
}
