using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Anvil.API;
using Anvil.Services;
using static Anvil.API.Events.ModuleEvents;
using XM.API.Constants;
using XM.UI;
using XM.UI.UI;
using GuiEventType = XM.API.Constants.GuiEventType;

namespace XM.Quest
{
    [ServiceBinding(typeof(JournalWindowReplacementService))]
    internal class JournalWindowReplacementService
    {
        private readonly GuiService _gui;

        public JournalWindowReplacementService(GuiService gui)
        {
            _gui = gui;

            NwModule.Instance.OnPlayerGuiEvent += OnPlayerGuiEvent;
        }

        private void OnPlayerGuiEvent(OnPlayerGuiEvent obj)
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
                _gui.TogglePlayerWindow(player, GuiWindowType.Quests);
            }
        }
    }
}
