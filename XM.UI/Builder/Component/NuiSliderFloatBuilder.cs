using Anvil.API;
using System.Linq.Expressions;
using System;

namespace XM.UI.Builder.Component
{
    public class NuiSliderFloatBuilder<TViewModel> : NuiBuilderBase<NuiSliderFloatBuilder<TViewModel>, NuiSliderFloat, TViewModel>
        where TViewModel: IViewModel
    {
        public NuiSliderFloatBuilder()
            : base(new NuiSliderFloat(0f, 0f, 0f))
        {
        }

        public NuiSliderFloatBuilder<TViewModel> SetValue(float value)
        {
            Element.Value = value;
            return this;
        }

        public NuiSliderFloatBuilder<TViewModel> SetMin(float min)
        {
            Element.Min = min;
            return this;
        }

        public NuiSliderFloatBuilder<TViewModel> SetMax(float max)
        {
            Element.Max = max;
            return this;
        }

        public NuiSliderFloatBuilder<TViewModel> SetStepSize(float stepSize)
        {
            Element.StepSize = stepSize;
            return this;
        }
        public NuiSliderFloatBuilder<TViewModel> BindValue(Expression<Func<TViewModel, float>> expression)
        {
            var bindName = GetBindName(expression);
            var bind = new NuiBind<float>(bindName);
            Element.Value = bind;

            return this;
        }

        public NuiSliderFloatBuilder<TViewModel> BindMin(Expression<Func<TViewModel, float>> expression)
        {
            var bindName = GetBindName(expression);
            var bind = new NuiBind<float>(bindName);
            Element.Min = bind;

            return this;
        }

        public NuiSliderFloatBuilder<TViewModel> BindMax(Expression<Func<TViewModel, float>> expression)
        {
            var bindName = GetBindName(expression);
            var bind = new NuiBind<float>(bindName);
            Element.Max = bind;

            return this;
        }

        public NuiSliderFloatBuilder<TViewModel> BindStepSize(Expression<Func<TViewModel, float>> expression)
        {
            var bindName = GetBindName(expression);
            var bind = new NuiBind<float>(bindName);
            Element.StepSize = bind;

            return this;
        }

    }
}
