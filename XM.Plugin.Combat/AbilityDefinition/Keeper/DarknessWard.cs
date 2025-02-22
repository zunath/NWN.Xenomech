using System.Collections.Generic;
using Anvil.Services;
using XM.Plugin.Combat.StatusEffectDefinition.Buff;
using XM.Progression.Ability;
using XM.Progression.Recast;
using XM.Progression.StatusEffect;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Plugin.Combat.AbilityDefinition.Keeper
{
    [ServiceBinding(typeof(IAbilityListDefinition))]
    internal class DarknessWard : IAbilityListDefinition
    {
        private readonly AbilityBuilder _builder = new();
        private readonly StatusEffectService _status;

        public DarknessWard(StatusEffectService status)
        {
            _status = status;
        }

        public Dictionary<FeatType, AbilityDetail> BuildAbilities()
        {
            DarknessWardAbility();

            return _builder.Build();
        }

        private void DarknessWardAbility()
        {
            _builder.Create(FeatType.DarknessWard)
                .Name(LocaleString.DarknessWard)
                .Description(LocaleString.DarknessWardDescription)
                .HasRecastDelay(RecastGroup.Ward, 10f)
                .HasActivationDelay(4f)
                .RequirementEP(30)
                .UsesAnimation(AnimationType.LoopingConjure1)
                .DisplaysVisualEffectWhenActivating()
                .ResonanceCost(1)
                .TelegraphSize(15f, 15f)
                .HasTelegraphSphereAction((activator, targets, location) =>
                {
                    foreach (var target in targets)
                    {
                        if (!GetFactionEqual(target, activator))
                            continue;

                        _status.ApplyStatusEffect<DarknessWardStatusEffect>(activator, target, 15);
                    }
                });
        }
    }
}
