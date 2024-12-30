using XM.Localization;

namespace XM.Inventory.KeyItem
{
    public enum KeyItemCategoryType
    {
        [KeyItemCategory(LocaleString.Invalid, false)]
        Invalid = 0,
        [KeyItemCategory(LocaleString.KeyItemCategoryMaps, true)]
        Maps = 1,
        [KeyItemCategory(LocaleString.KeyItemCategoryQuestItems, true)]
        QuestItems = 2,
        [KeyItemCategory(LocaleString.KeyItemCategoryDocuments, true)]
        Documents = 3,
        [KeyItemCategory(LocaleString.KeyItemCategoryBlueprints, true)]
        Blueprints = 4,
        [KeyItemCategory(LocaleString.KeyItemCategoryKeys, true)]
        Keys = 5
    }
}
