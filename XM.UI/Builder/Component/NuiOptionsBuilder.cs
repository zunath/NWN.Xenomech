using Anvil.API;

namespace XM.UI.Builder.Component
{
    public class NuiOptionsBuilder : NuiBuilderBase<NuiOptionsBuilder, NuiOptions>
    {
        public NuiOptionsBuilder()
            : base(new NuiOptions())
        {
        }

        public NuiOptionsBuilder SetDirection(NuiDirection direction)
        {
            Element.Direction = direction;
            return this;
        }

        public NuiOptionsBuilder AddOption(string option)
        {
            Element.Options.Add(option);
            return this;
        }

        public NuiOptionsBuilder SetSelection(int selection)
        {
            Element.Selection = selection;
            return this;
        }
    }


}
