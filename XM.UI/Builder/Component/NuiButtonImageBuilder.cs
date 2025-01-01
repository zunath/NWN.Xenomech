using Anvil.API;

namespace XM.UI.Builder.Component
{
    public class NuiButtonImageBuilder : NuiBuilderBase<NuiButtonImageBuilder, NuiButtonImage>
    {
        public NuiButtonImageBuilder()
            : base(new NuiButtonImage(string.Empty))
        {
        }

        public NuiButtonImageBuilder SetResRef(string resRef)
        {
            Element.ResRef = resRef;
            return this;
        }
    }

}
