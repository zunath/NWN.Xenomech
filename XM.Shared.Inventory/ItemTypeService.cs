using Anvil.Services;
using XM.Shared.API.Constants;

namespace XM.Inventory
{
    [ServiceBinding(typeof(ItemTypeService))]
    public class ItemTypeService
    {
        public bool IsShield(uint item)
        {
            var type = GetBaseItemType(item);

            return type == BaseItemType.SmallShield ||
                   type == BaseItemType.LargeShield ||
                   type == BaseItemType.TowerShield;
        }
    }
}
