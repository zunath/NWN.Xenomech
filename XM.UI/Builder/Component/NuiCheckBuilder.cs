using Anvil.API;
using System.Linq.Expressions;
using System;

namespace XM.UI.Builder.Component
{
    public class NuiCheckBuilder<TViewModel> : NuiBuilderBase<NuiCheckBuilder<TViewModel>, NuiCheck, TViewModel>
        where TViewModel: IViewModel
    {
        public NuiCheckBuilder(NuiEventCollection eventCollection)
            : base(new NuiCheck(string.Empty, false), eventCollection)
        {
        }

        public NuiCheckBuilder<TViewModel> Label(string label)
        {
            Element.Label = label;
            return this;
        }

        public NuiCheckBuilder<TViewModel> Selected(bool selected)
        {
            Element.Selected = selected;
            return this;
        }
        public NuiCheckBuilder<TViewModel> Label(Expression<Func<TViewModel, string>> expression)
        {
            var bindName = GetBindName(expression);
            var bind = new NuiBind<string>(bindName);
            Element.Label = bind;

            return this;
        }

        public NuiCheckBuilder<TViewModel> Selected(Expression<Func<TViewModel, bool>> expression)
        {
            var bindName = GetBindName(expression);
            var bind = new NuiBind<bool>(bindName);
            Element.Selected = bind;

            return this;
        }

    }

}
