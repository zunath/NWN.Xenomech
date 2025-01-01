using Anvil.API;
using System.Linq.Expressions;
using System;

namespace XM.UI.Builder.Component
{
    public class NuiOptionsBuilder<TViewModel> : NuiBuilderBase<NuiOptionsBuilder<TViewModel>, NuiOptions, TViewModel>
        where TViewModel: IViewModel
    {
        public NuiOptionsBuilder()
            : base(new NuiOptions())
        {
        }

        public NuiOptionsBuilder<TViewModel> SetDirection(NuiDirection direction)
        {
            Element.Direction = direction;
            return this;
        }

        public NuiOptionsBuilder<TViewModel> AddOption(string option)
        {
            Element.Options.Add(option);
            return this;
        }

        public NuiOptionsBuilder<TViewModel> SetSelection(int selection)
        {
            Element.Selection = selection;
            return this;
        }

        public NuiOptionsBuilder<TViewModel> BindSelection(Expression<Func<TViewModel, int>> expression)
        {
            var bindName = GetBindName(expression);
            var bind = new NuiBind<int>(bindName);
            Element.Selection = bind;

            return this;
        }

    }


}
