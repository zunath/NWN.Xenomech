using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Anvil.API;

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

        public void Bind(
            uint player, 
            int windowToken,
            uint tetherObject = OBJECT_INVALID)
        {
            Player = player;
            WindowToken = windowToken;
            TetherObject = tetherObject;
        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected T Get<T>([CallerMemberName] string propertyName = null)
        {
            if (!string.IsNullOrWhiteSpace(propertyName) && 
                _backingData.ContainsKey(propertyName))
                return (T)_backingData[propertyName];

            throw new Exception($"Property {propertyName} does not exist in view model data store.");
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

        public abstract void OnOpen();

        public void OnCloseInternal()
        {

        }

        public abstract void OnClose();
    }
}
