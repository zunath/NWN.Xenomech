using System;
using System.Collections.Generic;
using System.Linq;
using Anvil.Services;
using XM.Shared.Core.Extension;

namespace XM.Progression.Craft
{
    [ServiceBinding(typeof(CraftService))]
    public class CraftService
    {
        private const string TempStorageTag = "TEMP_ITEM_STORAGE";

        private readonly IList<IRecipeListDefinition> _recipeLists;
        private readonly Dictionary<RecipeType, RecipeDetail> _recipes = new();
        private readonly Dictionary<RecipeCategoryType, RecipeCategoryAttribute> _categories = new();

        public CraftService(
            IList<IRecipeListDefinition> recipeLists)
        {
            _recipeLists = recipeLists;

            RegisterEvents();
            SubscribeEvents();

            CacheCategories();
            CacheRecipes();
        }

        private void RegisterEvents()
        {

        }

        private void SubscribeEvents()
        {

        }

        private void CacheCategories()
        {
            var categories = Enum.GetValues(typeof(RecipeCategoryType)).Cast<RecipeCategoryType>();
            foreach (var category in categories)
            {
                var categoryDetail = category.GetAttribute<RecipeCategoryType, RecipeCategoryAttribute>();

                if(categoryDetail.IsActive)
                    _categories[category] = categoryDetail;
            }
        }

        private uint GetTempStorage()
        {
            var storage = GetObjectByTag(TempStorageTag);

            return storage;
        }

        private void CacheRecipes()
        {
            var storage = GetTempStorage();

            foreach (var definition in _recipeLists)
            {
                var recipes = definition.BuildRecipes();
                foreach (var (type, detail) in recipes)
                {
                    if (detail.IsActive)
                    {
                        var item = CreateItemOnObject(detail.Resref, storage);
                        var name = GetName(item);
                        DestroyObject(item);

                        detail.Name = name;
                        _recipes.Add(type, detail);
                    }
                }
            }
        }

        public RecipeDetail GetRecipe(RecipeType recipeType)
        {
            return _recipes[recipeType];
        }
    }
}
