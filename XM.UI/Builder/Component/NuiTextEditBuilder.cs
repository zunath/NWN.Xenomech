using Anvil.API;
using System.Linq.Expressions;
using System;

namespace XM.UI.Builder.Component
{
    public class NuiTextEditBuilder<TViewModel> : NuiBuilderBase<NuiTextEditBuilder<TViewModel>, NuiTextEdit, TViewModel>
        where TViewModel: IViewModel
    {
        public NuiTextEditBuilder(NuiEventCollection eventCollection)
            : base(new NuiTextEdit(string.Empty, string.Empty, 1000, false), eventCollection)
        {
        }

        public NuiTextEditBuilder<TViewModel> Label(string label)
        {
            Element.Label = label;
            return this;
        }

        public NuiTextEditBuilder<TViewModel> Value(string value)
        {
            Element.Value = value;
            return this;
        }

        public NuiTextEditBuilder<TViewModel> MaxLength(ushort maxLength)
        {
            Element.MaxLength = maxLength;
            return this;
        }

        public NuiTextEditBuilder<TViewModel> MultiLine(bool multiLine)
        {
            Element.MultiLine = multiLine;
            return this;
        }

        public NuiTextEditBuilder<TViewModel> WordWrap(bool wordWrap)
        {
            Element.WordWrap = wordWrap;
            return this;
        }
        public NuiTextEditBuilder<TViewModel> Label(Expression<Func<TViewModel, object>> expression)
        {
            var bindName = GetBindName(expression);
            var bind = new NuiBind<string>(bindName);
            Element.Label = bind;

            return this;
        }

        public NuiTextEditBuilder<TViewModel> Value(Expression<Func<TViewModel, object>> expression)
        {
            var bindName = GetBindName(expression);
            var bind = new NuiBind<string>(bindName);
            Element.Value = bind;

            return this;
        }

        public NuiTextEditBuilder<TViewModel> WordWrap(Expression<Func<TViewModel, object>> expression)
        {
            var bindName = GetBindName(expression);
            var bind = new NuiBind<bool>(bindName);
            Element.WordWrap = bind;

            return this;
        }

    }
}
