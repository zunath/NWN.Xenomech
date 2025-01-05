using Anvil.Services;
using XM.API.Constants;
using XM.Core.EventManagement;
using GuiEventType = XM.API.Constants.GuiEventType;

namespace XM.Quest
{
    [ServiceBinding(typeof(JournalWindowReplacementService))]
    internal class JournalWindowReplacementService
    {
        private readonly XMEventService _event;

        public JournalWindowReplacementService(XMEventService @event)
        {
            _event = @event;

            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            _event.Subscribe<ModuleEvent.OnPlayerGui>(OnPlayerGuiEvent);
        }

        private void OnPlayerGuiEvent()
        {
            ReplaceNWNGuis();
        }

        /// <summary>
        /// Skips the default NWN journal window open events and shows the XM windows instead.
        /// </summary>
        private void ReplaceNWNGuis()
        {
            var player = GetLastGuiEventPlayer();
            var type = GetLastGuiEventType();
            if (type != GuiEventType.DisabledPanelAttemptOpen) return;
            var target = GetLastGuiEventObject();

            var panelType = (GuiPanelType)GetLastGuiEventInteger();
            if (panelType == GuiPanelType.Journal)
            {
                //_gui.TogglePlayerWindow(player, GuiWindowType.Quests);
            }
        }
    }
}
