using System.Collections.Generic;
using Anvil.Services;
using XM.Plugin.Combat.StatusEffectDefinition.Buff;
using XM.Progression.Ability;
using XM.Progression.Recast;
using XM.Progression.StatusEffect;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Plugin.Combat.AbilityDefinition.Mender
{
    [ServiceBinding(typeof(IAbilityListDefinition))]
    internal class DivineSeal: IAbilityListDefinition
    {
        private readonly AbilityBuilder _builder = new();

        private readonly StatusEffectService _status;

        public DivineSeal(StatusEffectService status)
        {
            _status = status;
        }

        public Dictionary<FeatType, AbilityDetail> BuildAbilities()
        {
            DivineSealAbility();

            return _builder.Build();
        }

        private void DivineSealAbility()
        {
            _builder.Create(FeatType.DivineSeal)
                .Name(LocaleString.DivineSeal)
                .Description(LocaleString.DivineSealDescription)
                .HasRecastDelay(RecastGroup.DivineSeal, 60f * 5f)
                .UsesAnimation(AnimationType.LoopingConjure1)
                .DisplaysVisualEffectWhenActivating()
                .ResonanceCost(1)
                .HasImpactAction((activator, target, location) =>
                {
                    _status.ApplyStatusEffect<DivineSealStatusEffect>(activator, activator, 1);
                });
        }
    }
}
