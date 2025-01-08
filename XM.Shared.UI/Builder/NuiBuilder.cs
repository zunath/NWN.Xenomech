using System;
using Anvil.API;
using XM.UI.Builder.Layout;

namespace XM.UI.Builder
{
    public class NuiBuilder<TViewModel>: NuiBindable<TViewModel>
        where TViewModel: IViewModel
    {

        private NuiWindowBuilder<TViewModel> _windowBuilder;

        public NuiBuilder() 
            : base(new NuiEventCollection())
        {
        }

        public NuiBuilder<TViewModel> CreateWindow(Action<NuiWindowBuilder<TViewModel>> configure)
        {
            _windowBuilder = new NuiWindowBuilder<TViewModel>(RegisteredEvents);
            configure(_windowBuilder);
            return this;
        }

        public NuiBuiltWindow Build()
        {
            if (_windowBuilder == null)
            {
                throw new InvalidOperationException("No window has been created.");
            }

            var builtWindow = _windowBuilder.BuildWindow();
            builtWindow.EventCollection = RegisteredEvents;

            return builtWindow;
        }

    }
}
