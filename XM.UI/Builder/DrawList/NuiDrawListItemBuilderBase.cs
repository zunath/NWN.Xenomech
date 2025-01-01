using Anvil.API;

namespace XM.UI.Builder.DrawList
{
    public abstract class NuiDrawListItemBuilderBase<TBuilder, TElement>
        where TBuilder : NuiDrawListItemBuilderBase<TBuilder, TElement>
        where TElement : NuiDrawListItem
    {
        protected readonly TElement Element;

        protected NuiDrawListItemBuilderBase(TElement element)
        {
            Element = element;
        }

        public TBuilder WithColor(NuiProperty<Color>? color)
        {
            Element.Color = color;
            return (TBuilder)this;
        }

        public TBuilder WithEnabled(NuiProperty<bool> enabled)
        {
            Element.Enabled = enabled;
            return (TBuilder)this;
        }

        public TBuilder WithFill(NuiProperty<bool>? fill)
        {
            Element.Fill = fill;
            return (TBuilder)this;
        }

        public TBuilder WithLineThickness(NuiProperty<float>? lineThickness)
        {
            Element.LineThickness = lineThickness;
            return (TBuilder)this;
        }

        public TBuilder WithOrder(NuiDrawListItemOrder order)
        {
            Element.Order = order;
            return (TBuilder)this;
        }

        public TBuilder WithRender(NuiDrawListItemRender render)
        {
            Element.Render = render;
            return (TBuilder)this;
        }

        public TElement Build()
        {
            return Element;
        }
    }
}