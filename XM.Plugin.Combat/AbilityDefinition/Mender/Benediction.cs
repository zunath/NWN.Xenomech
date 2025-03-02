using System.Collections.Generic;
using Anvil.Services;
using XM.AI.Enmity;
using XM.Progression.Ability;
using XM.Progression.Recast;
using XM.Progression.Stat;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Plugin.Combat.AbilityDefinition.Mender
{
    [ServiceBinding(typeof(IAbilityListDefinition))]
    internal class Benediction: IAbilityListDefinition
    {
        private readonly AbilityBuilder _builder = new();

        private readonly EnmityService _enmity;
        private readonly StatService _stat;

        public Benediction(
            EnmityService enmity,
            StatService stat)
        {
            _enmity = enmity;
            _stat = stat;
        }

        public Dictionary<FeatType, AbilityDetail> BuildAbilities()
        {
            BenedictionAbility();

            return _builder.Build();
        }

        private void BenedictionAbility()
        {
            _builder.Create(FeatType.Benediction)
                .Name(LocaleString.Benediction)
                .Description(LocaleString.BenedictionDescription)
                .Classification(AbilityCategoryType.HPRestoration)
                .TargetingType(AbilityTargetingType.SelfTargetsParty)
                .HasRecastDelay(RecastGroup.JobCapstone, 60f * 30f)
                .IsCastedAbility()
                .RequirementEP(150)
                .UsesAnimation(AnimationType.LoopingConjure1)
                .HasActivationDelay(4f)
                .ResonanceCost(3)
                .TelegraphSize(15f, 15f)
                .HasTelegraphSphereAction((activator, targets, location) =>
                {
                    _enmity.ModifyEnmityOnAll(activator, EnmityType.Volatile, 4500);

                    foreach (var target in targets)
                    {
                        if (!GetFactionEqual(target, activator))
                            continue;

                        var maxHP = _stat.GetMaxHP(target);
                        ApplyEffectToObject(DurationType.Instant, EffectHeal(maxHP), target);
                        ApplyEffectToObject(DurationType.Instant, EffectVisualEffect(VisualEffectType.ImpHealingExtra), target);
                        _enmity.ModifyEnmityOnAll(activator, EnmityType.Volatile, maxHP);
                    }
                });
        }
    }
}
