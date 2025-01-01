using System;
using System.Collections.Generic;
using Anvil.API;
using Anvil.Services;
using XM.Core.EventManagement;
using XM.UI.TestUI;

namespace XM.UI
{
    [ServiceBinding(typeof(GuiService))]
    public class GuiService
    {
        [Inject]
        public IList<IView> Views { get; set; }

        private readonly Dictionary<Type, NuiWindow> _windows;
        private readonly Dictionary<Type, Json> _serializedWindows;
        
        private readonly XMEventService _event;

        public GuiService(XMEventService @event)
        {
            _event = @event;

            _windows = new Dictionary<Type, NuiWindow>();
            _serializedWindows = new Dictionary<Type, Json>();

            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            _event.Subscribe<NWNXEvent.OnModulePreload>(OnModulePreload);
        }

        private void OnModulePreload()
        {
            CacheViews();
        }

        private void CacheViews()
        {
            foreach (var view in Views)
            {
                var type = view.GetType();
                var window = view.Build();
                var json = JsonUtility.ToJson(window);
                _serializedWindows[type] = JsonParse(json);
                _windows[type] = window;
            }
        }

        public void ShowWindow<TView>(uint player)
            where TView : IView
        {
            var type = typeof(TView);
            var json = _serializedWindows[type];
            var window = _windows[type];

            NuiCreate(player, json, window.Id);

        }


        [ScriptHandler("bread_test")]
        public void ShowWindow()
        {
            var player = GetLastUsedBy();
            ShowWindow<TestView>(player);
        }
    }
}