using Anvil.API;
using System.Linq.Expressions;
using System;

namespace XM.UI.Builder.Component
{
    public class NuiButtonBuilder<TViewModel> : NuiBuilderBase<NuiButtonBuilder<TViewModel>, NuiButton, TViewModel>
        where TViewModel: IViewModel
    {
        public NuiButtonBuilder(NuiEventCollection eventCollection)
            : base(new NuiButton(string.Empty), eventCollection)
        {
        }

        public NuiButtonBuilder<TViewModel> Label(string label)
        {
            Element.Label = label;
            return this;
        }

        public NuiButtonBuilder<TViewModel> Label(Expression<Func<TViewModel, string>> expression)
        {
            var bindName = GetBindName(expression);
            var bind = new NuiBind<string>(bindName);
            Element.Label = bind;

            return this;
        }

        public NuiButtonBuilder<TViewModel> Label(Expression<Func<TViewModel, GuiBindingList<string>>> expression)
        {
            var bindName = GetBindName(expression);
            var bind = new NuiBind<string>(bindName);
            Element.Label = bind;

            return this;
        }
    }
}
