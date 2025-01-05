using Anvil.API;
using System.Linq.Expressions;
using System;

namespace XM.UI.Builder.Component
{
    public class NuiButtonImageBuilder<TViewModel> : NuiBuilderBase<NuiButtonImageBuilder<TViewModel>, NuiButtonImage, TViewModel>
        where TViewModel: IViewModel
    {
        public NuiButtonImageBuilder(NuiEventCollection eventCollection)
            : base(new NuiButtonImage(string.Empty), eventCollection)
        {
        }

        public NuiButtonImageBuilder<TViewModel> ResRef(string resRef)
        {
            Element.ResRef = resRef;
            return this;
        }
        public NuiButtonImageBuilder<TViewModel> ResRef(Expression<Func<TViewModel, string>> expression)
        {
            var bindName = GetBindName(expression);
            var bind = new NuiBind<string>(bindName);
            Element.ResRef = bind;

            return this;
        }

    }

}
