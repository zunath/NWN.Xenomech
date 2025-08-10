using Anvil.Services;
using XM.Shared.API.Constants;
using XM.Shared.Core.EventManagement;
using XM.UI;

namespace XM.UI.Codex
{
    [ServiceBinding(typeof(CodexService))]
    internal class CodexService
    {
        private readonly GuiService _gui;

        public CodexService(GuiService gui, XMEventService @event)
        {
            _gui = gui;

            @event.Subscribe<ModuleEvent.OnPlayerGui>(ToggleCodexWindow);
        }

        private void ToggleCodexWindow(uint objectSelf)
        {
            var type = GetLastGuiEventType();
            if (type != GuiEventType.DisabledPanelAttemptOpen)
                return;

            var player = GetLastGuiEventPlayer();
            var panelType = (GuiPanelType)GetLastGuiEventInteger();
            if (panelType != GuiPanelType.SpellBook)
                return;

            _gui.ToggleWindow<CodexView>(player);
        }
    }
}


