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
            float angleMax,
            NuiEventCollection eventCollection)
            : base(new NuiDrawListArc(color, fill, lineThickness, center, radius, angleMin, angleMax), eventCollection)
        {
        }

        public NuiDrawListArcBuilder<TViewModel> AngleMax(float angleMax)
        {
            Element.AngleMax = angleMax;
            return this;
        }

        public NuiDrawListArcBuilder<TViewModel> AngleMin(float angleMin)
        {
            Element.AngleMin = angleMin;
            return this;
        }

        public NuiDrawListArcBuilder<TViewModel> Center(NuiVector center)
        {
            Element.Center = center;
            return this;
        }

        public NuiDrawListArcBuilder<TViewModel> Radius(float radius)
        {
            Element.Radius = radius;
            return this;
        }
        public NuiDrawListArcBuilder<TViewModel> AngleMax(Expression<Func<TViewModel, float>> expression)
        {
            var bindName = GetBindName(expression);
            var bind = new NuiBind<float>(bindName);
            Element.AngleMax = bind;

            return this;
        }

        public NuiDrawListArcBuilder<TViewModel> AngleMin(Expression<Func<TViewModel, float>> expression)
        {
            var bindName = GetBindName(expression);
            var bind = new NuiBind<float>(bindName);
            Element.AngleMin = bind;

            return this;
        }

        public NuiDrawListArcBuilder<TViewModel> Center(Expression<Func<TViewModel, NuiVector>> expression)
        {
            var bindName = GetBindName(expression);
            var bind = new NuiBind<NuiVector>(bindName);
            Element.Center = bind;

            return this;
        }

        public NuiDrawListArcBuilder<TViewModel> Radius(Expression<Func<TViewModel, float>> expression)
        {
            var bindName = GetBindName(expression);
            var bind = new NuiBind<float>(bindName);
            Element.Radius = bind;

            return this;
        }

    }

}
