using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using Anvil.API;
using Anvil.Services;
using Newtonsoft.Json;
using XM.Shared.Core;
using XM.Shared.Core.EventManagement;

namespace XM.UI
{
    public abstract partial class ViewModel: 
        IViewModel, 
        INotifyPropertyChanged
    {
        protected uint Player { get; private set; }
        protected uint TetherObject { get; private set; }
        private int WindowToken { get; set; }
        private object InitialData { get; set; }

        public Dictionary<string, Json> PartialViews { get; set; }

        private Guid _onNuiEventToken;
        private readonly Dictionary<string, object> _backingData = new();
        private readonly Dictionary<string, IGuiBindingList> _boundLists = new();

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

            ChangePartialView(IViewModel.MainViewElementId, IViewModel.UserPartialId);
        }

        protected void ChangePartialView(string elementId, string partialViewId)
        {
            var json = PartialViews[partialViewId];
            NuiSetGroupLayout(Player, WindowToken, elementId, json);
        }

        public void Unbind()
        {
            Event.Unsubscribe<ModuleEvent.OnNuiEvent>(_onNuiEventToken);

            foreach (var (propertyName, list) in _boundLists)
            {
                UnbindList(list, propertyName);
            }
        }

        private void BindGeometry(NuiRect geometry)
        {
            Geometry = geometry;
            WatchOnClient(model => model.Geometry);
        }

        private void OnWatchEvent()
        {
            var @event = NuiGetEventType();
            var propertyName = NuiGetEventElement();

            if (@event == "watch")
            {
                var bind = NuiGetBind(Player, WindowToken, propertyName);
                var jsonString = JsonDump(bind);
                var type = GetType().GetProperty(propertyName)!.PropertyType;

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

            if (typeof(IGuiBindingList).IsAssignableFrom(typeof(T)))
            {
                var list = (IGuiBindingList)value;
                list.PropertyName = propertyName;
                RegisterList(list, propertyName);
            }

            OnPropertyChanged(propertyName);
        }

        private void BindList(IGuiBindingList list, string propertyName)
        {
            list.ListChanged += OnListChanged;
            _boundLists[propertyName] = list;
        }

        private void UnbindList(IGuiBindingList list, string propertyName)
        {
            list.ListChanged -= OnListChanged;
            list.PropertyName = string.Empty;
            _boundLists.Remove(propertyName);
        }

        private void RegisterList(IGuiBindingList list, string propertyName)
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
        protected void WatchOnClient<TProperty>(Expression<Func<IViewModel, TProperty>> expression)
        {
            var memberExpression = (MemberExpression)expression.Body;
            var propertyName = memberExpression.Member.Name;

            NuiSetBindWatch(Player, WindowToken, propertyName, true);
        }


        private void OnListChanged(object sender, ListChangedEventArgs e)
        {
            Console.WriteLine($"test");
            var list = (IGuiBindingList)sender;

            switch (e.ListChangedType)
            {
                case ListChangedType.ItemAdded:
                case ListChangedType.ItemDeleted:
                case ListChangedType.ItemChanged:
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

        public void NotifyChange<T>(uint player)
        {

        }

        public abstract void OnOpen();

        public abstract void OnClose();
    }
}
