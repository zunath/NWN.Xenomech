using Anvil.API;
using System.Linq.Expressions;
using System;
using XM.Shared.API.NUI;

namespace XM.UI.Builder.Component
{
    public class NuiTextEditBuilder<TViewModel> : NuiBuilderBase<NuiTextEditBuilder<TViewModel>, TViewModel>
        where TViewModel : IViewModel
    {
        private string _label;
        private string _labelBind;

        private string _value;
        private string _valueBind;

        private ushort _maxLength;
        private bool _multiLine;
        private bool _wordWrap;

        public NuiTextEditBuilder(NuiEventCollection eventCollection)
            : base(eventCollection)
        {
        }

        public NuiTextEditBuilder<TViewModel> Label(string label)
        {
            _label = label;
            return this;
        }

        public NuiTextEditBuilder<TViewModel> Value(string value)
        {
            _value = value;
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

        public NuiTextEditBuilder<TViewModel> Label(Expression<Func<TViewModel, string>> expression)
        {
            _labelBind = GetBindName(expression);
            return this;
        }

        public NuiTextEditBuilder<TViewModel> Value(Expression<Func<TViewModel, string>> expression)
        {
            _valueBind = GetBindName(expression);
            return this;
        }

        public override Json BuildEntity()
        {
            var label = string.IsNullOrWhiteSpace(_labelBind)
                ? JsonString(_label)
                : Nui.Bind(_labelBind);

            var value = string.IsNullOrWhiteSpace(_valueBind)
                ? JsonString(_value)
                : Nui.Bind(_valueBind);


            return Nui.TextEdit(label, value, _maxLength, _multiLine, _wordWrap);
        }
    }
}
