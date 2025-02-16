using System.Collections.Generic;
using Anvil.Services;
using XM.Plugin.Combat.StatusEffectDefinition;
using XM.Progression.Ability;
using XM.Progression.Recast;
using XM.Progression.StatusEffect;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Plugin.Combat.AbilityDefinition.Nightstalker
{
    [ServiceBinding(typeof(IAbilityListDefinition))]
    internal class SneakAttack : IAbilityListDefinition
    {
        private readonly AbilityBuilder _builder = new();
        private readonly StatusEffectService _status;
        public SneakAttack(StatusEffectService status)
        {
            _status = status;
        }

        public Dictionary<FeatType, AbilityDetail> BuildAbilities()
        {
            SneakAttack1();
            SneakAttack2();
            SneakAttack3();

            return _builder.Build();
        }

        private void SneakAttack1()
        {
            _builder.Create(FeatType.SneakAttack1)
                .Name(LocaleString.SneakAttackI)
                .Description(LocaleString.SneakAttackIDescription)
                .HasRecastDelay(RecastGroup.SneakAttack, 60f)
                .RequirementEP(15)
                .ResonanceCost(1)
                .HasImpactAction((activator, target, location) =>
                {
                    _status.ApplyStatusEffect<SneakAttack1StatusEffect>(activator, activator, 1);
                });
        }

        private void SneakAttack2()
        {
            _builder.Create(FeatType.SneakAttack2)
                .Name(LocaleString.SneakAttackII)
                .Description(LocaleString.SneakAttackIIDescription)
                .HasRecastDelay(RecastGroup.SneakAttack, 60f)
                .RequirementEP(35)
                .ResonanceCost(2)
                .HasImpactAction((activator, target, location) =>
                {
                    _status.ApplyStatusEffect<SneakAttack2StatusEffect>(activator, activator, 1);
                });
        }

        private void SneakAttack3()
        {
            _builder.Create(FeatType.SneakAttack3)
                .Name(LocaleString.SneakAttackIII)
                .Description(LocaleString.SneakAttackIIIDescription)
                .HasRecastDelay(RecastGroup.SneakAttack, 60f)
                .RequirementEP(55)
                .ResonanceCost(3)
                .HasImpactAction((activator, target, location) =>
                {
                    _status.ApplyStatusEffect<SneakAttack3StatusEffect>(activator, activator, 1);
                });
        }
    }
}