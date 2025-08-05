using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace XM.Shared.Core.Conversation
{
    public class ConversationResponse : INotifyPropertyChanged
    {
        private string _text = string.Empty;
        private ResponseAction _action = new();

        public string Text
        {
            get => _text;
            set => SetProperty(ref _text, value);
        }

        public ResponseAction Action
        {
            get => _action;
            set => SetProperty(ref _action, value);
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