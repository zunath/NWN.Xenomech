using Anvil.API;
using System.Linq.Expressions;
using System;

namespace XM.UI.Builder.Component
{
    public class NuiSliderBuilder<TViewModel> : NuiBuilderBase<NuiSliderBuilder<TViewModel>, NuiSlider, TViewModel>
        where TViewModel: IViewModel
    {
        public NuiSliderBuilder()
            : base(new NuiSlider(0, 0,0))
        {
        }

        public NuiSliderBuilder<TViewModel> SetValue(int value)
        {
            Element.Value = value;
            return this;
        }

        public NuiSliderBuilder<TViewModel> SetMin(int min)
        {
            Element.Min = min;
            return this;
        }

        public NuiSliderBuilder<TViewModel> SetMax(int max)
        {
            Element.Max = max;
            return this;
        }

        public NuiSliderBuilder<TViewModel> SetStep(int step)
        {
            Element.Step = step;
            return this;
        }
        public NuiSliderBuilder<TViewModel> BindValue(Expression<Func<TViewModel, int>> expression)
        {
            var bindName = GetBindName(expression);
            var bind = new NuiBind<int>(bindName);
            Element.Value = bind;

            return this;
        }

        public NuiSliderBuilder<TViewModel> BindMin(Expression<Func<TViewModel, int>> expression)
        {
            var bindName = GetBindName(expression);
            var bind = new NuiBind<int>(bindName);
            Element.Min = bind;

            return this;
        }

        public NuiSliderBuilder<TViewModel> BindMax(Expression<Func<TViewModel, int>> expression)
        {
            var bindName = GetBindName(expression);
            var bind = new NuiBind<int>(bindName);
            Element.Max = bind;

            return this;
        }

        public NuiSliderBuilder<TViewModel> BindStep(Expression<Func<TViewModel, int>> expression)
        {
            var bindName = GetBindName(expression);
            var bind = new NuiBind<int>(bindName);
            Element.Step = bind;

            return this;
        }

    }


}
