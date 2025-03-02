using System.Collections.Generic;
using Anvil.Services;
using XM.AI.Enmity;
using XM.Progression.Ability;
using XM.Progression.Recast;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Plugin.Combat.AbilityDefinition.Keeper
{
    [ServiceBinding(typeof(IAbilityListDefinition))]
    internal class Raise: IAbilityListDefinition
    {
        private readonly AbilityBuilder _builder = new();

        private readonly EnmityService _enmity;

        public Raise(EnmityService enmity)
        {
            _enmity = enmity;
        }

        public Dictionary<FeatType, AbilityDetail> BuildAbilities()
        {
            RaiseAbility();

            return _builder.Build();
        }

        private void RaiseAbility()
        {
            _builder.Create(FeatType.Raise)
                .Name(LocaleString.Raise)
                .Description(LocaleString.RaiseDescription)
                .Classification(AbilityCategoryType.Special)
                .HasRecastDelay(RecastGroup.Raise, 120f)
                .IsCastedAbility()
                .HasMaxRange(5f)
                .RequirementEP(60)
                .UsesAnimation(AnimationType.LoopingConjure1)
                .ResonanceCost(1)
                .HasCustomValidation((activator, target, location) =>
                {
                    if (!GetIsDead(target))
                    {
                        return LocaleString.YourTargetMustBeDead.ToLocalizedString();
                    }

                    return string.Empty;
                })
                .HasImpactAction((activator, target, location) =>
                {
                    ApplyEffectToObject(DurationType.Instant, EffectResurrection(), target);
                    _enmity.ModifyEnmityOnAll(activator, EnmityType.Volatile, 6000);
                });
        }
    }
}
