using NWN.Core.NWNX;

namespace NWN.Xenomech.API.NWNX.ItemPropertyPlugin
{
    public static class ItemPropertyPlugin
    {
        /// <summary>
        /// Unpacks a native item property into a structured representation.
        /// </summary>
        /// <param name="itemProperty">The native item property pointer to unpack.</param>
        /// <returns>A structured representation of the item property.</returns>
        public static NWNX_IPUnpacked UnpackItemProperty(BaseTypes.ItemProperty itemProperty)
        {
            return ItempropPlugin.UnpackIP(itemProperty);
        }

        /// <summary>
        /// Packs a structured representation into a native item property.
        /// </summary>
        /// <param name="unpackedProperty">The structured item property to pack.</param>
        /// <returns>The native item property pointer.</returns>
        public static IntPtr PackItemProperty(NWNX_IPUnpacked unpackedProperty)
        {
            return ItempropPlugin.PackIP(unpackedProperty);
        }

        /// <summary>
        /// Gets the active item property at the specified index on an item.
        /// </summary>
        /// <param name="item">The item object with the property.</param>
        /// <param name="index">The index of the item property.</param>
        /// <returns>A structured representation of the active item property.</returns>
        public static NWNX_IPUnpacked GetActiveItemProperty(uint item, int index)
        {
            return ItempropPlugin.GetActiveProperty(item, index);
        }
    }
}
