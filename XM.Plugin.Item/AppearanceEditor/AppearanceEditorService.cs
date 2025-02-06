using Anvil.Services;
using XM.Plugin.Item.AppearanceEditor.Event;
using XM.Shared.Core.EventManagement;

namespace XM.Plugin.Item.AppearanceEditor
{
    [ServiceBinding(typeof(AppearanceEditorService))]
    internal class AppearanceEditorService
    {
        private readonly XMEventService _event;

        public AppearanceEditorService(XMEventService @event)
        {
            _event = @event;

            RegisterEvents();
            SubscribeEvents();
        }

        private void RegisterEvents()
        {
            _event.RegisterEvent<AppearanceEditorEvents.EditAppearance>(AppearanceEditorScript.EditAppearanceScript);
        }

        private void SubscribeEvents()
        {

        }
    }
}
