
using Anvil.API;
using System.Linq.Expressions;
using System;

namespace XM.UI.Builder.DrawList
{
    public class NuiDrawListTextBuilder<TViewModel> : NuiDrawListItemBuilderBase<NuiDrawListTextBuilder<TViewModel>, NuiDrawListText, TViewModel>
        where TViewModel: IViewModel
    {
        public NuiDrawListTextBuilder(Color color, NuiRect rect, string text)
            : base(new NuiDrawListText(color, rect, text))
        {
        }

        public NuiDrawListTextBuilder<TViewModel> SetRect(NuiRect rect)
        {
            Element.Rect = rect;
            return this;
        }

        public NuiDrawListTextBuilder<TViewModel> SetText(string text)
        {
            Element.Text = text;
            return this;
        }

        public NuiDrawListTextBuilder<TViewModel> SetColor(Color color)
        {
            Element.Color = color;
            return this;
        }
        public NuiDrawListTextBuilder<TViewModel> BindRect(Expression<Func<TViewModel, NuiRect>> expression)
        {
            var bindName = GetBindName(expression);
            var bind = new NuiBind<NuiRect>(bindName);
            Element.Rect = bind;

            return this;
        }

        public NuiDrawListTextBuilder<TViewModel> BindText(Expression<Func<TViewModel, object>> expression)
        {
            var bindName = GetBindName(expression);
            var bind = new NuiBind<string>(bindName);
            Element.Text = bind;

            return this;
        }

        public NuiDrawListTextBuilder<TViewModel> BindColor(Expression<Func<TViewModel, object>> expression)
        {
            var bindName = GetBindName(expression);
            var bind = new NuiBind<Color>(bindName);
            Element.Color = bind;

            return this;
        }

    }

}
