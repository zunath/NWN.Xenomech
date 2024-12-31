using Anvil.Services;
using XM.Inventory.KeyItem;
using XM.Localization;

namespace XM.Quest.Reward
{
    [ServiceBinding(typeof(KeyItemReward))]
    internal class KeyItemReward : IQuestReward
    {
        public bool IsSelectable { get; set; }

        private readonly KeyItemService _keyItem;

        public KeyItemReward(KeyItemService keyItem)
        {
            _keyItem = keyItem;
        }

        public string MenuName
        {
            get
            {
                var detail = _keyItem.GetKeyItem(KeyItemType);
                return Locale.GetString(detail.Name);
            }
        }

        public KeyItemType KeyItemType { get; set; }

        public void GiveReward(uint player)
        {
            _keyItem.GiveKeyItem(player, KeyItemType);
        }
    }

}
