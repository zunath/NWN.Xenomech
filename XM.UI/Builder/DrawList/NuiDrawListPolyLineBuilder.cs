using Anvil.API;
using System.Collections.Generic;

namespace XM.UI.Builder.DrawList
{
    public class NuiDrawListPolyLineBuilder : NuiDrawListItemBuilderBase<NuiDrawListPolyLineBuilder, NuiDrawListPolyLine>
    {
        public NuiDrawListPolyLineBuilder(Color color, bool fill, float lineThickness, List<float> points)
            : base(new NuiDrawListPolyLine(color, fill, lineThickness, points))
        {
        }

        public NuiDrawListPolyLineBuilder WithPoints(List<float> points)
        {
            Element.Points = points;
            return this;
        }

        public NuiDrawListPolyLineBuilder WithLineThickness(float lineThickness)
        {
            Element.LineThickness = lineThickness;
            return this;
        }

        public NuiDrawListPolyLineBuilder WithFill(bool fill)
        {
            Element.Fill = fill;
            return this;
        }

        public NuiDrawListPolyLineBuilder WithColor(Color color)
        {
            Element.Color = color;
            return this;
        }
    }

}
