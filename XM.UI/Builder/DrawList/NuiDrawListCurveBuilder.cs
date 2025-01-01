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
            NuiVector control1)
            : base(new NuiDrawListCurve(color, lineThickness, pointA, pointB, control0, control1))
        {
        }

        public NuiDrawListCurveBuilder<TViewModel> SetControl0(NuiVector control0)
        {
            Element.Control0 = control0;
            return this;
        }

        public NuiDrawListCurveBuilder<TViewModel> SetControl1(NuiVector control1)
        {
            Element.Control1 = control1;
            return this;
        }

        public NuiDrawListCurveBuilder<TViewModel> SetPointA(NuiVector pointA)
        {
            Element.PointA = pointA;
            return this;
        }

        public NuiDrawListCurveBuilder<TViewModel> SetPointB(NuiVector pointB)
        {
            Element.PointB = pointB;
            return this;
        }
        public NuiDrawListCurveBuilder<TViewModel> BindControl0(Expression<Func<TViewModel, NuiVector>> expression)
        {
            var bindName = GetBindName(expression);
            var bind = new NuiBind<NuiVector>(bindName);
            Element.Control0 = bind;

            return this;
        }

        public NuiDrawListCurveBuilder<TViewModel> BindControl1(Expression<Func<TViewModel, NuiVector>> expression)
        {
            var bindName = GetBindName(expression);
            var bind = new NuiBind<NuiVector>(bindName);
            Element.Control1 = bind;

            return this;
        }

        public NuiDrawListCurveBuilder<TViewModel> BindPointA(Expression<Func<TViewModel, NuiVector>> expression)
        {
            var bindName = GetBindName(expression);
            var bind = new NuiBind<NuiVector>(bindName);
            Element.PointA = bind;

            return this;
        }

        public NuiDrawListCurveBuilder<TViewModel> BindPointB(Expression<Func<TViewModel, NuiVector>> expression)
        {
            var bindName = GetBindName(expression);
            var bind = new NuiBind<NuiVector>(bindName);
            Element.PointB = bind;

            return this;
        }

    }

}
