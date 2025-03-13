using System;
using System.Collections.Generic;
using XM.Shared.API.Constants;

namespace XM.Progression.Stat
{
    public class ItemStatCollection: Dictionary<InventorySlotType, ItemStatGroup>
    {
        public ItemStatCollection()
        {
            foreach (var slot in Enum.GetValues<InventorySlotType>())
            {
                this[slot] = new ItemStatGroup();
            }
        }

        public int CalculateStat(StatType type)
        {
            var total = 0;
            foreach (var (_, item) in this)
            {
                total += (int)(item.Stats[type] * item.Condition);
            }

            return total;
        }

        public int CalculateResist(ResistType resistType)
        {
            var resist = 0;

            foreach (var (_, item) in this)
            {
                resist += (int)(item.Resists[resistType] * item.Condition);
            }

            return resist;
        }
    }
}
