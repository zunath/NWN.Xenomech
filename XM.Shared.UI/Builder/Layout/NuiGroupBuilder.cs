using Anvil.API;
using System;
using XM.UI.Builder.Component;

namespace XM.UI.Builder.Layout
{
    public class NuiGroupBuilder<TViewModel> : NuiBuilderBase<NuiGroupBuilder<TViewModel>, NuiGroup, TViewModel>
        where TViewModel: IViewModel
    {
        public NuiGroupBuilder(NuiEventCollection eventCollection)
            : base(new NuiGroup(), eventCollection)
        {
        }

        public NuiGroupBuilder<TViewModel> SetBorder(bool border)
        {
            Element.Border = border;
            return this;
        }

        public NuiGroupBuilder<TViewModel> SetScrollbars(NuiScrollbars scrollbars)
        {
            Element.Scrollbars = scrollbars;
            return this;
        }

        public NuiGroupBuilder<TViewModel> SetLayout(Action<NuiColumnBuilder<TViewModel>> column)
        {
            var columnBuilder = new NuiColumnBuilder<TViewModel>(RegisteredEvents);
            column(columnBuilder);

            Element.Layout = columnBuilder.Build();
            return this;
        }
    }
}
