using System.Collections.Generic;
using XM.Progression.Skill;

namespace XM.Plugin.Craft
{
    internal class RecipeBuilder
    {
        private readonly Dictionary<RecipeType, RecipeDetail> _recipes = new();
        private RecipeDetail _activeRecipe;

        /// <summary>
        /// Creates a new recipe.
        /// </summary>
        /// <param name="type">The type of recipe to create.</param>
        /// <param name="skill">The skill associated with this recipe.</param>
        /// <returns>A recipe builder with the configured options</returns>
        public RecipeBuilder Create(RecipeType type, SkillType skill)
        {
            _activeRecipe = new RecipeDetail
            {
                Skill = skill
            };
            _recipes.Add(type, _activeRecipe);

            return this;
        }

        /// <summary>
        /// Sets the category of the recipe. If no category is set,
        /// the item will be displayed in the "Uncategorized" category in the menu.
        /// It's recommended all recipes have a category set.
        /// </summary>
        /// <param name="category">The category to put the recipe under.</param>
        /// <returns>A recipe builder with the configured options</returns>
        public RecipeBuilder Category(RecipeCategoryType category)
        {
            _activeRecipe.Category = category;
            return this;
        }

        /// <summary>
        /// Sets the level of the recipe which is used for success calculation.
        /// </summary>
        /// <param name="level">The level of the recipe.</param>
        /// <returns>A recipe builder with the configured options.</returns>
        public RecipeBuilder Level(int level)
        {
            _activeRecipe.Level = level;
            return this;
        }

        /// <summary>
        /// Sets the normal item and quantity created when a player crafts this recipe.
        /// </summary>
        /// <param name="resref">The resref of the item to create.</param>
        /// <param name="quantity">The quantity of the item to create.</param>
        /// <returns>A recipe builder with the configured options</returns>
        public RecipeBuilder NormalItem(string resref, int quantity = 1)
        {
            if (quantity < 1)
                quantity = 1;

            _activeRecipe.Items[RecipeQualityType.Normal] = new RecipeItem(resref, quantity);
            return this;
        }

        /// <summary>
        /// Sets the HQ resref of the item created when a player crafts this recipe.
        /// </summary>
        /// <param name="resref">The resref of the item to create.</param>
        /// <param name="quantity">The quantity of the item to create.</param>
        /// <returns>A recipe builder with the configured options</returns>
        public RecipeBuilder HQItem(string resref, int quantity = 1)
        {
            if (quantity < 1)
                quantity = 1;

            _activeRecipe.Items[RecipeQualityType.HQ] = new RecipeItem(resref, quantity);
            return this;
        }

        /// <summary>
        /// Sets the Ultra resref of the item created when a player crafts this recipe.
        /// </summary>
        /// <param name="resref">The resref of the item to create.</param>
        /// <param name="quantity">The quantity of the item to create.</param>
        /// <returns>A recipe builder with the configured options</returns>
        public RecipeBuilder UltraItem(string resref, int quantity = 1)
        {
            if (quantity < 1)
                quantity = 1;

            _activeRecipe.Items[RecipeQualityType.Ultra] = new RecipeItem(resref, quantity);
            return this;
        }

        /// <summary>
        /// Deactivates the recipe which will prevent players from learning and crafting the item.
        /// </summary>
        /// <returns>A recipe builder with the configured options</returns>
        public RecipeBuilder Inactive()
        {
            _activeRecipe.IsActive = false;
            return this;
        }

        /// <summary>
        /// Adds a component requirement to craft this recipe.
        /// </summary>
        /// <param name="resref">The item resref required.</param>
        /// <param name="quantity">The number of this component required.</param>
        /// <returns>A recipe builder with the configured options</returns>
        public RecipeBuilder Component(string resref, int quantity)
        {
            _activeRecipe.Components[resref] = quantity;
            return this;
        }

        /// <summary>
        /// Indicates this recipe must be unlocked and is not a standard recipe shown to all players.
        /// </summary>
        /// <returns>A recipe builder with the configured options</returns>
        public RecipeBuilder MustBeUnlocked()
        {
            _activeRecipe.MustBeUnlocked = true;
            return this;
        }

        /// <summary>
        /// Returns a built list of recipes.
        /// </summary>
        /// <returns>A list of built recipes.</returns>
        public Dictionary<RecipeType, RecipeDetail> Build()
        {
            return _recipes;
        }
    }
}
