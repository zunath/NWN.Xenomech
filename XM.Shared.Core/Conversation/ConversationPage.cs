using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace XM.Shared.Core.Conversation
{
    public class ConversationPage : INotifyPropertyChanged
    {
        private string _header = string.Empty;
        private List<ConversationResponse> _responses = new();

        public string Header
        {
            get => _header;
            set => SetProperty(ref _header, value);
        }

        public List<ConversationResponse> Responses
        {
            get => _responses;
            set => SetProperty(ref _responses, value);
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