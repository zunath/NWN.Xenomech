using System.Collections.Generic;
using Anvil.Services;
using XM.AI.Enmity;
using XM.Plugin.Combat.StatusEffectDefinition.Buff;
using XM.Progression.Ability;
using XM.Progression.Recast;
using XM.Progression.StatusEffect;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Plugin.Combat.AbilityDefinition.Elementalist
{
    [ServiceBinding(typeof(IAbilityListDefinition))]
    internal class Manafont: IAbilityListDefinition
    {
        private readonly AbilityBuilder _builder = new();
        private readonly StatusEffectService _status;
        private readonly EnmityService _enmity;

        public Manafont(
            StatusEffectService status,
            EnmityService enmity)
        {
            _status = status;
            _enmity = enmity;
        }

        public Dictionary<FeatType, AbilityDetail> BuildAbilities()
        {
            ManafontAbility();

            return _builder.Build();
        }

        private void ManafontAbility()
        {
            _builder.Create(FeatType.Manafont)
                .Name(LocaleString.Manafont)
                .Description(LocaleString.ManafontDescription)
                .HasRecastDelay(RecastGroup.JobCapstone, 60f * 30f)
                .IsCastedAbility()
                .RequirementEP(150)
                .UsesAnimation(AnimationType.LoopingConjure1)
                .HasActivationDelay(2f)
                .ResonanceCost(3)
                .HasImpactAction((activator, target, location) =>
                {
                    _status.ApplyStatusEffect<ManafontStatusEffect>(activator, activator, 1);
                    _enmity.ModifyEnmityOnAll(activator, EnmityType.Volatile, 3000);
                });
        }
    }
}
