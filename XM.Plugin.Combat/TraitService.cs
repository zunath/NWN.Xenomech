using Anvil.Services;
using NWN.Core.NWNX;
using XM.Plugin.Combat.StatusEffectDefinition.Buff;
using XM.Progression.StatusEffect;
using XM.Shared.API.Constants;
using XM.Shared.Core;
using XM.Shared.Core.EventManagement;

namespace XM.Plugin.Combat
{
    [ServiceBinding(typeof(TraitService))]
    internal class TraitService
    {
        private readonly XMEventService _event;
        private readonly StatusEffectService _statusEffect;

        public TraitService(
            XMEventService @event,
            StatusEffectService statusEffect)
        {
            _event = @event;
            _statusEffect = statusEffect;

            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            _event.Subscribe<ModuleEvent.OnPlayerEnter>(ApplyTraitEffects);
            _event.Subscribe<NWNXEvent.OnItemDecrementStacksizeBefore>(RecycleTrait);
        }

        private void ApplyTraitEffects(uint module)
        {
            var player = GetEnteringObject();

            _statusEffect.ApplyPermanentStatusEffect<NaturalRegenStatusEffect>(player, player);
        }

        private void RecycleTrait(uint item)
        {
            var type = GetBaseItemType(item);
            if (type != BaseItemType.Arrow &&
                type != BaseItemType.Bolt &&
                type != BaseItemType.Bullet)
                return;

            var possessor = GetItemPossessor(item);
            var chance = 0;
            if (GetHasFeat(FeatType.Recycle1, possessor))
            {
                chance += 20;
            }

            if (GetHasFeat(FeatType.Recycle2, possessor))
            {
                chance += 30;
            }

            if (XMRandom.D100(1) <= chance)
            {
                EventsPlugin.SkipEvent();
            }
        }

    }
}
