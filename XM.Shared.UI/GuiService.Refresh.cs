using System.Collections.Generic;
using System;
using System.Linq;

namespace XM.UI
{
    public partial class GuiService
    {
        private readonly HashSet<Type> _windowTypesWithRefresh = new();

        private void CacheRefreshables()
        {
            foreach (var vm in _viewModels)
            {
                if (vm is IRefreshable)
                {
                    var vmType = vm.GetType();
                    _windowTypesWithRefresh.Add(vmType);
                }
            }
        }

        private void OnRefreshUI(uint player)
        {
            PublishRefreshEvent(player);
        }

        private void PublishRefreshEvent(uint player)
        {
            if (!GetIsPC(player))
                return;

            if (!_playerViewModels.ContainsKey(player))
                return;

            foreach (var type in _windowTypesWithRefresh)
            {
                var viewModel = _playerViewModels[player]
                    .SingleOrDefault(s => s.Value.GetType() == type)
                    .Value;

                if (viewModel == null)
                    continue;

                var methodInfo = typeof(IRefreshable)
                    .GetMethod(nameof(IRefreshable.Refresh));

                methodInfo?.Invoke(viewModel, []);
            }
        }
    }
}
