using System.Collections.Generic;
using System;
using System.Linq;
using XM.Shared.Core.EventManagement;

namespace XM.UI
{
    public partial class GuiService
    {
        private readonly Dictionary<Type, List<IViewModel>> _windowTypesByRefreshEvent = new();

        private void CacheRefreshables()
        {
            foreach (var vm in ViewModels)
            {
                var vmType = vm.GetType();

                var refreshables = vmType
                    .GetInterfaces()
                    .Where(x => x.IsGenericType &&
                                x.GetGenericTypeDefinition() == typeof(IRefreshable<>));

                foreach (var refreshable in refreshables)
                {
                    var eventType = refreshable.GenericTypeArguments[0];
                    if (!_windowTypesByRefreshEvent.ContainsKey(eventType))
                    {
                        _windowTypesByRefreshEvent[eventType] = new List<IViewModel>();
                    }

                    _windowTypesByRefreshEvent[eventType].Add(vm);
                }
            }
        }

        public void PublishRefreshEvent<T>(uint player, T payload)
            where T: IXMEvent
        {
            if (!GetIsPC(player))
                return;

            if (!_windowTypesByRefreshEvent.ContainsKey(typeof(T)))
                return;

            if (!_playerViewModels.ContainsKey(player))
                return;

            foreach (var refreshVM in _windowTypesByRefreshEvent[typeof(T)])
            {
                var viewModel = _playerViewModels[player]
                    .SingleOrDefault(s => s.Value.GetType() == refreshVM.GetType())
                    .Value;

                if (viewModel == null)
                    continue;

                var methodInfo = typeof(IRefreshable<>)
                    .MakeGenericType(typeof(T))
                    .GetMethod(nameof(IRefreshable<IXMEvent>.Refresh));

                methodInfo?.Invoke(viewModel, [payload]);
            }
        }
    }
}
