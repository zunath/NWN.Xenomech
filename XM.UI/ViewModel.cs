using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using Anvil.API;
using Anvil.Services;
using XM.Core.EventManagement;

namespace XM.UI
{
    public abstract class ViewModel: 
        IViewModel, 
        INotifyPropertyChanged
    {
        protected uint Player { get; private set; }
        protected uint TetherObject { get; private set; }
        private int WindowToken { get; set; }

        private readonly Dictionary<string, object> _backingData = new();

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
            uint tetherObject = OBJECT_INVALID)
        {
            Player = player;
            WindowToken = windowToken;
            TetherObject = tetherObject;

            WatchOnClient(model => model.Geometry);
            Event.Subscribe<ModuleEvent.OnNuiEvent>(OnWatchEvent);
        }

        private void OnWatchEvent()
        {
            var @event = NuiGetEventType();
            var propertyName = NuiGetEventElement();

            if (@event == "watch")
            {
                Console.WriteLine($"watch = {propertyName}");
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected T Get<T>([CallerMemberName] string propertyName = null)
        {
            if (string.IsNullOrWhiteSpace(propertyName))
                return default(T);

            if (_backingData.ContainsKey(propertyName))
                return (T)_backingData[propertyName];

            return default;
        }

        protected void Set<T>(T value, [CallerMemberName] string propertyName = null)
        {
            SetField(value, propertyName);
            var serialized = JsonUtility.ToJson(value);
            var json = JsonParse(serialized);
            NuiSetBind(Player, WindowToken, propertyName, json);
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

            var json = JsonParse(JsonUtility.ToJson(Geometry));
            NuiSetBind(Player, WindowToken, propertyName, json);
            NuiSetBindWatch(Player, WindowToken, propertyName, true);
        }

        public abstract void OnOpen();

        public abstract void OnClose();
    }
}
