using System.Collections.Generic;
using Anvil.Services;
using XM.AI.Enmity;
using XM.Plugin.Combat.StatusEffectDefinition.Debuff;
using XM.Progression.Ability;
using XM.Progression.Recast;
using XM.Progression.Stat;
using XM.Progression.StatusEffect;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Plugin.Combat.AbilityDefinition.Hunter
{
    [ServiceBinding(typeof(IAbilityListDefinition))]
    internal class Cripple: IAbilityListDefinition
    {
        private readonly AbilityBuilder _builder = new();
        private readonly SpellService _spell;
        private readonly StatusEffectService _status;
        private readonly EnmityService _enmity;

        public Cripple(
            StatusEffectService status,
            SpellService spell,
            EnmityService enmity)
        {
            _spell = spell;
            _status = status;
            _enmity = enmity;
        }

        public Dictionary<FeatType, AbilityDetail> BuildAbilities()
        {
            Cripple1();
            Cripple2();

            return _builder.Build();
        }

        private void Cripple1()
        {
            _builder.Create(FeatType.Cripple1)
                .Name(LocaleString.CrippleI)
                .Description(LocaleString.CrippleIDescription)
                .Classification(AbilityCategoryType.Offensive)
                .TargetingType(AbilityTargetingType.SelfTargetsEnemy)
                .HasRecastDelay(RecastGroup.Cripple, 60f * 3f)
                .IsQueuedAttack()
                .RequirementEP(20)
                .ResonanceCost(1)
                .HasImpactAction((activator, target, location) =>
                {
                    var duration = _spell.CalculateResistedTicks(target, ResistType.Ice, 20);
                    _status.ApplyStatusEffect<Cripple1StatusEffect>(activator, target, duration);
                    _enmity.ModifyEnmity(activator, target, EnmityType.Volatile, 750);
                });
        }

        private void Cripple2()
        {
            _builder.Create(FeatType.Cripple2)
                .Name(LocaleString.CrippleII)
                .Description(LocaleString.CrippleIIDescription)
                .Classification(AbilityCategoryType.Offensive)
                .TargetingType(AbilityTargetingType.SelfTargetsEnemy)
                .HasRecastDelay(RecastGroup.Cripple, 60f * 3f)
                .IsQueuedAttack()
                .RequirementEP(45)
                .ResonanceCost(2)
                .HasImpactAction((activator, target, location) =>
                {
                    var duration = _spell.CalculateResistedTicks(target, ResistType.Ice, 40);
                    _status.ApplyStatusEffect<PoisonStatusEffect>(activator, target, duration);
                    _enmity.ModifyEnmity(activator, target, EnmityType.Volatile, 1100);
                });
        }
    }
}
