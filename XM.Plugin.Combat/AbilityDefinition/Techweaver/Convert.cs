using System.Collections.Generic;
using Anvil.Services;
using XM.Progression.Ability;
using XM.Progression.Recast;
using XM.Progression.Stat;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Plugin.Combat.AbilityDefinition.Techweaver
{
    [ServiceBinding(typeof(IAbilityListDefinition))]
    internal class Convert: IAbilityListDefinition
    {
        private readonly AbilityBuilder _builder = new();
        private readonly StatService _stat;

        public Convert(StatService stat)
        {
            _stat = stat;
        }

        public Dictionary<FeatType, AbilityDetail> BuildAbilities()
        {
            ConvertAbility();

            return _builder.Build();
        }

        private void ConvertAbility()
        {
            _builder.Create(FeatType.Convert)
                .Name(LocaleString.Convert)
                .Description(LocaleString.ConvertDescription)
                .HasRecastDelay(RecastGroup.Convert, 60f * 10f)
                .HasActivationDelay(1f)
                .UsesAnimation(AnimationType.LoopingConjure1)
                .DisplaysVisualEffectWhenActivating()
                .ResonanceCost(3)
                .HasCustomValidation((activator, target, location) =>
                {
                    if (_stat.GetCurrentEP(activator) <= 0)
                    {
                        return LocaleString.InsufficientEP.ToLocalizedString();
                    }

                    return string.Empty;
                })
                .HasImpactAction((activator, target, location) =>
                {
                    var hp = _stat.GetCurrentHP(activator);
                    var maxHP = _stat.GetMaxHP(activator);
                    var ep = _stat.GetCurrentEP(activator);
                    var maxEP = _stat.GetMaxEP(activator);

                    if (ep > maxHP)
                        ep = maxHP;

                    if (hp > maxEP)
                        hp = maxEP;

                    if(hp > ep)
                        ApplyEffectToObject(DurationType.Instant, EffectDamage(hp - ep), activator);
                    else if(hp < ep)
                        ApplyEffectToObject(DurationType.Instant, EffectHeal(ep - hp), activator);
                    
                    if(ep > hp)
                        _stat.ReduceEP(activator, ep - hp);
                    else if(ep < hp)
                        _stat.RestoreEP(activator, hp - ep);

                    ApplyEffectToObject(DurationType.Instant, EffectVisualEffect(VisualEffectType.ImpHarm), activator);
                });
        }
    }
}
