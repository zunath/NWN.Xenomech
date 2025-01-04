using Anvil.API;
using System.Linq.Expressions;
using System;

namespace XM.UI.Builder.Component
{
    public class NuiButtonSelectBuilder<TViewModel> : NuiBuilderBase<NuiButtonSelectBuilder<TViewModel>, NuiButtonSelect, TViewModel>
        where TViewModel: IViewModel
    {
        public NuiButtonSelectBuilder(NuiEventCollection eventCollection)
            : base(new NuiButtonSelect(string.Empty, false), eventCollection)
        {
        }

        public NuiButtonSelectBuilder<TViewModel> Label(string label)
        {
            Element.Label = label;
            return this;
        }

        public NuiButtonSelectBuilder<TViewModel> IsSelected(bool selected)
        {
            Element.Selected = selected;
            return this;
        }
        public NuiButtonSelectBuilder<TViewModel> Label(Expression<Func<TViewModel, string>> expression)
        {
            var bindName = GetBindName(expression);
            var bind = new NuiBind<string>(bindName);
            Element.Label = bind;

            return this;
        }

        public NuiButtonSelectBuilder<TViewModel> IsSelected(Expression<Func<TViewModel, bool>> expression)
        {
            var bindName = GetBindName(expression);
            var bind = new NuiBind<bool>(bindName);
            Element.Selected = bind;

            return this;
        }


    }
}
