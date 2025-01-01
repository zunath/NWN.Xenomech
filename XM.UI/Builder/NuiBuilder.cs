using Anvil.API;
using System;
using XM.UI.Builder.Layout;

namespace XM.UI.Builder
{
    public class NuiBuilder<TViewModel>
        where TViewModel: IViewModel
    {
        private NuiWindowBuilder<TViewModel> _windowBuilder;

        public NuiBuilder<TViewModel> CreateWindow(Action<NuiWindowBuilder<TViewModel>> configure)
        {
            _windowBuilder = new NuiWindowBuilder<TViewModel>(new NuiColumnBuilder<TViewModel>().Build(), "New Window");
            configure(_windowBuilder);
            return this;
        }

        public NuiBuilder<TViewModel> SetRoot(Action<NuiColumnBuilder<TViewModel>> configure)
        {
            if (_windowBuilder == null)
            {
                throw new InvalidOperationException("You must create a window before adding a column.");
            }

            var columnBuilder = new NuiColumnBuilder<TViewModel>();
            configure(columnBuilder);
            _windowBuilder.SetRoot(columnBuilder.Build());

            return this;
        }

        public NuiWindow Build()
        {
            if (_windowBuilder == null)
            {
                throw new InvalidOperationException("No window has been created.");
            }

            return _windowBuilder.Build();
        }
    }


}
