using System;
using System.Collections.Generic;
using System.Linq;
using Anvil.Services;
using NLog;
using XM.Inventory;
using XM.Progression.Craft.Entity;
using XM.Progression.Craft.UI;
using XM.Progression.Skill;
using XM.Shared.Core;
using XM.Shared.Core.Data;
using XM.Shared.Core.Extension;
using XM.Shared.Core.Localization;
using XM.UI;
using Color = Anvil.API.Color;

namespace XM.Progression.Craft
{
    [ServiceBinding(typeof(CraftService))]
    public class CraftService
    {
        private static Logger _log = LogManager.GetCurrentClassLogger();

        private static readonly Color _cyan = new(0, 255, 255);
        private static readonly Color _white = new(255, 255, 255);
        private static readonly Color _red = new(255, 0, 0);
        private static readonly Color _green = new(0, 255, 0);

        private readonly IList<IRecipeListDefinition> _recipeLists;
        private readonly Dictionary<RecipeType, RecipeDetail> _recipes = new();
        private static readonly Dictionary<RecipeCategoryType, RecipeCategoryAttribute> _allCategories = new();
        private static readonly Dictionary<RecipeCategoryType, RecipeCategoryAttribute> _activeCategories = new();
        private static readonly Dictionary<SkillType, Dictionary<RecipeType, RecipeDetail>> _recipesBySkill = new();
        private static readonly Dictionary<SkillType, Dictionary<RecipeCategoryType, Dictionary<RecipeType, RecipeDetail>>> _recipesBySkillAndCategory = new();
        private static readonly Dictionary<SkillType, Dictionary<RecipeCategoryType, RecipeCategoryAttribute>> _categoriesBySkill = new();
        private static readonly HashSet<string> _componentResrefs = new();
        private readonly DBService _db;
        private readonly Lazy<GuiService> _gui;
        private readonly SkillService _skill;
        private readonly ItemPropertyService _itemProperty;
        private readonly ItemCacheService _itemCache;

        public CraftService(
            DBService db,
            Lazy<GuiService> gui,
            SkillService skill,
            ItemPropertyService itemProperty,
            ItemCacheService itemCache,
            IList<IRecipeListDefinition> recipeLists)
        {
            _db = db;
            _gui = gui;
            _skill = skill;
            _itemProperty = itemProperty;
            _itemCache = itemCache;
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

        private void CacheRecipes()
        {
            foreach (var definition in _recipeLists)
            {
                var recipes = definition.BuildRecipes();
                foreach (var (recipeType, recipe) in recipes)
                {
                    if (_recipes.ContainsKey(recipeType))
                    {
                        _log.Error($"ERROR: Duplicate recipe detected: {recipeType}");
                        continue;
                    }

                    _recipes[recipeType] = recipe;

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

        public Dictionary<RecipeType, RecipeDetail> GetAllRecipesBySkill(SkillType skill)
        {
            return _recipesBySkill.ContainsKey(skill) 
                ? _recipesBySkill[skill]
                : new Dictionary<RecipeType, RecipeDetail>();
        }

        public bool CanPlayerCraftRecipe(uint player, RecipeType recipeType)
        {
            var recipe = GetRecipe(recipeType);

            if (recipe.MustBeUnlocked)
            {
                var playerId = PlayerId.Get(player);
                var dbPlayerCraft = _db.Get<PlayerCraft>(playerId);

                return dbPlayerCraft.LearnedRecipes.Contains(recipeType);
            }

            return true;
        }

        private bool HasUnlockedRecipe(uint player, RecipeType recipeType)
        {
            var detail = _recipes[recipeType];

            if (!detail.MustBeUnlocked)
                return true;

            var playerId = PlayerId.Get(player);
            var dbPlayerCraft = _db.Get<PlayerCraft>(playerId);
            return dbPlayerCraft.LearnedRecipes.Contains(recipeType);
        }

        public (XMBindingList<string>, XMBindingList<Color>) BuildRecipeDetail(uint player, RecipeType recipe)
        {
            var detail = GetRecipe(recipe);
            var recipeDetails = new XMBindingList<string>();
            var recipeDetailColors = new XMBindingList<Color>();

            recipeDetails.Add(LocaleString.COMPONENTS.ToLocalizedString());
            recipeDetailColors.Add(_cyan);
            foreach (var (resref, quantity) in detail.Components)
            {
                var componentName = _itemCache.GetItemNameByResref(resref);
                recipeDetails.Add($"{quantity}x {componentName}");
                recipeDetailColors.Add(_white);
            }

            recipeDetails.Add(string.Empty);
            recipeDetailColors.Add(_green);

            recipeDetails.Add(LocaleString.REQUIREMENTS.ToLocalizedString());
            recipeDetailColors.Add(_cyan);

            if (detail.MustBeUnlocked)
            {
                recipeDetails.Add(LocaleString.RecipeMustBeLearned.ToLocalizedString());

                recipeDetailColors.Add(HasUnlockedRecipe(player, recipe) 
                    ? _green 
                    : _red);
            }

            recipeDetails.Add(string.Empty);
            recipeDetailColors.Add(_green);

            recipeDetails.Add(LocaleString.PROPERTIES.ToLocalizedString());
            recipeDetailColors.Add(_cyan);
            var tempStorage = GetObjectByTag("TEMP_ITEM_STORAGE");
            var item = CreateItemOnObject(detail.Resref, tempStorage);

            foreach (var ip in _itemProperty.BuildItemPropertyList(item))
            {
                recipeDetails.Add(ip);
                recipeDetailColors.Add(_white);
            }

            DestroyObject(item);

            recipeDetails.Add(string.Empty);
            recipeDetailColors.Add(_white);

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
            _gui.Value.ShowWindow<CraftView>(player, payload, device);
        }

        public RecipeDetail GetRecipe(RecipeType recipeType)
        {
            return _recipes[recipeType];
        }
    }
}
