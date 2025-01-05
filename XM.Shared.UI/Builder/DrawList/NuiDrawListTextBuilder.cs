using Anvil.API;
using System.Linq.Expressions;
using System;

namespace XM.UI.Builder.DrawList
{
    public class NuiDrawListTextBuilder<TViewModel> : NuiDrawListItemBuilderBase<NuiDrawListTextBuilder<TViewModel>, NuiDrawListText, TViewModel>
        where TViewModel: IViewModel
    {
        public NuiDrawListTextBuilder()
            : base(new NuiDrawListText(
                Anvil.API.Color.FromRGBA(0), 
                new NuiRect(), 
                string.Empty))
        {
        }

        public NuiDrawListTextBuilder<TViewModel> Bounds(float x, float y, float width, float height)
        {
            Element.Rect = new NuiRect(x, y, width, height);
            return this;
        }

        public NuiDrawListTextBuilder<TViewModel> Bounds(NuiRect rect)
        {
            Element.Rect = rect;
            return this;
        }

        public NuiDrawListTextBuilder<TViewModel> Text(string text)
        {
            Element.Text = text;
            return this;
        }

        public NuiDrawListTextBuilder<TViewModel> Bounds(Expression<Func<TViewModel, NuiRect>> expression)
        {
            var bindName = GetBindName(expression);
            var bind = new NuiBind<NuiRect>(bindName);
            Element.Rect = bind;

            return this;
        }

        public NuiDrawListTextBuilder<TViewModel> Text(Expression<Func<TViewModel, string>> expression)
        {
            var bindName = GetBindName(expression);
            var bind = new NuiBind<string>(bindName);
            Element.Text = bind;

            return this;
        }

    }

}
