using System.Collections.Generic;
using Anvil.Services;
using XM.Inventory.Loot;
using XM.Progression.Ability;
using XM.Progression.Recast;
using XM.Progression.StatusEffect;
using XM.Shared.API.Constants;
using XM.Shared.Core;
using XM.Shared.Core.Localization;

namespace XM.Plugin.Combat.AbilityDefinition.Nightstalker
{
    [ServiceBinding(typeof(IAbilityListDefinition))]
    internal class Steal : IAbilityListDefinition
    {
        private readonly AbilityBuilder _builder = new();
        private readonly StatusEffectService _status;
        private readonly LootService _loot;

        public Steal(
            StatusEffectService status,
            LootService loot)
        {
            _status = status;
            _loot = loot;
        }

        public Dictionary<FeatType, AbilityDetail> BuildAbilities()
        {
            StealAbility();

            return _builder.Build();
        }

        private void StealAbility()
        {
            _builder.Create(FeatType.Steal)
                .Name(LocaleString.Steal)
                .Description(LocaleString.StealDescription)
                .Classification(AbilityCategoryType.Special)
                .HasRecastDelay(RecastGroup.Steal, 60f * 3f)
                .HasActivationDelay(2f)
                .RequirementEP(7)
                .ResonanceCost(1)
                .HasCustomValidation((activator, target, location) =>
                {
                    var attackTarget = GetAttackTarget(activator);

                    if (!GetIsObjectValid(attackTarget))
                        return LocaleString.NoAttackTargetAvailable.ToLocalizedString();

                    return string.Empty;
                })
                .HasImpactAction((activator, target, location) =>
                {
                    var attackTarget = GetAttackTarget(activator);
                    var item = _loot.GetRandomStealableItem(attackTarget);

                    if (!GetIsObjectValid(item))
                    {
                        var message = LocaleString.YourTargetHasNothingToSteal.ToLocalizedString();
                        SendMessageToPC(activator, ColorToken.Red(message));
                        return;
                    }

                    var copy = CopyItem(item, activator, true);
                    
                    DestroyObject(item);

                    var activatorName = GetName(activator);
                    var itemName = GetName(copy);
                    var targetName = GetName(target);
                    var stealMessage = LocaleString.XStoleYFromZ.ToLocalizedString(activatorName, itemName, targetName);
                    Messaging.SendMessageNearbyToPlayers(activator, stealMessage);
                });
        }
    }
}