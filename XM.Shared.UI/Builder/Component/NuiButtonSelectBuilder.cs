using Anvil.API;
using System.Linq.Expressions;
using System;
using XM.Shared.API.NUI;
using XM.Shared.Core;
using XM.Shared.Core.Localization;

namespace XM.UI.Builder.Component
{
    public class NuiButtonSelectBuilder<TViewModel> 
        : NuiBuilderBase<NuiButtonSelectBuilder<TViewModel>, TViewModel>
        where TViewModel: IViewModel
    {
        private LocaleString _label;
        private string _labelBind;

        private bool _isSelected;
        private string _isSelectedBind;

        public NuiButtonSelectBuilder(NuiEventCollection eventCollection)
            : base(eventCollection)
        {
        }

        public NuiButtonSelectBuilder<TViewModel> Label(LocaleString label)
        {
            _label = label;
            return this;
        }

        public NuiButtonSelectBuilder<TViewModel> IsSelected(bool isSelected)
        {
            _isSelected = isSelected;
            return this;
        }
        public NuiButtonSelectBuilder<TViewModel> Label(Expression<Func<TViewModel, string>> expression)
        {
            _labelBind = GetBindName(expression);
            return this;
        }
        public NuiButtonSelectBuilder<TViewModel> Label(Expression<Func<TViewModel, XMBindingList<string>>> expression)
        {
            _labelBind = GetBindName(expression);
            return this;
        }

        public NuiButtonSelectBuilder<TViewModel> IsSelected(Expression<Func<TViewModel, bool>> expression)
        {
            _isSelectedBind = GetBindName(expression);
            return this;
        }
        public NuiButtonSelectBuilder<TViewModel> IsSelected(Expression<Func<TViewModel, XMBindingList<bool>>> expression)
        {
            _isSelectedBind = GetBindName(expression);
            return this;
        }


        public override Json BuildEntity()
        {
            var label = string.IsNullOrWhiteSpace(_labelBind)
                ? JsonString(_label.ToLocalizedString())
                : Nui.Bind(_labelBind);

            var isSelected = string.IsNullOrWhiteSpace(_isSelectedBind)
                ? JsonBool(_isSelected)
                : Nui.Bind(_isSelectedBind);

            return Nui.ButtonSelect(label, isSelected);
        }
    }
}
