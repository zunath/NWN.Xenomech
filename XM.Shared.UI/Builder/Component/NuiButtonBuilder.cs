using Anvil.API;
using System.Linq.Expressions;
using System;
using XM.Shared.API.NUI;

namespace XM.UI.Builder.Component
{
    public class NuiButtonBuilder<TViewModel> 
        : NuiBuilderBase<NuiButtonBuilder<TViewModel>, TViewModel>
        where TViewModel: IViewModel
    {
        private string _label;
        private string _labelBind;

        public NuiButtonBuilder(NuiEventCollection eventCollection)
            : base(eventCollection)
        {
        }

        public NuiButtonBuilder<TViewModel> Label(string label)
        {
            _label = label;
            return this;
        }

        public NuiButtonBuilder<TViewModel> Label(Expression<Func<TViewModel, string>> expression)
        {
            _labelBind = GetBindName(expression);
            return this;
        }

        public NuiButtonBuilder<TViewModel> Label(Expression<Func<TViewModel, GuiBindingList<string>>> expression)
        {
            _labelBind = GetBindName(expression);
            return this;
        }

        public override Json BuildEntity()
        {
            var label = string.IsNullOrWhiteSpace(_labelBind)
                ? JsonString(_label)
                : Nui.Bind(_labelBind);

            return Nui.Button(label);
        }
    }
}
