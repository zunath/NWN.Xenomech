using System.Collections.Generic;
using Anvil.Services;
using XM.AI.Enmity;
using XM.Plugin.Combat.StatusEffectDefinition.Buff;
using XM.Progression.Ability;
using XM.Progression.Recast;
using XM.Progression.StatusEffect;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Plugin.Combat.AbilityDefinition.Nightstalker
{
    [ServiceBinding(typeof(IAbilityListDefinition))]
    internal class PerfectDodge: IAbilityListDefinition
    {
        private readonly AbilityBuilder _builder = new();

        private readonly EnmityService _enmity;
        private readonly StatusEffectService _status;

        public PerfectDodge(
            EnmityService enmity,
            StatusEffectService status)
        {
            _enmity = enmity;
            _status = status;
        }

        public Dictionary<FeatType, AbilityDetail> BuildAbilities()
        {
            PerfectDodgeAbility();

            return _builder.Build();
        }

        private void PerfectDodgeAbility()
        {
            _builder.Create(FeatType.PerfectDodge)
                .Name(LocaleString.PerfectDodge)
                .Description(LocaleString.PerfectDodgeDescription)
                .Classification(AbilityCategoryType.Defensive)
                .HasRecastDelay(RecastGroup.JobCapstone, 60f * 30f)
                .IsCastedAbility()
                .RequirementEP(150)
                .UsesAnimation(AnimationType.LoopingConjure1)
                .HasActivationDelay(2f)
                .ResonanceCost(3)
                .HasImpactAction((activator, target, location) =>
                {
                    _status.ApplyStatusEffect<PerfectDodgeStatusEffect>(activator, activator, 1);
                    _enmity.ModifyEnmityOnAll(activator, EnmityType.Volatile, 3000);
                });
        }
    }
}
