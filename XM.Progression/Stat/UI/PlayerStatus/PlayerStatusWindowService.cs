using Anvil.API;
using Anvil.API.Events;
using Anvil.Services;
using NWN.Core.NWNX;
using XM.Core.EventManagement.AreaEvent;
using XM.Core.EventManagement.NWNXEvent;
using XM.Core.EventManagement.PlayerEvent;
using XM.Progression.Stat.Event;
using XM.UI;

namespace XM.Progression.Stat.UI.PlayerStatus
{
    [ServiceBinding(typeof(PlayerStatusWindowService))]
    [ServiceBinding(typeof(IPlayerOnDamagedEvent))]
    [ServiceBinding(typeof(IPlayerEPAdjustedEvent))]
    [ServiceBinding(typeof(IHealAfterEvent))]
    [ServiceBinding(typeof(IAreaEnterEvent))]
    internal class PlayerStatusWindowService :
        IPlayerOnDamagedEvent,
        IPlayerEPAdjustedEvent,
        IHealAfterEvent,
        IAreaEnterEvent
    {
        private readonly GuiService _gui;

        public PlayerStatusWindowService(GuiService gui)
        {
            _gui = gui;
            
            HookEvents();
        }

        private void HookEvents()
        {
            NwModule.Instance.OnItemEquip += OnNWNXEquipItem;
            NwModule.Instance.OnItemUnequip += OnNWNXUnequipItem;

        }

        private void PublishEvents(uint target)
        {
            if (!GetIsPC(target) || GetIsDM(target) || GetIsDMPossessed(target))
                return;

            _gui.PublishRefreshEvent(target, new PlayerStatusRefreshEvent(PlayerStatusRefreshEvent.StatType.HP));
            _gui.PublishRefreshEvent(target, new PlayerStatusRefreshEvent(PlayerStatusRefreshEvent.StatType.EP));
        }

        private void OnNWNXEquipItem(OnItemEquip obj)
        {
            PublishEvents(OBJECT_SELF);
        }
        private void OnNWNXUnequipItem(OnItemUnequip obj)
        {
            PublishEvents(OBJECT_SELF);
        }

        public void PlayerOnDamaged()
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
