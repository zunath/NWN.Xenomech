using System.Collections.Generic;
using Anvil.Services;
using XM.Progression.Skill;

namespace XM.Progression.Craft.RecipeDefinition
{
    [ServiceBinding(typeof(IRecipeListDefinition))]
    internal class ThrowingRecipes : IRecipeListDefinition
    {
        private readonly RecipeBuilder _builder = new();

        public Dictionary<RecipeType, RecipeDetail> BuildRecipes()
        {
            BuildThrowingRecipes();
            return _builder.Build();
        }

        private void BuildThrowingRecipes()
        {
            // Level 4
            _builder.Create(RecipeType.CoarseShuriken, SkillType.Weaponcraft)
                .Level(4)
                .Category(RecipeCategoryType.Throwing)
                .NormalItem("coarse_shurikn", 25);

            // Level 7
            _builder.Create(RecipeType.ArcShuriken, SkillType.Weaponcraft)
                .Level(7)
                .Category(RecipeCategoryType.Throwing)
                .NormalItem("arc_shuriken", 25)
                .HQItem("arc_shuriken_p1", 25);

            // Level 35
            _builder.Create(RecipeType.VanguardShuriken, SkillType.Weaponcraft)
                .Level(35)
                .Category(RecipeCategoryType.Throwing)
                .NormalItem("vang_shuriken", 25);

            // Level 43
            _builder.Create(RecipeType.WingShuriken, SkillType.Weaponcraft)
                .Level(43)
                .Category(RecipeCategoryType.Throwing)
                .NormalItem("wing_shuriken", 25)
                .HQItem("wing_shurkn_p1", 25);

            // Level 55
            _builder.Create(RecipeType.EtherShuriken, SkillType.Weaponcraft)
                .Level(55)
                .Category(RecipeCategoryType.Throwing)
                .NormalItem("ether_shrkn", 25)
                .HQItem("ether_shrkn_p1", 25);

            // Level 67
            _builder.Create(RecipeType.LongstrikeShuriken, SkillType.Weaponcraft)
                .Level(67)
                .Category(RecipeCategoryType.Throwing)
                .NormalItem("longstrk_shrkn", 25);

            // Level 81
            _builder.Create(RecipeType.WarcastersShuriken, SkillType.Weaponcraft)
                .Level(81)
                .Category(RecipeCategoryType.Throwing)
                .NormalItem("warcast_shrkn", 25)
                .HQItem("warcast_shrkn_p1", 25)
                .UltraItem("warcast_shrkn_p2", 25);

            // Level 85
            _builder.Create(RecipeType.NoviceDuelistsShuriken, SkillType.Weaponcraft)
                .Level(85)
                .Category(RecipeCategoryType.Throwing)
                .NormalItem("nov_duel_shrkn", 25)
                .HQItem("nov_duel_shrkn_p1", 25)
                .UltraItem("nov_duel_shrkn_p2", 25);

            // Level 91
            _builder.Create(RecipeType.YagudoCryoShuriken, SkillType.Weaponcraft)
                .Level(91)
                .Category(RecipeCategoryType.Throwing)
                .NormalItem("yagudo_cryo_shr", 25);

            // Level 99
            _builder.Create(RecipeType.CometFangShuriken, SkillType.Weaponcraft)
                .Level(99)
                .Category(RecipeCategoryType.Throwing)
                .NormalItem("cometfang_shrkn", 25);
        }
    }
} 