using System;
using System.Collections.Generic;
using System.Linq;
using Anvil.API;
using Anvil.Services;
using NLog;
using XM.Shared.Core;
using XM.Shared.Core.Data;
using XM.Shared.Core.EventManagement;
using XM.UI.Builder;
using XM.Shared.Core.Entity;
using XM.UI.Event;
using Action = System.Action;

namespace XM.UI
{
    [ServiceBinding(typeof(GuiService))]
    [ServiceBinding(typeof(IInitializable))]
    [ServiceBinding(typeof(IDisposable))]
    public partial class GuiService: 
        IInitializable,
        IDisposable
    {
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

        private readonly IList<IView> _views;
        private readonly IList<IViewModel> _viewModels;

        private readonly Dictionary<Type, IView> _viewsByType = new();
        private readonly Dictionary<Type, NuiBuiltWindow> _builtWindowsByType = new();
        private readonly NuiEventCollection _registeredEvents = new();
        private readonly Dictionary<int, uint> _tokenToPlayer = new();
        private readonly Dictionary<int, Type> _tokenToType = new();
        private readonly Dictionary<uint, Dictionary<Type, int>> _playerToTokens = new();
        private readonly Dictionary<uint, Dictionary<int, IViewModel>> _playerViewModels = new();

        private readonly XMEventService _event;
        private readonly InjectionService _injection;
        private readonly DBService _db;

        public GuiService(
            XMEventService @event, 
            InjectionService injection,
            DBService db,
            IList<IView> views,
            IList<IViewModel> viewModels)
        {
            _event = @event;
            _injection = injection;
            _db = db;
            _views = views;
            _viewModels = viewModels;

            RegisterEvents();
            SubscribeEvents();
        }

        private void RegisterEvents()
        {
            _event.RegisterEvent<UIEvent.UIRefreshEvent>(UIEventScript.RefreshUIScript);
            _event.RegisterEvent<UIEvent.OpenWindow>(UIEventScript.OpenWindowScript);
            _event.RegisterEvent<UIEvent.CloseWindow>(UIEventScript.CloseWindowScript);
            _event.RegisterEvent<UIEvent.ToggleWindow>(UIEventScript.ToggleWindowScript);
        }

        private void SubscribeEvents()
        {
            _event.Subscribe<ModuleEvent.OnNuiEvent>(OnNuiEvent);
            _event.Subscribe<UIEvent.UIRefreshEvent>(OnRefreshUI);
            _event.Subscribe<UIEvent.OpenWindow>(OnOpenWindow);
            _event.Subscribe<UIEvent.CloseWindow>(OnCloseWindow);
            _event.Subscribe<UIEvent.ToggleWindow>(OnToggleWindow);
        }

        private void OnOpenWindow(uint player)
        {
            var data = _event.GetEventData<UIEvent.OpenWindow>();
            ShowWindow(player, data.ViewType);
        }
        private void OnCloseWindow(uint player)
        {
            var data = _event.GetEventData<UIEvent.CloseWindow>();
            CloseWindow(player, data.ViewType);
        }
        private void OnToggleWindow(uint player)
        {
            var data = _event.GetEventData<UIEvent.ToggleWindow>();
            ToggleWindow(player, data.ViewType);
        }

        public void Init()
        {
            CacheViews();
            CacheRefreshables();
        }
        private void OnNuiEvent(uint objectSelf)
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

            var eventDetail = _registeredEvents[elementId][type];
            var player = _tokenToPlayer[windowToken];
            var viewModel = _playerViewModels[player][windowToken];
            var method = viewModel.GetType().GetMethod(eventDetail.Method.Name);
            var args = eventDetail.Arguments.Select(s => s.Value);
            var action = method?.Invoke(viewModel, args.ToArray());
            ((Action)action)?.Invoke();

        }

        private void RunOpenWindow()
        {
            var windowToken = NuiGetEventWindow();
            var player = _tokenToPlayer[windowToken];
            var viewModel = _playerViewModels[player][windowToken];

            viewModel.OnOpenInternal();
        }

        private void RunCloseWindow()
        {
            var player = NuiGetEventPlayer();
            var windowToken = NuiGetEventWindow();
            DoCloseWindow(player, windowToken);
        }

        private void DoCloseWindow(uint player, int windowToken)
        {
            var viewModel = _playerViewModels[player][windowToken];
            var type = _tokenToType[windowToken];

            viewModel.OnRequestCloseWindow -= UserRequestedWindowClose;
            viewModel.Unbind();
            viewModel.OnClose();
            SaveWindowLocation(player, windowToken);
            _playerViewModels[player].Remove(windowToken);
            _tokenToPlayer.Remove(windowToken);
            _playerToTokens[player].Remove(type);
            _tokenToType.Remove(windowToken);
        }

