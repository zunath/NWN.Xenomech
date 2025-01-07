using Anvil.API;
using System.Linq.Expressions;
using System;
using XM.Shared.API.NUI;

namespace XM.UI.Builder.Component
{
    public class NuiSliderBuilder<TViewModel> : NuiBuilderBase<NuiSliderBuilder<TViewModel>, TViewModel>
        where TViewModel : IViewModel
    {
        private int _value;
        private string _valueBind;

        private int _min;
        private string _minBind;

        private int _max;
        private string _maxBind;

        private int _step;
        private string _stepBind;

        public NuiSliderBuilder(NuiEventCollection eventCollection)
            : base(eventCollection)
        {
        }

        public NuiSliderBuilder<TViewModel> Value(int value)
        {
            _value = value;
            return this;
        }

        public NuiSliderBuilder<TViewModel> Min(int min)
        {
            _min = min;
            return this;
        }

        public NuiSliderBuilder<TViewModel> Max(int max)
        {
            _max = max;
            return this;
        }

        public NuiSliderBuilder<TViewModel> Step(int step)
        {
            _step = step;
            return this;
        }

        public NuiSliderBuilder<TViewModel> Value(Expression<Func<TViewModel, int>> expression)
        {
            _valueBind = GetBindName(expression);
            return this;
        }

        public NuiSliderBuilder<TViewModel> Min(Expression<Func<TViewModel, int>> expression)
        {
            _minBind = GetBindName(expression);
            return this;
        }

        public NuiSliderBuilder<TViewModel> Max(Expression<Func<TViewModel, int>> expression)
        {
            _maxBind = GetBindName(expression);
            return this;
        }

        public NuiSliderBuilder<TViewModel> Step(Expression<Func<TViewModel, int>> expression)
        {
            _stepBind = GetBindName(expression);
            return this;
        }

        public override Json BuildEntity()
        {
            var value = string.IsNullOrWhiteSpace(_valueBind)
                ? JsonInt(_value)
                : Nui.Bind(_valueBind);

            var min = string.IsNullOrWhiteSpace(_minBind)
                ? JsonInt(_min)
                : Nui.Bind(_minBind);

            var max = string.IsNullOrWhiteSpace(_maxBind)
                ? JsonInt(_max)
                : Nui.Bind(_maxBind);

            var step = string.IsNullOrWhiteSpace(_stepBind)
                ? JsonInt(_step)
                : Nui.Bind(_stepBind);

            return Nui.Slider(value, min, max, step);
        }
    }
}
