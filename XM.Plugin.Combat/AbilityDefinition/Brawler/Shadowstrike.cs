using System;
using System.Collections.Generic;
using System.Numerics;
using Anvil.Services;
using XM.Plugin.Combat.StatusEffectDefinition;
using XM.Progression.Ability;
using XM.Progression.Recast;
using XM.Progression.StatusEffect;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;
using XM.Shared.Core.Party;

namespace XM.Plugin.Combat.AbilityDefinition.Brawler
{
    [ServiceBinding(typeof(IAbilityListDefinition))]
    internal class Shadowstrike: AbilityBase
    {
        private readonly AbilityBuilder _builder = new();

        public Shadowstrike(
            PartyService party,
            StatusEffectService status)
            : base(party, status)
        {
        }

        public override Dictionary<FeatType, AbilityDetail> BuildAbilities()
        {
            Shadowstrike1();

            return _builder.Build();
        }

        private void Shadowstrike1()
        {
            _builder.Create(FeatType.Shadowstrike1)
                .Name(LocaleString.ShadowStrike)
                .Description(LocaleString.ShadowstrikeIDescription)
                .HasRecastDelay(RecastGroup.Shadowstrike, 30f)
                .HasActivationDelay(4f)
                .RequirementEP(22)
                .ResonanceCost(1)
                .TelegraphSize(8f, 8f)
                .HasTelegraphSphereAction((activator, targets, targetLocation) =>
                {
                    Console.WriteLine($"firing shadowstrike");
                });
        }
    }
}
