using Anvil.API;

namespace XM.UI.Builder.Component
{
    public class NuiSliderBuilder : NuiBuilderBase<NuiSliderBuilder, NuiSlider>
    {
        public NuiSliderBuilder()
            : base(new NuiSlider(0, 0,0))
        {
        }

        public NuiSliderBuilder SetValue(int value)
        {
            Element.Value = value;
            return this;
        }

        public NuiSliderBuilder SetMin(int min)
        {
            Element.Min = min;
            return this;
        }

        public NuiSliderBuilder SetMax(int max)
        {
            Element.Max = max;
            return this;
        }

        public NuiSliderBuilder SetStep(int step)
        {
            Element.Step = step;
            return this;
        }
    }


}
