using Anvil.API;

namespace XM.UI.Builder.DrawList
{
    public class NuiDrawListImageBuilder : NuiDrawListItemBuilderBase<NuiDrawListImageBuilder, NuiDrawListImage>
    {
        public NuiDrawListImageBuilder(string resRef, NuiRect rect)
            : base(new NuiDrawListImage(resRef, rect))
        {
        }

        public NuiDrawListImageBuilder SetAspect(NuiAspect aspect)
        {
            Element.Aspect = aspect;
            return this;
        }

        public NuiDrawListImageBuilder SetHorizontalAlign(NuiHAlign horizontalAlign)
        {
            Element.HorizontalAlign = horizontalAlign;
            return this;
        }

        public NuiDrawListImageBuilder SetVerticalAlign(NuiVAlign verticalAlign)
        {
            Element.VerticalAlign = verticalAlign;
            return this;
        }

        public NuiDrawListImageBuilder SetRect(NuiRect rect)
        {
            Element.Rect = rect;
            return this;
        }

        public NuiDrawListImageBuilder SetResRef(string resRef)
        {
            Element.ResRef = resRef;
            return this;
        }

        public NuiDrawListImageBuilder SetImageRegion(NuiRect? imageRegion)
        {
            Element.ImageRegion = imageRegion;
            return this;
        }
    }

}
