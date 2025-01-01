using Anvil.API;

namespace XM.UI.Builder
{
    public class NuiButtonImageBuilder : NuiBuilderBase<NuiButtonImageBuilder, NuiButtonImage>
    {
        public NuiButtonImageBuilder(string resRef)
            : base(new NuiButtonImage(resRef))
        {
        }

        public NuiButtonImageBuilder WithResRef(string resRef)
        {
            Element.ResRef = resRef;
            return this;
        }
    }

}
