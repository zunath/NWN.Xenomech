using System.Collections.Generic;
using Anvil.API;
using Anvil.Services;
using NLog;
using NLog.Fluent;
using XM.Plugin.Item.Market.Entity;
using XM.Shared.API.NWNX.ObjectPlugin;
using XM.Shared.Core;
using XM.Shared.Core.Data;
using XM.Shared.Core.Localization;
using XM.UI;
using Action = System.Action;

namespace XM.Plugin.Item.Market.UI
{
    internal class MarketBuyViewModel: ViewModel<MarketBuyViewModel>
    {
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();
        private const int ListingsPerPage = 20;
        private readonly List<MarketCategoryType> _categoryTypes = new();
        private readonly XMBindingList<string> _categories = new();

        private bool _skipPaginationSearch;
        private readonly List<int> _activeCategoryIdFilters = new();

        [Inject]
        public MarketService Market { get; set; }

        [Inject]
        public DBService DB { get; set; }

        public string WindowTitle
        {
            get => Get<string>();
            set => Set(value);
        }

        public string SearchText
        {
            get => Get<string>();
            set => Set(value);
        }

        public XMBindingList<NuiComboEntry> PageNumbers
        {
            get => Get<XMBindingList<NuiComboEntry>>();
            set => Set(value);
        }

        public int SelectedPageIndex
        {
            get => Get<int>();
            set
            {
                Set(value);

                if (!_skipPaginationSearch)
                    Search();
            }
        }

        private readonly List<string> _itemIds = new();
        private readonly List<int> _itemPrices = new();

        public XMBindingList<string> CategoryNames
        {
            get => Get<XMBindingList<string>>();
            set => Set(value);
        }

        public XMBindingList<bool> CategoryToggles
        {
            get => Get<XMBindingList<bool>>();
            set => Set(value);
        }

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

        public XMBindingList<string> ItemPriceNames
        {
            get => Get<XMBindingList<string>>();
            set => Set(value);
        }

        public XMBindingList<string> ItemSellerNames
        {
            get => Get<XMBindingList<string>>();
            set => Set(value);
        }

        public XMBindingList<bool> ItemBuyEnabled
        {
            get => Get<XMBindingList<bool>>();
            set => Set(value);
        }

        private void LoadData()
        {
            var categoryToggles = new XMBindingList<bool>();

            foreach (var unused in _categories)
            {
                categoryToggles.Add(false);
            }

            CategoryNames = _categories;
            CategoryToggles = categoryToggles;
        }

        private void Search()
        {
            var query = new DBQuery()
                .AddFieldSearch(nameof(MarketItem.IsListed), true);

            if (!string.IsNullOrWhiteSpace(SearchText))
                query.AddFieldSearch(nameof(MarketItem.Name), SearchText, true);

            if (_activeCategoryIdFilters.Count > 0)
            {
                query.AddFieldSearch(nameof(MarketItem.Category), _activeCategoryIdFilters);
            }

            query.AddPaging(ListingsPerPage, ListingsPerPage * SelectedPageIndex);

            var totalRecordCount = DB.SearchCount<MarketItem>(query);
            UpdatePagination(totalRecordCount);

            var credits = GetGold(Player);
            var results = DB.Search<MarketItem>(query);

            _itemIds.Clear();
            _itemPrices.Clear();
            var itemIconResrefs = new XMBindingList<string>();
            var itemNames = new XMBindingList<string>();
            var itemPriceNames = new XMBindingList<string>();
            var itemSellerNames = new XMBindingList<string>();
            var itemBuyEnabled = new XMBindingList<bool>();

            foreach (var record in results)
            {
                _itemIds.Add(record.Id);
                _itemPrices.Add(record.Price);
                itemIconResrefs.Add(record.IconResref);
                itemNames.Add($"{record.Quantity}x {record.Name}");
                itemPriceNames.Add($"{record.Price} cr");
                itemSellerNames.Add(record.SellerName);
                itemBuyEnabled.Add(credits >= record.Price);
            }

            ItemIconResrefs = itemIconResrefs;
            ItemNames = itemNames;
            ItemPriceNames = itemPriceNames;
            ItemSellerNames = itemSellerNames;
            ItemBuyEnabled = itemBuyEnabled;
        }

        private void UpdatePagination(long totalRecordCount)
        {
            _skipPaginationSearch = true;
            var pageNumbers = new XMBindingList<NuiComboEntry>();
            var pages = (int)(totalRecordCount / ListingsPerPage + (totalRecordCount % ListingsPerPage == 0 ? 0 : 1));

            // Always add page 1. In the event no items are for sale,
            // it still needs to be displayed.
            pageNumbers.Add(new NuiComboEntry($"Page 1", 0));
            for (var x = 2; x <= pages; x++)
            {
                pageNumbers.Add(new NuiComboEntry($"Page {x}", x - 1));
            }

            PageNumbers = pageNumbers;

            // In the event no results are found, default the index to zero
            if (pages <= 0)
                SelectedPageIndex = 0;
            // Otherwise, if current page is outside the new page bounds,
            // set it to the last page in the list.
            else if (SelectedPageIndex > pages - 1)
                SelectedPageIndex = pages - 1;

            _skipPaginationSearch = false;
        }

        public Action OnClickSearch => Search;

        public Action OnClickClearSearch => () =>
        {
            SearchText = string.Empty;
            Search();
        };

