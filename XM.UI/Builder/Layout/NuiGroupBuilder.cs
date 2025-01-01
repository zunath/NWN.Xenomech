using Anvil.API;
using System;
using XM.UI.Builder.Component;

namespace XM.UI.Builder.Layout
{
    public class NuiGroupBuilder : NuiBuilderBase<NuiGroupBuilder, NuiGroup>
    {
        public NuiGroupBuilder()
            : base(new NuiGroup())
        {
        }

        public NuiGroupBuilder SetBorder(bool border)
        {
            Element.Border = border;
            return this;
        }

        public NuiGroupBuilder SetScrollbars(NuiScrollbars scrollbars)
        {
            Element.Scrollbars = scrollbars;
            return this;
        }

        public NuiGroupBuilder SetLayout(Action<NuiColumnBuilder> column)
        {
            var columnBuilder = new NuiColumnBuilder();
            column(columnBuilder);

            Element.Layout = columnBuilder.Build();
            return this;
        }
    }

}
