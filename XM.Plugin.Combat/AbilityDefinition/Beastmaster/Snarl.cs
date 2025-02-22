using System.Collections.Generic;
using Anvil.Services;
using XM.Plugin.Combat.StatusEffectDefinition.Buff;
using XM.Progression.Ability;
using XM.Progression.Recast;
using XM.Progression.Stat;
using XM.Progression.StatusEffect;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Plugin.Combat.AbilityDefinition.Beastmaster
{
    [ServiceBinding(typeof(IAbilityListDefinition))]
    internal class Snarl: IAbilityListDefinition
    {
        private readonly AbilityBuilder _builder = new();

        private readonly StatusEffectService _status;
        private readonly SpellService _spell;

        public Snarl(
            StatusEffectService status,
            SpellService spell)
        {
            _status = status;
            _spell = spell;
        }

        public Dictionary<FeatType, AbilityDetail> BuildAbilities()
        {
            SnarlAbility();

            return _builder.Build();
        }

        private void SnarlAbility()
        {
            _builder.Create(FeatType.Snarl)
                .Name(LocaleString.Snarl)
                .Description(LocaleString.SnarlDescription)
                .HasRecastDelay(RecastGroup.Snarl, 30f)
                .HasActivationDelay(2f)
                .RequirementEP(20)
                .UsesAnimation(AnimationType.LoopingConjure1)
                .DisplaysVisualEffectWhenActivating()
                .ResonanceCost(1)
                .TelegraphSize(4f, 4f)
                .HasTelegraphSphereAction((activator, targets, location) =>
                {
                    foreach (var target in targets)
                    {
                        var duration = _spell.CalculateResistedTicks(target, ResistType.Wind, 20);
                        _status.ApplyStatusEffect<SnarlStatusEffect>(activator, target, duration);
                    }
                });
        }
    }
}
