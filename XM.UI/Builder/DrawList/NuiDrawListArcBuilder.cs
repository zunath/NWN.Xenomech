using Anvil.API;

namespace XM.UI.Builder.DrawList
{
    public class NuiDrawListArcBuilder : NuiDrawListItemBuilderBase<NuiDrawListArcBuilder, NuiDrawListArc>
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

        public NuiDrawListArcBuilder SetAngleMax(float angleMax)
        {
            Element.AngleMax = angleMax;
            return this;
        }

        public NuiDrawListArcBuilder SetAngleMin(float angleMin)
        {
            Element.AngleMin = angleMin;
            return this;
        }

        public NuiDrawListArcBuilder SetCenter(NuiVector center)
        {
            Element.Center = center;
            return this;
        }

        public NuiDrawListArcBuilder SetRadius(float radius)
        {
            Element.Radius = radius;
            return this;
        }
    }

}
