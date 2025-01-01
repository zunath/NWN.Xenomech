using Anvil.API;
using System.Linq.Expressions;
using System;

namespace XM.UI.Builder.Component
{
    public class NuiLabelBuilder<TViewModel> : NuiBuilderBase<NuiLabelBuilder<TViewModel>, NuiLabel, TViewModel>
        where TViewModel: IViewModel
    {
        public NuiLabelBuilder()
            : base(new NuiLabel(string.Empty))
        {
        }

        public NuiLabelBuilder<TViewModel> SetHorizontalAlign(NuiHAlign horizontalAlign)
        {
            Element.HorizontalAlign = horizontalAlign;
            return this;
        }

        public NuiLabelBuilder<TViewModel> SetVerticalAlign(NuiVAlign verticalAlign)
        {
            Element.VerticalAlign = verticalAlign;
            return this;
        }

        public NuiLabelBuilder<TViewModel> SetLabel(string label)
        {
            Element.Label = label;
            return this;
        }

        public NuiLabelBuilder<TViewModel> BindHorizontalAlign(Expression<Func<TViewModel, NuiHAlign>> expression)
        {
            var bindName = GetBindName(expression);
            var bind = new NuiBind<NuiHAlign>(bindName);
            Element.HorizontalAlign = bind;

            return this;
        }

        public NuiLabelBuilder<TViewModel> BindVerticalAlign(Expression<Func<TViewModel, NuiVAlign>> expression)
        {
            var bindName = GetBindName(expression);
            var bind = new NuiBind<NuiVAlign>(bindName);
            Element.VerticalAlign = bind;

            return this;
        }

        public NuiLabelBuilder<TViewModel> BindLabel(Expression<Func<TViewModel, object>> expression)
        {
            var bindName = GetBindName(expression);
            var bind = new NuiBind<string>(bindName);
            Element.Label = bind;

            return this;
        }

    }

}
