using System;
using System.Text;
using Anvil.API;
using Anvil.Services;
using XM.Shared.API.Constants;
using XM.Shared.Core.EventManagement;
using BaseItemType = XM.Shared.API.Constants.BaseItemType;
using ItemPropertyType = XM.Shared.API.Constants.ItemPropertyType;

namespace XM.Inventory
{
    [ServiceBinding(typeof(ItemPropertyService))]
    public class ItemPropertyService
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

        public string BuildItemPropertyString(uint item)
        {
            var sb = new StringBuilder();

            for (var ip = GetFirstItemProperty(item); GetIsItemPropertyValid(ip); ip = GetNextItemProperty(item))
            {
                BuildSingleItemPropertyString(sb, ip);
                sb.Append("\n");
            }

            return sb.ToString();
        }

        private void BuildSingleItemPropertyString(StringBuilder sb, ItemProperty ip)
        {
            var typeId = (int)GetItemPropertyType(ip);
            var gameStringRef = Get2DAString("itempropdef", "GameStrRef", typeId);
            if (string.IsNullOrWhiteSpace(gameStringRef))
                return;

            var name = GetStringByStrRef(Convert.ToInt32(gameStringRef));
            sb.Append(name);

            var subTypeId = GetItemPropertySubType(ip);
            if (subTypeId != -1)
            {
                var subTypeResref = Get2DAString("itempropdef", "SubTypeResRef", typeId);
                var strRefId = StringToInt(Get2DAString(subTypeResref, "Name", subTypeId));
                if (strRefId != 0)
                {
                    var text = $" {GetStringByStrRef(strRefId)}";
                    sb.Append(text);
                }
            }

            var param1 = GetItemPropertyParam1(ip);
            if (param1 != -1)
            {
                var paramResref = Get2DAString("iprp_paramtable", "TableResRef", param1);
                var strRef = StringToInt(Get2DAString(paramResref, "Name", GetItemPropertyParam1Value(ip)));
                if (strRef != 0)
                {
                    var text = $" {GetStringByStrRef(strRef)}";
                    sb.Append(text);
                }
            }

            var costTable = GetItemPropertyCostTable(ip);
            if (costTable != -1)
            {
                var costTableResref = Get2DAString("iprp_costtable", "Name", costTable);
                var strRef = StringToInt(Get2DAString(costTableResref, "Name", GetItemPropertyCostTableValue(ip)));
                if (strRef != 0)
                {
                    var text = $" {GetStringByStrRef(strRef)}";
                    sb.Append(text);
                }
            }
        }
    }
}
