using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace XM.Shared.Core.Conversation
{
    public class ConversationModel : INotifyPropertyChanged
    {
        private ConversationMetadata _metadata = new();
        private ConversationData _conversation = new();

        public ConversationMetadata Metadata
        {
            get => _metadata;
            set => SetProperty(ref _metadata, value);
        }

        public ConversationData Conversation
        {
            get => _conversation;
            set => SetProperty(ref _conversation, value);
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