using Anvil.API;

namespace XM.UI.Builder
{
    public class NuiOptionsBuilder : NuiBuilderBase<NuiOptionsBuilder, NuiOptions>
    {
        public NuiOptionsBuilder()
            : base(new NuiOptions())
        {
        }

        public NuiOptionsBuilder WithDirection(NuiDirection direction)
        {
            Element.Direction = direction;
            return this;
        }

        public NuiOptionsBuilder AddOption(string option)
        {
            Element.Options.Add(option);
            return this;
        }

        public NuiOptionsBuilder WithSelection(int selection)
        {
            Element.Selection = selection;
            return this;
        }
    }


}
