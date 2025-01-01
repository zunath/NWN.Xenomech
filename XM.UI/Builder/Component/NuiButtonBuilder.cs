using Anvil.API;
using System.Linq.Expressions;
using System;

namespace XM.UI.Builder.Component
{
    public class NuiButtonBuilder<TViewModel> : NuiBuilderBase<NuiButtonBuilder<TViewModel>, NuiButton, TViewModel>
        where TViewModel: IViewModel
    {
        public NuiButtonBuilder()
            : base(new NuiButton(string.Empty))
        {
        }

        public NuiButtonBuilder<TViewModel> SetLabel(string label)
        {
            Element.Label = label;
            return this;
        }

        public NuiButtonBuilder<TViewModel> BindLabel(Expression<Func<TViewModel, string>> expression)
        {
            var bindName = GetBindName(expression);
            var bind = new NuiBind<string>(bindName);
            Element.Label = bind;

            return this;
        }
    }
}
