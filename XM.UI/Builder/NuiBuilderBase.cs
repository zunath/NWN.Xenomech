using Anvil.API;
using System.Collections.Generic;

namespace XM.UI.Builder
{
    public abstract class NuiBuilderBase<TBuilder, TElement>
        where TBuilder : NuiBuilderBase<TBuilder, TElement>
        where TElement : NuiElement
    {
        protected readonly TElement Element;

        protected NuiBuilderBase(TElement element)
        {
            Element = element;
        }

        public TBuilder WithAspect(float? aspect)
        {
            Element.Aspect = aspect;
            return (TBuilder)this;
        }

        public TBuilder WithEnabled(NuiProperty<bool>? enabled)
        {
            Element.Enabled = enabled;
            return (TBuilder)this;
        }

        public TBuilder WithForegroundColor(NuiProperty<Color>? foregroundColor)
        {
            Element.ForegroundColor = foregroundColor;
            return (TBuilder)this;
        }

        public TBuilder WithHeight(float? height)
        {
            Element.Height = height;
            return (TBuilder)this;
        }

        public TBuilder WithId(string? id)
        {
            Element.Id = id;
            return (TBuilder)this;
        }

        public TBuilder WithMargin(float? margin)
        {
            Element.Margin = margin;
            return (TBuilder)this;
        }

        public TBuilder WithPadding(float? padding)
        {
            Element.Padding = padding;
            return (TBuilder)this;
        }

        public TBuilder WithTooltip(NuiProperty<string>? tooltip)
        {
            Element.Tooltip = tooltip;
            return (TBuilder)this;
        }

        public TBuilder WithVisible(NuiProperty<bool>? visible)
        {
            Element.Visible = visible;
            return (TBuilder)this;
        }

        public TBuilder WithWidth(float? width)
        {
            Element.Width = width;
            return (TBuilder)this;
        }

        public TBuilder WithDrawList(List<NuiDrawListItem>? drawList)
        {
            Element.DrawList = drawList;
            return (TBuilder)this;
        }

        public TBuilder WithScissor(NuiProperty<bool>? scissor)
        {
            Element.Scissor = scissor;
            return (TBuilder)this;
        }

        public TBuilder WithDisabledTooltip(NuiProperty<string>? disabledTooltip)
        {
            Element.DisabledTooltip = disabledTooltip;
            return (TBuilder)this;
        }

        public TBuilder WithEncouraged(NuiProperty<bool>? encouraged)
        {
            Element.Encouraged = encouraged;
            return (TBuilder)this;
        }

        public TElement Build()
        {
            return Element;
        }
    }
}
