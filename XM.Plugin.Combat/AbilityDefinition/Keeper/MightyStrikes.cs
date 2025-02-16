using System.Collections.Generic;
using Anvil.Services;
using XM.AI.Enmity;
using XM.Plugin.Combat.StatusEffectDefinition;
using XM.Progression.Ability;
using XM.Progression.Recast;
using XM.Progression.StatusEffect;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Plugin.Combat.AbilityDefinition.Keeper
{
    [ServiceBinding(typeof(IAbilityListDefinition))]
    internal class MightyStrikes: IAbilityListDefinition
    {
        private readonly AbilityBuilder _builder = new();

        private readonly EnmityService _enmity;
        private readonly StatusEffectService _status;

        public MightyStrikes(
            EnmityService enmity,
            StatusEffectService status)
        {
            _enmity = enmity;
            _status = status;
        }

        public Dictionary<FeatType, AbilityDetail> BuildAbilities()
        {
            MightyStrikesAbility();

            return _builder.Build();
        }

        private void MightyStrikesAbility()
        {
            _builder.Create(FeatType.MightyStrikes)
                .Name(LocaleString.MightyStrikes)
                .Description(LocaleString.MightyStrikesDescription)
                .HasRecastDelay(RecastGroup.JobCapstone, 60f * 30f)
                .IsCastedAbility()
                .RequirementEP(150)
                .UsesAnimation(AnimationType.LoopingConjure2)
                .HasActivationDelay(3f)
                .ResonanceCost(3)
                .HasImpactAction((activator, target, location) =>
                {
                    _status.ApplyStatusEffect<MightyStrikesStatusEffect>(activator, activator, 1);
                    _enmity.ModifyEnmityOnAll(activator, EnmityType.Volatile, 4500);
                });
        }
    }
}
