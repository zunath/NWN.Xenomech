using Anvil.API;
using System.Linq.Expressions;
using System;

namespace XM.UI.Builder.DrawList
{
    public class NuiDrawListArcBuilder<TViewModel> : NuiDrawListItemBuilderBase<NuiDrawListArcBuilder<TViewModel>, NuiDrawListArc, TViewModel>
        where TViewModel: IViewModel
    {
        public NuiDrawListArcBuilder(
            Color color,
            bool fill,
            float lineThickness,
            NuiVector center,
            float radius,
            float angleMin,
            float angleMax)
            : base(new NuiDrawListArc(color, fill, lineThickness, center, radius, angleMin, angleMax))
        {
        }

        public NuiDrawListArcBuilder<TViewModel> SetAngleMax(float angleMax)
        {
            Element.AngleMax = angleMax;
            return this;
        }

        public NuiDrawListArcBuilder<TViewModel> SetAngleMin(float angleMin)
        {
            Element.AngleMin = angleMin;
            return this;
        }

        public NuiDrawListArcBuilder<TViewModel> SetCenter(NuiVector center)
        {
            Element.Center = center;
            return this;
        }

        public NuiDrawListArcBuilder<TViewModel> SetRadius(float radius)
        {
            Element.Radius = radius;
            return this;
        }
        public NuiDrawListArcBuilder<TViewModel> BindAngleMax(Expression<Func<TViewModel, float>> expression)
        {
            var bindName = GetBindName(expression);
            var bind = new NuiBind<float>(bindName);
            Element.AngleMax = bind;

            return this;
        }

        public NuiDrawListArcBuilder<TViewModel> BindAngleMin(Expression<Func<TViewModel, float>> expression)
        {
            var bindName = GetBindName(expression);
            var bind = new NuiBind<float>(bindName);
            Element.AngleMin = bind;

            return this;
        }

        public NuiDrawListArcBuilder<TViewModel> BindCenter(Expression<Func<TViewModel, NuiVector>> expression)
        {
            var bindName = GetBindName(expression);
            var bind = new NuiBind<NuiVector>(bindName);
            Element.Center = bind;

            return this;
        }

        public NuiDrawListArcBuilder<TViewModel> BindRadius(Expression<Func<TViewModel, float>> expression)
        {
            var bindName = GetBindName(expression);
            var bind = new NuiBind<float>(bindName);
            Element.Radius = bind;

            return this;
        }

    }

}
