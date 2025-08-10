using System;
using Anvil.Services;
using XM.Inventory;
using XM.Inventory.Durability;
using XM.Progression.Skill;
using XM.Shared.API.Constants;
using XM.Shared.Core;
using XM.Shared.Core.Localization;
using XM.UI;
using ObjectType = XM.Shared.API.Constants.ObjectType;

namespace XM.Plugin.Craft.UI
{
    internal class ItemRepairViewModel: ViewModel<ItemRepairViewModel>
    {
        // Skill-only scaling for 90% cap: 0.90% per level (90% at level 100)
        private const float SkillDiscountPerLevel = 0.009f; // 0.90% per level
        private const float MaxDiscount = 0.90f;             // 90% cap

        private const int PricePerRepairPoint = 1000;
        private const string BlankTexture = "Blank";
        private uint _item;

        [Inject]
        public SkillService Skill { get; set; }

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

        public int GetCraftSkillLevelForItem(uint player, uint item)
        {
            var craft = MapItemToRelevantCraft(item);
            if (craft == SkillType.Invalid)
                return 0;

            return Skill.GetCraftSkillLevel(player, craft);
        }

        private static SkillType MapItemToRelevantCraft(uint item)
        {
            var baseItemType = GetBaseItemType(item);

            switch (baseItemType)
            {
                // Armor, shields
                case BaseItemType.Armor:
                case BaseItemType.Helmet:
                case BaseItemType.Gloves:
                case BaseItemType.Boots:
                case BaseItemType.Bracer:
                case BaseItemType.Cloak:
                case BaseItemType.SmallShield:
                case BaseItemType.LargeShield:
                case BaseItemType.TowerShield:
                    return SkillType.Armorcraft;

                // Weapons (including firearms/whip)
                case BaseItemType.ShortSword:
                case BaseItemType.Longsword:
                case BaseItemType.BattleAxe:
                case BaseItemType.BastardSword:
                case BaseItemType.LightFlail:
                case BaseItemType.WarHammer:
                case BaseItemType.HeavyCrossbow:
                case BaseItemType.LightCrossbow:
                case BaseItemType.LongBow:
                case BaseItemType.LightMace:
                case BaseItemType.Halberd:
                case BaseItemType.ShortBow:
                case BaseItemType.TwoBladedSword:
                case BaseItemType.GreatSword:
                case BaseItemType.GreatAxe:
                case BaseItemType.Dagger:
                case BaseItemType.Club:
                case BaseItemType.DireMace:
                case BaseItemType.DoubleAxe:
                case BaseItemType.HeavyFlail:
                case BaseItemType.LightHammer:
                case BaseItemType.HandAxe:
                case BaseItemType.Kama:
                case BaseItemType.Katana:
                case BaseItemType.Kukri:
                case BaseItemType.MagicRod:
                case BaseItemType.MagicStaff:
                case BaseItemType.MagicWand:
                case BaseItemType.MorningStar:
                case BaseItemType.QuarterStaff:
                case BaseItemType.Rapier:
                case BaseItemType.Scimitar:
                case BaseItemType.Scythe:
                case BaseItemType.ShortSpear:
                case BaseItemType.Shuriken:
                case BaseItemType.Sickle:
                case BaseItemType.Sling:
                case BaseItemType.ThrowingAxe:
                case BaseItemType.Trident:
                case BaseItemType.Pistol:
                case BaseItemType.Rifle:
                case BaseItemType.Whip:
                    return SkillType.Weaponcraft;

                // Accessories/jewelry
                case BaseItemType.Amulet:
                case BaseItemType.Ring:
                case BaseItemType.Belt:
                    return SkillType.Fabrication;

                default:
                    return SkillType.Invalid;
            }
        }

        public int CalculateCost(uint player, uint item, int repairPoints, int baseRatePerPoint)
        {
            var level = Math.Clamp(GetCraftSkillLevelForItem(player, item), 0, 100);
            var skillDiscount = level * SkillDiscountPerLevel; // up to 90%
            var totalDiscount = Math.Clamp(skillDiscount, 0f, MaxDiscount);

            var rate = (int)MathF.Max(1, MathF.Round(baseRatePerPoint * (1 - totalDiscount))); // floor: 1 credit/point
            var total = checked(rate * repairPoints);
            return total;
        }

        public (int totalCost, float totalDiscountPercent) CalculateCostWithDiscount(uint player, uint item, int repairPoints, int baseRatePerPoint)
        {
            var level = Math.Clamp(GetCraftSkillLevelForItem(player, item), 0, 100);
            var skillDiscount = level * SkillDiscountPerLevel; // up to 90%
            var totalDiscount = Math.Clamp(skillDiscount, 0f, MaxDiscount);
            var rate = (int)MathF.Max(1, MathF.Round(baseRatePerPoint * (1 - totalDiscount)));
            var total = checked(rate * repairPoints);
            return (total, totalDiscount * 100f);
        }

        private int CalculateCost()
        {
            var pointsToRepair = SelectedRepairPoints;
            if (pointsToRepair < 0)
                pointsToRepair = 0;
            if (_item == OBJECT_INVALID || pointsToRepair == 0)
                return 0;

            var cost = CalculateCost(Player, _item, pointsToRepair, PricePerRepairPoint);

            return cost;
        }

        private void UpdatePrice()
        {
            var pointsToRepair = SelectedRepairPoints;
            if (pointsToRepair < 0) pointsToRepair = 0;
            if (_item == OBJECT_INVALID || pointsToRepair == 0)
            {
                Price = string.Empty;
            }
            else
            {
                var (cost, discountPercent) = CalculateCostWithDiscount(Player, _item, pointsToRepair, PricePerRepairPoint);
                if (discountPercent > 0.0f)
                {
                    // e.g., "Cost: 1234 cr (-25%)"
                    Price = $"{LocaleString.RepairCostXCredits.ToLocalizedString(cost)} (-{(int)discountPercent}%)";
                }
                else
                {
                    Price = LocaleString.RepairCostXCredits.ToLocalizedString(cost);
                }
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