        private void SaveWindowLocation(uint player, int windowToken)
        {
            var playerId = PlayerId.Get(player);
            var dbPlayerUI = _db.Get<PlayerUI>(playerId) ?? new(playerId);
            var viewModel = _playerViewModels[player][windowToken];
            var windowId = NuiGetWindowId(player, windowToken);

            dbPlayerUI.WindowGeometries[windowId] = viewModel.Geometry;

            _db.Set(dbPlayerUI);
        }

        private void CacheViews()
        {
            foreach (var view in _views)
            {
                var type = view.GetType();
                var builtWindow = view.Build();

                var elementEvents = builtWindow.EventCollection;

                _builtWindowsByType[type] = builtWindow;
                _viewsByType[type] = view;

                foreach (var (elementId, eventCollection) in elementEvents)
                {
                    if (!_registeredEvents.ContainsKey(elementId))
                        _registeredEvents[elementId] = new Dictionary<NuiEventType, NuiEventDetail>();

                    foreach (var (eventType, methodName) in eventCollection)
                    {
                        _registeredEvents[elementId][eventType] = methodName;
                    }

                }
            }
        }

        public void ShowWindow<TView>(
            uint player, 
            object initialData = default,
            uint tetherObject = OBJECT_INVALID)
            where TView : IView
        {
            var viewType = typeof(TView);
            ShowWindow(player, viewType, initialData, tetherObject);
        }

        private void ShowWindow(
            uint player, 
            Type viewType,
            object initialData = default,
            uint tetherObject = OBJECT_INVALID)
        {
            var window = _builtWindowsByType[viewType];

            if (NuiFindWindow(player, window.WindowId) == 0)
            {
                var view = _viewsByType[viewType];
                var json = _builtWindowsByType[viewType].Window;
                var viewModel = view.CreateViewModel();
                viewModel = _injection.Inject(viewModel);

                var windowId = viewModel.GetType().FullName!;
                var geometry = _builtWindowsByType[viewType].DefaultGeometry;
                var partialViews = _builtWindowsByType[viewType].PartialViews;
                var playerId = PlayerId.Get(player);
                var playerUI = _db.Get<PlayerUI>(playerId);

                if (playerUI != null && playerUI.WindowGeometries.ContainsKey(windowId))
                {
                    geometry = playerUI.WindowGeometries[windowId];
                }

                var windowToken = NuiCreate(player, json, window.WindowId);

                if (!_playerViewModels.ContainsKey(player))
                    _playerViewModels[player] = new Dictionary<int, IViewModel>();

                _playerViewModels[player][windowToken] = viewModel;
                _tokenToPlayer[windowToken] = player;
                _tokenToType[windowToken] = viewType;

                if (!_playerToTokens.ContainsKey(player))
                    _playerToTokens[player] = new Dictionary<Type, int>();

                _playerToTokens[player][viewType] = windowToken;

                viewModel.Bind(
                viewType,
                    player,
                    windowToken,
                    geometry,
                    partialViews,
                    initialData,
                    tetherObject);

                viewModel.OnRequestCloseWindow += UserRequestedWindowClose;
            }
        }

        private void UserRequestedWindowClose(object sender, RequestCloseWindowEventArgs args)
        {
            CloseWindow(args.Player, args.ViewType);
        }

        public void CloseWindow<TView>(uint player)
            where TView: IView
        {
            var type = typeof(TView);
            CloseWindow(player, type);
        }

        internal void CloseWindow(uint player, Type type)
        {
            if (!_playerToTokens.ContainsKey(player))
                return;

            if (!_playerToTokens[player].ContainsKey(type))
                return;

            var windowToken = _playerToTokens[player][type];

            DoCloseWindow(player, windowToken);
            NuiDestroy(player, windowToken);
        }

        public void ToggleWindow<TView>(uint player)
            where TView: IView
        {
            var viewType = typeof(TView);
            ToggleWindow(player, viewType);
        }

        private void ToggleWindow(uint player, Type viewType)
        {
            if (_playerToTokens.ContainsKey(player) &&
                _playerToTokens[player].ContainsKey(viewType))
            {
                CloseWindow(player, viewType);
            }
            else
            {
                ShowWindow(player, viewType);
            }
        }

        private void ClearOpenPlayerWindows()
        {
            foreach (var (player, tokens) in _playerToTokens)
            {
                foreach (var (_, windowToken) in tokens)
                {
                    NuiDestroy(player, windowToken);
                }
            }
        }

        public void Dispose()
        {
            Console.WriteLine($"Disposing {nameof(GuiService)}");
            _viewModels.Clear();
            _views.Clear();

            _windowTypesWithRefresh.Clear();

            ClearOpenPlayerWindows();

            _playerViewModels.Clear();
            _playerToTokens.Clear();
            _tokenToType.Clear();
            _tokenToPlayer.Clear();
            _registeredEvents.Clear();
            _builtWindowsByType.Clear();
            _viewsByType.Clear();
        }
    }
}