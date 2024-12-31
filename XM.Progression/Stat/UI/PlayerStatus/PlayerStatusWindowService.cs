using Anvil.API;
using Anvil.API.Events;
using Anvil.Services;
using NWN.Core.NWNX;
using XM.Core.EventManagement;
using XM.Core.EventManagement.AreaEvent;
using XM.Core.EventManagement.NWNXEvent;
using XM.Core.EventManagement.PlayerEvent;
using XM.Progression.Stat.Event;
using XM.UI;

namespace XM.Progression.Stat.UI.PlayerStatus
{
    [ServiceBinding(typeof(PlayerStatusWindowService))]
    internal class PlayerStatusWindowService
    {
        private readonly GuiService _gui;
        private readonly XMEventService _event;

        public PlayerStatusWindowService(GuiService gui, XMEventService @event)
        {
            _gui = gui;
            _event = @event;
            
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            _event.Subscribe<ItemEquipBeforeEvent>(OnNWNXEquipItem);
            _event.Subscribe<ItemUnequipBeforeEvent>(OnNWNXUnequipItem);
            _event.Subscribe<PlayerOnDamagedEvent>(OnPlayerOnDamaged);
            _event.Subscribe<PlayerEPAdjustedEvent>(OnPlayerEPAdjusted);
            _event.Subscribe<HealAfterEvent>(OnHealAfter);
            _event.Subscribe<AreaEnterEvent>(OnAreaEnter);
        }

        private void PublishEvents(uint target)
        {
            if (!GetIsPC(target) || GetIsDM(target) || GetIsDMPossessed(target))
                return;

            _gui.PublishRefreshEvent(target, new PlayerStatusRefreshEvent(PlayerStatusRefreshEvent.StatType.HP));
            _gui.PublishRefreshEvent(target, new PlayerStatusRefreshEvent(PlayerStatusRefreshEvent.StatType.EP));
        }

        private void OnNWNXEquipItem()
        {
            PublishEvents(OBJECT_SELF);
        }
        private void OnNWNXUnequipItem()
        {
            PublishEvents(OBJECT_SELF);
        }

        public void OnPlayerOnDamaged()
        {
            PublishEvents(OBJECT_SELF);
        }

        public void OnPlayerEPAdjusted()
        {
            PublishEvents(OBJECT_SELF);
        }

        public void OnHealAfter()
        {
            var target = StringToObject(EventsPlugin.GetEventData("TARGET_OBJECT_ID"));
            PublishEvents(target);
        }

        public void OnAreaEnter()
        {
            
        }
    }
}
