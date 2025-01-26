using System;
using Anvil.Services;
using XM.Shared.API.Constants;
using XM.Shared.Core.EventManagement;

namespace XM.Inventory
{
    [ServiceBinding(typeof(ItemPropertyService))]
    internal class ItemPropertyService
    {
        public ItemPropertyService(
            XMEventService @event)
        {
            @event.Subscribe<ModuleEvent.OnEquipItem>(AddOnHitCastSpellItemProperty);
        }

        private void AddOnHitCastSpellItemProperty(uint module)
        {
            var item = GetPCItemLastEquipped();
            var type = GetBaseItemType(item);
            var typeId = (int)type;
            var weaponType = Get2DAString("baseitems", "WeaponType", typeId);

            if (string.IsNullOrWhiteSpace(weaponType))
                return;

            var weaponTypeId = Convert.ToInt32(weaponType);
            if (weaponTypeId <= 0 && 
                type != BaseItemType.Arrow &&
                type != BaseItemType.Bolt &&
                type != BaseItemType.Bullet)
                return;

            for (var ip = GetFirstItemProperty(item); GetIsItemPropertyValid(ip); ip = GetNextItemProperty(item))
            {
                if (GetItemPropertyType(ip) == ItemPropertyType.OnHitCastSpell)
                {
                    if (GetItemPropertySubType(ip) == (int)IPConstOnHitCastSpellType.OnHitUniquePower)
                    {
                        return;
                    }
                }
            }

            var newIP = ItemPropertyOnHitCastSpell(IPConstOnHitCastSpellType.OnHitUniquePower, 40);
            BiowareXP2.IPSafeAddItemProperty(item, newIP, 0f, AddItemPropertyPolicy.ReplaceExisting, false, false);
        }
    }
}
