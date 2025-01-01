using Anvil.API;

namespace XM.UI.Builder.DrawList
{
    public class NuiDrawListImageBuilder : NuiDrawListItemBuilderBase<NuiDrawListImageBuilder, NuiDrawListImage>
    {
        public NuiDrawListImageBuilder(string resRef, NuiRect rect)
            : base(new NuiDrawListImage(resRef, rect))
        {
        }

        public NuiDrawListImageBuilder WithAspect(NuiAspect aspect)
        {
            Element.Aspect = aspect;
            return this;
        }

        public NuiDrawListImageBuilder WithHorizontalAlign(NuiHAlign horizontalAlign)
        {
            Element.HorizontalAlign = horizontalAlign;
            return this;
        }

        public NuiDrawListImageBuilder WithVerticalAlign(NuiVAlign verticalAlign)
        {
            Element.VerticalAlign = verticalAlign;
            return this;
        }

        public NuiDrawListImageBuilder WithRect(NuiRect rect)
        {
            Element.Rect = rect;
            return this;
        }

        public NuiDrawListImageBuilder WithResRef(string resRef)
        {
            Element.ResRef = resRef;
            return this;
        }

        public NuiDrawListImageBuilder WithImageRegion(NuiRect? imageRegion)
        {
            Element.ImageRegion = imageRegion;
            return this;
        }
    }

}
