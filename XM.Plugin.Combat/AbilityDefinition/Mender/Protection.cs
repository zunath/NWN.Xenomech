using System.Collections.Generic;
using Anvil.Services;
using XM.Plugin.Combat.StatusEffectDefinition;
using XM.Progression.Ability;
using XM.Progression.Recast;
using XM.Progression.StatusEffect;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;
using XM.Shared.Core.Party;

namespace XM.Plugin.Combat.AbilityDefinition.Mender
{
    [ServiceBinding(typeof(IAbilityListDefinition))]
    internal class Protection : AbilityBase
    {
        private readonly AbilityBuilder _builder = new();

        public override Dictionary<FeatType, AbilityDetail> BuildAbilities()
        {
            Protection1();
            Protection2();
            Protection3();

            return _builder.Build();
        }

        public Protection(PartyService party, StatusEffectService status) 
            : base(party, status)
        {
        }

        private void Protection1()
        {
            _builder.Create(FeatType.Protection1)
                .Name(LocaleString.ProtectionI)
                .Description(LocaleString.ProtectionIDescription)
                .HasRecastDelay(RecastGroup.Protection, 15f)
                .HasActivationDelay(4f)
                .RequirementEP(9)
                .UsesAnimation(AnimationType.LoopingConjure1)
                .DisplaysVisualEffectWhenActivating()
                .ResonanceCost(1)
                .HasImpactAction((activator, target, location) =>
                {
                    ApplyPartyStatusAOE<Protection1StatusEffect>(activator, target, 10f, 30);
                });
        }

        private void Protection2()
        {
            _builder.Create(FeatType.Protection2)
                .Name(LocaleString.ProtectionII)
                .Description(LocaleString.ProtectionIIDescription)
                .HasRecastDelay(RecastGroup.Protection, 15f)
                .HasActivationDelay(4f)
                .RequirementEP(28)
                .UsesAnimation(AnimationType.LoopingConjure1)
                .DisplaysVisualEffectWhenActivating()
                .ResonanceCost(2)
                .HasImpactAction((activator, target, location) =>
                {
                    ApplyPartyStatusAOE<Protection2StatusEffect>(activator, target, 10f, 30);
                });
        }

        private void Protection3()
        {
            _builder.Create(FeatType.Protection3)
                .Name(LocaleString.ProtectionIII)
                .Description(LocaleString.ProtectionIIIDescription)
                .HasRecastDelay(RecastGroup.Protection, 15f)
                .HasActivationDelay(4f)
                .RequirementEP(56)
                .UsesAnimation(AnimationType.LoopingConjure1)
                .DisplaysVisualEffectWhenActivating()
                .ResonanceCost(3)
                .HasImpactAction((activator, target, location) =>
                {
                    ApplyPartyStatusAOE<Protection3StatusEffect>(activator, target, 10f, 30);
                });
        }
    }
}