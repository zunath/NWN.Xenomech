using Anvil.API;
using System.Linq.Expressions;
using System;

namespace XM.UI.Builder.Component
{
    public class NuiSliderBuilder<TViewModel> : NuiBuilderBase<NuiSliderBuilder<TViewModel>, NuiSlider, TViewModel>
        where TViewModel: IViewModel
    {
        public NuiSliderBuilder(NuiEventCollection eventCollection)
            : base(new NuiSlider(0, 0,0), eventCollection)
        {
        }

        public NuiSliderBuilder<TViewModel> Value(int value)
        {
            Element.Value = value;
            return this;
        }

        public NuiSliderBuilder<TViewModel> Min(int min)
        {
            Element.Min = min;
            return this;
        }

        public NuiSliderBuilder<TViewModel> Max(int max)
        {
            Element.Max = max;
            return this;
        }

        public NuiSliderBuilder<TViewModel> Step(int step)
        {
            Element.Step = step;
            return this;
        }
        public NuiSliderBuilder<TViewModel> Value(Expression<Func<TViewModel, int>> expression)
        {
            var bindName = GetBindName(expression);
            var bind = new NuiBind<int>(bindName);
            Element.Value = bind;

            return this;
        }

        public NuiSliderBuilder<TViewModel> Min(Expression<Func<TViewModel, int>> expression)
        {
            var bindName = GetBindName(expression);
            var bind = new NuiBind<int>(bindName);
            Element.Min = bind;

            return this;
        }

        public NuiSliderBuilder<TViewModel> Max(Expression<Func<TViewModel, int>> expression)
        {
            var bindName = GetBindName(expression);
            var bind = new NuiBind<int>(bindName);
            Element.Max = bind;

            return this;
        }

        public NuiSliderBuilder<TViewModel> Step(Expression<Func<TViewModel, int>> expression)
        {
            var bindName = GetBindName(expression);
            var bind = new NuiBind<int>(bindName);
            Element.Step = bind;

            return this;
        }

    }


}