        public Action OnClickPreviousPage => () =>
        {
            _skipPaginationSearch = true;
            var newPage = SelectedPageIndex - 1;
            if (newPage < 0)
                newPage = 0;

            SelectedPageIndex = newPage;
            _skipPaginationSearch = false;
        };

        public Action OnClickNextPage => () =>
        {
            _skipPaginationSearch = true;
            var newPage = SelectedPageIndex + 1;
            if (newPage > PageNumbers.Count - 1)
                newPage = PageNumbers.Count - 1;

            SelectedPageIndex = newPage;
            _skipPaginationSearch = false;
        };

        public Action OnClickExamine => () =>
        {
            var index = NuiGetEventArrayIndex();
            var itemId = _itemIds[index];
            var dbItem = DB.Get<MarketItem>(itemId);

            // todo: finish below
            //var item = ObjectPlugin.Deserialize(dbItem.Data);
            //var payload = new ExamineItemPayload(GetName(item), GetDescription(item), Item.BuildItemPropertyString(item));
            //Gui.TogglePlayerWindow(Player, GuiWindowType.ExamineItem, payload);
            //DestroyObject(item);
        };

        public Action OnClickBuy => () =>
        {
            var index = NuiGetEventArrayIndex();
            var itemId = _itemIds[index];
            var itemName = ItemNames[index];
            var price = _itemPrices[index];

            ShowModal(LocaleString.AreYouSureYouWantToBuyXForYCredits.ToLocalizedString(itemName, price), () =>
            {
                var dbItem = DB.Get<MarketItem>(itemId);

                // If another player buys the item or the item gets removed from the market,
                // prevent the player from purchasing it.
                if (dbItem == null)
                {
                    FloatingTextStringOnCreature(LocaleString.ThisItemIsNoLongerAvailable.ToLocalizedString(), Player, false);
                    return;
                }

                // If the player no longer has enough money to purchase the item, prevent them from purchasing it.
                // This can happen if the player opens the modal, drops their money and clicks yes.
                // Another potential scenario is the seller adjusts the price on the item while they're mid-purchase.
                if (GetGold(Player) < price)
                {
                    FloatingTextStringOnCreature(LocaleString.YouDoNotHaveEnoughCreditsToPurchaseThisItem.ToLocalizedString(), Player, false);
                    return;
                }

                // Item's price has been changed since the player's search.
                // Notify them and refresh the search.
                if (dbItem.Price != _itemPrices[index])
                {
                    FloatingTextStringOnCreature(LocaleString.ThePriceOfThisItemHasChanged.ToLocalizedString(), Player, false);
                    Search();
                    return;
                }

                // Take money and give the item to the buyer.
                AssignCommand(Player, () =>
                {
                    TakeGoldFromCreature(price, Player, true);
                });
                var item = ObjectPlugin.Deserialize(dbItem.Data);

                var log = LocaleString.PurchaseLogMessage.ToLocalizedString(
                    GetName(Player),
                    PlayerId.Get(Player),
                    GetItemStackSize(item),
                    GetName(item),
                    dbItem.SellerName,
                    price);
                _logger.Info(log);
                ObjectPlugin.AcquireItem(Player, item);

                // Remove this item from the client's search results.
                _itemIds.RemoveAt(index);
                _itemPrices.RemoveAt(index);
                ItemIconResrefs.RemoveAt(index);
                ItemNames.RemoveAt(index);
                ItemPriceNames.RemoveAt(index);
                ItemSellerNames.RemoveAt(index);
                ItemBuyEnabled.RemoveAt(index);

                // Remove the item from the database.
                DB.Delete<MarketItem>(itemId);

                // Give the money to the seller.
                var sellerPlayerId = dbItem.PlayerId;
                var dbSeller = DB.Get<PlayerMarket>(sellerPlayerId);
                var proceeds = (int)(price - (price * MarketService.TaxRate));
                dbSeller.MarketTill += proceeds;
                DB.Set(dbSeller);
            });
        };

        public Action OnClickClearFilters => () =>
        {
            LoadData();
            _activeCategoryIdFilters.Clear();
            Search();
        };

        public Action OnClickCategory => () =>
        {
            var index = NuiGetEventArrayIndex();
            var categoryType = (int)_categoryTypes[index];

            if (CategoryToggles[index] && !_activeCategoryIdFilters.Contains(categoryType))
                _activeCategoryIdFilters.Add(categoryType);
            else if (!CategoryToggles[index] && _activeCategoryIdFilters.Contains(categoryType))
                _activeCategoryIdFilters.Remove(categoryType);

            Search();
        };

        private void LoadCategories()
        {
            foreach (var (type, category) in Market.GetActiveCategories())
            {
                _categoryTypes.Add(type);
                _categories.Add(category.Name.ToLocalizedString());
            }
        }

        private void Initialize()
        {
            _skipPaginationSearch = true;
            _activeCategoryIdFilters.Clear();
            SelectedPageIndex = 0;
            SearchText = string.Empty;
            WindowTitle = LocaleString.GlobalMarket.ToLocalizedString();
            LoadData();
            Search();

            WatchOnClient(model => model.SearchText);
            WatchOnClient(model => model.SelectedPageIndex);
            WatchOnClient(model => model.CategoryToggles);
            _skipPaginationSearch = false;
        }

        public override void OnOpen()
        {
            LoadCategories();

            Initialize();
        }

        public override void OnClose()
        {
            
        }
    }
}
