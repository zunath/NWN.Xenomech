using System.Collections.Generic;
using Anvil.Services;
using XM.AI.Enmity;
using XM.Plugin.Combat.StatusEffectDefinition.Debuff;
using XM.Progression.Ability;
using XM.Progression.Recast;
using XM.Progression.Stat;
using XM.Progression.StatusEffect;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;
using XM.Shared.Core.Party;

namespace XM.Plugin.Combat.AbilityDefinition.Elementalist
{
    [ServiceBinding(typeof(IAbilityListDefinition))]
    internal class GravitonField: AbilityBase
    {
        private readonly AbilityBuilder _builder = new();
        private readonly SpellService _spell;
        private readonly StatusEffectService _status;
        private readonly EnmityService _enmity;

        public GravitonField(
            PartyService party,
            StatusEffectService status,
            SpellService spell,
            EnmityService enmity)
            : base(party, status)
        {
            _spell = spell;
            _status = status;
            _enmity = enmity;
        }

        public override Dictionary<FeatType, AbilityDetail> BuildAbilities()
        {
            GravitonFieldAbility();

            return _builder.Build();
        }

        private void GravitonFieldAbility()
        {
            _builder.Create(FeatType.GravitonField)
                .Name(LocaleString.GravitonField)
                .Description(LocaleString.GravitonFieldDescription)
                .HasRecastDelay(RecastGroup.GravitonField, 60f)
                .HasActivationDelay(4f)
                .DisplaysVisualEffectWhenActivating()
                .UsesAnimation(AnimationType.LoopingConjure1)
                .IsCastedAbility()
                .RequirementEP(90)
                .ResonanceCost(2)
                .HasMaxRange(10f)
                .IsHostile()
                .ResistType(ResistType.Darkness)
                .TelegraphSize(4f, 4f)
                .HasTelegraphSphereAction((activator, targets, targetLocation) =>
                {
                    ApplyEffectAtLocation(DurationType.Instant, EffectVisualEffect(VisualEffectType.ImpPdkGenericPulse), targetLocation);

                    foreach (var target in targets)
                    {
                        if (!GetIsReactionTypeHostile(target, activator))
                            continue;

                        var duration = _spell.CalculateResistedTicks(target, ResistType.Darkness, 60);
                        _status.ApplyStatusEffect<GravitonFieldStatusEffect>(activator, target, duration);
                        _enmity.ModifyEnmity(activator, target, EnmityType.Volatile, 500);
                    }
                });
        }
    }
}
