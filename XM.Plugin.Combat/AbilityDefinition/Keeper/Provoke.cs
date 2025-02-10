using System.Collections.Generic;
using Anvil.Services;
using XM.AI.Enmity;
using XM.Progression.Ability;
using XM.Progression.Recast;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Plugin.Combat.AbilityDefinition.Keeper
{
    [ServiceBinding(typeof(IAbilityListDefinition))]
    internal class Provoke: IAbilityListDefinition
    {
        private readonly AbilityBuilder _builder = new();

        private readonly EnmityService _enmity;

        public Provoke(EnmityService enmity)
        {
            _enmity = enmity;
        }

        public Dictionary<FeatType, AbilityDetail> BuildAbilities()
        {
            Provoke1();
            Provoke2();

            return _builder.Build();
        }

        private string Validation(uint target)
        {
            if (GetIsPC(target))
            {
                return "This ability cannot be used on players.";
            }

            return string.Empty;
        }

        private void Impact(uint activator, uint target, int enmity)
        {
            if (!LineOfSightObject(activator, target))
                return;

            _enmity.ModifyEnmity(activator, target, EnmityType.Volatile, enmity);
            ApplyEffectToObject(DurationType.Instant, EffectVisualEffect(VisualEffectType.FnfHowlOdd), target);
        }

        private void Provoke1()
        {
            _builder.Create(FeatType.Provoke1)
                .Name(LocaleString.ProvokeI)
                .Description(LocaleString.ProvokeIDescription)
                .HasRecastDelay(RecastGroup.Provoke, 30f)
                .UsesAnimation(AnimationType.FireForgetTaunt)
                .IsCastedAbility()
                .HasMaxRange(15f)
                .RequirementEP(2)
                .ResonanceCost(1)
                .HasCustomValidation((activator, target, location) => Validation(target))
                .HasImpactAction((activator, target, location) =>
                {
                    Impact(activator, target, 1800);
                });
        }

        private void Provoke2()
        {
            _builder.Create(FeatType.Provoke2)
                .Name(LocaleString.ProvokeII)
                .Description(LocaleString.ProvokeIIDescription)
                .HasRecastDelay(RecastGroup.Provoke, 30f)
                .UsesAnimation(AnimationType.FireForgetTaunt)
                .IsCastedAbility()
                .HasMaxRange(15f)
                .RequirementEP(4)
                .ResonanceCost(2)
                .HasCustomValidation((activator, target, location) => Validation(target))
                .HasImpactAction((activator, target, location) =>
                {
                    var nth = 1;
                    var nearest = GetNearestCreatureToLocation(CreatureType.IsAlive, 1, location, nth);

                    while (GetIsObjectValid(nearest))
                    {
                        if (GetDistanceBetweenLocations(GetLocation(nearest), location) > 8f)
                            break;

                        if (!GetIsPC(nearest))
                        {
                            Impact(activator, nearest, 2000);
                        }

                        nth++;
                        nearest = GetNearestCreatureToLocation(CreatureType.IsAlive, 1, location, nth);
                    }
                });
        }
    }
}
