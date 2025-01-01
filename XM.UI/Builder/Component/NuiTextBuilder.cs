using Anvil.API;
using System.Linq.Expressions;
using System;

namespace XM.UI.Builder.Component
{
    public class NuiTextBuilder<TViewModel> : NuiBuilderBase<NuiTextBuilder<TViewModel>, NuiText, TViewModel>
        where TViewModel: IViewModel
    {
        public NuiTextBuilder()
            : base(new NuiText(string.Empty))
        {
        }

        public NuiTextBuilder<TViewModel> SetText(string text)
        {
            Element.Text = text;
            return this;
        }

        public NuiTextBuilder<TViewModel> SetBorder(bool border)
        {
            Element.Border = border;
            return this;
        }

        public NuiTextBuilder<TViewModel> SetScrollbars(NuiScrollbars scrollbars)
        {
            Element.Scrollbars = scrollbars;
            return this;
        }
        public NuiTextBuilder<TViewModel> BindText(Expression<Func<TViewModel, object>> expression)
        {
            var bindName = GetBindName(expression);
            var bind = new NuiBind<string>(bindName);
            Element.Text = bind;

            return this;
        }
    }
}