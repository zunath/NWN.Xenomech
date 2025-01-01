using Anvil.API;

namespace XM.UI.Builder
{
    public class NuiImageBuilder : NuiBuilderBase<NuiImageBuilder, NuiImage>
    {
        public NuiImageBuilder(string resRef)
            : base(new NuiImage(resRef))
        {
        }

        public NuiImageBuilder WithHorizontalAlign(NuiHAlign horizontalAlign)
        {
            Element.HorizontalAlign = horizontalAlign;
            return this;
        }

        public NuiImageBuilder WithVerticalAlign(NuiVAlign verticalAlign)
        {
            Element.VerticalAlign = verticalAlign;
            return this;
        }

        public NuiImageBuilder WithImageAspect(NuiAspect aspect)
        {
            Element.ImageAspect = aspect;
            return this;
        }

        public NuiImageBuilder WithImageRegion(NuiRect region)
        {
            Element.ImageRegion = region;
            return this;
        }

        public NuiImageBuilder WithResRef(string resRef)
        {
            Element.ResRef = resRef;
            return this;
        }
    }

}
