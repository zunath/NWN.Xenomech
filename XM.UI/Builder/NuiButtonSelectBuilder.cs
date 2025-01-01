using Anvil.API;

namespace XM.UI.Builder
{
    public class NuiButtonSelectBuilder : NuiBuilderBase<NuiButtonSelectBuilder, NuiButtonSelect>
    {
        public NuiButtonSelectBuilder(string label, bool selected)
            : base(new NuiButtonSelect(label, selected))
        {
        }

        public NuiButtonSelectBuilder WithLabel(string label)
        {
            Element.Label = label;
            return this;
        }

        public NuiButtonSelectBuilder WithSelected(bool selected)
        {
            Element.Selected = selected;
            return this;
        }
    }
}
