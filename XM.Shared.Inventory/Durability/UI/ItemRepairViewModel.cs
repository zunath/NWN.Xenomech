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

        public int SelectedRepairPoints
        {
            get => Get<int>();
            set
            {
                Set(value);
                UpdatePrice();
            }
        }

        public int MinRepairPoints
        {
            get => Get<int>();
            set => Set(value);
        }

        public int MaxRepairPoints
        {
            get => Get<int>();
            set => Set(value);
        }

        public override void OnOpen()
        {
            ItemIconResref = BlankTexture;
            IsRepairButtonEnabled = false;
            Name = LocaleString.SelectAnItem.ToLocalizedString();
            RepairButtonText = LocaleString.Repair.ToLocalizedString();
            Price = string.Empty;
            MinRepairPoints = 0;
            MaxRepairPoints = 0;
            SelectedRepairPoints = 0;

            WatchOnClient(model => model.SelectedRepairPoints);
        }

        public override void OnClose()
        {
            
        }

        private int CalculateCost()
        {
            var pointsToRepair = SelectedRepairPoints;
            if (pointsToRepair < 0)
                pointsToRepair = 0;
            var cost = PricePerRepairPoint * pointsToRepair;

            return cost;
        }

        private void UpdatePrice()
        {
            var cost = CalculateCost();
            if (cost <= 0)
            {
                Price = string.Empty;
            }
            else
            {
                Price = LocaleString.RepairCostXCredits.ToLocalizedString(cost);
            }
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

                MinRepairPoints = 1;
                MaxRepairPoints = durability.MaxDurability - durability.CurrentDurability;
                SelectedRepairPoints = MaxRepairPoints;

                UpdatePrice();

                IsRepairButtonEnabled = true;
                RepairButtonText = LocaleString.Repair.ToLocalizedString();
            });
        };

        public Action OnRepairItem() => () =>
        {
            var price = CalculateCost();
            var prompt = LocaleString.RepairConfirmationPartial.ToLocalizedString(SelectedRepairPoints, price);
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
                var maxPossible = durability.MaxDurability - durability.CurrentDurability;
                var toRepair = SelectedRepairPoints;
                if (toRepair > maxPossible) toRepair = maxPossible;
                if (toRepair < 0) toRepair = 0;
                if (toRepair == 0)
                {
                    var msg = LocaleString.NothingToRepair.ToLocalizedString();
                    SendMessageToPC(Player, msg);
                    return;
                }
                ItemDurability.RestoreDurability(Player, _item, toRepair);

                _item = OBJECT_INVALID;
                IsRepairButtonEnabled = false;
                ItemIconResref = BlankTexture;
                Name = LocaleString.SelectAnItem.ToLocalizedString();
                Price = string.Empty;
                MinRepairPoints = 0;
                MaxRepairPoints = 0;
                SelectedRepairPoints = 0;
            });
        };
    }
}
