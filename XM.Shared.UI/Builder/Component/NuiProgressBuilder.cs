using Anvil.API;
using System.Linq.Expressions;
using System;

namespace XM.UI.Builder.Component
{
    public class NuiProgressBuilder<TViewModel> : NuiBuilderBase<NuiProgressBuilder<TViewModel>, NuiProgress, TViewModel>
        where TViewModel: IViewModel
    {
        public NuiProgressBuilder(NuiEventCollection eventCollection)
            : base(new NuiProgress(0f), eventCollection)
        {
        }

        public NuiProgressBuilder<TViewModel> Value(float value)
        {
            Element.Value = value;
            return this;
        }
        public NuiProgressBuilder<TViewModel> Value(Expression<Func<TViewModel, float>> expression)
        {
            var bindName = GetBindName(expression);
            var bind = new NuiBind<float>(bindName);
            Element.Value = bind;

            return this;
        }

    }
}
