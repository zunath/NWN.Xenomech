using System;
using Anvil.Services;
using NRediSearch.Aggregation;
using XM.Shared.API.Constants;
using XM.Shared.Core;
using XM.Shared.Core.Localization;
using XM.UI;

namespace XM.Inventory.Durability.UI
{
    internal class ItemRepairViewModel: ViewModel<ItemRepairViewModel>
    {
        private const int PricePerRepairPoint = 1000;
        private const string BlankTexture = "Blank";
        private uint _item;

        [Inject]
        public ItemTypeService ItemType { get; set; }

        [Inject]
        public ItemDurabilityService ItemDurability { get; set; }

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

        public string Price
        {
            get => Get<string>();
            set => Set(value);
        }

        public string RepairButtonText
        {
            get => Get<string>();
            set => Set(value);
        }

        public bool IsRepairButtonEnabled
        {
            get => Get<bool>();
            set => Set(value);
        }

        public override void OnOpen()
        {
            ItemIconResref = BlankTexture;
            IsRepairButtonEnabled = false;
            Name = LocaleString.SelectAnItem.ToLocalizedString();
            RepairButtonText = LocaleString.Repair.ToLocalizedString();
        }

        public override void OnClose()
        {
            
        }

        private int CalculateCost()
        {
            var durability = new ItemDurability(_item);
            var pointsToRepair = durability.MaxDurability - durability.CurrentDurability;
            var cost = PricePerRepairPoint * pointsToRepair;

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
                Name = $"{GetName(item)} ({durability.CurrentDurability} / {durability.MaxDurability})";

                var cost = CalculateCost();
                Price = $"{cost}" + LocaleString.cr;

                RepairButtonText = LocaleString.RepairXCredits.ToLocalizedString(cost);
                IsRepairButtonEnabled = true;
                RepairButtonText = LocaleString.Repair.ToLocalizedString();
            });
        };

        public Action OnRepairItem() => () =>
        {
            var price = CalculateCost();
            var prompt = LocaleString.RepairConfirmation.ToLocalizedString(price);
            ShowModal(prompt, () =>
            {
                price = CalculateCost();

                if (GetGold(Player) < price)
                {
                    var message = LocaleString.InsufficientCredits.ToLocalizedString();
                    SendMessageToPC(Player, message);
                    return;
                }

                AssignCommand(Player, () => TakeGoldFromCreature(price, Player, true));

                var durability = new ItemDurability(_item);
                ItemDurability.RestoreDurability(Player, _item, durability.MaxDurability);

                _item = OBJECT_INVALID;
                IsRepairButtonEnabled = false;
                ItemIconResref = BlankTexture;
                Name = LocaleString.SelectAnItem.ToLocalizedString();
            });
        };
    }
}
