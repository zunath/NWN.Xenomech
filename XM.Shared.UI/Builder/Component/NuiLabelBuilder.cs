using Anvil.API;
using System.Linq.Expressions;
using System;
using XM.Shared.API.NUI;
using XM.Shared.Core;
using XM.Shared.Core.Localization;

namespace XM.UI.Builder.Component
{
    public class NuiLabelBuilder<TViewModel> : NuiBuilderBase<NuiLabelBuilder<TViewModel>, TViewModel>
        where TViewModel : IViewModel
    {
        private NuiHAlign _horizontalAlign;
        private string _horizontalAlignBind;

        private NuiVAlign _verticalAlign;
        private string _verticalAlignBind;

        private LocaleString _label;
        private string _labelBind;

        public NuiLabelBuilder(NuiEventCollection eventCollection)
            : base(eventCollection)
        {
        }

        public NuiLabelBuilder<TViewModel> HorizontalAlign(NuiHAlign horizontalAlign)
        {
            _horizontalAlign = horizontalAlign;
            return this;
        }

        public NuiLabelBuilder<TViewModel> VerticalAlign(NuiVAlign verticalAlign)
        {
            _verticalAlign = verticalAlign;
            return this;
        }

        public NuiLabelBuilder<TViewModel> Label(LocaleString label)
        {
            _label = label;
            return this;
        }

        public NuiLabelBuilder<TViewModel> HorizontalAlign(Expression<Func<TViewModel, NuiHAlign>> expression)
        {
            _horizontalAlignBind = GetBindName(expression);
            return this;
        }

        public NuiLabelBuilder<TViewModel> VerticalAlign(Expression<Func<TViewModel, NuiVAlign>> expression)
        {
            _verticalAlignBind = GetBindName(expression);
            return this;
        }

        public NuiLabelBuilder<TViewModel> Label(Expression<Func<TViewModel, string>> expression)
        {
            _labelBind = GetBindName(expression);
            return this;
        }
        public NuiLabelBuilder<TViewModel> Label(Expression<Func<TViewModel, XMBindingList<string>>> expression)
        {
            _labelBind = GetBindName(expression);
            return this;
        }

        public override Json BuildEntity()
        {
            var horizontalAlign = string.IsNullOrWhiteSpace(_horizontalAlignBind)
                ? JsonInt((int)_horizontalAlign)
                : Nui.Bind(_horizontalAlignBind);

            var verticalAlign = string.IsNullOrWhiteSpace(_verticalAlignBind)
                ? JsonInt((int)_verticalAlign)
                : Nui.Bind(_verticalAlignBind);

            var labelText = Locale.GetString(_label);
            var label = string.IsNullOrWhiteSpace(_labelBind)
                ? JsonString(labelText)
                : Nui.Bind(_labelBind);

            return Nui.Label(label, horizontalAlign, verticalAlign);
        }
    }
}
