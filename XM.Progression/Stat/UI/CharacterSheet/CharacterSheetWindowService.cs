using Anvil.Services;
using XM.API.Constants;
using XM.Core.EventManagement;
using XM.UI;
using XM.UI.UI;
using GuiEventType = XM.API.Constants.GuiEventType;

namespace XM.Progression.Stat.UI.CharacterSheet
{
    [ServiceBinding(typeof(CharacterSheetWindowService))]
    internal class CharacterSheetWindowService
    {
        private readonly GuiService _gui;
        private readonly XMEventService _event;

        public CharacterSheetWindowService(GuiService gui, XMEventService @event)
        {
            _gui = gui;
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
        /// Skips the default character sheet NWN window open events and shows the XM windows instead.
        /// </summary>
        private void ReplaceNWNGuis()
        {
            var player = GetLastGuiEventPlayer();
            var type = GetLastGuiEventType();
            if (type != GuiEventType.DisabledPanelAttemptOpen) return;
            var target = GetLastGuiEventObject();

            var panelType = (GuiPanelType)GetLastGuiEventInteger();
            if (panelType == GuiPanelType.CharacterSheet)
            {
                // Player character sheet
                if (target == player)
                {
                    var payload = new CharacterSheetPayload(player, true);
                    _gui.TogglePlayerWindow(player, GuiWindowType.CharacterSheet, payload);
                }
                // Associate character sheet (droid, pet, etc.)
                else if (GetMaster(target) == player)
                {
                    var payload = new CharacterSheetPayload(target, false);
                    _gui.TogglePlayerWindow(player, GuiWindowType.CharacterSheet, payload);
                }
            }
        }
    }
}
