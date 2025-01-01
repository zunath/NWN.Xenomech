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

        private readonly Dictionary<Type, IView> _viewsByType = new();
        private readonly Dictionary<Type, NuiWindow> _windows = new();
        private readonly Dictionary<Type, Json> _serializedWindows = new();
        
        private readonly XMEventService _event;

        public GuiService(XMEventService @event)
        {
            _event = @event;

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
                _viewsByType[type] = view;
            }
        }

        public void ShowWindow<TView>(uint player)
            where TView : IView
        {
            var type = typeof(TView);
            var window = _windows[type];

            if (NuiFindWindow(player, window.Id) == 0)
            {
                var view = _viewsByType[type];
                var json = _serializedWindows[type];
                var viewModel = view.CreateViewModel(player);

                var windowToken = NuiCreate(player, json, window.Id);
                viewModel.Bind(player, windowToken);
                viewModel.OnOpen();
            }
        }


        [ScriptHandler("bread_test")]
        public void ShowWindow()
        {
            var player = GetLastUsedBy();
            ShowWindow<TestView>(player);
        }
    }
}