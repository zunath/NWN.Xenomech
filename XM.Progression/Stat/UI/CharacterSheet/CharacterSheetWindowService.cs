using Anvil.API;
using Anvil.Services;
using XM.API.Constants;
using XM.UI;
using XM.UI.WindowDefinition;
using static Anvil.API.Events.ModuleEvents;
using GuiEventType = XM.API.Constants.GuiEventType;

namespace XM.Progression.Stat.UI.CharacterSheet
{
    [ServiceBinding(typeof(CharacterSheetWindowService))]
    internal class CharacterSheetWindowService
    {
        private readonly GuiService _gui;

        public CharacterSheetWindowService(GuiService gui)
        {
            _gui = gui;

            NwModule.Instance.OnPlayerGuiEvent += OnPlayerGuiEvent;
        }

        private void OnPlayerGuiEvent(OnPlayerGuiEvent obj)
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
