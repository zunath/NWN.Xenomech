using System.Collections.Generic;
using Anvil.Services;
using XM.AI.Enmity;
using XM.Progression.Ability;
using XM.Progression.Recast;
using XM.Progression.Stat;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Plugin.Combat.AbilityDefinition.Techweaver
{
    [ServiceBinding(typeof(IAbilityListDefinition))]
    internal class Etherwarp: IAbilityListDefinition
    {
        private readonly AbilityBuilder _builder = new();

        private readonly EnmityService _enmity;
        private readonly StatService _stat;

        public Etherwarp(
            EnmityService enmity,
            StatService stat)
        {
            _enmity = enmity;
            _stat = stat;
        }

        public Dictionary<FeatType, AbilityDetail> BuildAbilities()
        {
            EtherwarpAbility();

            return _builder.Build();
        }

        private void EtherwarpAbility()
        {
            _builder.Create(FeatType.Etherwarp)
                .Name(LocaleString.Etherwarp)
                .Description(LocaleString.EtherwarpDescription)
                .Classification(AbilityCategoryType.Defensive)
                .HasRecastDelay(RecastGroup.JobCapstone, 60f * 30f)
                .IsCastedAbility()
                .RequirementEP(150)
                .UsesAnimation(AnimationType.LoopingConjure1)
                .HasActivationDelay(4f)
                .ResonanceCost(3)
                .TelegraphSize(13f, 13f)
                .HasTelegraphSphereAction((activator, targets, location) =>
                {
                    _enmity.ModifyEnmityOnAll(activator, EnmityType.Volatile, 4500);
                    foreach (var target in targets)
                    {
                        if (!GetFactionEqual(target, activator))
                            continue;

                        var maxEP = _stat.GetMaxEP(target);
                        _stat.RestoreEP(target, maxEP);
                        ApplyEffectToObject(DurationType.Instant, EffectVisualEffect(VisualEffectType.ImpHealingExtra), target);
                        _enmity.ModifyEnmityOnAll(activator, EnmityType.Volatile, maxEP);
                    }
                });
        }
    }
}
