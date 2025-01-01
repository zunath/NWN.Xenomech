using Anvil.API;
using System.Linq.Expressions;
using System;

namespace XM.UI.Builder.Component
{
    public class NuiTextEditBuilder<TViewModel> : NuiBuilderBase<NuiTextEditBuilder<TViewModel>, NuiTextEdit, TViewModel>
        where TViewModel: IViewModel
    {
        public NuiTextEditBuilder()
            : base(new NuiTextEdit(string.Empty, string.Empty, 1000, false))
        {
        }

        public NuiTextEditBuilder<TViewModel> SetLabel(string label)
        {
            Element.Label = label;
            return this;
        }

        public NuiTextEditBuilder<TViewModel> SetValue(string value)
        {
            Element.Value = value;
            return this;
        }

        public NuiTextEditBuilder<TViewModel> SetMaxLength(ushort maxLength)
        {
            Element.MaxLength = maxLength;
            return this;
        }

        public NuiTextEditBuilder<TViewModel> SetMultiLine(bool multiLine)
        {
            Element.MultiLine = multiLine;
            return this;
        }

        public NuiTextEditBuilder<TViewModel> SetWordWrap(bool wordWrap)
        {
            Element.WordWrap = wordWrap;
            return this;
        }
        public NuiTextEditBuilder<TViewModel> BindLabel(Expression<Func<TViewModel, object>> expression)
        {
            var bindName = GetBindName(expression);
            var bind = new NuiBind<string>(bindName);
            Element.Label = bind;

            return this;
        }

        public NuiTextEditBuilder<TViewModel> BindValue(Expression<Func<TViewModel, object>> expression)
        {
            var bindName = GetBindName(expression);
            var bind = new NuiBind<string>(bindName);
            Element.Value = bind;

            return this;
        }

        public NuiTextEditBuilder<TViewModel> BindWordWrap(Expression<Func<TViewModel, object>> expression)
        {
            var bindName = GetBindName(expression);
            var bind = new NuiBind<bool>(bindName);
            Element.WordWrap = bind;

            return this;
        }

    }
}
