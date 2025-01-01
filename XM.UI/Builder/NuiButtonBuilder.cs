using Anvil.API;
using System.Xml.Linq;

namespace XM.UI.Builder
{
    public class NuiButtonBuilder : NuiBuilderBase<NuiButtonBuilder, NuiButton>
    {
        public NuiButtonBuilder(string label)
            : base(new NuiButton(label))
        {
        }

        public NuiButtonBuilder WithLabel(string label)
        {
            Element.Label = label;
            return this;
        }
    }
}
