using Anvil.Services;
using XM.Progression.StatusEffect;
using XM.Progression.StatusEffect.StatusEffectDefinition;
using XM.Shared.API.Constants;
using XM.Shared.Core.EventManagement;
using XM.Shared.Core.Localization;
using XM.Shared.Core.Party;

namespace XM.Plugin.Combat
{
    [ServiceBinding(typeof(PlayerRestService))]
    internal class PlayerRestService
    {
        private readonly PartyService _party;
        private readonly StatusEffectService _statusEffect;

        public PlayerRestService(
            XMEventService @event, 
            PartyService party,
            StatusEffectService statusEffect)
        {
            _party = party;
            _statusEffect = statusEffect;

            @event.Subscribe<ModuleEvent.OnPlayerRest>(OnPlayerRest);
        }

        private LocaleString CanRest(uint player)
        {
            if (GetIsInCombat(player))
                return LocaleString.YouCannotRestDuringCombat;


            var nearestEnemy = GetNearestCreature(CreatureType.Reputation, (int)ReputationType.Enemy, player);
            if (GetIsObjectValid(nearestEnemy) && GetDistanceBetween(player, nearestEnemy) <= 20f)
            {
                return LocaleString.YouCannotRestWhileEnemiesAreNearby;
            }

            foreach (var member in _party.GetAllPartyMembersWithinRange(player, 20f))
            {
                if (GetIsInCombat(member))
                {
                    return LocaleString.YouCannotRestWhileEnemiesAreNearby;
                }
            }

            return LocaleString.Invalid;
        }

        private void OnPlayerRest(uint module)
        {
            var type = GetLastRestEventType();

            if (type != RestEventType.RestStarted)
                return;

            var player = GetLastPCRested();
            AssignCommand(player, () => ClearAllActions());

            var canRest = CanRest(player);
            if (canRest != LocaleString.Invalid)
            {
                SendMessageToPC(player, canRest.ToLocalizedString());
                return;
            }

            _statusEffect.ApplyPermanentStatusEffect<RestStatusEffect>(player);
        }
    }
}
