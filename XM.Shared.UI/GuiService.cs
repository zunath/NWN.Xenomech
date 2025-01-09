using System;
using System.Collections.Generic;
using System.Numerics;
using Anvil.API;
using Anvil.Services;
using NLog;
using XM.Shared.Core;
using XM.Shared.Core.Data;
using XM.Shared.Core.EventManagement;
using XM.UI.Builder;
using XM.UI.Entity;
using Action = System.Action;

namespace XM.UI
{
    [ServiceBinding(typeof(GuiService))]
    [ServiceBinding(typeof(IUpdateable))]
    [ServiceBinding(typeof(IInitializable))]
    public partial class GuiService: 
        IUpdateable, 
        IInitializable
    {
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

        [Inject]
        public IList<IView> Views { get; set; }

        [Inject]
        public IList<IViewModel> ViewModels { get; set; }

        private readonly Dictionary<Type, IView> _viewsByType = new();
        private readonly Dictionary<Type, NuiBuiltWindow> _builtWindowsByType = new();
        private readonly NuiEventCollection _registeredEvents = new();
        private readonly Dictionary<int, uint> _tokenToPlayer = new();
        private readonly Dictionary<uint, Dictionary<Type, int>> _playerToTokens = new();
        private readonly Dictionary<uint, Dictionary<int, IViewModel>> _playerViewModels = new();

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
            var player = _tokenToPlayer[windowToken];
            var viewModel = _playerViewModels[player][windowToken];
            var vmType = viewModel.GetType();
            
            var property = vmType.GetProperty(methodName);
            if (property != null)
            {
                var action = (Action)property.GetValue(viewModel);
                EnqueueUIAction(viewModel, action);
            }
            else
            {
                var method = vmType.GetMethod(methodName);
                if (method != null)
                {
                    var action = (Action)method.Invoke(viewModel, null);
                    EnqueueUIAction(viewModel, action);
                }
            }
        }

        private void RunOpenWindow()
        {
            var windowToken = NuiGetEventWindow();
            var player = _tokenToPlayer[windowToken];
            var viewModel = _playerViewModels[player][windowToken];

            viewModel.OnOpen();
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

            viewModel.Unbind();
            viewModel.OnClose();
            SaveWindowLocation(player, windowToken);
            _playerViewModels[player].Remove(windowToken);
            _tokenToPlayer.Remove(windowToken);

            if (_lastEventTimestamps.ContainsKey(viewModel))
                _lastEventTimestamps.Remove(viewModel);
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
            foreach (var view in Views)
            {
                var type = view.GetType();
                var builtWindow = view.Build();
                
                var elementEvents = builtWindow.EventCollection;

                _builtWindowsByType[type] = builtWindow;
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

        public void ShowWindow<TView>(
            uint player, 
            object initialData = default,
            uint tetherObject = OBJECT_INVALID)
            where TView : IView
        {
            var type = typeof(TView);
            var window = _builtWindowsByType[type];

            if (NuiFindWindow(player, window.WindowId) == 0)
            {
                var view = _viewsByType[type];
                var json = _builtWindowsByType[type].Window;
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
                
                var windowToken = NuiCreate(player, json, window.WindowId);

                if (!_playerViewModels.ContainsKey(player))
                    _playerViewModels[player] = new Dictionary<int, IViewModel>();

                _playerViewModels[player][windowToken] = viewModel;
                _tokenToPlayer[windowToken] = player;

                if (!_playerToTokens.ContainsKey(player))
                    _playerToTokens[player] = new Dictionary<Type, int>();

                _playerToTokens[player][type] = windowToken;

                viewModel.Bind(
                    player, 
                    windowToken, 
                    geometry,
                    partialViews,
                    tetherObject);
            }
        }

        public void CloseWindow<TView>(uint player)
            where TView: IView
        {
            var type = typeof(TView);

            if (!_playerToTokens.ContainsKey(player))
                return;

            if (!_playerToTokens[player].ContainsKey(type))
                return;

            var windowToken = _playerToTokens[player][type];
            
            DoCloseWindow(player, windowToken);
            NuiDestroy(player, windowToken);
        }

        private void ClearOpenPlayerWindows()
        {
            for (var player = GetFirstPC(); GetIsObjectValid(player); player = GetNextPC())
            {
                var nth = 0;
                var windowToken = NuiGetNthWindow(player, nth);
                while (windowToken != 0)
                {
                    NuiDestroy(player, windowToken);

                    nth++;
                    windowToken = NuiGetNthWindow(player, nth);
                }
            }
        }

        public void Init()
        {
            CacheViews();
            CacheRefreshables();
            ClearOpenPlayerWindows();
        }
    }
}