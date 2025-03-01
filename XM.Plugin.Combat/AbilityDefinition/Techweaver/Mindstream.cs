using System.Collections.Generic;
using Anvil.Services;
using XM.Plugin.Combat.StatusEffectDefinition.Buff;
using XM.Progression.Ability;
using XM.Progression.Recast;
using XM.Progression.StatusEffect;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Plugin.Combat.AbilityDefinition.Techweaver
{
    [ServiceBinding(typeof(IAbilityListDefinition))]
    internal class Mindstream: IAbilityListDefinition
    {
        private readonly AbilityBuilder _builder = new();

        private readonly StatusEffectService _status;

        public Mindstream(StatusEffectService status)
        {
            _status = status;
        }

        public Dictionary<FeatType, AbilityDetail> BuildAbilities()
        {
            Mindstream1();
            Mindstream2();

            return _builder.Build();
        }

        private void Mindstream1()
        {
            _builder.Create(FeatType.Mindstream1)
                .Name(LocaleString.MindstreamI)
                .Description(LocaleString.MindstreamIDescription)
                .Classification(AbilityCategoryType.Defensive)
                .HasRecastDelay(RecastGroup.Mindstream, 18f)
                .HasActivationDelay(5f)
                .UsesAnimation(AnimationType.LoopingConjure1)
                .DisplaysVisualEffectWhenActivating()
                .RequirementEP(40)
                .ResonanceCost(2)
                .HasImpactAction((activator, target, location) =>
                {
                    _status.ApplyStatusEffect<Mindstream1StatusEffect>(activator, target, 1);
                });
        }

        private void Mindstream2()
        {
            _builder.Create(FeatType.Mindstream2)
                .Name(LocaleString.MindstreamII)
                .Description(LocaleString.MindstreamIIDescription)
                .Classification(AbilityCategoryType.Defensive)
                .HasRecastDelay(RecastGroup.Mindstream, 18f)
                .HasActivationDelay(5f)
                .UsesAnimation(AnimationType.LoopingConjure1)
                .DisplaysVisualEffectWhenActivating()
                .RequirementEP(60)
                .ResonanceCost(3)
                .HasImpactAction((activator, target, location) =>
                {
                    _status.ApplyStatusEffect<Mindstream2StatusEffect>(activator, target, 1);
                });
        }
    }
}
