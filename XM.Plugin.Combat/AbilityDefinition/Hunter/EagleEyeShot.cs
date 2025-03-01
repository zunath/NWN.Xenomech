using System.Collections.Generic;
using Anvil.Services;
using XM.AI.Enmity;
using XM.Plugin.Combat.StatusEffectDefinition.Buff;
using XM.Progression.Ability;
using XM.Progression.Recast;
using XM.Progression.StatusEffect;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Plugin.Combat.AbilityDefinition.Hunter
{
    [ServiceBinding(typeof(IAbilityListDefinition))]
    internal class EagleEyeShot: IAbilityListDefinition
    {
        private readonly AbilityBuilder _builder = new();
        private readonly StatusEffectService _status;
        private readonly EnmityService _enmity;

        public EagleEyeShot(
            StatusEffectService status,
            EnmityService enmity)
        {
            _status = status;
            _enmity = enmity;
        }

        public Dictionary<FeatType, AbilityDetail> BuildAbilities()
        {
            EagleEyeShotAbility();

            return _builder.Build();
        }

        private void EagleEyeShotAbility()
        {
            _builder.Create(FeatType.EagleEyeShot)
                .Name(LocaleString.EagleEyeShot)
                .Description(LocaleString.EagleEyeShotDescription)
                .Classification(AbilityCategoryType.Offensive)
                .HasRecastDelay(RecastGroup.JobCapstone, 60f * 30f)
                .IsCastedAbility()
                .RequirementEP(150)
                .UsesAnimation(AnimationType.LoopingConjure1)
                .HasActivationDelay(2f)
                .ResonanceCost(3)
                .HasImpactAction((activator, target, location) =>
                {
                    _status.ApplyStatusEffect<EagleEyeShotStatusEffect>(activator, activator, 1);
                    _enmity.ModifyEnmityOnAll(activator, EnmityType.Volatile, 5000);
                });
        }
    }
}
