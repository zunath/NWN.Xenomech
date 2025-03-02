using System.Collections.Generic;
using Anvil.Services;
using XM.AI.Enmity;
using XM.Progression.Ability;
using XM.Progression.Recast;
using XM.Progression.Stat;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Plugin.Combat.AbilityDefinition.Mender
{
    [ServiceBinding(typeof(IAbilityListDefinition))]
    internal class Revive: IAbilityListDefinition
    {
        private readonly AbilityBuilder _builder = new();

        private readonly EnmityService _enmity;
        private readonly StatService _stat;

        public Revive(EnmityService enmity, StatService stat)
        {
            _enmity = enmity;
            _stat = stat;
        }

        public Dictionary<FeatType, AbilityDetail> BuildAbilities()
        {
            ReviveAbility();

            return _builder.Build();
        }

        private void ReviveAbility()
        {
            _builder.Create(FeatType.Revive)
                .Name(LocaleString.Revive)
                .Description(LocaleString.ReviveDescription)
                .Classification(AbilityCategoryType.Special)
                .HasRecastDelay(RecastGroup.Revive, 120f)
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
                    var maxHP = _stat.GetMaxHP(target);
                    var heal = maxHP / 2;
                    ApplyEffectToObject(DurationType.Instant, EffectResurrection(), target);
                    AssignCommand(activator, () => ApplyEffectToObject(DurationType.Instant, EffectHeal(heal), target));

                    _enmity.ModifyEnmityOnAll(activator, EnmityType.Volatile, 8000);
                });
        }
    }
}
