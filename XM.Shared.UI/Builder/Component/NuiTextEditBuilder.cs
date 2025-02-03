using Anvil.API;
using System.Linq.Expressions;
using System;
using XM.Shared.API.NUI;
using XM.Shared.Core.Localization;

namespace XM.UI.Builder.Component
{
    public class NuiTextEditBuilder<TViewModel> : NuiBuilderBase<NuiTextEditBuilder<TViewModel>, TViewModel>
        where TViewModel : IViewModel
    {
        private LocaleString _placeholder;
        private string _placeholderBind;

        private string _valueBind;

        private ushort _maxLength = 32;
        private bool _multiLine;
        private bool _wordWrap;

        public NuiTextEditBuilder(NuiEventCollection eventCollection)
            : base(eventCollection)
        {
        }

        public NuiTextEditBuilder<TViewModel> Placeholder(LocaleString label)
        {
            _placeholder = label;
            return this;
        }

        public NuiTextEditBuilder<TViewModel> MaxLength(ushort maxLength)
        {
            _maxLength = maxLength;
            return this;
        }

        public NuiTextEditBuilder<TViewModel> MultiLine(bool multiLine)
        {
            _multiLine = multiLine;
            return this;
        }

        public NuiTextEditBuilder<TViewModel> WordWrap(bool wordWrap)
        {
            _wordWrap = wordWrap;
            return this;
        }

        public NuiTextEditBuilder<TViewModel> Placeholder(Expression<Func<TViewModel, string>> expression)
        {
            _placeholderBind = GetBindName(expression);
            return this;
        }

        public NuiTextEditBuilder<TViewModel> Value(Expression<Func<TViewModel, string>> expression)
        {
            AssignId();
            _valueBind = GetBindName(expression);
            return this;
        }

        public override Json BuildEntity()
        {
            var placeholder = string.IsNullOrWhiteSpace(_placeholderBind)
                ? JsonString(_placeholder.ToLocalizedString())
                : Nui.Bind(_placeholderBind);

            var value = Nui.Bind(_valueBind);

            return Nui.TextEdit(placeholder, value, _maxLength, _multiLine, _wordWrap);
        }
    }
}
