using Anvil.API;
using System.Linq.Expressions;
using System;
using XM.Shared.API.NUI;

namespace XM.UI.Builder.Component
{
    public class NuiButtonImageBuilder<TViewModel> 
        : NuiBuilderBase<NuiButtonImageBuilder<TViewModel>, TViewModel>
        where TViewModel: IViewModel
    {
        private string _resRef;
        private string _resRefBind;

        public NuiButtonImageBuilder(NuiEventCollection eventCollection)
            : base(eventCollection)
        {
        }

        public NuiButtonImageBuilder<TViewModel> ResRef(string resRef)
        {
            _resRef = resRef;
            return this;
        }
        public NuiButtonImageBuilder<TViewModel> ResRef(Expression<Func<TViewModel, string>> expression)
        {
            _resRefBind = GetBindName(expression);
            return this;
        }

        public override Json BuildEntity()
        {
            var resRef = string.IsNullOrWhiteSpace(_resRefBind)
                ? JsonString(_resRef)
                : Nui.Bind(_resRefBind);

            return Nui.ButtonImage(resRef);
        }
    }

}
