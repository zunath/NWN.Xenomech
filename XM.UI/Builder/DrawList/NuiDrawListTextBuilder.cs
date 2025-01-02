
using Anvil.API;
using System.Linq.Expressions;
using System;

namespace XM.UI.Builder.DrawList
{
    public class NuiDrawListTextBuilder<TViewModel> : NuiDrawListItemBuilderBase<NuiDrawListTextBuilder<TViewModel>, NuiDrawListText, TViewModel>
        where TViewModel: IViewModel
    {
        public NuiDrawListTextBuilder(
            Color color, 
            NuiRect rect, 
            string text,
            NuiEventCollection eventCollection)
            : base(new NuiDrawListText(color, rect, text), eventCollection)
        {
        }

        public NuiDrawListTextBuilder<TViewModel> Rect(NuiRect rect)
        {
            Element.Rect = rect;
            return this;
        }

        public NuiDrawListTextBuilder<TViewModel> Text(string text)
        {
            Element.Text = text;
            return this;
        }

        public NuiDrawListTextBuilder<TViewModel> Rect(Expression<Func<TViewModel, NuiRect>> expression)
        {
            var bindName = GetBindName(expression);
            var bind = new NuiBind<NuiRect>(bindName);
            Element.Rect = bind;

            return this;
        }

        public NuiDrawListTextBuilder<TViewModel> Text(Expression<Func<TViewModel, object>> expression)
        {
            var bindName = GetBindName(expression);
            var bind = new NuiBind<string>(bindName);
            Element.Text = bind;

            return this;
        }

        public NuiDrawListTextBuilder<TViewModel> Color(Expression<Func<TViewModel, object>> expression)
        {
            var bindName = GetBindName(expression);
            var bind = new NuiBind<Color>(bindName);
            Element.Color = bind;

            return this;
        }

    }

}
