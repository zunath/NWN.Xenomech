using Anvil.API;
using System.Linq.Expressions;
using System;

namespace XM.UI.Builder.DrawList
{
    public class NuiDrawListCurveBuilder<TViewModel> : NuiDrawListItemBuilderBase<NuiDrawListCurveBuilder<TViewModel>, NuiDrawListCurve, TViewModel>
        where TViewModel: IViewModel
    {
        public NuiDrawListCurveBuilder(
            Color color,
            float lineThickness,
            NuiVector pointA,
            NuiVector pointB,
            NuiVector control0,
            NuiVector control1,
            NuiEventCollection eventCollection)
            : base(new NuiDrawListCurve(color, lineThickness, pointA, pointB, control0, control1), eventCollection)
        {
        }

        public NuiDrawListCurveBuilder<TViewModel> Control0(NuiVector control0)
        {
            Element.Control0 = control0;
            return this;
        }

        public NuiDrawListCurveBuilder<TViewModel> Control1(NuiVector control1)
        {
            Element.Control1 = control1;
            return this;
        }

        public NuiDrawListCurveBuilder<TViewModel> PointA(NuiVector pointA)
        {
            Element.PointA = pointA;
            return this;
        }

        public NuiDrawListCurveBuilder<TViewModel> PointB(NuiVector pointB)
        {
            Element.PointB = pointB;
            return this;
        }
        public NuiDrawListCurveBuilder<TViewModel> Control0(Expression<Func<TViewModel, NuiVector>> expression)
        {
            var bindName = GetBindName(expression);
            var bind = new NuiBind<NuiVector>(bindName);
            Element.Control0 = bind;

            return this;
        }

        public NuiDrawListCurveBuilder<TViewModel> Control1(Expression<Func<TViewModel, NuiVector>> expression)
        {
            var bindName = GetBindName(expression);
            var bind = new NuiBind<NuiVector>(bindName);
            Element.Control1 = bind;

            return this;
        }

        public NuiDrawListCurveBuilder<TViewModel> PointA(Expression<Func<TViewModel, NuiVector>> expression)
        {
            var bindName = GetBindName(expression);
            var bind = new NuiBind<NuiVector>(bindName);
            Element.PointA = bind;

            return this;
        }

        public NuiDrawListCurveBuilder<TViewModel> PointB(Expression<Func<TViewModel, NuiVector>> expression)
        {
            var bindName = GetBindName(expression);
            var bind = new NuiBind<NuiVector>(bindName);
            Element.PointB = bind;

            return this;
        }

    }

}
