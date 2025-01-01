using Anvil.API;

namespace XM.UI.Builder
{
    public class NuiLabelBuilder : NuiBuilderBase<NuiLabelBuilder, NuiLabel>
    {
        public NuiLabelBuilder(string label)
            : base(new NuiLabel(label))
        {
        }

        public NuiLabelBuilder WithHorizontalAlign(NuiHAlign horizontalAlign)
        {
            Element.HorizontalAlign = horizontalAlign;
            return this;
        }

        public NuiLabelBuilder WithVerticalAlign(NuiVAlign verticalAlign)
        {
            Element.VerticalAlign = verticalAlign;
            return this;
        }

        public NuiLabelBuilder WithLabel(string label)
        {
            Element.Label = label;
            return this;
        }
    }

}
