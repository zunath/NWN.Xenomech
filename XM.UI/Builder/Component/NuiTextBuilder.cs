using Anvil.API;
using System.Linq.Expressions;
using System;

namespace XM.UI.Builder.Component
{
    public class NuiTextBuilder<TViewModel> : NuiBuilderBase<NuiTextBuilder<TViewModel>, NuiText, TViewModel>
        where TViewModel: IViewModel
    {
        public NuiTextBuilder(NuiEventCollection eventCollection)
            : base(new NuiText(string.Empty), eventCollection)
        {
        }

        public NuiTextBuilder<TViewModel> Text(string text)
        {
            Element.Text = text;
            return this;
        }

        public NuiTextBuilder<TViewModel> Border(bool border)
        {
            Element.Border = border;
            return this;
        }

        public NuiTextBuilder<TViewModel> Scrollbars(NuiScrollbars scrollbars)
        {
            Element.Scrollbars = scrollbars;
            return this;
        }
        public NuiTextBuilder<TViewModel> Text(Expression<Func<TViewModel, object>> expression)
        {
            var bindName = GetBindName(expression);
            var bind = new NuiBind<string>(bindName);
            Element.Text = bind;

            return this;
        }
    }
}