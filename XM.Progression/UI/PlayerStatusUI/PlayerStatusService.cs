using XM.Core.EventManagement;
using XM.UI;

namespace XM.Progression.UI.PlayerStatusUI
{
    //[ServiceBinding(typeof(PlayerStatusService))]
    internal class PlayerStatusService
    {
        private readonly GuiService _gui;

        public PlayerStatusService(
            XMEventService @event,
            GuiService gui)
        {
            @event.Subscribe<ModuleEvent.OnPlayerEnter>(OnPlayerEnter);
            _gui = gui;
        }

        private void OnPlayerEnter()
        {
            var player = GetEnteringObject();
            _gui.ShowWindow<PlayerStatusView>(player);
        }
    }
}
