using System.Collections.Generic;
using Anvil.Services;
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

            return _builder.Build();
        }

        private readonly StatusEffectService _status;
        public Protection(PartyService party, StatusEffectService status) 
            : base(party, status)
        {
            _status = status;
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
                    //ApplyPartyAOE<ProtectionStatusEffect>(activator, target, 10f, 30);
                });
        }
    }
}