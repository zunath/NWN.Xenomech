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

namespace XM.Plugin.Combat.AbilityDefinition.Brawler
{
    [ServiceBinding(typeof(IAbilityListDefinition))]
    internal class Flash: IAbilityListDefinition
    {
        private readonly AbilityBuilder _builder = new();
        private readonly EnmityService _enmity;
        private readonly SpellService _spell;
        private readonly StatusEffectService _status;

        public Flash(
            StatusEffectService status,
            EnmityService enmity,
            SpellService spell)
        {
            _status = status;
            _enmity = enmity;
            _spell = spell;
        }

        public Dictionary<FeatType, AbilityDetail> BuildAbilities()
        {
            FlashAbility();

            return _builder.Build();
        }

        private void Impact(uint activator, uint target, int enmity)
        {
            if (!LineOfSightObject(activator, target))
                return;

            var duration = _spell.CalculateResistedTicks(target, ResistType.Lightning, 60);
            _enmity.ModifyEnmity(activator, target, EnmityType.Volatile, enmity);
            _status.ApplyStatusEffect<FlashStatusEffect>(activator, target, duration);
        }

        private void FlashAbility()
        {
            _builder.Create(FeatType.Flash)
                .Name(LocaleString.Flash)
                .Description(LocaleString.FlashDescription)
                .Classification(AbilityCategoryType.Offensive)
                .TargetingType(AbilityTargetingType.SelfTargetsEnemy)
                .HasRecastDelay(RecastGroup.Flash, 30f)
                .HasActivationDelay(2f)
                .UsesAnimation(AnimationType.FireForgetTaunt)
                .IsCastedAbility()
                .RequirementEP(8)
                .ResonanceCost(1)
                .TelegraphSize(8f, 8f)
                .HasTelegraphSphereAction((activator, targets, location) =>
                {
                    ApplyEffectToObject(DurationType.Instant, EffectVisualEffect(VisualEffectType.FnfHowlOdd), activator);

                    foreach (var target in targets)
                    {
                        if (GetIsPC(target) || !GetIsReactionTypeHostile(target, activator))
                            continue;

                        Impact(activator, target, 1500);
                    }
                });
        }
    }
}
