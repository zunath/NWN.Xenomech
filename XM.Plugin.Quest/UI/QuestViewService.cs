using Anvil.Services;
using XM.Shared.API.Constants;
using XM.Shared.Core.EventManagement;
using XM.UI;

namespace XM.Quest.UI
{
    [ServiceBinding(typeof(QuestViewService))]
    internal class QuestViewService
    {
        public GuiService _gui { get; set; }

        public QuestViewService(
            GuiService gui,
            XMEventService @event)
        {
            _gui = gui;

            @event.Subscribe<ModuleEvent.OnPlayerGui>(ToggleQuestWindow);
        }

        private void ToggleQuestWindow()
        {
            var type = GetLastGuiEventType();
            if (type != GuiEventType.DisabledPanelAttemptOpen)
                return;

            var player = GetLastGuiEventPlayer();
            var panelType = (GuiPanelType)GetLastGuiEventInteger();
            if (panelType != GuiPanelType.Journal)
                return;

            _gui.ToggleWindow<QuestView>(player);
        }

    }
}
