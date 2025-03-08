using System.Collections.Generic;
using Anvil.Services;
using XM.Inventory;
using XM.Progression.Craft;
using XM.Progression.Craft.Entity;
using XM.Shared.Core;
using XM.Shared.Core.Data;
using XM.Shared.Core.Localization;

namespace XM.Plugin.Item.ItemDefinition
{
    [ServiceBinding(typeof(IItemListDefinition))]
    internal class RecipeItemDefinition: IItemListDefinition
    {
        private const string RecipeId = "RECIPE_ID";

        private readonly ItemBuilder _builder = new();

        private readonly DBService _db;
        private readonly CraftService _craft;
        private readonly ItemCacheService _itemCache;

        public RecipeItemDefinition(
            DBService db,
            CraftService craft,
            ItemCacheService itemCache)
        {
            _db = db;
            _craft = craft;
            _itemCache = itemCache;
        }

        public Dictionary<string, ItemDetail> BuildItems()
        {
            Recipe();

            return _builder.Build();
        }

        private void Recipe()
        {
            _builder.Create("RECIPE")
                .Delay(1f)
                .ReducesItemCharge()
                .ValidationAction((user, item, target, location, index) =>
                {
                    if (!GetIsPC(user) || GetIsDM(user))
                    {
                        return LocaleString.OnlyPlayersMayUseThisItem.ToLocalizedString();
                    }

                    var recipeId = GetLocalInt(item, RecipeId);
                    if (recipeId <= 0)
                    {
                        return LocaleString.ItemMisconfiguredError.ToLocalizedString();
                    }

                    var recipeType = (RecipeType)recipeId;
                    var playerId = PlayerId.Get(user);
                    var dbPlayerCraft = _db.Get<PlayerCraft>(playerId);
                    if (dbPlayerCraft.LearnedRecipes.Contains(recipeType))
                    {
                        return LocaleString.YouAlreadyLearnedThisRecipe.ToLocalizedString();
                    }

                    return string.Empty;
                })
                .ApplyAction((user, item, target, location, index) =>
                {
                    var playerId = PlayerId.Get(user);
                    var dbPlayerCraft = _db.Get<PlayerCraft>(playerId);
                    var recipeType = (RecipeType)GetLocalInt(item, RecipeId);
                    var recipeDetail = _craft.GetRecipe(recipeType);

                    dbPlayerCraft.LearnedRecipes.Add(recipeType);
                    _db.Set(dbPlayerCraft);

                    var name = _itemCache.GetItemNameByResref(recipeDetail.Resref);
                    var message = LocaleString.RecipeLearnedX.ToLocalizedString(name);
                    SendMessageToPC(user, message);
                });
        }
    }
}
