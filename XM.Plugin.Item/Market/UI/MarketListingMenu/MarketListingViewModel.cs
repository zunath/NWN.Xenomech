using System;
using System.Collections.Generic;
using Anvil.Services;
using XM.Inventory;
using XM.Plugin.Item.Market.Entity;
using XM.Plugin.Item.Market.Event;
using XM.Plugin.Item.Market.UI.PriceSelection;
using XM.Shared.API.Constants;
using XM.Shared.API.NWNX.ObjectPlugin;
using XM.Shared.Core;
using XM.Shared.Core.Data;
using XM.Shared.Core.Localization;
using XM.UI;

namespace XM.Plugin.Item.Market.UI.MarketListingMenu
{
    internal class MarketListingViewModel: ViewModel<MarketListingViewModel>
    {
        [Inject]
        public DBService DB { get; set; }

        [Inject]
        public MarketService Market { get; set; }

        [Inject]
        public ItemTypeService ItemType { get; set; }

        [Inject]
        public GuiService Gui { get; set; }

        public string SearchText
        {
            get => Get<string>();
            set => Set(value);
        }

        public bool IsAddItemEnabled
        {
            get => Get<bool>();
            set => Set(value);
        }

        public string ListCount
        {
            get => Get<string>();
            set => Set(value);
        }

        public string WindowTitle
        {
            get => Get<string>();
            set => Set(value);
        }

        private int _itemCount;

        private readonly List<string> _itemIds = new();

        public XMBindingList<string> ItemIconResrefs
        {
            get => Get<XMBindingList<string>>();
            set => Set(value);
        }

        public XMBindingList<string> ItemNames
        {
            get => Get<XMBindingList<string>>();
            set => Set(value);
        }

        private readonly List<int> _itemPrices = new();
        public XMBindingList<string> ItemPriceNames
        {
            get => Get<XMBindingList<string>>();
            set => Set(value);
        }

        public XMBindingList<bool> ItemListed
        {
            get => Get<XMBindingList<bool>>();
            set => Set(value);
        }

        public XMBindingList<bool> ListingCheckboxEnabled
        {
            get => Get<XMBindingList<bool>>();
            set => Set(value);
        }

        public string ShopTill
        {
            get => Get<string>();
            set => Set(value);
        }

        public bool IsShopTillEnabled
        {
            get => Get<bool>();
            set => Set(value);
        }

        private void LoadData()
        {
            var itemIconResrefs = new XMBindingList<string>();
            var itemNames = new XMBindingList<string>();
            var itemPriceNames = new XMBindingList<string>();
            var itemListed = new XMBindingList<bool>();
            var listingCheckboxEnabled = new XMBindingList<bool>();

            _itemIds.Clear();
            _itemPrices.Clear();
            var playerId = GetObjectUUID(Player);
            var query = new DBQuery()
                .AddFieldSearch(nameof(MarketItem.PlayerId), playerId, false)
                .OrderBy(nameof(MarketItem.Name));
            var records = DB.Search<MarketItem>(query);
            var count = 0;

            foreach (var record in records)
            {
                count++;

                if (!string.IsNullOrWhiteSpace(SearchText) &&
                    !record.Name.ToLower().Contains(SearchText.ToLower()))
                    continue;

                _itemIds.Add(record.Id);
                itemIconResrefs.Add(record.IconResref);
                itemNames.Add($"{record.Quantity}x {record.Name}");
                _itemPrices.Add(record.Price);
                itemPriceNames.Add(LocaleString.XCr.ToLocalizedString(record.Price));
                itemListed.Add(record.IsListed && record.Price > 0);
                listingCheckboxEnabled.Add(record.Price > 0);
            }

            _itemCount = count;
            UpdateItemCount();

            ItemIconResrefs = itemIconResrefs;
            ItemNames = itemNames;
            ItemPriceNames = itemPriceNames;
            ItemListed = itemListed;
            ListingCheckboxEnabled = listingCheckboxEnabled;

            var dbPlayer = DB.Get<PlayerMarket>(playerId);
            ShopTill = LocaleString.TillXCr.ToLocalizedString(dbPlayer.MarketTill);
            IsShopTillEnabled = dbPlayer.MarketTill > 0;

        }

