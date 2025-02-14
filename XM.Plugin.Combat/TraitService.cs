using Anvil.Services;
using XM.Plugin.Combat.StatusEffect;
using XM.Plugin.Combat.StatusEffect.StatusEffectDefinition;
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
        }

        private void ApplyTraitEffects(uint module)
        {
            var player = GetEnteringObject();

            _statusEffect.ApplyPermanentStatusEffect<NaturalRegenStatusEffect>(player);
        }
    }
}
