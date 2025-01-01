
using Anvil.API;

namespace XM.UI.Builder.DrawList
{
    public class NuiDrawListTextBuilder : NuiDrawListItemBuilderBase<NuiDrawListTextBuilder, NuiDrawListText>
    {
        public NuiDrawListTextBuilder(Color color, NuiRect rect, string text)
            : base(new NuiDrawListText(color, rect, text))
        {
        }

        public NuiDrawListTextBuilder SetRect(NuiRect rect)
        {
            Element.Rect = rect;
            return this;
        }

        public NuiDrawListTextBuilder SetText(string text)
        {
            Element.Text = text;
            return this;
        }

        public NuiDrawListTextBuilder SetColor(Color color)
        {
            Element.Color = color;
            return this;
        }
    }

}
