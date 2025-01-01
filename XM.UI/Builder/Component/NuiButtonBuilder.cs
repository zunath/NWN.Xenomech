using Anvil.API;

namespace XM.UI.Builder.Component
{
    public class NuiButtonBuilder : NuiBuilderBase<NuiButtonBuilder, NuiButton>
    {
        public NuiButtonBuilder()
            : base(new NuiButton(string.Empty))
        {
        }

        public NuiButtonBuilder SetLabel(string label)
        {
            Element.Label = label;
            return this;
        }
    }
}
