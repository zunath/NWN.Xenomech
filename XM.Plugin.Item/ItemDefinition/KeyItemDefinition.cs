using System.Collections.Generic;
using Anvil.Services;
using XM.Inventory.KeyItem;
using NLog;
using XM.Shared.Core.Localization;

namespace XM.Plugin.Item.ItemDefinition
{
    [ServiceBinding(typeof(IItemListDefinition))]
    internal class KeyItemDefinition : IItemListDefinition
    {
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();
        private const string KeyItemIdVariable = "KEY_ITEM_ID";

        private readonly ItemBuilder _builder = new();
        private readonly KeyItemService _keyItem;

        public KeyItemDefinition(KeyItemService keyItem)
        {
            _keyItem = keyItem;
        }

        public Dictionary<string, ItemDetail> BuildItems()
        {
            KeyItem();

            return _builder.Build();
        }

        private void KeyItem()
        {
            _builder.Create("KEY_ITEM")
                .Delay(1f)
                .ReducesItemCharge()
                .ValidationAction((user, item, target, location, itemPropertyIndex) =>
                {
                    var keyItemId = GetLocalInt(item, KeyItemIdVariable);

                    if (keyItemId <= 0)
                    {
                        _logger.Error(LocaleString.KeyItemIdForItemXIsNotSetProperly.ToLocalizedString(GetName(item)));
                        return LocaleString.KeyItemIdIsNotConfiguredProperlyOnTheItem.ToLocalizedString();
                    }

                    try
                    {
                        var keyItemType = (KeyItemType)keyItemId;

                        if (_keyItem.HasKeyItem(user, keyItemType))
                        {
                            return LocaleString.YouAlreadyHaveThisKeyItem.ToLocalizedString();
                        }

                    }
                    catch
                    {
                        _logger.Error(LocaleString.KeyItemIdXForItemYIsNotAssignedToAValidKeyItemType.ToLocalizedString(keyItemId, GetName(item)));
                        return LocaleString.KeyItemIdIsNotConfiguredProperlyOnTheItem.ToLocalizedString();
                    }

                    return string.Empty;
                })
                .ApplyAction((user, item, target, location, itemPropertyIndex) =>
                {
                    var keyItemId = GetLocalInt(item, KeyItemIdVariable);
                    var keyItemType = (KeyItemType)keyItemId;
                    _keyItem.GiveKeyItem(user, keyItemType);
                });
        }
    }
}
