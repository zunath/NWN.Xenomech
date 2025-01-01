using Anvil.API;

namespace XM.UI.Builder
{
    public class NuiCheckBuilder : NuiBuilderBase<NuiCheckBuilder, NuiCheck>
    {
        public NuiCheckBuilder(string label, bool selected)
            : base(new NuiCheck(label, selected))
        {
        }

        public NuiCheckBuilder WithLabel(string label)
        {
            Element.Label = label;
            return this;
        }

        public NuiCheckBuilder WithSelected(bool selected)
        {
            Element.Selected = selected;
            return this;
        }
    }

}
