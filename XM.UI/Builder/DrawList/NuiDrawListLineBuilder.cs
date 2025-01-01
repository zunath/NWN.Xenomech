using Anvil.API;

namespace XM.UI.Builder.DrawList
{
    public class NuiDrawListLineBuilder : NuiDrawListItemBuilderBase<NuiDrawListLineBuilder, NuiDrawListLine>
    {
        public NuiDrawListLineBuilder(Color color, bool fill, float lineThickness, NuiVector pointA, NuiVector pointB)
            : base(new NuiDrawListLine(color, fill, lineThickness, pointA, pointB))
        {
        }

        public NuiDrawListLineBuilder SetPointA(NuiVector pointA)
        {
            Element.PointA = pointA;
            return this;
        }

        public NuiDrawListLineBuilder SetPointB(NuiVector pointB)
        {
            Element.PointB = pointB;
            return this;
        }

        public NuiDrawListLineBuilder SetLineThickness(float lineThickness)
        {
            Element.LineThickness = lineThickness;
            return this;
        }

        public NuiDrawListLineBuilder SetFill(bool fill)
        {
            Element.Fill = fill;
            return this;
        }

        public NuiDrawListLineBuilder SetColor(Color color)
        {
            Element.Color = color;
            return this;
        }
    }

}
