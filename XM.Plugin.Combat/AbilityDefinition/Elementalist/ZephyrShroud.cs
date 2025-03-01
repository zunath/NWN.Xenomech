using System.Collections.Generic;
using Anvil.Services;
using XM.Plugin.Combat.StatusEffectDefinition.Buff;
using XM.Progression.Ability;
using XM.Progression.Recast;
using XM.Progression.StatusEffect;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Plugin.Combat.AbilityDefinition.Elementalist
{
    [ServiceBinding(typeof(IAbilityListDefinition))]
    internal class ZephyrShroud : IAbilityListDefinition
    {
        private readonly AbilityBuilder _builder = new();
        private readonly StatusEffectService _status;

        public ZephyrShroud(StatusEffectService status)
        {
            _status = status;
        }

        public Dictionary<FeatType, AbilityDetail> BuildAbilities()
        {
            ZephyrShroudAbility();

            return _builder.Build();
        }

        private void ZephyrShroudAbility()
        {
            _builder.Create(FeatType.ZephyrShroud)
                .Name(LocaleString.ZephyrShroud)
                .Description(LocaleString.ZephyrShroudDescription)
                .Classification(AbilityCategoryType.Defensive)
                .HasRecastDelay(RecastGroup.ZephyrShroud, 60f * 5f)
                .IsCastedAbility()
                .RequirementEP(40)
                .UsesAnimation(AnimationType.LoopingConjure1)
                .HasActivationDelay(2f)
                .ResonanceCost(2)
                .TelegraphSize(10f, 10f)
                .HasTelegraphSphereAction((activator, targets, location) =>
                {
                    foreach (var target in targets)
                    {
                        if (!GetFactionEqual(target, activator))
                            continue;

                        _status.ApplyStatusEffect<ZephyrShroudStatusEffect>(activator, target, 1);
                    }
                });
        }
    }
}
