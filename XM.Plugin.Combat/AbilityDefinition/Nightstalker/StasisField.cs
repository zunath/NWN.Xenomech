using System.Collections.Generic;
using Anvil.Services;
using XM.AI.Enmity;
using XM.Plugin.Combat.StatusEffectDefinition;
using XM.Progression.Ability;
using XM.Progression.Recast;
using XM.Progression.StatusEffect;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Plugin.Combat.AbilityDefinition.Nightstalker
{
    [ServiceBinding(typeof(IAbilityListDefinition))]
    internal class StasisField: IAbilityListDefinition
    {
        private readonly AbilityBuilder _builder = new();

        private readonly EnmityService _enmity;
        private readonly StatusEffectService _status;

        public StasisField(
            EnmityService enmity,
            StatusEffectService status)
        {
            _enmity = enmity;
            _status = status;
        }

        public Dictionary<FeatType, AbilityDetail> BuildAbilities()
        {
            StasisFieldAbility();

            return _builder.Build();
        }

        private void StasisFieldAbility()
        {
            _builder.Create(FeatType.StasisField)
                .Name(LocaleString.StasisField)
                .Description(LocaleString.StasisFieldDescription)
                .HasRecastDelay(RecastGroup.StasisField, 60f * 5f)
                .IsCastedAbility()
                .RequirementEP(40)
                .UsesAnimation(AnimationType.LoopingConjure1)
                .HasActivationDelay(2f)
                .ResonanceCost(2)
                .HasImpactAction((activator, target, location) =>
                {
                    _status.ApplyStatusEffect<StasisFieldStatusEffect>(activator, activator, 1);
                    _enmity.ModifyEnmityOnAll(activator, EnmityType.Volatile, 500);
                });
        }
    }
}
