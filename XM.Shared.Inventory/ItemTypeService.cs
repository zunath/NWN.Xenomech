using Anvil.Services;
using XM.Shared.API.Constants;

namespace XM.Inventory
{
    [ServiceBinding(typeof(ItemTypeService))]
    public class ItemTypeService
    {
        public string GetIconResref(uint item)
        {
            var baseItem = GetBaseItemType(item);

            if (baseItem == BaseItemType.Cloak) // Cloaks use PLTs so their default icon doesn't really work
                return "iit_cloak";
            else if (baseItem == BaseItemType.SpellScroll || baseItem == BaseItemType.EnchantedScroll)
            {// Scrolls get their icon from the cast spell property
                if (GetItemHasItemProperty(item, ItemPropertyType.CastSpell))
                {
                    for (var ip = GetFirstItemProperty(item); GetIsItemPropertyValid(ip); ip = GetNextItemProperty(item))
                    {
                        if (GetItemPropertyType(ip) == ItemPropertyType.CastSpell)
                            return Get2DAString("iprp_spells", "Icon", GetItemPropertySubType(ip));
                    }
                }
            }
            else if (Get2DAString("baseitems", "ModelType", (int)baseItem) == "0")
            {// Create the icon resref for simple modeltype items
                var sSimpleModelId = GetItemAppearance(item, ItemAppearanceType.SimpleModel, 0).ToString();
                while (GetStringLength(sSimpleModelId) < 3)
                {
                    sSimpleModelId = "0" + sSimpleModelId;
                }

                var sDefaultIcon = Get2DAString("baseitems", "DefaultIcon", (int)baseItem);
                switch (baseItem)
                {
                    case BaseItemType.MiscSmall:
                    case BaseItemType.CraftMaterialSmall:
                        sDefaultIcon = "iit_smlmisc_" + sSimpleModelId;
                        break;
                    case BaseItemType.MiscMedium:
                    case BaseItemType.CraftMaterialMedium:
                    case BaseItemType.CraftBase:
                        sDefaultIcon = "iit_midmisc_" + sSimpleModelId;
                        break;
                    case BaseItemType.MiscLarge:
                        sDefaultIcon = "iit_talmisc_" + sSimpleModelId;
                        break;
                    case BaseItemType.MiscThin:
                        sDefaultIcon = "iit_thnmisc_" + sSimpleModelId;
                        break;
                }

                var nLength = GetStringLength(sDefaultIcon);
                if (GetSubString(sDefaultIcon, nLength - 4, 1) == "_")// Some items have a default icon of xx_yyy_001, we strip the last 4 symbols if that is the case
                    sDefaultIcon = GetStringLeft(sDefaultIcon, nLength - 4);
                var sIcon = sDefaultIcon + "_" + sSimpleModelId;
                if (ResManGetAliasFor(sIcon, ResourceType.Tga) != "")// Check if the icon actually exists, if not, we'll fall through and return the default icon
                    return sIcon;
            }

            // For everything else use the item's default icon
            return Get2DAString("baseitems", "DefaultIcon", (int)baseItem);
        }
        public bool IsShield(uint item)
        {
            var type = GetBaseItemType(item);

            return type == BaseItemType.SmallShield ||
                   type == BaseItemType.LargeShield ||
                   type == BaseItemType.TowerShield;
        }
    }
}