        private void UpdateItemCount()
        {
            ListCount = LocaleString.XOutOfYItemsListed.ToLocalizedString(_itemCount, MarketService.MaxListings);
            IsAddItemEnabled = _itemIds.Count < MarketService.MaxListings;
        }

        public Action OnClickAddItem => () =>
        {
            ClosePriceWindow();

            Targeting.EnterTargetingMode(Player, ObjectType.Item, LocaleString.PleaseClickOnAnItemWithinYourInventory.ToLocalizedString(), AddItem);
            EnterTargetingMode(Player, ObjectType.Item);
            SetLocalBool(Player, "MARKET_LISTING_TARGETING_MODE", true);
        };

        public void AddItem(uint item)
        {
            if (GetItemPossessor(item) != Player)
            {
                FloatingTextStringOnCreature(LocaleString.ItemMustBeInYourInventory.ToLocalizedString(), Player, false);
                return;
            }

            if (GetHasInventory(item))
            {
                FloatingTextStringOnCreature(LocaleString.ContainersCannotBeListed.ToLocalizedString(), Player, false);
                return;
            }

            if (_itemIds.Count >= MarketService.MaxListings)
            {
                FloatingTextStringOnCreature(LocaleString.YouCannotListAnyMoreItems.ToLocalizedString(), Player, false);
                return;
            }

            if (GetItemCursedFlag(item) ||
                GetPlotFlag(item))
            {
                FloatingTextStringOnCreature(LocaleString.ThisItemCannotBeSoldOnTheMarket.ToLocalizedString(), Player, false);
                return;
            }

            var listing = new MarketItem
            {
                Id = GetObjectUUID(item),
                PlayerId = GetObjectUUID(Player),
                SellerName = GetName(Player),
                Price = 0,
                IsListed = false,
                Name = GetName(item),
                Tag = GetTag(item),
                Resref = GetResRef(item),
                Data = ObjectPlugin.Serialize(item),
                Quantity = GetItemStackSize(item),
                IconResref = ItemType.GetIconResref(item),
                Category = Market.GetItemMarketCategory(item)
            };

            DB.Set(listing);
            DestroyObject(item);

            _itemIds.Add(listing.Id);
            ItemIconResrefs.Add(listing.IconResref);
            ItemNames.Add($"{listing.Quantity}x {listing.Name}");
            _itemPrices.Add(listing.Price);
            ItemPriceNames.Add(LocaleString.XCr.ToLocalizedString(listing.Price));
            ItemListed.Add(listing.IsListed && listing.Price > 0);
            ListingCheckboxEnabled.Add(listing.Price > 0);

            _itemCount++;
            UpdateItemCount();
        }

        public Action OnClickRemove => () =>
        {
            ClosePriceWindow();
            var index = NuiGetEventArrayIndex();
            var itemId = _itemIds[index];

            ShowModal(LocaleString.AreYouSureYouWantToRemoveThisItemListing.ToLocalizedString(), () =>
            {
                var dbListing = DB.Get<MarketItem>(itemId);

                // The item was either bought or removed already. 
                // Remove it from the client's view, but don't take any action on the server.
                if (dbListing != null)
                {
                    var deserialized = ObjectPlugin.Deserialize(dbListing.Data);
                    ObjectPlugin.AcquireItem(Player, deserialized);
                    DB.Delete<MarketItem>(itemId);
                }
                else
                {
                    FloatingTextStringOnCreature(LocaleString.YourListingForXHasBeenRemovedOrSoldAlready.ToLocalizedString(), Player, false);
                }

                _itemIds.RemoveAt(index);
                ItemIconResrefs.RemoveAt(index);
                ItemNames.RemoveAt(index);
                _itemPrices.RemoveAt(index);
                ItemPriceNames.RemoveAt(index);
                ItemListed.RemoveAt(index);
                ListingCheckboxEnabled.RemoveAt(index);

                _itemCount--;
                UpdateItemCount();
            });
        };

