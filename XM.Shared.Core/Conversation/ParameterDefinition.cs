using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.Generic;

namespace XM.Shared.Core.Conversation
{
    public class ParameterDefinition : INotifyPropertyChanged
    {
        private string _type = string.Empty;
        private string _description = string.Empty;
        private bool _required;
        private object? _defaultValue;

        public string Type
        {
            get => _type;
            set => SetProperty(ref _type, value);
        }

        public string Description
        {
            get => _description;
            set => SetProperty(ref _description, value);
        }

        public bool Required
        {
            get => _required;
            set => SetProperty(ref _required, value);
        }

        public object? DefaultValue
        {
            get => _defaultValue;
            set => SetProperty(ref _defaultValue, value);
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetProperty<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }
    }
} 