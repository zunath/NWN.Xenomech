using System.Collections.Generic;

namespace XM.Progression.Craft
{
    public interface IRecipeListDefinition
    {
        public Dictionary<RecipeType, RecipeDetail> BuildRecipes();
    }
}
