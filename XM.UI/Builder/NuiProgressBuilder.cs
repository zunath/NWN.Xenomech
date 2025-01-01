using Anvil.API;

namespace XM.UI.Builder
{
    public class NuiProgressBuilder : NuiBuilderBase<NuiProgressBuilder, NuiProgress>
    {
        public NuiProgressBuilder(float value)
            : base(new NuiProgress(value))
        {
        }

        public NuiProgressBuilder WithValue(float value)
        {
            Element.Value = value;
            return this;
        }
    }


}
