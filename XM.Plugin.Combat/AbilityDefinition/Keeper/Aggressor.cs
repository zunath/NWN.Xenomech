using System.Collections.Generic;
using Anvil.Services;
using XM.Plugin.Combat.StatusEffectDefinition;
using XM.Progression.Ability;
using XM.Progression.Recast;
using XM.Progression.StatusEffect;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Plugin.Combat.AbilityDefinition.Keeper
{
    [ServiceBinding(typeof(IAbilityListDefinition))]
    internal class Aggressor: IAbilityListDefinition
    {
        private readonly AbilityBuilder _builder = new();

        private readonly StatusEffectService _status;

        public Aggressor(StatusEffectService status)
        {
            _status = status;
        }

        public Dictionary<FeatType, AbilityDetail> BuildAbilities()
        {
            Aggressor1();
            Aggressor2();

            return _builder.Build();
        }

        private bool HasEffect(uint activator)
        {
            return _status.HasEffect<Aggressor1StatusEffect>(activator) ||
                   _status.HasEffect<Aggressor2StatusEffect>(activator);
        }

        private void RemoveEffects(uint activator)
        {
            _status.RemoveStatusEffect<Aggressor1StatusEffect>(activator);
            _status.RemoveStatusEffect<Aggressor2StatusEffect>(activator);
        }

        private void Aggressor1()
        {
            _builder.Create(FeatType.Aggressor1)
                .Name(LocaleString.AggressorI)
                .Description(LocaleString.AggressorIDescription)
                .HasRecastDelay(RecastGroup.Aggressor, 60f)
                .HasActivationDelay(2f)
                .UsesAnimation(AnimationType.FireForgetTaunt)
                .IsCastedAbility()
                .ResonanceCost(1)
                .IsToggled(HasEffect)
                .HasToggleAction((activator, isToggled) =>
                {
                    if (isToggled)
                    {
                        _status.ApplyPermanentStatusEffect<Aggressor1StatusEffect>(activator, activator);
                    }
                    else
                    {
                        RemoveEffects(activator);
                    }
                });
        }

        private void Aggressor2()
        {
            _builder.Create(FeatType.Aggressor2)
                .Name(LocaleString.AggressorII)
                .Description(LocaleString.AggressorIIDescription)
                .HasRecastDelay(RecastGroup.Aggressor, 60f)
                .HasActivationDelay(2f)
                .UsesAnimation(AnimationType.FireForgetTaunt)
                .IsCastedAbility()
                .ResonanceCost(2)
                .IsToggled(HasEffect)
                .HasToggleAction((activator, isToggled) =>
                {
                    if (isToggled)
                    {
                        _status.ApplyPermanentStatusEffect<Aggressor2StatusEffect>(activator, activator);
                    }
                    else
                    {
                        RemoveEffects(activator);
                    }
                });
        }
    }
}
