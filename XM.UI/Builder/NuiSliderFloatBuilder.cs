using Anvil.API;

namespace XM.UI.Builder
{
    public class NuiSliderFloatBuilder : NuiBuilderBase<NuiSliderFloatBuilder, NuiSliderFloat>
    {
        public NuiSliderFloatBuilder(float value, float min, float max)
            : base(new NuiSliderFloat(value, min, max))
        {
        }

        public NuiSliderFloatBuilder WithValue(float value)
        {
            Element.Value = value;
            return this;
        }

        public NuiSliderFloatBuilder WithMin(float min)
        {
            Element.Min = min;
            return this;
        }

        public NuiSliderFloatBuilder WithMax(float max)
        {
            Element.Max = max;
            return this;
        }

        public NuiSliderFloatBuilder WithStepSize(float stepSize)
        {
            Element.StepSize = stepSize;
            return this;
        }
    }


}
