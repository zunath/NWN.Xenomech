using XM.Shared.Core.Localization;

namespace XM.Inventory.KeyItem
{
    public enum KeyItemType
    {
        [KeyItem(KeyItemCategoryType.Invalid, LocaleString.Invalid, false)]
        Invalid = 0,
        [KeyItem(KeyItemCategoryType.Keys, LocaleString.Attributes, true, LocaleString.Combat)]
        TestKeyItem = 1,
    }
}
