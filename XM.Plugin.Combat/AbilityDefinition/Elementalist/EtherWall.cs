using System.Collections.Generic;
using Anvil.Services;
using XM.Plugin.Combat.StatusEffectDefinition;
using XM.Progression.Ability;
using XM.Progression.Recast;
using XM.Progression.StatusEffect;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Plugin.Combat.AbilityDefinition.Elementalist
{
    [ServiceBinding(typeof(IAbilityListDefinition))]
    internal class EtherWall: IAbilityListDefinition
    {
        private readonly AbilityBuilder _builder = new();

        private readonly StatusEffectService _status;

        public EtherWall(
            StatusEffectService status)
        {
            _status = status;
        }

        public Dictionary<FeatType, AbilityDetail> BuildAbilities()
        {
            EtherWallAbility();

            return _builder.Build();
        }

        private void EtherWallAbility()
        {
            _builder.Create(FeatType.EtherWall)
                .Name(LocaleString.EtherWall)
                .Description(LocaleString.EtherWallDescription)
                .HasRecastDelay(RecastGroup.EtherWall, 60f * 3f)
                .HasActivationDelay(2f)
                .UsesAnimation(AnimationType.LoopingConjure1)
                .DisplaysVisualEffectWhenActivating()
                .RequirementEP(10)
                .ResonanceCost(1)
                .HasImpactAction((activator, target, location) =>
                {
                    _status.ApplyStatusEffect<EtherWallStatusEffect>(activator, activator, 1);
                });
        }
    }
}
