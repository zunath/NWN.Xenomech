using Anvil.API;
using System.Linq.Expressions;
using System;
using XM.Shared.API.NUI;
using XM.Shared.Core.Localization;

namespace XM.UI.Builder.Component
{
    public class NuiCheckBuilder<TViewModel> 
        : NuiBuilderBase<NuiCheckBuilder<TViewModel>, TViewModel>
        where TViewModel : IViewModel
    {
        private LocaleString _label;
        private string _labelBind;

        private bool _selected;
        private string _selectedBind;

        public NuiCheckBuilder(NuiEventCollection eventCollection)
            : base(eventCollection)
        {
        }

        public NuiCheckBuilder<TViewModel> Label(LocaleString label)
        {
            _label = label;
            return this;
        }

        public NuiCheckBuilder<TViewModel> Selected(bool selected)
        {
            _selected = selected;
            return this;
        }

        public NuiCheckBuilder<TViewModel> Label(Expression<Func<TViewModel, string>> expression)
        {
            _labelBind = GetBindName(expression);
            return this;
        }

        public NuiCheckBuilder<TViewModel> Selected(Expression<Func<TViewModel, bool>> expression)
        {
            _selectedBind = GetBindName(expression);
            return this;
        }

        public override Json BuildEntity()
        {
            var labelText = Locale.GetString(_label);
            var label = string.IsNullOrWhiteSpace(_labelBind)
                ? JsonString(labelText)
                : Nui.Bind(_labelBind);

            var selected = string.IsNullOrWhiteSpace(_selectedBind)
                ? JsonBool(_selected)
                : Nui.Bind(_selectedBind);

            return Nui.Check(label, selected);
        }
    }
}