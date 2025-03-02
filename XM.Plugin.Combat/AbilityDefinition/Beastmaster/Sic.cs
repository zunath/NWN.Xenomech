using System.Collections.Generic;
using Anvil.Services;
using XM.AI.Enmity;
using XM.Progression.Ability;
using XM.Progression.Beast;
using XM.Progression.Recast;
using XM.Progression.StatusEffect;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Plugin.Combat.AbilityDefinition.Beastmaster
{
    [ServiceBinding(typeof(IAbilityListDefinition))]
    internal class Sic: IAbilityListDefinition
    {
        private readonly AbilityBuilder _builder = new();

        private readonly EnmityService _enmity;
        private readonly StatusEffectService _status;
        private readonly BeastService _beast;

        public Sic(
            EnmityService enmity,
            StatusEffectService status,
            BeastService beast)
        {
            _enmity = enmity;
            _status = status;
            _beast = beast;
        }

        public Dictionary<FeatType, AbilityDetail> BuildAbilities()
        {
            SicAbility();

            return _builder.Build();
        }

        private void SicAbility()
        {
            _builder.Create(FeatType.Sic)
                .Name(LocaleString.Sic)
                .Description(LocaleString.SicDescription)
                .Classification(AbilityCategoryType.Offensive)
                .TargetingType(AbilityTargetingType.SelfTargetsEnemy)
                .HasRecastDelay(RecastGroup.Sic, 60f)
                .IsCastedAbility()
                .RequirementEP(10)
                .UsesAnimation(AnimationType.LoopingConjure1)
                .HasActivationDelay(1f)
                .ResonanceCost(2)
                .HasCustomValidation((activator, target, location) =>
                {
                    var beast = _beast.GetBeast(activator);
                    if (!GetIsObjectValid(beast))
                        return LocaleString.YouDoNotHaveAnActiveBeast.ToLocalizedString();

                    var attackTarget = GetAttackTarget(beast);
                    if (!GetIsObjectValid(attackTarget))
                        return LocaleString.YourBeastHasNoTarget.ToLocalizedString();

                    var type = _beast.GetBeastType(beast);
                    if (!_beast.HasSicAbilities(type))
                        return LocaleString.YourBeastCannotUseSic.ToLocalizedString();

                    return string.Empty;
                })
                .HasImpactAction((activator, target, location) =>
                {
                    var beast = _beast.GetBeast(activator);
                    var type = _beast.GetBeastType(beast);
                    var feat = _beast.GetRandomSicAbility(type);
                    var attackTarget = GetAttackTarget(beast);

                    AssignCommand(beast, () => ActionUseFeat(feat, attackTarget));
                });
        }
    }
}
