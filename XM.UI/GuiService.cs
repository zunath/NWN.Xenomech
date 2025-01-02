using System;
using System.Collections.Generic;
using Anvil.API;
using Anvil.Services;
using XM.Core.EventManagement;
using XM.UI.Builder;
using XM.UI.TestUI;

namespace XM.UI
{
    [ServiceBinding(typeof(GuiService))]
    public class GuiService
    {
        [Inject]
        public IList<IView> Views { get; set; }

        private readonly Dictionary<Type, IView> _viewsByType = new();
        private readonly Dictionary<Type, NuiWindow> _windowsByType = new();
        private readonly Dictionary<Type, Json> _serializedWindowsByType = new();
        private readonly NuiEventCollection _registeredEvents = new();

        private readonly XMEventService _event;

        public GuiService(XMEventService @event)
        {
            _event = @event;

            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            _event.Subscribe<NWNXEvent.OnModulePreload>(OnModulePreload);
            _event.Subscribe<ModuleEvent.OnNuiEvent>(OnNuiEvent);
        }

        private void OnNuiEvent()
        {
            Console.WriteLine($"Nui event firing");
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
                var buildResult = view.Build();
                var window = buildResult.Window;
                var elementEvents = buildResult.EventCollection;
                var json = JsonUtility.ToJson(window);
                _serializedWindowsByType[type] = JsonParse(json);
                _windowsByType[type] = window;
                _viewsByType[type] = view;

                foreach (var (elementId, eventCollection) in elementEvents)
                {
                    if (!_registeredEvents.ContainsKey(elementId))
                        _registeredEvents[elementId] = new Dictionary<NuiEventType, string>();

                    foreach (var (eventType, methodName) in eventCollection)
                    {
                        _registeredEvents[elementId][eventType] = methodName;
                    }

                }
            }
        }

        public void ShowWindow<TView>(uint player)
            where TView : IView
        {
            var type = typeof(TView);
            var window = _windowsByType[type];

            if (NuiFindWindow(player, window.Id) == 0)
            {
                var view = _viewsByType[type];
                var json = _serializedWindowsByType[type];
                var viewModel = view.CreateViewModel();

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