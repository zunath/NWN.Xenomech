using System.Collections.Generic;
using Anvil.Services;
using XM.Progression.Skill;

namespace XM.Progression.Craft.RecipeDefinition
{
    [ServiceBinding(typeof(IRecipeListDefinition))]
    internal class GreatAxeRecipes : IRecipeListDefinition
    {
        private readonly RecipeBuilder _builder = new();

        public Dictionary<RecipeType, RecipeDetail> BuildRecipes()
        {
            BuildGreatAxeRecipes();
            return _builder.Build();
        }

        private void BuildGreatAxeRecipes()
        {
            // Level 9
            _builder.Create(RecipeType.TraineeWaraxe, SkillType.Weaponcraft)
                .Level(9)
                .Category(RecipeCategoryType.GreatAxe)
                .NormalItem("trainee_waraxe", 1);

            // Level 11
            _builder.Create(RecipeType.RazorwingAxe, SkillType.Weaponcraft)
                .Level(11)
                .Category(RecipeCategoryType.GreatAxe)
                .NormalItem("razorw_axe", 1)
                .HQItem("razorw_axe_p1", 1);

            // Level 15
            _builder.Create(RecipeType.InfernalCleaver, SkillType.Weaponcraft)
                .Level(15)
                .Category(RecipeCategoryType.GreatAxe)
                .NormalItem("infernal_cleavr", 1)
                .UltraItem("hellfire_clvr", 1);

            // Level 24
            _builder.Create(RecipeType.TitanGreataxe, SkillType.Weaponcraft)
                .Level(24)
                .Category(RecipeCategoryType.GreatAxe)
                .NormalItem("titan_greatax", 1)
                .HQItem("titan_greatax_p1", 1);

            // Level 26
            _builder.Create(RecipeType.HydroCleaver, SkillType.Weaponcraft)
                .Level(26)
                .Category(RecipeCategoryType.GreatAxe)
                .NormalItem("hydro_cleavr", 1)
                .HQItem("hydro_cleavr_p1", 1);

            // Level 37
            _builder.Create(RecipeType.MothfangAxe, SkillType.Weaponcraft)
                .Level(37)
                .Category(RecipeCategoryType.GreatAxe)
                .NormalItem("mothfang_axe", 1);

            // Level 48
            _builder.Create(RecipeType.VanguardSplitter, SkillType.Weaponcraft)
                .Level(48)
                .Category(RecipeCategoryType.GreatAxe)
                .NormalItem("vang_splittr", 1);

            // Level 51
            _builder.Create(RecipeType.VerdantSlayer, SkillType.Weaponcraft)
                .Level(51)
                .Category(RecipeCategoryType.GreatAxe)
                .NormalItem("verdant_slyr", 1);

            // Level 57
            _builder.Create(RecipeType.CenturionCleaver, SkillType.Weaponcraft)
                .Level(57)
                .Category(RecipeCategoryType.GreatAxe)
                .NormalItem("centur_clvr", 1);

            // Level 61
            _builder.Create(RecipeType.TwinfangAxe, SkillType.Weaponcraft)
                .Level(61)
                .Category(RecipeCategoryType.GreatAxe)
                .NormalItem("twinfang_axe", 1);

            // Level 69
            _builder.Create(RecipeType.ReapersVoulge, SkillType.Weaponcraft)
                .Level(69)
                .Category(RecipeCategoryType.GreatAxe)
                .NormalItem("reaper_voulg", 1)
                .HQItem("reaper_voulg_p1", 1);

            // Level 71
            _builder.Create(RecipeType.HydroCleaver2, SkillType.Weaponcraft)
                .Level(71)
                .Category(RecipeCategoryType.GreatAxe)
                .NormalItem("hydro_cleavr", 1)
                .HQItem("hydro_cleavr_p1", 1);

            // Level 72
            _builder.Create(RecipeType.TitanWaraxe, SkillType.Weaponcraft)
                .Level(72)
                .Category(RecipeCategoryType.GreatAxe)
                .NormalItem("titan_waraxe", 1)
                .HQItem("titan_waraxe_p1", 1);

            // Level 83
            _builder.Create(RecipeType.KhetenWarblade, SkillType.Weaponcraft)
                .Level(83)
                .Category(RecipeCategoryType.GreatAxe)
                .NormalItem("kheten_wrbl", 1)
                .HQItem("kheten_wrbl_p1", 1);

            // Level 89
            _builder.Create(RecipeType.JuggernautMothAxe, SkillType.Weaponcraft)
                .Level(89)
                .Category(RecipeCategoryType.GreatAxe)
                .NormalItem("jugger_mothaxe", 1);

            // Level 91
            _builder.Create(RecipeType.TewhaReaver, SkillType.Weaponcraft)
                .Level(91)
                .Category(RecipeCategoryType.GreatAxe)
                .NormalItem("tewha_reaver", 1)
                .HQItem("tewha_reavr_p1", 1);

            // Level 100
            _builder.Create(RecipeType.LeucosReapersVoulge, SkillType.Weaponcraft)
                .Level(100)
                .Category(RecipeCategoryType.GreatAxe)
                .NormalItem("leucos_reap_vlg", 1)
                .HQItem("leucos_reap_vlg_p1", 1);
        }
    }
} 