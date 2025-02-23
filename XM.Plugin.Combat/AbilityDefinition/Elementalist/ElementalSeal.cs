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
    internal class ElementalSeal: IAbilityListDefinition
    {
        private readonly AbilityBuilder _builder = new();

        private readonly StatusEffectService _status;

        public ElementalSeal(StatusEffectService status)
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
            _builder.Create(FeatType.ElementalSeal)
                .Name(LocaleString.ElementalSeal)
                .Description(LocaleString.ElementalSealDescription)
                .HasRecastDelay(RecastGroup.ElementalSeal, 60f)
                .HasActivationDelay(2f)
                .UsesAnimation(AnimationType.LoopingConjure1)
                .DisplaysVisualEffectWhenActivating()
                .RequirementEP(40)
                .ResonanceCost(1)
                .HasImpactAction((activator, target, location) =>
                {
                    _status.ApplyStatusEffect<ElementalSealStatusEffect>(activator, activator, 1);
                });
        }
    }
}
