using System;
using System.Collections.Generic;
using System.Reflection;
using Anvil.API;
using Anvil.Services;
using XM.Core;
using XM.Core.EventManagement;
using XM.Data;
using XM.UI.Builder;
using XM.UI.Entity;
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
        private readonly Dictionary<Type, NuiBuiltWindow> _builtWindowsByType = new();
        private readonly Dictionary<Type, Json> _serializedWindowsByType = new();
        private readonly NuiEventCollection _registeredEvents = new();
        private readonly Dictionary<int, IViewModel> _playerViewModels = new();

        private readonly XMEventService _event;
        private readonly InjectionService _injection;
        private readonly DBService _db;

        public GuiService(
            XMEventService @event, 
            InjectionService injection,
            DBService db)
        {
            _event = @event;
            _injection = injection;
            _db = db;

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
            else
            {
                var method = vmType.GetMethod(methodName);
                if (method != null)
                {
                    var action = (Action)method.Invoke(viewModel, null);
                    action?.Invoke();
                }
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
            var player = NuiGetEventPlayer();
            var windowToken = NuiGetEventWindow();
            var viewModel = _playerViewModels[windowToken];

            viewModel.Unbind();
            viewModel.OnClose();
            SaveWindowLocation(player, windowToken);
            _playerViewModels.Remove(windowToken);
        }

        private void SaveWindowLocation(uint player, int windowToken)
        {
            var playerId = PlayerId.Get(player);
            var dbPlayerUI = _db.Get<PlayerUI>(playerId) ?? new(playerId);
            var viewModel = _playerViewModels[windowToken];
            var windowId = NuiGetWindowId(player, windowToken);

            dbPlayerUI.WindowGeometries[windowId] = viewModel.Geometry;

            _db.Set(dbPlayerUI);
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
                var builtWindow = view.Build();
                var window = builtWindow.Window;
                var elementEvents = builtWindow.EventCollection;
                var json = JsonUtility.ToJson(window);

                _builtWindowsByType[type] = builtWindow;
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

        public void ShowWindow<TView>(uint player, uint tetherObject = OBJECT_INVALID)
            where TView : IView
        {
            var type = typeof(TView);
            var window = _windowsByType[type];

            if (NuiFindWindow(player, window.Id) == 0)
            {
                var view = _viewsByType[type];
                var json = _serializedWindowsByType[type];
                var viewModel = view.CreateViewModel();
                viewModel = _injection.Inject(viewModel);

                var windowId = viewModel.GetType().FullName!;
                var geometry = _builtWindowsByType[type].DefaultGeometry;
                var partialViews = _builtWindowsByType[type].PartialViews;
                var playerId = PlayerId.Get(player);
                var playerUI = _db.Get<PlayerUI>(playerId);

                if (playerUI != null && playerUI.WindowGeometries.ContainsKey(windowId))
                {
                    geometry = playerUI.WindowGeometries[windowId];
                }
                
                var windowToken = NuiCreate(player, json, window.Id);
                viewModel.Bind(
                    player, 
                    windowToken, 
                    geometry, 
                    partialViews,
                    tetherObject);

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