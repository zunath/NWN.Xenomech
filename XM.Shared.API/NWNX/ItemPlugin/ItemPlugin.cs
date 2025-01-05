using XM.Shared.API.Constants;

namespace XM.Shared.API.NWNX.ItemPlugin
{
    public static class ItemPlugin
    {
        /// <summary>
        /// Sets the weight of the specified item.
        /// </summary>
        /// <param name="item">The item object.</param>
        /// <param name="weight">The weight to set, in tenths of a pound.</param>
        public static void SetWeight(uint item, int weight)
        {
            NWN.Core.NWNX.ItemPlugin.SetWeight(item, weight);
        }

        /// <summary>
        /// Sets the base gold piece value of the specified item.
        /// </summary>
        /// <param name="item">The item object.</param>
        /// <param name="value">The base gold piece value to set.</param>
        public static void SetBaseGoldPieceValue(uint item, int value)
        {
            NWN.Core.NWNX.ItemPlugin.SetBaseGoldPieceValue(item, value);
        }

        /// <summary>
        /// Sets the additional gold piece value of the specified item.
        /// </summary>
        /// <param name="item">The item object.</param>
        /// <param name="value">The additional gold piece value to set.</param>
        public static void SetAddGoldPieceValue(uint item, int value)
        {
            NWN.Core.NWNX.ItemPlugin.SetAddGoldPieceValue(item, value);
        }

        /// <summary>
        /// Gets the base gold piece value of the specified item.
        /// </summary>
        /// <param name="item">The item object.</param>
        /// <returns>The base gold piece value of the item.</returns>
        public static int GetBaseGoldPieceValue(uint item)
        {
            return NWN.Core.NWNX.ItemPlugin.GetBaseGoldPieceValue(item);
        }

        /// <summary>
        /// Gets the additional gold piece value of the specified item.
        /// </summary>
        /// <param name="item">The item object.</param>
        /// <returns>The additional gold piece value of the item.</returns>
        public static int GetAddGoldPieceValue(uint item)
        {
            return NWN.Core.NWNX.ItemPlugin.GetAddGoldPieceValue(item);
        }

        /// <summary>
        /// Sets the base item type of the specified item.
        /// </summary>
        /// <param name="item">The item object.</param>
        /// <param name="baseItemType">The new base item type to set.</param>
        public static void SetBaseItemType(uint item, BaseItemType baseItemType)
        {
            NWN.Core.NWNX.ItemPlugin.SetBaseItemType(item, (int)baseItemType);
        }

        /// <summary>
        /// Sets the appearance of the specified item.
        /// </summary>
        /// <param name="item">The item object.</param>
        /// <param name="type">The appearance type.</param>
        /// <param name="index">The appearance index.</param>
        /// <param name="value">The appearance value.</param>
        /// <param name="updateCreatureAppearance">True to update the creature's appearance if the item is equipped; otherwise, false.</param>
        public static void SetItemAppearance(uint item, ItemAppearanceType type, int index, int value, bool updateCreatureAppearance = false)
        {
            NWN.Core.NWNX.ItemPlugin.SetItemAppearance(item, (int)type, index, value, updateCreatureAppearance ? 1 : 0);
        }

        /// <summary>
        /// Gets a string representing the entire appearance of the specified item.
        /// </summary>
        /// <param name="item">The item object.</param>
        /// <returns>A string representing the item's appearance.</returns>
        public static string GetEntireItemAppearance(uint item)
        {
            return NWN.Core.NWNX.ItemPlugin.GetEntireItemAppearance(item);
        }

        /// <summary>
        /// Restores the appearance of the specified item using the given appearance string.
        /// </summary>
        /// <param name="item">The item object.</param>
        /// <param name="appearance">The appearance string.</param>
        public static void RestoreItemAppearance(uint item, string appearance)
        {
            NWN.Core.NWNX.ItemPlugin.RestoreItemAppearance(item, appearance);
        }

        /// <summary>
        /// Gets the base armor class of the specified item.
        /// </summary>
        /// <param name="item">The item object.</param>
        /// <returns>The base armor class of the item.</returns>
        public static int GetBaseArmorClass(uint item)
        {
            return NWN.Core.NWNX.ItemPlugin.GetBaseArmorClass(item);
        }

        /// <summary>
        /// Gets the minimum level required to equip the specified item.
        /// </summary>
        /// <param name="item">The item object.</param>
        /// <returns>The minimum level required to equip the item.</returns>
        public static int GetMinEquipLevel(uint item)
        {
            return NWN.Core.NWNX.ItemPlugin.GetMinEquipLevel(item);
        }

        /// <summary>
        /// Moves the specified item to the target object.
        /// </summary>
        /// <param name="item">The item object.</param>
        /// <param name="target">The target object.</param>
        /// <param name="hideFeedback">True to hide feedback messages; otherwise, false.</param>
        /// <returns>True if the item was successfully moved; otherwise, false.</returns>
        public static bool MoveTo(uint item, uint target, bool hideFeedback = false)
        {
            return NWN.Core.NWNX.ItemPlugin.MoveTo(item, target, hideFeedback ? 1 : 0) == 1;
        }

        /// <summary>
        /// Sets the modifier for the minimum level required to equip the specified item.
        /// </summary>
        /// <param name="item">The item object.</param>
        /// <param name="modifier">The modifier value.</param>
        /// <param name="persist">True to persist the modifier across server resets; otherwise, false.</param>
        public static void SetMinEquipLevelModifier(uint item, int modifier, bool persist = true)
        {
            NWN.Core.NWNX.ItemPlugin.SetMinEquipLevelModifier(item, modifier, persist ? 1 : 0);
        }

        /// <summary>
        /// Gets the modifier for the minimum level required to equip the specified item.
        /// </summary>
        /// <param name="item">The item object.</param>
        /// <returns>The modifier value.</returns>
        public static int GetMinEquipLevelModifier(uint item)
        {
            return NWN.Core.NWNX.ItemPlugin.GetMinEquipLevelModifier(item);
        }

        /// <summary>
        /// Sets the override for the minimum level required to equip the specified item.
        /// </summary>
        /// <param name="item">The item object.</param>
        /// <param name="overrideValue">The override value.</param>
        /// <param name="persist">True to persist the override across server resets; otherwise, false.</param>
        public static void SetMinEquipLevelOverride(uint item, int overrideValue, bool persist = true)
        {
            NWN.Core.NWNX.ItemPlugin.SetMinEquipLevelOverride(item, overrideValue, persist ? 1 : 0);
        }

        /// <summary>
        /// Gets the override for the minimum level required to equip the specified item.
        /// </summary>
        /// <param name="item">The item object.</param>
        /// <returns>The override value.</returns>
        public static int GetMinEquipLevelOverride(uint item)
        {
            return NWN.Core.NWNX.ItemPlugin.GetMinEquipLevelOverride(item);
        }

    }
}
