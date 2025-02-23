using System.Collections.Generic;
using Anvil.Services;
using XM.Plugin.Combat.StatusEffectDefinition.Buff;
using XM.Progression.Ability;
using XM.Progression.Recast;
using XM.Progression.StatusEffect;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Plugin.Combat.AbilityDefinition.Beastmaster
{
    [ServiceBinding(typeof(IAbilityListDefinition))]
    internal class FeralHowl: IAbilityListDefinition
    {
        private readonly AbilityBuilder _builder = new();

        private readonly StatusEffectService _status;

        public FeralHowl(StatusEffectService status)
        {
            _status = status;
        }

        public Dictionary<FeatType, AbilityDetail> BuildAbilities()
        {
            FeralHowlAbility();

            return _builder.Build();
        }

        private void FeralHowlAbility()
        {
            _builder.Create(FeatType.FeralHowl)
                .Name(LocaleString.FeralHowl)
                .Description(LocaleString.FeralHowlDescription)
                .HasRecastDelay(RecastGroup.FeralHowl, 60f * 3f)
                .HasActivationDelay(2f)
                .RequirementEP(38)
                .UsesAnimation(AnimationType.LoopingConjure1)
                .DisplaysVisualEffectWhenActivating()
                .ResonanceCost(2)
                .TelegraphSize(4f, 4f)
                .HasTelegraphSphereAction((activator, targets, location) =>
                {
                    ApplyEffectToObject(DurationType.Instant, EffectVisualEffect(VisualEffectType.ImpPdkGenericPulse), activator);
                    foreach (var target in targets)
                    {
                        if (!GetFactionEqual(target, activator))
                            continue;

                        _status.ApplyStatusEffect<FeralHowlStatusEffect>(activator, target, 1);
                    }
                });
        }
    }
}
