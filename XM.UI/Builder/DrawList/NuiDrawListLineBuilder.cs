using Anvil.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XM.UI.Builder.DrawList
{
    public class NuiDrawListLineBuilder : NuiDrawListItemBuilderBase<NuiDrawListLineBuilder, NuiDrawListLine>
    {
        public NuiDrawListLineBuilder(Color color, bool fill, float lineThickness, NuiVector pointA, NuiVector pointB)
            : base(new NuiDrawListLine(color, fill, lineThickness, pointA, pointB))
        {
        }

        public NuiDrawListLineBuilder WithPointA(NuiVector pointA)
        {
            Element.PointA = pointA;
            return this;
        }

        public NuiDrawListLineBuilder WithPointB(NuiVector pointB)
        {
            Element.PointB = pointB;
            return this;
        }

        public NuiDrawListLineBuilder WithLineThickness(float lineThickness)
        {
            Element.LineThickness = lineThickness;
            return this;
        }

        public NuiDrawListLineBuilder WithFill(bool fill)
        {
            Element.Fill = fill;
            return this;
        }

        public NuiDrawListLineBuilder WithColor(Color color)
        {
            Element.Color = color;
            return this;
        }
    }

}
