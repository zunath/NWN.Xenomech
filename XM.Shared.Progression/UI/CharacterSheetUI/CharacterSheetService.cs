using Anvil.Services;
using XM.Shared.API.Constants;
using XM.Shared.Core.EventManagement;
using XM.UI;

namespace XM.Progression.UI.CharacterSheetUI
{
    [ServiceBinding(typeof(CharacterSheetService))]
    internal class CharacterSheetService
    {
        private readonly GuiService _gui;
        private readonly XMEventService _event;

        public CharacterSheetService(
            GuiService gui,
            XMEventService @event)
        {
            _gui = gui;
            _event = @event;

            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            _event.Subscribe<ModuleEvent.OnPlayerGui>(ToggleCharacterSheetWindow);
        }

        private void ToggleCharacterSheetWindow()
        {
            var type = GetLastGuiEventType();
            if (type != GuiEventType.DisabledPanelAttemptOpen)
                return;

            var player = GetLastGuiEventPlayer();
            var panelType = (GuiPanelType)GetLastGuiEventInteger();
            if (panelType != GuiPanelType.CharacterSheet)
                return;

            _gui.ToggleWindow<CharacterSheetView>(player);
        }
    }
}
