using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;

namespace XM.Shared.Core.Conversation
{
    public class ResponseAction : INotifyPropertyChanged
    {
        private ConversationActionType _type = ConversationActionType.EndConversation;
        private Dictionary<string, object> _parameters = new();

        [JsonConverter(typeof(ConversationActionTypeConverter))]
        public ConversationActionType Type
        {
            get => _type;
            set => SetProperty(ref _type, value);
        }

        public Dictionary<string, object> Parameters
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