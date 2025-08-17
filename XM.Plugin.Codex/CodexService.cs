using Anvil.Services;
using XM.Shared.API.Constants;
using XM.Shared.Core.EventManagement;
using XM.UI;

namespace XM.Codex
{
    [ServiceBinding(typeof(CodexService))]
    internal class CodexService
    {
        private readonly GuiService _gui;

        public CodexService(GuiService gui, XMEventService @event)
        {
            _gui = gui;

            @event.Subscribe<ModuleEvent.OnPlayerGui>(ToggleCodexWindow);
            @event.Subscribe<XMEvent.OnPlayerOpenCodexMenu>(OnOpenCodexMenu);
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

        private void OnOpenCodexMenu(uint objectSelf)
        {
            _gui.ToggleWindow<CodexView>(objectSelf);
        }
    }
}


