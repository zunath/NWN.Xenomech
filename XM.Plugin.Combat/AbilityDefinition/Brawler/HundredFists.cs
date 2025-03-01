using System.Collections.Generic;
using Anvil.Services;
using XM.AI.Enmity;
using XM.Plugin.Combat.StatusEffectDefinition.Buff;
using XM.Progression.Ability;
using XM.Progression.Recast;
using XM.Progression.StatusEffect;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Plugin.Combat.AbilityDefinition.Brawler
{
    [ServiceBinding(typeof(IAbilityListDefinition))]
    internal class HundredFists: IAbilityListDefinition
    {
        private readonly AbilityBuilder _builder = new();

        private readonly EnmityService _enmity;
        private readonly StatusEffectService _status;

        public HundredFists(
            EnmityService enmity,
            StatusEffectService status)
        {
            _enmity = enmity;
            _status = status;
        }

        public Dictionary<FeatType, AbilityDetail> BuildAbilities()
        {
            HundredFistsAbility();

            return _builder.Build();
        }

        private void HundredFistsAbility()
        {
            _builder.Create(FeatType.HundredFists)
                .Name(LocaleString.HundredFists)
                .Description(LocaleString.HundredFistsDescription)
                .Classification(AbilityCategoryType.Offensive)
                .HasRecastDelay(RecastGroup.JobCapstone, 60f * 30f)
                .IsCastedAbility()
                .RequirementEP(150)
                .UsesAnimation(AnimationType.LoopingConjure1)
                .HasActivationDelay(2f)
                .ResonanceCost(3)
                .HasImpactAction((activator, target, location) =>
                {
                    _status.ApplyStatusEffect<HundredFistsStatusEffect>(activator, activator, 1);
                    _enmity.ModifyEnmityOnAll(activator, EnmityType.Volatile, 6000);
                });
        }
    }
}
