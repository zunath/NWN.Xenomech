using Anvil.Services;
using NWN.Core.NWNX;
using XM.Progression.Event;
using XM.Shared.Core.EventManagement;
using XM.UI;
using XM.UI.Event;

namespace XM.Progression.UI.PlayerStatus
{
    [ServiceBinding(typeof(PlayerStatusService))]
    internal class PlayerStatusService
    {
        private readonly GuiService _gui;
        private readonly XMEventService _event;

        public PlayerStatusService(
            XMEventService @event,
            GuiService gui)
        {
            _gui = gui;
            _event = @event;

            SubscribeEvents();
        }

        private void SubscribeEvents()
        {

            _event.Subscribe<ModuleEvent.OnPlayerEnter>(OnPlayerEnter);
            _event.Subscribe<AreaEvent.OnAreaEnter>(OnPlayerEnter);

            _event.Subscribe<NWNXEvent.OnHealAfter>(OnPlayerHealed);
            _event.Subscribe<StatEvent.PlayerEPAdjustedEvent>(OnPlayerEPAdjusted);
            _event.Subscribe<PlayerEvent.OnDamaged>(OnPlayerDamaged);

            _event.Subscribe<NWNXEvent.OnItemEquipAfter>(OnPlayerEquipItem);
            _event.Subscribe<NWNXEvent.OnItemUnequipAfter>(OnPlayerUnequipItem);
        }


        private void OnPlayerEnter(uint objectSelf)
        {
            var player = GetEnteringObject();
            if (!GetIsPC(player) || GetIsDM(player) || GetIsDMPossessed(player))
                return;

            _gui.ShowWindow<PlayerStatusView>(player);
        }

        private void OnPlayerEquipItem(uint objectSelf)
        {
            var player = OBJECT_SELF;
            if (!GetIsPC(player) || GetIsDM(player) || GetIsDMPossessed(player))
                return;

            _event.PublishEvent<UIEvent.UIRefreshEvent>(player);
        }

        private void OnPlayerUnequipItem(uint objectSelf)
        {
            var player = OBJECT_SELF;
            if (!GetIsPC(player) || GetIsDM(player) || GetIsDMPossessed(player))
                return;

            _event.PublishEvent<UIEvent.UIRefreshEvent>(player);
        }

        private void OnPlayerHealed(uint objectSelf)
        {
            var player = StringToObject(EventsPlugin.GetEventData("TARGET_OBJECT_ID"));
            if (!GetIsPC(player) || GetIsDM(player) || GetIsDMPossessed(player))
                return;

            _event.PublishEvent<UIEvent.UIRefreshEvent>(player);
        }

        private void OnPlayerEPAdjusted(uint objectSelf)
        {
            var player = OBJECT_SELF;
            if (!GetIsPC(player) || GetIsDM(player) || GetIsDMPossessed(player))
                return;

            _event.PublishEvent<UIEvent.UIRefreshEvent>(player);
        }

        private void OnPlayerDamaged(uint objectSelf)
        {
            var player = OBJECT_SELF;
            if (!GetIsPC(player) || GetIsDM(player) || GetIsDMPossessed(player))
                return;

            _event.PublishEvent<UIEvent.UIRefreshEvent>(player);
        }


    }
}
