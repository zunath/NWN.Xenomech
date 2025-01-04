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
            var root = new NuiColumnBuilder<TViewModel>(RegisteredEvents).Build();
            _windowBuilder = new NuiWindowBuilder<TViewModel>(root, "New Window", RegisteredEvents);
            configure(_windowBuilder);
            return this;
        }

        public NuiBuilder<TViewModel> SetRoot(Action<NuiColumnBuilder<TViewModel>> configure)
        {
            if (_windowBuilder == null)
            {
                throw new InvalidOperationException("You must create a window before adding a column.");
            }

            var columnBuilder = new NuiColumnBuilder<TViewModel>(RegisteredEvents);
            configure(columnBuilder);
            _windowBuilder.SetRoot(columnBuilder.Build());

            return this;
        }

        public NuiBuiltWindow Build()
        {
            if (_windowBuilder == null)
            {
                throw new InvalidOperationException("No window has been created.");
            }

            var builtWindow = _windowBuilder.Build();
            builtWindow.EventCollection = RegisteredEvents;

            return builtWindow;
        }

    }
}
