using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using Anvil.API;
using Anvil.Services;
using XM.Shared.Core;
using XM.Shared.Core.EventManagement;
using XM.Shared.Core.Json;

namespace XM.UI
{
    [ServiceBinding(typeof(IDisposable))]
    public abstract partial class ViewModel<TViewModel>:
        IViewModel,
        INotifyPropertyChanged,
        IDisposable
        where TViewModel : IViewModel
    {
        protected uint Player { get; private set; }
        protected uint TetherObject { get; private set; }
        private int WindowToken { get; set; }
        private object InitialData { get; set; }

        public Dictionary<string, Json> PartialViews { get; set; }

        private Guid _onNuiEventToken;
        private readonly Dictionary<string, object> _backingData = new();
        private readonly Dictionary<string, IXMBindingList> _boundLists = new();

        private readonly List<string> _watches = new();

        [Inject]
        public XMEventService Event { get; set; }

        public NuiRect Geometry
        {
            get => Get<NuiRect>();
            set => Set(value);
        }

        public void Bind(
            uint player, 
            int windowToken,
            NuiRect geometry,
            Dictionary<string, Json> partialViews,
            object initialData = default,
            uint tetherObject = OBJECT_INVALID)
        {
            Player = player;
            WindowToken = windowToken;
            TetherObject = tetherObject;
            PartialViews = partialViews;
            InitialData = initialData;

            BindGeometry(geometry);
            _onNuiEventToken = Event.Subscribe<ModuleEvent.OnNuiEvent>(OnWatchEvent);
        }

        protected void ChangePartialView(string elementId, string partialViewId)
        {
            var json = PartialViews[partialViewId];
            NuiSetGroupLayout(Player, WindowToken, elementId, json);

            ApplyRefreshBugFix();
        }

        public void Unbind()
        {
            Event.Unsubscribe<ModuleEvent.OnNuiEvent>(_onNuiEventToken);

            foreach (var (propertyName, list) in _boundLists)
            {
                UnbindList(list, propertyName);
            }

            ClearWatches();
        }

        public void OnOpenInternal()
        {
            ChangePartialView(IViewModel.MainViewElementId, IViewModel.UserPartialId);
            OnOpen();
        }

        private void BindGeometry(NuiRect geometry)
        {
            Geometry = geometry;
            WatchOnClient(model => model.Geometry);
        }

        private void OnWatchEvent(uint objectSelf)
        {
            var @event = NuiGetEventType();
            var propertyName = NuiGetEventElement();
            var windowToken = NuiGetEventWindow();
            if (windowToken != WindowToken)
                return;

            if (@event == "watch")
            {
                var bind = NuiGetBind(Player, WindowToken, propertyName);
                var jsonString = JsonDump(bind);
                var property = GetType().GetProperty(propertyName);
                if (property == null)
                    return;

                var type = property.PropertyType;
                var value = XMJsonUtility.DeserializeObject(jsonString, type);
                _backingData[propertyName] = value;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

            var value = _backingData[propertyName!];
            var json = XMJsonUtility.Serialize(value);
            NuiSetBind(Player, WindowToken, propertyName, JsonParse(json));
            UpdateRowCount(propertyName);
        }

        private void UpdateRowCount(string propertyName)
        {
            if (_boundLists.ContainsKey(propertyName))
            {
                var list = _boundLists[propertyName];
                NuiSetBind(Player, WindowToken, propertyName + "_" + nameof(NuiList.RowCount), JsonInt(list.Count));
            }
        }

        protected T Get<T>([CallerMemberName] string propertyName = null)
        {
            if (string.IsNullOrWhiteSpace(propertyName))
                return default;

            if (_backingData.ContainsKey(propertyName))
                return (T)_backingData[propertyName];

            return default;
        }

        protected void Set<T>(T value, [CallerMemberName] string propertyName = null)
        {
            SetField(value, propertyName);

            if (typeof(IXMBindingList).IsAssignableFrom(typeof(T)))
            {
                var list = (IXMBindingList)value;
                list.PropertyName = propertyName;
                RegisterList(list, propertyName);
                UpdateRowCount(propertyName);
            }
        }

        private void BindList(IXMBindingList list, string propertyName)
        {
            list.ListChanged += OnListChanged;
            _boundLists[propertyName] = list;
        }

        private void UnbindList(IXMBindingList list, string propertyName)
        {
            list.ListChanged -= OnListChanged;
            list.PropertyName = string.Empty;
            _boundLists.Remove(propertyName);
        }

        private void RegisterList(IXMBindingList list, string propertyName)
        {
            if (!_boundLists.ContainsKey(propertyName))
            {
                BindList(list, propertyName);
                list.PropertyName = propertyName;
            }

            if (!ReferenceEquals(_boundLists[propertyName], list))
            {
                UnbindList(_boundLists[propertyName], propertyName);
                BindList(list, propertyName);
            }
        }


        private void SetField<T>(T value, [CallerMemberName] string propertyName = null)
        {
            if(string.IsNullOrWhiteSpace(propertyName))
                throw new Exception($"Property {propertyName} does not exist in view model.");

            if (_backingData.ContainsKey(propertyName))
            {
                var currentValue = _backingData[propertyName];
                if (EqualityComparer<T>.Default.Equals((T)currentValue, value))
                {
                    return;
                }
            }

            _backingData[propertyName] = value;
            OnPropertyChanged(propertyName);
        }

        /// <summary>
        /// Watches a property on the client.
        /// </summary>
        /// <typeparam name="TProperty">The property of the view model.</typeparam>
        /// <param name="expression">Expression to target the property.</param>
        protected void WatchOnClient<TProperty>(Expression<Func<TViewModel, TProperty>> expression)
        {
            var memberExpression = (MemberExpression)expression.Body;
            var propertyName = memberExpression.Member.Name;
            
            if(!_watches.Contains(propertyName))
                _watches.Add(propertyName);

            NuiSetBindWatch(Player, WindowToken, propertyName, true);
        }


        private void OnListChanged(object sender, ListChangedEventArgs e)
        {
            var list = (IXMBindingList)sender;

            switch (e.ListChangedType)
            {
                case ListChangedType.ItemAdded:
                case ListChangedType.ItemDeleted:
                case ListChangedType.ItemChanged:
                case ListChangedType.Reset:
                    OnPropertyChanged(list.PropertyName);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        protected TData GetInitialData<TData>()
            where TData: class
        {
            return InitialData as TData;
        }

        private void ClearWatches()
        {
            foreach (var watch in _watches)
            {
                NuiSetBindWatch(Player, WindowToken, watch, false);
            }
        }

        public abstract void OnOpen();

        public abstract void OnClose();
        public void Dispose()
        {
            ClearWatches();
            Event.Unsubscribe<ModuleEvent.OnNuiEvent>(_onNuiEventToken);
        }

        // The following method works around a NUI issue where the new partial view won't display on screen until the window resizes.
        // We force a change to the geometry of the window to ensure it redraws appropriately.
        // If/when a fix is implemented by Beamdog, this can be removed.
        private void ApplyRefreshBugFix()
        {
            Geometry = new NuiRect(
                Geometry.X,
                Geometry.Y,
                Geometry.Width,
            Geometry.Height + 1);

            var json = XMJsonUtility.Serialize(Geometry);
            NuiSetBind(Player, WindowToken, nameof(Geometry), Json.Parse(json));

            DelayCommand(0.1f, () =>
            {
                Geometry = new NuiRect(
                    Geometry.X,
                    Geometry.Y,
                    Geometry.Width,
                    Geometry.Height - 1);

                json = XMJsonUtility.Serialize(Geometry);
                NuiSetBind(Player, WindowToken, nameof(Geometry), Json.Parse(json));
            });
        }
    }
}
