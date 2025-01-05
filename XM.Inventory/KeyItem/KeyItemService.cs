using Anvil.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using XM.Core;
using XM.Core.Data;
using XM.Core.EventManagement;
using XM.Core.Extension;
using XM.Inventory.Entity;
using XM.Localization;

namespace XM.Inventory.KeyItem
{
    [ServiceBinding(typeof(KeyItemService))]
    public class KeyItemService
    {
        // All categories/key items
        private readonly Dictionary<KeyItemCategoryType, KeyItemCategoryAttribute> _allCategories = new();
        private readonly Dictionary<KeyItemType, KeyItemAttribute> _allKeyItems = new();
        private readonly Dictionary<KeyItemCategoryType, List<KeyItemType>> _allKeyItemsByCategory = new();

        // Active categories/key items
        private readonly Dictionary<KeyItemType, KeyItemAttribute> _activeKeyItems = new();
        private readonly Dictionary<KeyItemCategoryType, KeyItemCategoryAttribute> _activeKeyItemCategories = new();
        private readonly Dictionary<KeyItemCategoryType, Dictionary<KeyItemType, KeyItemAttribute>> _activeKeyItemsByCategory = new();

        // By key item type name or Id
        private readonly Dictionary<string, KeyItemType> _keyItemsByTypeName = new();
        private readonly Dictionary<int, KeyItemType> _keyItemsByTypeId = new();

        private readonly DBService _db;

        public KeyItemService(DBService db, XMEventService @event)
        {
            _db = db;

            @event.Subscribe<XMEvent.OnCacheDataBefore>(OnCacheDataBefore);
        }

        private void OnCacheDataBefore()
        {
            // Organize categories
            var categories = Enum.GetValues(typeof(KeyItemCategoryType)).Cast<KeyItemCategoryType>();
            foreach (var category in categories)
            {
                var categoryDetail = category.GetAttribute<KeyItemCategoryType, KeyItemCategoryAttribute>();
                _allCategories[category] = categoryDetail;
                _allKeyItemsByCategory[category] = new List<KeyItemType>();

                if (categoryDetail.IsActive)
                {
                    _activeKeyItemCategories[category] = categoryDetail;
                    _activeKeyItemsByCategory[category] = new Dictionary<KeyItemType, KeyItemAttribute>();
                }
            }

            // Organize key items
            var keyItems = Enum.GetValues(typeof(KeyItemType)).Cast<KeyItemType>();
            foreach (var keyItem in keyItems)
            {
                var keyItemDetail = keyItem.GetAttribute<KeyItemType, KeyItemAttribute>();
                _allKeyItems[keyItem] = keyItemDetail;
                _allKeyItemsByCategory[keyItemDetail.Category].Add(keyItem);

                var enumName = Enum.GetName(typeof(KeyItemType), keyItem);
                if (!string.IsNullOrWhiteSpace(enumName))
                {
                    _keyItemsByTypeName[enumName] = keyItem;
                }

                _keyItemsByTypeId[(int)keyItem] = keyItem;

                if (keyItemDetail.IsActive)
                {
                    _activeKeyItems[keyItem] = keyItemDetail;
                    if (_activeKeyItemsByCategory.ContainsKey(keyItemDetail.Category))
                    {
                        _activeKeyItemsByCategory[keyItemDetail.Category][keyItem] = keyItemDetail;
                    }
                }
            }
        }

        /// <summary>
        /// Gives a specific key item to a player.
        /// If player is not a PC or is a DM, nothing will happen.
        /// If player already has the key item, nothing will happen.
        /// </summary>
        /// <param name="player">The player</param>
        /// <param name="keyItem">The key item type to give.</param>
        public void GiveKeyItem(uint player, KeyItemType keyItem)
        {
            if (!GetIsPC(player) || GetIsDM(player)) return;

            var playerId = PlayerId.Get(player);
            var dbPlayerKeyItem = _db.Get<PlayerKeyItem>(playerId);

            if (dbPlayerKeyItem.KeyItems.ContainsKey(keyItem))
                return;

            dbPlayerKeyItem.KeyItems[keyItem] = DateTime.UtcNow;
            _db.Set(dbPlayerKeyItem);

            var keyItemDetail = _allKeyItems[keyItem];
            SendMessageToPC(player, Locale.GetString(LocaleString.YouAcquireKeyItem, keyItemDetail.Name));
            //Gui.PublishRefreshEvent(player, new KeyItemReceivedRefreshEvent(keyItem));
        }

