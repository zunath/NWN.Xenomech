using Anvil.Services;
using XM.Inventory;

namespace XM.Quest.Reward
{
    internal class ItemReward : IQuestReward
    {
        public bool IsSelectable { get; set;  }
        public string MenuName => GetName();
        public string Resref { get; set; }
        public int Quantity { get; set; }


        private readonly ItemCacheService _itemCache;

        public ItemReward(ItemCacheService itemCache)
        {
            _itemCache = itemCache;
        }

        private string GetName()
        {
            var itemName = _itemCache.GetItemNameByResref(Resref);

            if (Quantity > 1)
                return Quantity + "x " + itemName;
            else
                return itemName;
        }

        public void GiveReward(uint player)
        {
            CreateItemOnObject(Resref, player, Quantity);
        }
    }

}
