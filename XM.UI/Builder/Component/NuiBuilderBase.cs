using Anvil.API;
using System.Collections.Generic;

namespace XM.UI.Builder.Component
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

        public TBuilder SetAspect(float? aspect)
        {
            Element.Aspect = aspect;
            return (TBuilder)this;
        }

        public TBuilder SetEnabled(NuiProperty<bool> enabled)
        {
            Element.Enabled = enabled;
            return (TBuilder)this;
        }

        public TBuilder SetForegroundColor(NuiProperty<Color> foregroundColor)
        {
            Element.ForegroundColor = foregroundColor;
            return (TBuilder)this;
        }

        public TBuilder SetHeight(float? height)
        {
            Element.Height = height;
            return (TBuilder)this;
        }

        public TBuilder SetId(string id)
        {
            Element.Id = id;
            return (TBuilder)this;
        }

        public TBuilder SetMargin(float? margin)
        {
            Element.Margin = margin;
            return (TBuilder)this;
        }

        public TBuilder SetPadding(float? padding)
        {
            Element.Padding = padding;
            return (TBuilder)this;
        }

        public TBuilder SetTooltip(NuiProperty<string> tooltip)
        {
            Element.Tooltip = tooltip;
            return (TBuilder)this;
        }

        public TBuilder SetVisible(NuiProperty<bool> visible)
        {
            Element.Visible = visible;
            return (TBuilder)this;
        }

        public TBuilder SetWidth(float? width)
        {
            Element.Width = width;
            return (TBuilder)this;
        }

        public TBuilder SetDrawList(List<NuiDrawListItem> drawList)
        {
            Element.DrawList = drawList;
            return (TBuilder)this;
        }

        public TBuilder SetScissor(NuiProperty<bool> scissor)
        {
            Element.Scissor = scissor;
            return (TBuilder)this;
        }

        public TBuilder SetDisabledTooltip(NuiProperty<string> disabledTooltip)
        {
            Element.DisabledTooltip = disabledTooltip;
            return (TBuilder)this;
        }

        public TBuilder SetEncouraged(NuiProperty<bool> encouraged)
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