        public Action OnClickSearch => () =>
        {
            ClosePriceWindow();
            LoadData();
        };

        public Action OnClickClear => () =>
        {
            ClosePriceWindow();
            SearchText = string.Empty;
            LoadData();
        };

        public Action OnClickSaveChanges => () =>
        {
            ClosePriceWindow();

            for (var index = 0; index < _itemIds.Count; index++)
            {
                var id = _itemIds[index];
                var dbListing = DB.Get<MarketItem>(id);

                // It's possible the item was sold already, in which case there won't be a DB record.
                // Skip this update.
                if (dbListing == null)
                    continue;

                // Only do updates if either the price or listing status has changed.
                if (dbListing.Price == _itemPrices[index] &&
                    dbListing.IsListed == ItemListed[index])
                    continue;

                // Do the update for this record.
                dbListing.Price = _itemPrices[index];
                dbListing.IsListed = ItemListed[index];

                if (dbListing.IsListed)
                    dbListing.DateListed = DateTime.UtcNow;

                DB.Set(dbListing);
            }

            LoadData();
        };

        private void ClosePriceWindow()
        {
            Gui.CloseWindow<PriceSelectionView>(Player);
        }

        public Action OnClickChangePrice => () =>
        {
            // There is a defect with NUI which prevents text boxes from working within lists.
            // As a workaround, we use a button to display a price change window. 
            // The price is entered by the user and saved, which then updates this window.
            // If/when the defect gets fixed, this can be replaced in favor of a simple text edit control.
            var index = NuiGetEventArrayIndex();
            var recordId = _itemIds[index];
            var currentPrice = _itemPrices[index];
            var itemName = ItemNames[index];
            var payload = new PriceSelectionPayload(
                recordId, 
                currentPrice, 
                itemName);

            Gui.ShowWindow<PriceSelectionView>(Player, payload);
        };

        public Action OnClickShopTill => () =>
        {
            var playerId = GetObjectUUID(Player);
            var dbPlayer = DB.Get<PlayerMarket>(playerId);
            var credits = dbPlayer.MarketTill;

            if (credits <= 0)
                return;

            GiveGoldToCreature(Player, credits);
            dbPlayer.MarketTill = 0;
            DB.Set(dbPlayer);

            IsShopTillEnabled = false;
            ShopTill = LocaleString.TillXCr.ToLocalizedString(0);
        };

        private void Initialize()
        {
            var taxRate = MarketService.TaxRate * 100;
            WindowTitle = LocaleString.GlobalMarketTaxRateX.ToLocalizedString($"{taxRate:0.#}");
            SearchText = string.Empty;
            LoadData();

            WatchOnClient(model => model.SearchText);
            WatchOnClient(model => model.ItemListed);
        }

        private Guid _changeMarketPriceSubscriptionId;

        public override void OnOpen()
        {
            Initialize();
            _changeMarketPriceSubscriptionId = Event.Subscribe<MarketEvent.ChangeMarketPrice>(ChangeMarketPrice);
        }

        private void ChangeMarketPrice(uint player)
        {
            var data = Event.GetEventData<MarketEvent.ChangeMarketPrice>();
            var index = _itemIds.IndexOf(data.RecordId);

            // Couldn't find the record.
            if (index <= -1)
                return;

            _itemPrices[index] = data.Price;
            ItemPriceNames[index] = LocaleString.XCr.ToLocalizedString(data.Price);

            if (data.Price <= 0)
            {
                ListingCheckboxEnabled[index] = false;
                ItemListed[index] = false;
            }
            else
            {
                ListingCheckboxEnabled[index] = true;
            }

        }

        public override void OnClose()
        {
            Event.Unsubscribe<MarketEvent.ChangeMarketPrice>(_changeMarketPriceSubscriptionId);
        }
    }
}
