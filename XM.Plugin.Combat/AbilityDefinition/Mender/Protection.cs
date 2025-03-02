﻿using System.Collections.Generic;
using Anvil.Services;
using XM.Plugin.Combat.StatusEffectDefinition.Buff;
using XM.Progression.Ability;
using XM.Progression.Recast;
using XM.Progression.StatusEffect;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Plugin.Combat.AbilityDefinition.Mender
{
    [ServiceBinding(typeof(IAbilityListDefinition))]
    internal class Protection : IAbilityListDefinition
    {
        private readonly AbilityBuilder _builder = new();
        private readonly StatusEffectService _status;

        public Protection(StatusEffectService status)
        {
            _status = status;
        }

        public Dictionary<FeatType, AbilityDetail> BuildAbilities()
        {
            Protection1();
            Protection2();
            Protection3();

            return _builder.Build();
        }

        private void Protection1()
        {
            _builder.Create(FeatType.Protection1)
                .Name(LocaleString.ProtectionI)
                .Description(LocaleString.ProtectionIDescription)
                .Classification(AbilityCategoryType.Defensive)
                .TargetingType(AbilityTargetingType.SelfTargetsParty)
                .HasRecastDelay(RecastGroup.Protection, 15f)
                .HasActivationDelay(4f)
                .RequirementEP(9)
                .UsesAnimation(AnimationType.LoopingConjure1)
                .DisplaysVisualEffectWhenActivating()
                .ResonanceCost(1)
                .TelegraphSize(10f, 10f)
                .HasTelegraphSphereAction((activator, targets, location) =>
                {
                    foreach (var target in targets)
                    {
                        if (!GetFactionEqual(target, activator))
                            continue;

                        _status.ApplyStatusEffect<Protection1StatusEffect>(activator, target, 30);
                    }
                });
        }

        private void Protection2()
        {
            _builder.Create(FeatType.Protection2)
                .Name(LocaleString.ProtectionII)
                .Description(LocaleString.ProtectionIIDescription)
                .Classification(AbilityCategoryType.Defensive)
                .TargetingType(AbilityTargetingType.SelfTargetsParty)
                .HasRecastDelay(RecastGroup.Protection, 15f)
                .HasActivationDelay(4f)
                .RequirementEP(28)
                .UsesAnimation(AnimationType.LoopingConjure1)
                .DisplaysVisualEffectWhenActivating()
                .ResonanceCost(2)
                .TelegraphSize(10f, 10f)
                .HasTelegraphSphereAction((activator, targets, location) =>
                {
                    foreach (var target in targets)
                    {
                        if (!GetFactionEqual(target, activator))
                            continue;

                        _status.ApplyStatusEffect<Protection2StatusEffect>(activator, target, 30);
                    }
                });
        }

        private void Protection3()
        {
            _builder.Create(FeatType.Protection3)
                .Name(LocaleString.ProtectionIII)
                .Description(LocaleString.ProtectionIIIDescription)
                .Classification(AbilityCategoryType.Defensive)
                .TargetingType(AbilityTargetingType.SelfTargetsParty)
                .HasRecastDelay(RecastGroup.Protection, 15f)
                .HasActivationDelay(4f)
                .RequirementEP(56)
                .UsesAnimation(AnimationType.LoopingConjure1)
                .DisplaysVisualEffectWhenActivating()
                .ResonanceCost(3)
                .TelegraphSize(10f, 10f)
                .HasTelegraphSphereAction((activator, targets, location) =>
                {
                    foreach (var target in targets)
                    {
                        if (!GetFactionEqual(target, activator))
                            continue;

                        _status.ApplyStatusEffect<Protection3StatusEffect>(activator, target, 30);
                    }
                });
        }
    }
}