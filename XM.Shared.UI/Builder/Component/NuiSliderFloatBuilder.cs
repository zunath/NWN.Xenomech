using Anvil.API;
using System.Linq.Expressions;
using System;
using XM.Shared.API.NUI;

namespace XM.UI.Builder.Component
{
    public class NuiSliderFloatBuilder<TViewModel> : NuiBuilderBase<NuiSliderFloatBuilder<TViewModel>, TViewModel>
        where TViewModel : IViewModel
    {
        private float _value;
        private string _valueBind;

        private float _min;
        private string _minBind;

        private float _max;
        private string _maxBind;

        private float _stepSize;
        private string _stepSizeBind;

        public NuiSliderFloatBuilder(NuiEventCollection eventCollection)
            : base(eventCollection)
        {
        }

        public NuiSliderFloatBuilder<TViewModel> Value(float value)
        {
            _value = value;
            return this;
        }

        public NuiSliderFloatBuilder<TViewModel> Min(float min)
        {
            _min = min;
            return this;
        }

        public NuiSliderFloatBuilder<TViewModel> Max(float max)
        {
            _max = max;
            return this;
        }

        public NuiSliderFloatBuilder<TViewModel> StepSize(float stepSize)
        {
            _stepSize = stepSize;
            return this;
        }

        public NuiSliderFloatBuilder<TViewModel> Value(Expression<Func<TViewModel, float>> expression)
        {
            _valueBind = GetBindName(expression);
            return this;
        }

        public NuiSliderFloatBuilder<TViewModel> Min(Expression<Func<TViewModel, float>> expression)
        {
            _minBind = GetBindName(expression);
            return this;
        }

        public NuiSliderFloatBuilder<TViewModel> Max(Expression<Func<TViewModel, float>> expression)
        {
            _maxBind = GetBindName(expression);
            return this;
        }

        public NuiSliderFloatBuilder<TViewModel> StepSize(Expression<Func<TViewModel, float>> expression)
        {
            _stepSizeBind = GetBindName(expression);
            return this;
        }

        public override Json BuildEntity()
        {
            var value = string.IsNullOrWhiteSpace(_valueBind)
                ? JsonFloat(_value)
                : Nui.Bind(_valueBind);

            var min = string.IsNullOrWhiteSpace(_minBind)
                ? JsonFloat(_min)
                : Nui.Bind(_minBind);

            var max = string.IsNullOrWhiteSpace(_maxBind)
                ? JsonFloat(_max)
                : Nui.Bind(_maxBind);

            var stepSize = string.IsNullOrWhiteSpace(_stepSizeBind)
                ? JsonFloat(_stepSize)
                : Nui.Bind(_stepSizeBind);

            return Nui.SliderFloat(value, min, max, stepSize);
        }
    }
}
