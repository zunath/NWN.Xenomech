using Anvil.API;
using System;
using XM.UI.Builder.Layout;

namespace XM.UI.Builder
{
    public class NuiBuilder
    {
        private NuiWindowBuilder _windowBuilder;

        public NuiBuilder CreateWindow(Action<NuiWindowBuilder> configure)
        {
            _windowBuilder = new NuiWindowBuilder(new NuiColumnBuilder().Build(), "New Window");
            configure(_windowBuilder);
            return this;
        }

        public NuiBuilder AddColumn(Action<NuiColumnBuilder> configure)
        {
            if (_windowBuilder == null)
            {
                throw new InvalidOperationException("You must create a window before adding a column.");
            }

            var columnBuilder = new NuiColumnBuilder();
            configure(columnBuilder);
            
            // todo add col

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
