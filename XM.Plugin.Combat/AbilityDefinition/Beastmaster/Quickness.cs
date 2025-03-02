using System.Collections.Generic;
using Anvil.Services;
using XM.Plugin.Combat.StatusEffectDefinition.Buff;
using XM.Progression.Ability;
using XM.Progression.Recast;
using XM.Progression.StatusEffect;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Plugin.Combat.AbilityDefinition.Beastmaster
{
    [ServiceBinding(typeof(IAbilityListDefinition))]
    internal class Quickness: IAbilityListDefinition
    {
        private readonly AbilityBuilder _builder = new();

        private readonly StatusEffectService _status;

        public Quickness(StatusEffectService status)
        {
            _status = status;
        }

        public Dictionary<FeatType, AbilityDetail> BuildAbilities()
        {
            QuicknessAbility();

            return _builder.Build();
        }

        private void QuicknessAbility()
        {
            _builder.Create(FeatType.Quickness)
                .Name(LocaleString.Quickness)
                .Description(LocaleString.Quickness)
                .Classification(AbilityCategoryType.Defensive)
                .HasRecastDelay(RecastGroup.Quickness, 60f * 10f)
                .HasActivationDelay(2f)
                .RequirementEP(15)
                .UsesAnimation(AnimationType.LoopingConjure1)
                .DisplaysVisualEffectWhenActivating()
                .ResonanceCost(1)
                .HasImpactAction((activator, target, location) =>
                {
                    _status.ApplyStatusEffect<QuicknessStatusEffect>(activator, activator, 1);
                });
        }
    }
}
