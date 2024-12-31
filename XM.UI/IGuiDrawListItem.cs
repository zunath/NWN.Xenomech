using XM.API.BaseTypes;

namespace XM.UI
{
    internal interface IGuiDrawListItem
    {
        /// <summary>
        /// Builds the draw list item element.
        /// </summary>
        /// <returns>Json representing the draw list item element.</returns>
        Json ToJson();
    }
}
