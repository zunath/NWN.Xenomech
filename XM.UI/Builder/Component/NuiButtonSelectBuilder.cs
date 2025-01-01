using Anvil.API;

namespace XM.UI.Builder.Component
{
    public class NuiButtonSelectBuilder : NuiBuilderBase<NuiButtonSelectBuilder, NuiButtonSelect>
    {
        public NuiButtonSelectBuilder()
            : base(new NuiButtonSelect(string.Empty, false))
        {
        }

        public NuiButtonSelectBuilder SetLabel(string label)
        {
            Element.Label = label;
            return this;
        }

        public NuiButtonSelectBuilder SetSelected(bool selected)
        {
            Element.Selected = selected;
            return this;
        }
    }
}
