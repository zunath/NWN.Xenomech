using System.Collections.Generic;
using Anvil.Services;
using XM.AI.Enmity;
using XM.Plugin.Combat.StatusEffectDefinition.Buff;
using XM.Progression.Ability;
using XM.Progression.Beast;
using XM.Progression.Recast;
using XM.Progression.StatusEffect;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Plugin.Combat.AbilityDefinition.Beastmaster
{
    [ServiceBinding(typeof(IAbilityListDefinition))]
    internal class Familiar: IAbilityListDefinition
    {
        private readonly AbilityBuilder _builder = new();

        private readonly EnmityService _enmity;
        private readonly StatusEffectService _status;
        private readonly BeastService _beast;

        public Familiar(
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
            FamiliarAbility();

            return _builder.Build();
        }

        private void FamiliarAbility()
        {
            _builder.Create(FeatType.Familiar)
                .Name(LocaleString.Familiar)
                .Description(LocaleString.FamiliarDescription)
                .HasRecastDelay(RecastGroup.JobCapstone, 60f * 30f)
                .IsCastedAbility()
                .RequirementEP(150)
                .UsesAnimation(AnimationType.LoopingConjure1)
                .HasActivationDelay(3f)
                .ResonanceCost(3)
                .HasCustomValidation((activator, target, location) =>
                {
                    var beast = _beast.GetBeast(activator);
                    if (!GetIsObjectValid(beast))
                        return LocaleString.YouDoNotHaveAnActiveBeast.ToLocalizedString();

                    return string.Empty;
                })
                .HasImpactAction((activator, target, location) =>
                {
                    var beast = _beast.GetBeast(activator);

                    _status.ApplyStatusEffect<FamiliarStatusEffect>(activator, beast, 1);
                    _enmity.ModifyEnmityOnAll(activator, EnmityType.Volatile, 4500);
                    ApplyEffectToObject(DurationType.Instant, EffectVisualEffect(VisualEffectType.FnfHowlWarCry), beast);
                });
        }
    }
}