        /// <summary>
        /// Removes a key item from a player.
        /// If player is not a PC or is a DM, nothing will happen.
        /// If player does not have the key item, nothing will happen.
        /// </summary>
        /// <param name="player">The player</param>
        /// <param name="keyItem">The key item type to remove.</param>
        public void RemoveKeyItem(uint player, KeyItemType keyItem)
        {
            if (!GetIsPC(player) || GetIsDM(player)) return;

            var playerId = PlayerId.Get(player);
            var dbPlayer = _db.Get<PlayerKeyItem>(playerId);

            if (!dbPlayer.KeyItems.ContainsKey(keyItem))
                return;

            dbPlayer.KeyItems.Remove(keyItem);
            _db.Set(dbPlayer);

            var keyItemDetail = _allKeyItems[keyItem];
            SendMessageToPC(player, Locale.GetString(LocaleString.YouLostKeyItem, keyItemDetail.Name));
        }

        /// <summary>
        /// Checks whether a player has a key item.
        /// Returns false if player is non-PC or DM.
        /// </summary>
        /// <param name="player">The player to check.</param>
        /// <param name="keyItem">The type of key item to check for.</param>
        /// <returns>true if the player has a key item, false otherwise</returns>
        public bool HasKeyItem(uint player, KeyItemType keyItem)
        {
            if (!GetIsPC(player) || GetIsDM(player)) return false;

            var playerId = PlayerId.Get(player);
            var dbPlayer = _db.Get<PlayerKeyItem>(playerId);

            return dbPlayer.KeyItems.ContainsKey(keyItem);
        }

        /// <summary>
        /// Checks whether a player has all of the specified key items.
        /// Returns false if player is non-PC or DM.
        /// </summary>
        /// <param name="player">The player to check.</param>
        /// <param name="keyItems">Required key items.</param>
        /// <returns>true if player has all key items, false otherwise</returns>
        public bool HasAllKeyItems(uint player, List<KeyItemType> keyItems)
        {
            if (!GetIsPC(player) || GetIsDM(player)) return false;

            // No key items specified, default to true.
            if (keyItems == null)
                return true;

            var playerId = PlayerId.Get(player);
            var dbPlayer = _db.Get<PlayerKeyItem>(playerId);

            foreach (var ki in keyItems)
            {
                if (!dbPlayer.KeyItems.ContainsKey(ki))
                    return false;
            }

            return true;
        }

        /// <summary>
        /// Gets a key item's detail by its type.
        /// </summary>
        /// <param name="keyItem">The type of key item to retrieve.</param>
        /// <returns>A key item detail</returns>
        public KeyItemAttribute GetKeyItem(KeyItemType keyItem)
        {
            return _allKeyItems[keyItem];
        }

        /// <summary>
        /// Retrieves a key item type by its integer Id.
        /// Returns KeyItemType.Invalid if not found.
        /// </summary>
        /// <param name="keyItemId">The Id to search for</param>
        /// <returns>A KeyItemType matching the Id.</returns>
        public KeyItemType GetKeyItemTypeById(int keyItemId)
        {
            return !_keyItemsByTypeId.ContainsKey(keyItemId) ?
                KeyItemType.Invalid :
                _keyItemsByTypeId[keyItemId];
        }

        /// <summary>
        /// Retrieves a key item type by its name.
        /// Returns KeyItemType.Invalid if not found.
        /// </summary>
        /// <param name="name">The name to search for</param>
        /// <returns>A KeyItemType matching the name.</returns>
        public KeyItemType GetKeyItemTypeByName(string name)
        {
            return !_keyItemsByTypeName.ContainsKey(name) ?
                KeyItemType.Invalid :
                _keyItemsByTypeName[name];
        }
    }
}
