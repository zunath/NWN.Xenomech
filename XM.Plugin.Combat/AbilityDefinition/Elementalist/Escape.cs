using System.Collections.Generic;
using Anvil.Services;
using XM.Progression.Ability;
using XM.Progression.Recast;
using XM.Progression.StatusEffect;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;
using XM.Shared.Core.Party;

namespace XM.Plugin.Combat.AbilityDefinition.Elementalist
{
    [ServiceBinding(typeof(IAbilityListDefinition))]
    internal class Escape: AbilityBase
    {
        private readonly AbilityBuilder _builder = new();

        public Escape(
            StatusEffectService status,
            PartyService party)
            : base(party, status)
        {
        }

        private const string EscapePointVariable = "ESCAPE_POINT";

        public override Dictionary<FeatType, AbilityDetail> BuildAbilities()
        {
            EscapeAbility();

            return _builder.Build();
        }

        private void EscapeAbility()
        {
            _builder.Create(FeatType.Escape)
                .Name(LocaleString.Escape)
                .Description(LocaleString.EscapeDescription)
                .HasRecastDelay(RecastGroup.Escape, 60f)
                .IsCastedAbility()
                .RequirementEP(40)
                .UsesAnimation(AnimationType.LoopingConjure1)
                .HasActivationDelay(15f)
                .ResonanceCost(2)
                .HasCustomValidation((activator, target, location) =>
                {
                    var area = GetArea(activator);
                    var escapePoint = GetLocalString(area, EscapePointVariable);
                    var waypoint = GetWaypointByTag(escapePoint);
                    if (string.IsNullOrWhiteSpace(escapePoint) || !GetIsObjectValid(waypoint))
                    {
                        return LocaleString.YouCannotEscapeFromThisArea.ToLocalizedString();
                    }

                    return string.Empty;
                })
                .HasImpactAction((activator, target, location) =>
                {
                    var area = GetArea(activator);
                    var escapePoint = GetLocalString(area, EscapePointVariable);
                    var waypoint = GetWaypointByTag(escapePoint);
                    var escapeLocation = GetLocation(waypoint);

                    ApplyPartyAOE(activator, 10f, member =>
                    {
                        AssignCommand(member, () => ClearAllActions());
                        AssignCommand(member, () => ActionJumpToLocation(escapeLocation));
                        ApplyEffectToObject(DurationType.Instant, EffectVisualEffect(VisualEffectType.ImpUnsummon), member);
                    });
                });
        }
    }
}
