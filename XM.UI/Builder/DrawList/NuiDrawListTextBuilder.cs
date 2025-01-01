
using Anvil.API;

namespace XM.UI.Builder.DrawList
{
    public class NuiDrawListTextBuilder : NuiDrawListItemBuilderBase<NuiDrawListTextBuilder, NuiDrawListText>
    {
        public NuiDrawListTextBuilder(Color color, NuiRect rect, string text)
            : base(new NuiDrawListText(color, rect, text))
        {
        }

        public NuiDrawListTextBuilder WithRect(NuiRect rect)
        {
            Element.Rect = rect;
            return this;
        }

        public NuiDrawListTextBuilder WithText(string text)
        {
            Element.Text = text;
            return this;
        }

        public NuiDrawListTextBuilder WithColor(Color color)
        {
            Element.Color = color;
            return this;
        }
    }

}
