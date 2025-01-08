using Anvil.Services;
using NWN.Core.NWNX;
using XM.Progression.Stat.Event;
using XM.Shared.API.BaseTypes;
using XM.Shared.Core.EventManagement;
using XM.UI;

namespace XM.Progression.UI.PlayerStatusUI
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
            _event.Subscribe<AreaEvent.AreaEnterEvent>(OnPlayerEnter);

            _event.Subscribe<NWNXEvent.OnHealAfter>(OnPlayerHealed);
            _event.Subscribe<PlayerEPAdjustedEvent>(OnPlayerEPAdjusted);
            _event.Subscribe<PlayerEvent.OnDamaged>(OnPlayerDamaged);

            _event.Subscribe<NWNXEvent.OnItemEquipAfter>(OnPlayerEquipItem);
            _event.Subscribe<NWNXEvent.OnItemUnequipAfter>(OnPlayerUnequipItem);
        }

        private void OnPlayerEnter()
        {
            var player = GetEnteringObject();
            if (!GetIsPC(player) || GetIsDM(player) || GetIsDMPossessed(player))
                return;

            _gui.ShowWindow<PlayerStatusView>(player);
        }

        private void OnPlayerEquipItem()
        {
            var player = OBJECT_SELF;
            if (!GetIsPC(player) || GetIsDM(player) || GetIsDMPossessed(player))
                return;

            _gui.PublishRefreshEvent(player, new PlayerHPAdjustedEvent());
            _gui.PublishRefreshEvent(player, new PlayerEPAdjustedEvent());
        }

        private void OnPlayerUnequipItem()
        {
            var player = OBJECT_SELF;
            if (!GetIsPC(player) || GetIsDM(player) || GetIsDMPossessed(player))
                return;

            _gui.PublishRefreshEvent(player, new PlayerHPAdjustedEvent());
            _gui.PublishRefreshEvent(player, new PlayerEPAdjustedEvent());
        }

        private void OnPlayerHealed()
        {
            var player = StringToObject(EventsPlugin.GetEventData("TARGET_OBJECT_ID"));
            if (!GetIsPC(player) || GetIsDM(player) || GetIsDMPossessed(player))
                return;


            _gui.PublishRefreshEvent(player, new PlayerHPAdjustedEvent());
        }

        private void OnPlayerEPAdjusted()
        {
            var player = OBJECT_SELF;
            if (!GetIsPC(player) || GetIsDM(player) || GetIsDMPossessed(player))
                return;

            _gui.PublishRefreshEvent(player, new PlayerEPAdjustedEvent());
        }

        private void OnPlayerDamaged()
        {
            var player = OBJECT_SELF;
            if (!GetIsPC(player) || GetIsDM(player) || GetIsDMPossessed(player))
                return;

            _gui.PublishRefreshEvent(player, new PlayerHPAdjustedEvent());
        }


    }
}
