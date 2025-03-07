using System.Collections.Generic;
using Anvil.Services;
using XM.Progression.Skill;

namespace XM.Progression.Craft.RecipeDefinition
{
    [ServiceBinding(typeof(IRecipeListDefinition))]
    internal class TestRecipe: IRecipeListDefinition
    {
        private readonly RecipeBuilder _builder = new();

        public Dictionary<RecipeType, RecipeDetail> BuildRecipes()
        {
            Test();

            return _builder.Build();
        }

        private void Test()
        {
            _builder.Create(RecipeType.Test, SkillType.Weaponcraft)
                .Level(1)
                .Category(RecipeCategoryType.Longsword)
                .Quantity(1)
                .Resref("test_longsword")
                .Component("test_component", 3);
        }
    }
}
