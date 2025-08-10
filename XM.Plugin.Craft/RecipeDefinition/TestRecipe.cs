using System.Collections.Generic;
using Anvil.Services;
using XM.Progression.Skill;

namespace XM.Plugin.Craft.RecipeDefinition
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
                .NormalItem("test_longsword", 1)
                .HQItem("test_longsword1", 1)
                .UltraItem("test_longsword2", 1)
                .Component("test_component", 3);
        }
    }
}
