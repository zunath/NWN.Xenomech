using Anvil.API;
using System.Linq.Expressions;
using System;
using XM.Shared.API.NUI;
using XM.Shared.Core.Localization;

namespace XM.UI.Builder.Component
{
    public class NuiButtonBuilder<TViewModel> 
        : NuiBuilderBase<NuiButtonBuilder<TViewModel>, TViewModel>
        where TViewModel: IViewModel
    {
        private LocaleString _label;
        private string _labelBind;

        public NuiButtonBuilder(NuiEventCollection eventCollection)
            : base(eventCollection)
        {
        }

        public NuiButtonBuilder<TViewModel> Label(LocaleString label)
        {
            _label = label;
            return this;
        }

        public NuiButtonBuilder<TViewModel> Label(Expression<Func<TViewModel, LocaleString>> expression)
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
            var labelText = Locale.GetString(_label);
            var label = string.IsNullOrWhiteSpace(_labelBind)
                ? JsonString(labelText)
                : Nui.Bind(_labelBind);

            return Nui.Button(label);
        }
    }
}
