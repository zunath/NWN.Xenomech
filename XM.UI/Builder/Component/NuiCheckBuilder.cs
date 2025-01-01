using Anvil.API;
using System.Linq.Expressions;
using System;

namespace XM.UI.Builder.Component
{
    public class NuiCheckBuilder<TViewModel> : NuiBuilderBase<NuiCheckBuilder<TViewModel>, NuiCheck, TViewModel>
        where TViewModel: IViewModel
    {
        public NuiCheckBuilder()
            : base(new NuiCheck(string.Empty, false))
        {
        }

        public NuiCheckBuilder<TViewModel> SetLabel(string label)
        {
            Element.Label = label;
            return this;
        }

        public NuiCheckBuilder<TViewModel> SetSelected(bool selected)
        {
            Element.Selected = selected;
            return this;
        }
        public NuiCheckBuilder<TViewModel> BindLabel(Expression<Func<TViewModel, string>> expression)
        {
            var bindName = GetBindName(expression);
            var bind = new NuiBind<string>(bindName);
            Element.Label = bind;

            return this;
        }

        public NuiCheckBuilder<TViewModel> BindSelected(Expression<Func<TViewModel, bool>> expression)
        {
            var bindName = GetBindName(expression);
            var bind = new NuiBind<bool>(bindName);
            Element.Selected = bind;

            return this;
        }

    }

}
