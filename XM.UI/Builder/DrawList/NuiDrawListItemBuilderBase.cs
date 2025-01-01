using Anvil.API;
using System.Linq.Expressions;
using System;

namespace XM.UI.Builder.DrawList
{
    public abstract class NuiDrawListItemBuilderBase<TBuilder, TElement, TViewModel>: NuiBindable<TViewModel>
        where TBuilder : NuiDrawListItemBuilderBase<TBuilder, TElement, TViewModel>
        where TElement : NuiDrawListItem
        where TViewModel: IViewModel
    {
        protected readonly TElement Element;

        protected NuiDrawListItemBuilderBase(TElement element)
        {
            Element = element;
        }

        public TBuilder SetColor(Color color)
        {
            Element.Color = color;
            return (TBuilder)this;
        }

        public TBuilder SetEnabled(bool enabled)
        {
            Element.Enabled = enabled;
            return (TBuilder)this;
        }

        public TBuilder SetFill(bool fill)
        {
            Element.Fill = fill;
            return (TBuilder)this;
        }

        public TBuilder SetLineThickness(float lineThickness)
        {
            Element.LineThickness = lineThickness;
            return (TBuilder)this;
        }

        public TBuilder SetOrder(NuiDrawListItemOrder order)
        {
            Element.Order = order;
            return (TBuilder)this;
        }

        public TBuilder SetRender(NuiDrawListItemRender render)
        {
            Element.Render = render;
            return (TBuilder)this;
        }

        public TBuilder BindColor(Expression<Func<TViewModel, Color>> expression)
        {
            var bindName = GetBindName(expression);
            var bind = new NuiBind<Color>(bindName);
            Element.Color = bind;
            return (TBuilder)this;
        }

        public TBuilder BindEnabled(Expression<Func<TViewModel, bool>> expression)
        {
            var bindName = GetBindName(expression);
            var bind = new NuiBind<bool>(bindName);
            Element.Enabled = bind;
            return (TBuilder)this;
        }

        public TBuilder BindFill(Expression<Func<TViewModel, bool>> expression)
        {
            var bindName = GetBindName(expression);
            var bind = new NuiBind<bool>(bindName);
            Element.Fill = bind;
            return (TBuilder)this;
        }

        public TBuilder BindLineThickness(Expression<Func<TViewModel, float>> expression)
        {
            var bindName = GetBindName(expression);
            var bind = new NuiBind<float>(bindName);
            Element.LineThickness = bind;
            return (TBuilder)this;
        }

        public TElement Build()
        {
            return Element;
        }
    }
}