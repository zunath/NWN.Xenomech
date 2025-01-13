using Anvil.Services;
using System;
using XM.Shared.API.Constants;

namespace XM.Inventory
{
    [ServiceBinding(typeof(InventoryService))]
    public class InventoryService
    {
        /// <summary>
        /// Returns an item to a target.
        /// </summary>
        /// <param name="target">The target receiving the item.</param>
        /// <param name="item">The item being returned.</param>
        public void ReturnItem(uint target, uint item)
        {
            if (GetHasInventory(item))
            {
                var possessor = GetItemPossessor(item);
                AssignCommand(possessor, () =>
                {
                    ActionGiveItem(item, target);
                });
            }
            else
            {
                CopyItem(item, target, true);
                DestroyObject(item);
            }
        }


        /// <summary>
        /// Reduces an item stack by a specific amount.
        /// If there are not enough items in the stack to reduce, false will be returned.
        /// If the stack size of the item will reach 0, the item is destroyed and true will be returned.
        /// If the stack size will reach a number greater than 0, the item's stack size will be updated and true will be returned.
        /// </summary>
        /// <param name="item">The item to adjust</param>
        /// <param name="reduceBy">The amount to reduce by. Absolute value is used to determine this value.</param>
        /// <returns>true if successfully reduced or destroyed, false otherwise</returns>
        public bool ReduceItemStack(uint item, int reduceBy)
        {
            var amount = Math.Abs(reduceBy);
            var stackSize = GetItemStackSize(item);

            // Have to reduce by at least one.
            if (amount <= 0)
                return false;

            // Stack size cannot be smaller than the amount we're reducing by.
            if (stackSize < reduceBy)
                return false;

            var remaining = stackSize - reduceBy;
            if (remaining <= 0)
            {
                DestroyObject(item);
                return true;
            }
            else
            {
                SetItemStackSize(item, remaining);
                return true;
            }
        }

        public void UnequipAllItems(uint creature)
        {
            for (var slot = 0; slot < GeneralConstants.NumberOfInventorySlots; slot++)
            {
                var inventory = (InventorySlotType)slot;
                if (inventory == InventorySlotType.CreatureArmor ||
                    inventory == InventorySlotType.CreatureWeaponBite ||
                    inventory == InventorySlotType.CreatureWeaponRight ||
                    inventory == InventorySlotType.CreatureWeaponLeft)
                    continue;

                var item = GetItemInSlot(inventory, creature);
                AssignCommand(creature, () => ActionUnequipItem(item));
            }
        }

        public int GetDMG(uint item)
        {
            var dmg = 0;
            for (var ip = GetFirstItemProperty(item); GetIsItemPropertyValid(ip); ip = GetNextItemProperty(item))
            {
                if (GetItemPropertyType(ip) == ItemPropertyType.DMG)
                {
                    dmg += GetItemPropertyCostTableValue(ip);
                }
            }

            if (dmg < 1)
                dmg = 1;

            return dmg;
        }
    }
}
