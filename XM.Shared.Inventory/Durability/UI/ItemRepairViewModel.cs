using System;
using Anvil.Services;
using XM.Shared.API.Constants;
using XM.Shared.Core;
using XM.Shared.Core.Localization;
using XM.UI;

namespace XM.Inventory.Durability.UI
{
    internal class ItemRepairViewModel: ViewModel<ItemRepairViewModel>
    {
        private uint _item;

        [Inject]
        public ItemTypeService ItemType { get; set; }

        public string Name
        {
            get => Get<string>();
            set => Set(value);
        }

        public string ItemIconResref
        {
            get => Get<string>();
            set => Set(value);
        }

        public string Durability
        {
            get => Get<string>();
            set => Set(value);
        }

        public string Price
        {
            get => Get<string>();
            set => Set(value);
        }

        public override void OnOpen()
        {
            
        }

        public override void OnClose()
        {
            
        }

        private int CalculateCost()
        {
            const int PricePerPoint = 1000;
            var durability = new ItemDurability(_item);
            var pointsToRepair = durability.MaxDurability - durability.CurrentDurability;
            var cost = PricePerPoint * pointsToRepair;

            return cost;
        }

        public Action OnSelectItem() => () =>
        {
            var message = LocaleString.SelectAnItemFromYourInventory.ToLocalizedString();
            Targeting.EnterTargetingMode(Player, ObjectType.Item, message, item =>
            {
                var durability = new ItemDurability(item);

                if (GetItemPossessor(item) != Player)
                {
                    message = LocaleString.TheItemMustBeInYourInventory.ToLocalizedString();
                    SendMessageToPC(Player, message);
                    return;
                }

                if (durability.CurrentDurability >= durability.MaxDurability)
                {
                    message = LocaleString.ThatItemDoesNotNeedRepairs.ToLocalizedString();
                    SendMessageToPC(Player, message);
                    return;
                }

                ItemIconResref = ItemType.GetIconResref(item);
                _item = item;
                Name = GetName(item);

                var cost = CalculateCost();
                Price = $"{cost}" + LocaleString.cr;
            });
        };

        public Action OnRepairItem() => () =>
        {
            var price = CalculateCost();
            var prompt = LocaleString.RepairConfirmation.ToLocalizedString(price);
            ShowModal(prompt, () =>
            {
                AssignCommand(Player, () => TakeGoldFromCreature(price, Player, true));
                var durability = new ItemDurability(_item);
                durability.CurrentDurability = durability.MaxDurability;
                durability.SaveProperties();

                _item = OBJECT_INVALID;
            });
        };
    }
}
