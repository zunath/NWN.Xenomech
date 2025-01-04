using Anvil.API;
using System.Linq.Expressions;
using System;

namespace XM.UI.Builder.Component
{
    public class NuiOptionsBuilder<TViewModel> : NuiBuilderBase<NuiOptionsBuilder<TViewModel>, NuiOptions, TViewModel>
        where TViewModel: IViewModel
    {
        public NuiOptionsBuilder(NuiEventCollection eventCollection)
            : base(new NuiOptions(), eventCollection)
        {
        }

        public NuiOptionsBuilder<TViewModel> Direction(NuiDirection direction)
        {
            Element.Direction = direction;
            return this;
        }

        public NuiOptionsBuilder<TViewModel> Option(string option)
        {
            Element.Options.Add(option);
            return this;
        }

        public NuiOptionsBuilder<TViewModel> Selection(int selection)
        {
            Element.Selection = selection;
            return this;
        }

        public NuiOptionsBuilder<TViewModel> Selection(Expression<Func<TViewModel, int>> expression)
        {
            var bindName = GetBindName(expression);
            var bind = new NuiBind<int>(bindName);
            Element.Selection = bind;

            return this;
        }
    }
}