using Anvil.API;

namespace XM.UI.Builder.Component
{
    public class NuiProgressBuilder : NuiBuilderBase<NuiProgressBuilder, NuiProgress>
    {
        public NuiProgressBuilder()
            : base(new NuiProgress(0f))
        {
        }

        public NuiProgressBuilder SetValue(float value)
        {
            Element.Value = value;
            return this;
        }
    }


}
