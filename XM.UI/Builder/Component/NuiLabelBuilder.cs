using Anvil.API;

namespace XM.UI.Builder.Component
{
    public class NuiLabelBuilder : NuiBuilderBase<NuiLabelBuilder, NuiLabel>
    {
        public NuiLabelBuilder()
            : base(new NuiLabel(string.Empty))
        {
        }

        public NuiLabelBuilder SetHorizontalAlign(NuiHAlign horizontalAlign)
        {
            Element.HorizontalAlign = horizontalAlign;
            return this;
        }

        public NuiLabelBuilder SetVerticalAlign(NuiVAlign verticalAlign)
        {
            Element.VerticalAlign = verticalAlign;
            return this;
        }

        public NuiLabelBuilder SetLabel(string label)
        {
            Element.Label = label;
            return this;
        }
    }

}
