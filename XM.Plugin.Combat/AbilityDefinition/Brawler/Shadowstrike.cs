using System.Collections.Generic;
using Anvil.Services;
using XM.Plugin.Combat.StatusEffectDefinition.Debuff;
using XM.Progression.Ability;
using XM.Progression.Recast;
using XM.Progression.Stat;
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
        private readonly StatusEffectService _status;
        private readonly SpellService _spell;

        public Shadowstrike(
            PartyService party,
            StatusEffectService status,
            SpellService spell)
            : base(party, status)
        {
            _status = status;
            _spell = spell;
        }

        private void Impact(uint activator, List<uint> targets, int dmg)
        {
            ApplyEffectToObject(DurationType.Instant, EffectVisualEffect(VisualEffectType.ImpDeathWard), activator);

            foreach (var target in targets)
            {
                if (target == activator)
                    continue;

                var damage = _spell.CalculateSpellDamage(activator, target, dmg, ResistType.Darkness, AbilityType.Might, AbilityType.Might);
                var duration = _spell.CalculateResistedTicks(activator, ResistType.Darkness, 32);

                _status.ApplyStatusEffect<KnockdownStatusEffect>(activator, target, duration);
                AssignCommand(activator, () =>
                {
                    ApplyEffectToObject(DurationType.Instant, EffectDamage(damage, DamageType.Darkness), target);
                });
            }
        }

        public override Dictionary<FeatType, AbilityDetail> BuildAbilities()
        {
            Shadowstrike1();
            Shadowstrike2();

            return _builder.Build();
        }

        private void Shadowstrike1()
        {
            _builder.Create(FeatType.Shadowstrike1)
                .Name(LocaleString.ShadowstrikeI)
                .Description(LocaleString.ShadowstrikeIDescription)
                .HasRecastDelay(RecastGroup.Shadowstrike, 30f)
                .HasActivationDelay(2f)
                .DisplaysVisualEffectWhenActivating()
                .IsCastedAbility()
                .RequirementEP(22)
                .ResonanceCost(1)
                .TelegraphSize(4f, 4f)
                .HasTelegraphSphereAction((activator, targets, targetLocation) =>
                {
                    Impact(activator, targets, 18);
                });
        }

        private void Shadowstrike2()
        {
            _builder.Create(FeatType.Shadowstrike2)
                .Name(LocaleString.ShadowstrikeII)
                .Description(LocaleString.ShadowstrikeIIDescription)
                .HasRecastDelay(RecastGroup.Shadowstrike, 30f)
                .HasActivationDelay(2f)
                .DisplaysVisualEffectWhenActivating()
                .IsCastedAbility()
                .RequirementEP(45)
                .ResonanceCost(2)
                .TelegraphSize(4f, 4f)
                .HasTelegraphSphereAction((activator, targets, targetLocation) =>
                {
                    Impact(activator, targets, 45);
                });
        }
    }
}
