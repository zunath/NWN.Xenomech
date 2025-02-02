using Anvil.Services;
using NLog;
using System.Collections.Generic;
using XM.Shared.API.NWNX.UtilPlugin;
using XM.Shared.Core.Data;
using XM.Shared.Core.Entity;
using XM.Shared.Core.EventManagement;

namespace XM.Inventory
{
    [ServiceBinding(typeof(ItemCacheService))]
    [ServiceBinding(typeof(IInitializable))]
    public class ItemCacheService: IInitializable
    {
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

        private const string TempStoragePlaceableTag = "TEMP_ITEM_STORAGE";

        private readonly DBService _db;
        private readonly XMEventService _event;
        private bool _cachedThisRun;
        private Dictionary<string, string> _itemNamesByResref;

        public ItemCacheService(DBService db, XMEventService @event)
        {
            _db = db;
            _event = @event;
            _itemNamesByResref = new Dictionary<string, string>();

            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            _event.Subscribe<XMEvent.OnModuleContentChanged>(OnModuleContentChanged);
        }

        private void OnModuleContentChanged(uint objectSelf)
        {
            var resref = UtilPlugin.GetFirstResRef(ResRefType.Item);

            while (!string.IsNullOrWhiteSpace(resref))
            {
                CacheItemNameByResref(resref);
                resref = UtilPlugin.GetNextResRef();
            }

            var dbModuleCache = _db.Get<ModuleCache>(ModuleCache.CacheIdName) ?? new ModuleCache();
            dbModuleCache.ItemNamesByResref = _itemNamesByResref;
            _db.Set(dbModuleCache);

            _cachedThisRun = true;

            _logger.Info($"Cached and loaded {_itemNamesByResref.Count} item names by resref.");
        }

        private void LoadItemCache()
        {
            // No need to load from the DB, it's already in memory.
            if (_cachedThisRun)
                return;

            var dbModuleCache = _db.Get<ModuleCache>(ModuleCache.CacheIdName) ?? new ModuleCache();
            _itemNamesByResref = dbModuleCache.ItemNamesByResref;

            _logger.Info($"Loaded {_itemNamesByResref.Count} item names by resref.");
        }

        /// <summary>
        /// Stores the name of an individual item into the cache.
        /// </summary>
        /// <param name="resref">The resref of the item we want to cache.</param>
        private void CacheItemNameByResref(string resref)
        {
            var storageContainer = GetObjectByTag(TempStoragePlaceableTag);
            var item = CreateItemOnObject(resref, storageContainer);
            _itemNamesByResref[resref] = GetName(item);
            DestroyObject(item);
        }

        /// <summary>
        /// Retrieves the name of an item by its resref. If resref cannot be found, an empty string will be returned.
        /// </summary>
        /// <param name="resref">The resref to search for.</param>
        /// <returns>The name of an item, or an empty string if it cannot be found.</returns>
        public string GetItemNameByResref(string resref)
        {
            if (string.IsNullOrWhiteSpace(resref))
                return string.Empty;

            // Item couldn't be found in the cache. Spawn it, get its details, put them in the cache, then destroy it.
            if (!_itemNamesByResref.ContainsKey(resref))
            {
                CacheItemNameByResref(resref);
            }

            // Item wasn't in the module. Return an empty string.
            if (!_itemNamesByResref.ContainsKey(resref))
                return string.Empty;

            return _itemNamesByResref[resref];
        }

        public void Init()
        {
            LoadItemCache();
        }
    }
}
