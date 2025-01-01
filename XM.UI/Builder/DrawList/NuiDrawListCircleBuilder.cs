using Anvil.API;

namespace XM.UI.Builder.DrawList
{
    public class NuiDrawListCircleBuilder : NuiDrawListItemBuilderBase<NuiDrawListCircleBuilder, NuiDrawListCircle>
    {
        public NuiDrawListCircleBuilder(
            Color color,
            bool fill,
            float lineThickness,
            NuiRect rect)
            : base(new NuiDrawListCircle(color, fill, lineThickness, rect))
        {
        }

        public NuiDrawListCircleBuilder WithRect(NuiRect rect)
        {
            Element.Rect = rect;
            return this;
        }
    }

}
