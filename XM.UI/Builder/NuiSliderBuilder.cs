using Anvil.API;

namespace XM.UI.Builder
{
    public class NuiSliderBuilder : NuiBuilderBase<NuiSliderBuilder, NuiSlider>
    {
        public NuiSliderBuilder(int value, int min, int max)
            : base(new NuiSlider(value, min, max))
        {
        }

        public NuiSliderBuilder WithValue(int value)
        {
            Element.Value = value;
            return this;
        }

        public NuiSliderBuilder WithMin(int min)
        {
            Element.Min = min;
            return this;
        }

        public NuiSliderBuilder WithMax(int max)
        {
            Element.Max = max;
            return this;
        }

        public NuiSliderBuilder WithStep(int step)
        {
            Element.Step = step;
            return this;
        }
    }


}
