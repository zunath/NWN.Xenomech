using Anvil.Services;
using XM.Plugin.Item.AppearanceEditor.Event;
using XM.Plugin.Item.AppearanceEditor.UI;
using XM.Shared.Core.EventManagement;
using XM.UI;
using static XM.Shared.Core.EventManagement.XMEvent;

namespace XM.Plugin.Item.AppearanceEditor
{
    [ServiceBinding(typeof(AppearanceEditorService))]
    internal class AppearanceEditorService
    {
        private readonly XMEventService _event;
        private readonly GuiService _gui;

        public AppearanceEditorService(
            XMEventService @event,
            GuiService gui)
        {
            _event = @event;
            _gui = gui;

            RegisterEvents();
            SubscribeEvents();
        }

        private void RegisterEvents()
        {
            _event.RegisterEvent<AppearanceEditorEvents.EditAppearance>(AppearanceEditorScript.EditAppearanceScript);
        }

        private void SubscribeEvents()
        {
            _event.Subscribe<OnPlayerOpenAppearanceMenu>(OpenAppearanceEditor);
            _event.Subscribe<NWNXEvent.OnDmPossessBefore>(CloseAppearanceEditor);
            _event.Subscribe<NWNXEvent.OnDmPossessFullPowerBefore>(CloseAppearanceEditor);
        }

        private void CloseAppearanceEditor(uint dm)
        {
            _gui.CloseWindow<AppearanceEditorView>(dm);
        }

        private void OpenAppearanceEditor(uint player)
        {
            _gui.ShowWindow<AppearanceEditorView>(player);
        }
    }
}
