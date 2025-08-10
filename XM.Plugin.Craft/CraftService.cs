using Anvil.Services;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using XM.Inventory;
using XM.Inventory.Durability;
using XM.Shared.Core.Entity;
using XM.Plugin.Craft.UI;
using XM.Progression.Skill;
using XM.Shared.API.Constants;
using XM.Shared.Core;
using XM.Shared.Core.Data;
using XM.Shared.Core.Extension;
using XM.Shared.Core.Localization;
using XM.UI;
using Color = Anvil.API.Color;

namespace XM.Plugin.Craft
{
    [ServiceBinding(typeof(CraftService))]
    public class CraftService
    {
        private static readonly Logger _log = LogManager.GetCurrentClassLogger();

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
        private readonly ItemDurabilityService _itemDurability;

        public CraftService(
            DBService db,
            Lazy<GuiService> gui,
            SkillService skill,
            ItemPropertyService itemProperty,
            ItemCacheService itemCache,
            ItemDurabilityService itemDurability,
            IList<IRecipeListDefinition> recipeLists)
        {
            _db = db;
            _gui = gui;
            _skill = skill;
            _itemProperty = itemProperty;
            _itemCache = itemCache;
            _itemDurability = itemDurability;
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
            const int MaxDelta = -10;
            var playerId = PlayerId.Get(player);
            var dbPlayerCraft = _db.Get<PlayerCraft>(playerId);
            var recipe = GetRecipe(recipeType);
            var skill = _skill.GetCraftSkillLevel(player, recipe.Skill);
            var delta = skill - recipe.Level;

            if (delta <= MaxDelta)
            {
                return false;
            }

            if (dbPlayerCraft.PrimaryCraftSkillCode != recipe.Skill.Value &&
                dbPlayerCraft.SecondaryCraftSkillCode != recipe.Skill.Value)
            {
                return false;
            }

            if (recipe.MustBeUnlocked)
            {
                return dbPlayerCraft.LearnedRecipes.Contains((int)recipeType);
            }

            return true;
        }

        public bool CanPlayerSkillUp(uint player, RecipeType recipeType)
        {
            const int MaxDelta = 5;
            var playerId = PlayerId.Get(player);
            var dbPlayerCraft = _db.Get<PlayerCraft>(playerId);
            var recipe = GetRecipe(recipeType);
            var skill = _skill.GetCraftSkillLevel(player, recipe.Skill);
            var definition = _skill.GetCraftSkillDefinition(recipe.Skill);
            var delta = skill - recipe.Level;

            if (delta > MaxDelta)
                return false;

            if (dbPlayerCraft.PrimaryCraftSkillCode != recipe.Skill.Value &&
                dbPlayerCraft.SecondaryCraftSkillCode != recipe.Skill.Value)
            {
                return false;
            }

            if (skill >= definition.LevelCap)
                return false;

            return true;
        }

        public int CalculateSkillUpChance(uint player, RecipeType recipeType)
        {
            var recipe = GetRecipe(recipeType);
            var skill = _skill.GetCraftSkillLevel(player, recipe.Skill);
            skill = Math.Clamp(skill, 0, 100);
            var baseChance = 80.0f * Math.Exp(-0.03 * skill);
            var delta = recipe.Level - skill;
            var difficultyModifier = 1.0f + delta * 0.02f;
            var chance = baseChance * difficultyModifier;
            chance = Math.Clamp(chance, 5, 95);

            return (int)Math.Round(chance, 2);
        }

        internal void LevelUpCraftSkill(uint player, SkillType skillType)
        {
            var playerId = PlayerId.Get(player);
            var dbPlayerSkill = _db.Get<PlayerSkill>(playerId);

            if (!dbPlayerSkill.Skills.ContainsKey(skillType.Value))
                dbPlayerSkill.Skills[skillType.Value] = 0;

            dbPlayerSkill.Skills[skillType.Value]++;

            _db.Set(dbPlayerSkill);

            var definition = _skill.GetCraftSkillDefinition(skillType);
            var name = definition.Name.ToLocalizedString();
            var level = dbPlayerSkill.Skills[skillType.Value];
            var message = LocaleString.YourXSkillIncreasesToY.ToLocalizedString(name, level);
            SendMessageToPC(player, message);
        }

        private bool HasUnlockedRecipe(uint player, RecipeType recipeType)
        {
            var detail = _recipes[recipeType];

            if (!detail.MustBeUnlocked)
                return true;

            var playerId = PlayerId.Get(player);
            var dbPlayerCraft = _db.Get<PlayerCraft>(playerId);
            return dbPlayerCraft.LearnedRecipes.Contains((int)recipeType);
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
            var normalItem = detail.Items[RecipeQualityType.Normal];
            var item = CreateItemOnObject(normalItem.Resref, tempStorage);

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
            var skillType = SkillType.FromValue(GetLocalInt(device, "SKILL_ID"));
            var playerId = PlayerId.Get(player);
            var dbPlayerCraft = _db.Get<PlayerCraft>(playerId);

            if (dbPlayerCraft.PrimaryCraftSkillCode == skillType.Value ||
                dbPlayerCraft.SecondaryCraftSkillCode == skillType.Value)
            {
                var payload = new CraftPayload(skillType);
                _gui.Value.ShowWindow<CraftView>(player, payload, device);
            }
            else if (dbPlayerCraft.PrimaryCraftSkillCode == 0 ||
                     dbPlayerCraft.SecondaryCraftSkillCode == 0)
            {
                var payload = new SelectCraftPayload(skillType);
                _gui.Value.ShowWindow<SelectCraftView>(player, payload, device);
            }
            else
            {
                var message = ColorToken.Red(LocaleString.YouCannotUseThatSkill.ToLocalizedString());
                SendMessageToPC(player, message);
            }
        }

        public RecipeDetail GetRecipe(RecipeType recipeType)
        {
            return _recipes[recipeType];
        }


        [ScriptHandler("repair_terminal")]
        public void OpenRepairWindow()
        {
            var player = GetLastUsedBy();
            if (!GetIsPC(player) || GetIsDM(player))
            {
                var message = LocaleString.OnlyPlayersMayUseThisItem.ToLocalizedString();
                SendMessageToPC(player, message);
                return;
            }

            _gui.Value.ShowWindow<ItemRepairView>(player, null, OBJECT_SELF);
        }

        [ScriptHandler("bread_test3")]
        public void DurabilityReduceTest()
        {
            var player = GetLastUsedBy();
            var item = GetItemInSlot(InventorySlotType.Chest, player);

            _itemDurability.ReduceDurability(player, item);
        }
    }
}
