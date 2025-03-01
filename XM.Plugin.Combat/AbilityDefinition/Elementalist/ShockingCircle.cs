using System.Collections.Generic;
using Anvil.Services;
using XM.Plugin.Combat.StatusEffectDefinition.Debuff;
using XM.Progression.Ability;
using XM.Progression.Recast;
using XM.Progression.Stat;
using XM.Progression.StatusEffect;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Plugin.Combat.AbilityDefinition.Elementalist
{
    [ServiceBinding(typeof(IAbilityListDefinition))]
    internal class ShockingCircle: IAbilityListDefinition
    {
        private readonly AbilityBuilder _builder = new();
        private readonly StatusEffectService _status;
        private readonly SpellService _spell;

        public ShockingCircle(
            StatusEffectService status,
            SpellService spell)
        {
            _status = status;
            _spell = spell;
        }

        public Dictionary<FeatType, AbilityDetail> BuildAbilities()
        {
            ShockingCircleAbility();

            return _builder.Build();
        }

        private void ShockingCircleAbility()
        {
            _builder.Create(FeatType.ShockingCircle)
                .Name(LocaleString.ShockingCircle)
                .Description(LocaleString.ShockingCircleDescription)
                .Classification(AbilityCategoryType.Offensive)
                .HasRecastDelay(RecastGroup.ShockingCircle, 60f * 5f)
                .HasActivationDelay(2f)
                .DisplaysVisualEffectWhenActivating()
                .IsCastedAbility()
                .RequirementEP(35)
                .ResonanceCost(2)
                .TelegraphSize(3f, 3f)
                .HasTelegraphSphereAction((activator, targets, targetLocation) =>
                {
                    foreach (var target in targets)
                    {
                        if (target == activator)
                            continue;

                        var duration = _spell.CalculateResistedTicks(activator, ResistType.Darkness, 30);
                        _status.ApplyStatusEffect<ParalyzeStatusEffect>(activator, target, duration);
                        ApplyEffectToObject(DurationType.Instant, EffectVisualEffect(VisualEffectType.ImpLightningSmall), target);
                    }
                });
        }
    }
}
