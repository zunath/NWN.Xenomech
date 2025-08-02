using System.Collections.Generic;
using Anvil.Services;
using XM.Progression.Skill;

namespace XM.Progression.Craft.RecipeDefinition
{
    [ServiceBinding(typeof(IRecipeListDefinition))]
    internal class GreatSwordRecipes : IRecipeListDefinition
    {
        private readonly RecipeBuilder _builder = new();

        public Dictionary<RecipeType, RecipeDetail> BuildRecipes()
        {
            BuildGreatSwordRecipes();
            return _builder.Build();
        }

        private void BuildGreatSwordRecipes()
        {
            // Level 3
            _builder.Create(RecipeType.CorrodedGreatblade, SkillType.Weaponcraft)
                .Level(3)
                .Category(RecipeCategoryType.GreatSword)
                .NormalItem("corrod_grtblad", 1);

            // Level 12
            _builder.Create(RecipeType.TitanClaymore, SkillType.Weaponcraft)
                .Level(12)
                .Category(RecipeCategoryType.GreatSword)
                .NormalItem("titan_claymr", 1)
                .HQItem("titan_claymr_p1", 1);

            // Level 13
            _builder.Create(RecipeType.GaleforgedClaymore, SkillType.Weaponcraft)
                .Level(13)
                .Category(RecipeCategoryType.GreatSword)
                .NormalItem("galef_claymr", 1)
                .HQItem("galef_claymr_p1", 1);

            // Level 17
            _builder.Create(RecipeType.VulcanWarblade, SkillType.Weaponcraft)
                .Level(17)
                .Category(RecipeCategoryType.GreatSword)
                .NormalItem("vulcan_warblad", 1);

            // Level 25
            _builder.Create(RecipeType.VanguardBattalionSword, SkillType.Weaponcraft)
                .Level(25)
                .Category(RecipeCategoryType.GreatSword)
                .NormalItem("vang_battswrd", 1);

            // Level 29
            _builder.Create(RecipeType.UnmarkedGreatsword, SkillType.Weaponcraft)
                .Level(29)
                .Category(RecipeCategoryType.GreatSword)
                .NormalItem("unmrk_greatswd", 1);

            // Level 33
            _builder.Create(RecipeType.WarbornBlade, SkillType.Weaponcraft)
                .Level(33)
                .Category(RecipeCategoryType.GreatSword)
                .NormalItem("warborn_blade", 1)
                .HQItem("warborn_blade_p1", 1);

            // Level 39
            _builder.Create(RecipeType.ZephyrCutter, SkillType.Weaponcraft)
                .Level(39)
                .Category(RecipeCategoryType.GreatSword)
                .NormalItem("zephyr_cutter", 1)
                .UltraItem("dominion_wrbl", 1);

            // Level 48
            _builder.Create(RecipeType.AbyssalBlade, SkillType.Weaponcraft)
                .Level(48)
                .Category(RecipeCategoryType.GreatSword)
                .NormalItem("abyssal_bld", 1);

            // Level 63
            _builder.Create(RecipeType.ReapersFalx, SkillType.Weaponcraft)
                .Level(63)
                .Category(RecipeCategoryType.GreatSword)
                .NormalItem("reaper_falx", 1)
                .HQItem("reaper_falx_p1", 1);

            // Level 65
            _builder.Create(RecipeType.MythriteClaymore, SkillType.Weaponcraft)
                .Level(65)
                .Category(RecipeCategoryType.GreatSword)
                .NormalItem("mythrite_clymr", 1);

            // Level 73
            _builder.Create(RecipeType.RegalVanguardBlade, SkillType.Weaponcraft)
                .Level(73)
                .Category(RecipeCategoryType.GreatSword)
                .NormalItem("regal_vangbl", 1)
                .HQItem("regal_vangbl_p1", 1)
                .UltraItem("regal_vangbl_p2", 1);

            // Level 76
            _builder.Create(RecipeType.TitanGreatsword, SkillType.Weaponcraft)
                .Level(76)
                .Category(RecipeCategoryType.GreatSword)
                .NormalItem("titan_grtswd", 1)
                .HQItem("titan_grtswd_p1", 1);

            // Level 89
            _builder.Create(RecipeType.ZephyrWarblade, SkillType.Weaponcraft)
                .Level(89)
                .Category(RecipeCategoryType.GreatSword)
                .NormalItem("zephyr_wrbl", 1)
                .HQItem("zephyr_wrbl_p1", 1);

            // Level 90
            _builder.Create(RecipeType.ExecutionersFaussar, SkillType.Weaponcraft)
                .Level(90)
                .Category(RecipeCategoryType.GreatSword)
                .NormalItem("exec_faussar", 1)
                .HQItem("exec_faussr_p1", 1);

            // Level 94
            _builder.Create(RecipeType.CobraWarblade, SkillType.Weaponcraft)
                .Level(94)
                .Category(RecipeCategoryType.GreatSword)
                .NormalItem("cobra_wrbl", 1);
        }
    }
} 