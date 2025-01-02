using System;
using System.Collections.Generic;
using System.Reflection;
using Anvil.API;
using Anvil.Services;
using XM.Core.EventManagement;
using XM.UI.Builder;
using XM.UI.TestUI;
using Action = System.Action;

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
        private readonly Dictionary<int, IViewModel> _playerViewModels = new();

        private readonly XMEventService _event;
        private readonly IServiceManager _serviceManager;

        public GuiService(XMEventService @event, IServiceManager serviceManager)
        {
            _event = @event;
            _serviceManager = serviceManager;

            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            _event.Subscribe<NWNXEvent.OnModulePreload>(OnModulePreload);
            _event.Subscribe<ModuleEvent.OnNuiEvent>(OnNuiEvent);
        }

        private void OnNuiEvent()
        {
            var elementId = NuiGetEventElement();
            var @event = NuiGetEventType();
            var windowToken = NuiGetEventWindow();

            switch (@event)
            {
                case "click":
                    RunUserEvent(elementId, NuiEventType.Click, windowToken);
                    break;
                case "watch":
                    RunUserEvent(elementId, NuiEventType.Watch, windowToken);
                    break;
                case "open":
                    RunOpenWindow();
                    break;
                case "close":
                    RunCloseWindow();
                    break;
                case "focus":
                    RunUserEvent(elementId, NuiEventType.Focus, windowToken);
                    break;
                case "blur":
                    RunUserEvent(elementId, NuiEventType.Blur, windowToken);
                    break;
                case "mousedown":
                    RunUserEvent(elementId, NuiEventType.MouseDown, windowToken);
                    break;
                case "mouseup":
                    RunUserEvent(elementId, NuiEventType.MouseUp, windowToken);
                    break;
            }

        }

        private void RunUserEvent(
            string elementId, 
            NuiEventType type,
            int windowToken)
        {
            if (!_registeredEvents.ContainsKey(elementId))
                return;

            if (!_registeredEvents[elementId].ContainsKey(type))
                return;

            var methodName = _registeredEvents[elementId][type];
            var viewModel = _playerViewModels[windowToken];
            var vmType = viewModel.GetType();

            var property = vmType.GetProperty(methodName);
            if (property != null)
            {
                var action = (Action)property.GetValue(viewModel);
                action?.Invoke();
            }
        }

        private void RunOpenWindow()
        {
            var windowToken = NuiGetEventWindow();
            var viewModel = _playerViewModels[windowToken];

            viewModel.OnOpen();
        }

        private void RunCloseWindow()
        {
            var windowToken = NuiGetEventWindow();
            var viewModel = _playerViewModels[windowToken];

            viewModel.OnClose();
            _playerViewModels.Remove(windowToken);
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
                var viewModel = (IViewModel)_serviceManager.AnvilServiceContainer.Create(view.ViewModel);
                
                var windowToken = NuiCreate(player, json, window.Id);
                viewModel.Bind(player, windowToken);
                viewModel.OnOpen();

                _playerViewModels[windowToken] = viewModel;
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