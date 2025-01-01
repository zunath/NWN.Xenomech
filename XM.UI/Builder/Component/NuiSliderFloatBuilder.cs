using Anvil.API;

namespace XM.UI.Builder.Component
{
    public class NuiSliderFloatBuilder : NuiBuilderBase<NuiSliderFloatBuilder, NuiSliderFloat>
    {
        public NuiSliderFloatBuilder()
            : base(new NuiSliderFloat(0f, 0f, 0f))
        {
        }

        public NuiSliderFloatBuilder SetValue(float value)
        {
            Element.Value = value;
            return this;
        }

        public NuiSliderFloatBuilder SetMin(float min)
        {
            Element.Min = min;
            return this;
        }

        public NuiSliderFloatBuilder SetMax(float max)
        {
            Element.Max = max;
            return this;
        }

        public NuiSliderFloatBuilder SetStepSize(float stepSize)
        {
            Element.StepSize = stepSize;
            return this;
        }
    }


}
