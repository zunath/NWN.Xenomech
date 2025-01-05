using Anvil.API;
using System.Linq.Expressions;
using System;

namespace XM.UI.Builder.Component
{
    public class NuiSliderFloatBuilder<TViewModel> : NuiBuilderBase<NuiSliderFloatBuilder<TViewModel>, NuiSliderFloat, TViewModel>
        where TViewModel: IViewModel
    {
        public NuiSliderFloatBuilder(NuiEventCollection eventCollection)
            : base(new NuiSliderFloat(0f, 0f, 0f), eventCollection)
        {
        }

        public NuiSliderFloatBuilder<TViewModel> Value(float value)
        {
            Element.Value = value;
            return this;
        }

        public NuiSliderFloatBuilder<TViewModel> Min(float min)
        {
            Element.Min = min;
            return this;
        }

        public NuiSliderFloatBuilder<TViewModel> Max(float max)
        {
            Element.Max = max;
            return this;
        }

        public NuiSliderFloatBuilder<TViewModel> StepSize(float stepSize)
        {
            Element.StepSize = stepSize;
            return this;
        }
        public NuiSliderFloatBuilder<TViewModel> Value(Expression<Func<TViewModel, float>> expression)
        {
            var bindName = GetBindName(expression);
            var bind = new NuiBind<float>(bindName);
            Element.Value = bind;

            return this;
        }

        public NuiSliderFloatBuilder<TViewModel> Min(Expression<Func<TViewModel, float>> expression)
        {
            var bindName = GetBindName(expression);
            var bind = new NuiBind<float>(bindName);
            Element.Min = bind;

            return this;
        }

        public NuiSliderFloatBuilder<TViewModel> Max(Expression<Func<TViewModel, float>> expression)
        {
            var bindName = GetBindName(expression);
            var bind = new NuiBind<float>(bindName);
            Element.Max = bind;

            return this;
        }

        public NuiSliderFloatBuilder<TViewModel> StepSize(Expression<Func<TViewModel, float>> expression)
        {
            var bindName = GetBindName(expression);
            var bind = new NuiBind<float>(bindName);
            Element.StepSize = bind;

            return this;
        }

    }
}
