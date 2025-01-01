using Anvil.API;

namespace XM.UI.Builder.Component
{
    public class NuiImageBuilder : NuiBuilderBase<NuiImageBuilder, NuiImage>
    {
        public NuiImageBuilder()
            : base(new NuiImage(string.Empty))
        {
        }

        public NuiImageBuilder SetHorizontalAlign(NuiHAlign horizontalAlign)
        {
            Element.HorizontalAlign = horizontalAlign;
            return this;
        }

        public NuiImageBuilder SetVerticalAlign(NuiVAlign verticalAlign)
        {
            Element.VerticalAlign = verticalAlign;
            return this;
        }

        public NuiImageBuilder SetImageAspect(NuiAspect aspect)
        {
            Element.ImageAspect = aspect;
            return this;
        }

        public NuiImageBuilder SetImageRegion(NuiRect region)
        {
            Element.ImageRegion = region;
            return this;
        }

        public NuiImageBuilder SetResRef(string resRef)
        {
            Element.ResRef = resRef;
            return this;
        }
    }

}
