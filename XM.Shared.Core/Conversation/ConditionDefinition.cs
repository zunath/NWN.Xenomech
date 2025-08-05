using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace XM.Shared.Core.Conversation
{
    public class ConditionDefinition : INotifyPropertyChanged
    {
        private string _description = string.Empty;
        private Dictionary<string, ParameterDefinition> _parameters = new();

        public string Description
        {
            get => _description;
            set => SetProperty(ref _description, value);
        }

        public Dictionary<string, ParameterDefinition> Parameters
        {
            get => _parameters;
            set => SetProperty(ref _parameters, value);
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