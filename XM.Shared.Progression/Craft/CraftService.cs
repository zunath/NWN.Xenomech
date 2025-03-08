using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Anvil.Services;
using XM.Progression.Craft.UI;
using XM.Progression.Skill;
using XM.Shared.Core;
using XM.Shared.Core.Extension;
using XM.Shared.Core.Localization;
using XM.UI;

namespace XM.Progression.Craft
{
    [ServiceBinding(typeof(CraftService))]
    public class CraftService
    {
        private const string TempStorageTag = "TEMP_ITEM_STORAGE";

        private readonly IList<IRecipeListDefinition> _recipeLists;
        private readonly Dictionary<RecipeType, RecipeDetail> _recipes = new();
        private static readonly Dictionary<RecipeCategoryType, RecipeCategoryAttribute> _allCategories = new();
        private static readonly Dictionary<RecipeCategoryType, RecipeCategoryAttribute> _activeCategories = new();
        private static readonly Dictionary<SkillType, Dictionary<RecipeType, RecipeDetail>> _recipesBySkill = new();
        private static readonly Dictionary<SkillType, Dictionary<RecipeCategoryType, Dictionary<RecipeType, RecipeDetail>>> _recipesBySkillAndCategory = new();
        private static readonly Dictionary<SkillType, Dictionary<RecipeCategoryType, RecipeCategoryAttribute>> _categoriesBySkill = new();
        private static readonly HashSet<string> _componentResrefs = new();
        private readonly GuiService _gui;
        private readonly SkillService _skill;

        public CraftService(
            GuiService gui,
            SkillService skill,
            IList<IRecipeListDefinition> recipeLists)
        {
            _gui = gui;
            _skill = skill;
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
                _allCategories[category] = categoryDetail;

                if (categoryDetail.IsActive)
                    _activeCategories[category] = categoryDetail;
            }

            Console.WriteLine($"Loaded {_allCategories.Count} recipe category types.");
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
                foreach (var (recipeType, recipe) in recipes)
                {
                    var item = CreateItemOnObject(recipe.Resref, storage);
                    var name = GetName(item);
                    DestroyObject(item);
                    recipe.Name = name;

                    // Organize recipes by skill.
                    if (!_recipesBySkill.ContainsKey(recipe.Skill))
                        _recipesBySkill[recipe.Skill] = new Dictionary<RecipeType, RecipeDetail>();
                    _recipesBySkill[recipe.Skill][recipeType] = recipe;

                    // Organize recipe by skill and category.
                    if (!_recipesBySkillAndCategory.ContainsKey(recipe.Skill))
                        _recipesBySkillAndCategory[recipe.Skill] = new Dictionary<RecipeCategoryType, Dictionary<RecipeType, RecipeDetail>>();

                    if (!_recipesBySkillAndCategory[recipe.Skill].ContainsKey(recipe.Category))
                        _recipesBySkillAndCategory[recipe.Skill][recipe.Category] = new Dictionary<RecipeType, RecipeDetail>();

                    _recipesBySkillAndCategory[recipe.Skill][recipe.Category][recipeType] = recipe;

                    // Organize categories by skill based on whether there are any recipes under that category.
                    if (recipe.IsActive)
                    {
                        if (!_categoriesBySkill.ContainsKey(recipe.Skill))
                            _categoriesBySkill[recipe.Skill] = new Dictionary<RecipeCategoryType, RecipeCategoryAttribute>();

                        if (!_categoriesBySkill[recipe.Skill].ContainsKey(recipe.Category))
                            _categoriesBySkill[recipe.Skill][recipe.Category] = _allCategories[recipe.Category];

                        // Cache the resrefs into a hashset for later use in determining if an item is a component
                        foreach (var (resref, _) in recipe.Components)
                        {
                            if (!_componentResrefs.Contains(resref))
                                _componentResrefs.Add(resref);
                        }
                    }
                }
            }
        }
        internal Dictionary<RecipeCategoryType, RecipeCategoryAttribute> GetRecipeCategoriesBySkill(SkillType skill)
        {
            if (!_categoriesBySkill.ContainsKey(skill))
                return new Dictionary<RecipeCategoryType, RecipeCategoryAttribute>();

            return _categoriesBySkill[skill].ToDictionary(x => x.Key, y => y.Value);
        }

        public Dictionary<RecipeType, RecipeDetail> GetRecipesBySkillAndCategory(SkillType skill, RecipeCategoryType category)
        {
            if (!_recipesBySkillAndCategory.ContainsKey(skill))
                return new Dictionary<RecipeType, RecipeDetail>();

            if (!_recipesBySkillAndCategory[skill].ContainsKey(category))
                return new Dictionary<RecipeType, RecipeDetail>();

            return _recipesBySkillAndCategory[skill][category].ToDictionary(x => x.Key, y => y.Value);
        }

        public Dictionary<RecipeType, RecipeDetail> GetAllRecipes()
        {
            return _recipes;
        }
        public bool CanPlayerCraftRecipe(uint player, RecipeType recipeType)
        {
            var recipe = GetRecipe(recipeType);



            return true;
        }
        public (XMBindingList<string>, XMBindingList<Color>) BuildRecipeDetail(uint player, RecipeType recipe)
        {
            var detail = GetRecipe(recipe);
            var recipeDetails = new XMBindingList<string>();
            var recipeDetailColors = new XMBindingList<Color>();

            recipeDetails.Add("[COMPONENTS]");
            recipeDetailColors.Add(Color.Cyan);
            foreach (var (resref, quantity) in detail.Components)
            {
                var componentName = Cache.GetItemNameByResref(resref);
                recipeDetails.Add($"{quantity}x {componentName}");
                recipeDetailColors.Add(Color.White);
            }

            recipeDetails.Add(string.Empty);
            recipeDetailColors.Add(Color.Green);

            recipeDetails.Add("[REQUIREMENTS]");
            recipeDetailColors.Add(Color.Cyan);
            foreach (var req in detail.Requirements)
            {
                recipeDetails.Add(req.RequirementText);
                recipeDetailColors.Add(string.IsNullOrWhiteSpace(req.CheckRequirements(player))
                    ? Color.Green
                    : Color.Red);
            }

            recipeDetails.Add(string.Empty);
            recipeDetailColors.Add(Color.Green);

            recipeDetails.Add("[PROPERTIES]");
            recipeDetailColors.Add(Color.Cyan);
            var tempStorage = GetObjectByTag("TEMP_ITEM_STORAGE");
            var item = CreateItemOnObject(detail.Resref, tempStorage);

            foreach (var ip in Item.BuildItemPropertyList(item))
            {
                recipeDetails.Add(ip);
                recipeDetailColors.Add(Color.White);
            }

            DestroyObject(item);

            recipeDetails.Add(string.Empty);
            recipeDetailColors.Add(Color.White);

            return (recipeDetails, recipeDetailColors);
        }

        [ScriptHandler("craft_device")]
        public void UseCraftDevice()
        {
            var player = GetLastUsedBy();

            if (!GetIsPC(player) || GetIsDM(player))
            {
                var message = LocaleString.OnlyPlayersMayUseThisItem.ToLocalizedString();
                SendMessageToPC(player, message);
                return;
            }

            var device = OBJECT_SELF;
            var skillType = (SkillType)GetLocalInt(device, "SKILL_ID");
            var payload = new CraftPayload(skillType);
            _gui.ShowWindow<CraftView>(player, payload, device);
        }

        public RecipeDetail GetRecipe(RecipeType recipeType)
        {
            return _recipes[recipeType];
        }
    }
}
