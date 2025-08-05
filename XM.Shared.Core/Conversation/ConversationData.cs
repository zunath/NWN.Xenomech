using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace XM.Shared.Core.Conversation
{
    public class ConversationData : INotifyPropertyChanged
    {
        private Dictionary<string, ConversationPage> _pages = new();
        private string _defaultPage = string.Empty;
        private Dictionary<string, ActionDefinition> _actions = new();
        private Dictionary<string, ConditionDefinition> _conditions = new();

        public Dictionary<string, ConversationPage> Pages
        {
            get => _pages;
            set => SetProperty(ref _pages, value);
        }

        public string DefaultPage
        {
            get => _defaultPage;
            set => SetProperty(ref _defaultPage, value);
        }

        public Dictionary<string, ActionDefinition> Actions
        {
            get => _actions;
            set => SetProperty(ref _actions, value);
        }

        public Dictionary<string, ConditionDefinition> Conditions
        {
            get => _conditions;
            set => SetProperty(ref _conditions, value);
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