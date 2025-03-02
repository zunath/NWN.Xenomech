using System.Collections.Generic;
using Anvil.Services;
using XM.Progression.Ability;
using XM.Progression.Beast;
using XM.Progression.Recast;
using XM.Progression.Stat;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Plugin.Combat.AbilityDefinition.Beastmaster
{
    [ServiceBinding(typeof(IAbilityListDefinition))]
    internal class Assault: IAbilityListDefinition
    {
        private readonly AbilityBuilder _builder = new();
        private readonly BeastService _beast;

        public Assault(BeastService beast)
        {
            _beast = beast;
        }

        public Dictionary<FeatType, AbilityDetail> BuildAbilities()
        {
            AssaultAbility();

            return _builder.Build();
        }

        private void AssaultAbility()
        {
            _builder.Create(FeatType.Assault)
                .Name(LocaleString.Assault)
                .Description(LocaleString.AssaultDescription)
                .Classification(AbilityCategoryType.Offensive)
                .TargetingType(AbilityTargetingType.SelfTargetsEnemy)
                .HasRecastDelay(RecastGroup.Assault, 60f)
                .HasActivationDelay(2f)
                .RequirementEP(22)
                .UsesAnimation(AnimationType.LoopingConjure1)
                .DisplaysVisualEffectWhenActivating()
                .ResonanceCost(1)
                .IsQueuedAttack()
                .IncreasesStat(StatType.QueuedDMGBonus, 25)
                .HasCustomValidation((activator, target, location) =>
                {
                    var beast = _beast.GetBeast(activator);
                    if (!GetIsObjectValid(beast))
                        return LocaleString.YouDoNotHaveAnActiveBeast.ToLocalizedString();

                    return string.Empty;
                })
                .ModifyActivator(caster => _beast.GetBeast(caster));
        }
    }
}
