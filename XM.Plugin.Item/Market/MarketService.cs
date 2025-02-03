using System;
using System.Collections.Generic;
using System.Linq;
using Anvil.Services;
using XM.Plugin.Item.Market.Dialog;
using XM.Plugin.Item.Market.Entity;
using XM.Plugin.Item.Market.UI.MarketBuyMenu;
using XM.Shared.API.Constants;
using XM.Shared.Core.Data;
using XM.Shared.Core.EventManagement;
using XM.Shared.Core.Extension;
using XM.Shared.Core.Localization;
using DialogService = XM.Shared.Core.Dialog.DialogService;

namespace XM.Plugin.Item.Market
{
    [ServiceBinding(typeof(MarketService))]
    internal class MarketService: IInitializable
    {
        public const int MaxListings = 100;
        public const float TaxRate = 0.09f;
        private Dictionary<MarketCategoryType, MarketCategoryAttribute> _activeMarketCategories = new();

        private readonly DBService _db;
        private readonly XMEventService _event;

        private readonly DialogService _dialog;

        public MarketService(
            DBService db,
            XMEventService @event,
            DialogService dialog)
        {
            _db = db;
            _event = @event;
            _dialog = dialog;

            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            _event.Subscribe<ModuleEvent.OnPlayerEnter>(CheckMarketTill);
        }

        private void CheckMarketTill(uint module)
        {
            var player = GetEnteringObject();

            if (!GetIsPC(player) || GetIsDM(player))
                return;

            var playerId = GetObjectUUID(player);
            var dbPlayer = _db.Get<PlayerMarket>(playerId);

            if (dbPlayer.MarketTill == 1)
            {
                var message = LocaleString.OneCreditIsInYourMarketTill.ToLocalizedString();
                SendMessageToPC(player, message);
            }
            else if (dbPlayer.MarketTill > 1)
            {
                var message = LocaleString.XCreditsAreInYourMarketTill.ToLocalizedString(dbPlayer.MarketTill);
                SendMessageToPC(player, message);
            }
        }

        private void RemoveOldListings()
        {
            var query = new DBQuery()
                .AddFieldSearch(nameof(MarketItem.IsListed), true);
            var count = _db.SearchCount<MarketItem>(query);
            var listings = _db.Search<MarketItem>(query
                .AddPaging(count, 0));
            var now = DateTime.UtcNow;

            foreach (var listing in listings)
            {
                if (listing.DateListed != null && Math.Abs(now.Subtract((DateTime)listing.DateListed).Days) >= 14)
                {
                    listing.IsListed = false;

                    _db.Set(listing);
                }
            }
        }

        public void Init()
        {
            LoadMarketCategories();
            RemoveOldListings();
        }

        [ScriptHandler("market_terminal")]
        public void UseMarketTerminal()
        {
            var player = GetLastUsedBy();
            var terminal = OBJECT_SELF;
            _dialog.StartConversation<MarketDialog>(player, terminal);
        }

        private void LoadMarketCategories()
        {
            var categories = Enum.GetValues(typeof(MarketCategoryType)).Cast<MarketCategoryType>();
            foreach (var category in categories)
            {
                var attribute = category.GetAttribute<MarketCategoryType, MarketCategoryAttribute>();

                if (attribute.IsActive)
                    _activeMarketCategories[category] = attribute;
            }

            _activeMarketCategories = _activeMarketCategories.OrderBy(o => o.Value.Name)
                .ToDictionary(x => x.Key, y => y.Value);
        }
        public Dictionary<MarketCategoryType, MarketCategoryAttribute> GetActiveCategories()
        {
            return _activeMarketCategories.ToDictionary(x => x.Key, y => y.Value);
        }

        public MarketCategoryType GetItemMarketCategory(uint item)
        {
            var baseItemType = GetBaseItemType(item);

            switch (baseItemType)
            {
                case BaseItemType.ShortSword:
                    return MarketCategoryType.ShortSword;
                case BaseItemType.LongSword:
                    return MarketCategoryType.Longsword;
                case BaseItemType.BattleAxe:
                    return MarketCategoryType.Axe;
                case BaseItemType.LongBow:
                    return MarketCategoryType.Bow;
                case BaseItemType.LightMace:
                    return MarketCategoryType.Club;
                case BaseItemType.Halberd:
                    return MarketCategoryType.Polearm;
                case BaseItemType.ShortBow:
                    return MarketCategoryType.Bow;
                case BaseItemType.GreatSword:
                    return MarketCategoryType.GreatSword;
                case BaseItemType.SmallShield:
                    return MarketCategoryType.Shield;
                case BaseItemType.Armor:
                    return MarketCategoryType.Body;
                case BaseItemType.Helmet:
                    return MarketCategoryType.Head;
                case BaseItemType.GreatAxe:
                    return MarketCategoryType.GreatAxe;
                case BaseItemType.Amulet:
                    return MarketCategoryType.Neck;
                case BaseItemType.Arrow:
                    return MarketCategoryType.Arrow;
                case BaseItemType.Belt:
                    return MarketCategoryType.Waist;
                case BaseItemType.Dagger:
                    return MarketCategoryType.Dagger;
                case BaseItemType.Bolt:
                    return MarketCategoryType.Bullet;
                case BaseItemType.Boots:
                    return MarketCategoryType.Feet;
                case BaseItemType.Club:
                    return MarketCategoryType.Club;
                case BaseItemType.Dart:
                    return MarketCategoryType.Throwing;
                case BaseItemType.Gloves:
                    return MarketCategoryType.Arm;
                case BaseItemType.HandAxe:
                    return MarketCategoryType.Axe;
                case BaseItemType.Potions:
                    return MarketCategoryType.Potion;
                case BaseItemType.QuarterStaff:
                    return MarketCategoryType.Staff;
                case BaseItemType.Ring:
                    return MarketCategoryType.Finger;
                case BaseItemType.LargeShield:
                    return MarketCategoryType.Shield;
                case BaseItemType.TowerShield:
                    return MarketCategoryType.Shield;
                case BaseItemType.ShortSpear:
                    return MarketCategoryType.Polearm;
                case BaseItemType.Shuriken:
                    return MarketCategoryType.Throwing;
                case BaseItemType.Sling:
                    return MarketCategoryType.Throwing;
                case BaseItemType.ThrowingAxe:
                    return MarketCategoryType.Throwing;
                case BaseItemType.LargeBox:
                    return MarketCategoryType.Container;
                case BaseItemType.Bracer:
                    return MarketCategoryType.Arm;
                case BaseItemType.Cloak:
                    return MarketCategoryType.Back;
                case BaseItemType.DwarvenWarAxe:
                    return MarketCategoryType.GreatAxe;
                case BaseItemType.Claw:
                    return MarketCategoryType.HandToHand;
                case BaseItemType.Pistol:
                    return MarketCategoryType.Pistol;
                case BaseItemType.Rifle:
                    return MarketCategoryType.Rifle;
            }

            return MarketCategoryType.Miscellaneous;
        }
    }
}
