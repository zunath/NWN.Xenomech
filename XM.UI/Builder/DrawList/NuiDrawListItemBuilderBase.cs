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

        protected NuiDrawListItemBuilderBase(TElement element, NuiEventCollection eventCollection) 
            : base(eventCollection)
        {
            Element = element;
        }

        public TBuilder Color(Color color)
        {
            Element.Color = color;
            return (TBuilder)this;
        }

        public TBuilder Enabled(bool enabled)
        {
            Element.Enabled = enabled;
            return (TBuilder)this;
        }

        public TBuilder Fill(bool fill)
        {
            Element.Fill = fill;
            return (TBuilder)this;
        }

        public TBuilder LineThickness(float lineThickness)
        {
            Element.LineThickness = lineThickness;
            return (TBuilder)this;
        }

        public TBuilder Order(NuiDrawListItemOrder order)
        {
            Element.Order = order;
            return (TBuilder)this;
        }

        public TBuilder Render(NuiDrawListItemRender render)
        {
            Element.Render = render;
            return (TBuilder)this;
        }

        public TBuilder Color(Expression<Func<TViewModel, Color>> expression)
        {
            var bindName = GetBindName(expression);
            var bind = new NuiBind<Color>(bindName);
            Element.Color = bind;
            return (TBuilder)this;
        }

        public TBuilder Enabled(Expression<Func<TViewModel, bool>> expression)
        {
            var bindName = GetBindName(expression);
            var bind = new NuiBind<bool>(bindName);
            Element.Enabled = bind;
            return (TBuilder)this;
        }

        public TBuilder Fill(Expression<Func<TViewModel, bool>> expression)
        {
            var bindName = GetBindName(expression);
            var bind = new NuiBind<bool>(bindName);
            Element.Fill = bind;
            return (TBuilder)this;
        }

        public TBuilder LineThickness(Expression<Func<TViewModel, float>> expression)
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