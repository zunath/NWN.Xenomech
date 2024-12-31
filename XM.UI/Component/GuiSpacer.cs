
using XM.API.BaseTypes;
using XM.API.NUI;

namespace XM.UI.Component
{
    public class GuiSpacer<T> : GuiWidget<T, GuiSpacer<T>>
        where T: IGuiViewModel
    {
        /// <summary>
        /// Builds the Spacer element.
        /// </summary>
        /// <returns>Json representing the spacer element.</returns>
        public override Json BuildElement()
        {
            return Nui.Spacer();
        }
    }
}
