using Anvil.API;
using System;
using XM.Shared.API.NUI;
using XM.UI.Builder.Component;
using NuiScrollbars = XM.Shared.API.NUI.NuiScrollbars;

namespace XM.UI.Builder.Layout
{
    public class NuiGroupBuilder<TViewModel> 
        : NuiBuilderBase<NuiGroupBuilder<TViewModel>, TViewModel>
        where TViewModel: IViewModel
    {
        private bool _border;
        private NuiScrollbars _scrollbars;
        private NuiColumnBuilder<TViewModel> _layout;

        public NuiGroupBuilder(NuiEventCollection eventCollection) : base(eventCollection)
        {
        }

        public NuiGroupBuilder<TViewModel> Border(bool border)
        {
            _border = border;
            return this;
        }

        public NuiGroupBuilder<TViewModel> Scrollbars(NuiScrollbars scrollbars)
        {
            _scrollbars = scrollbars;
            return this;
        }

        internal NuiGroupBuilder<TViewModel> SetLayout(NuiColumnBuilder<TViewModel> layout)
        {
            _layout = layout;
            return this;
        }

        public NuiGroupBuilder<TViewModel> SetLayout(Action<NuiColumnBuilder<TViewModel>> column)
        {
            _layout = new NuiColumnBuilder<TViewModel>(RegisteredEvents);
            column(_layout);
            return this;
        }

        public override Json BuildEntity()
        {
            var layout = _layout.Build();

            return Nui.Group(layout, _border, _scrollbars);
        }

    }
}
