using Anvil.API;

namespace XM.UI.Builder.Component
{
    public class NuiCheckBuilder : NuiBuilderBase<NuiCheckBuilder, NuiCheck>
    {
        public NuiCheckBuilder()
            : base(new NuiCheck(string.Empty, false))
        {
        }

        public NuiCheckBuilder SetLabel(string label)
        {
            Element.Label = label;
            return this;
        }

        public NuiCheckBuilder SetSelected(bool selected)
        {
            Element.Selected = selected;
            return this;
        }
    }

}
