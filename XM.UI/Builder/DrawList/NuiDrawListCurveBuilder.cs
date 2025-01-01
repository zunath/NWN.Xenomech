using Anvil.API;

namespace XM.UI.Builder.DrawList
{
    public class NuiDrawListCurveBuilder : NuiDrawListItemBuilderBase<NuiDrawListCurveBuilder, NuiDrawListCurve>
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

        public NuiDrawListCurveBuilder WithControl0(NuiVector control0)
        {
            Element.Control0 = control0;
            return this;
        }

        public NuiDrawListCurveBuilder WithControl1(NuiVector control1)
        {
            Element.Control1 = control1;
            return this;
        }

        public NuiDrawListCurveBuilder WithPointA(NuiVector pointA)
        {
            Element.PointA = pointA;
            return this;
        }

        public NuiDrawListCurveBuilder WithPointB(NuiVector pointB)
        {
            Element.PointB = pointB;
            return this;
        }
    }

}
