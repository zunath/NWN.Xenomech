using System.Collections.Generic;

namespace XM.Plugin.Craft
{
    public interface IRecipeListDefinition
    {
        public Dictionary<RecipeType, RecipeDetail> BuildRecipes();
    }
}
