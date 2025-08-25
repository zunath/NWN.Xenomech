using System.Collections.Generic;
using Anvil.Services;
using XM.Progression.Skill;

namespace XM.Plugin.Craft.RecipeDefinition.Weaponcraft
{
    [ServiceBinding(typeof(IRecipeListDefinition))]
    internal class GreatswordRecipes : IRecipeListDefinition
    {
        private readonly RecipeBuilder _builder = new();

        public Dictionary<RecipeType, RecipeDetail> BuildRecipes()
        {
            // Greatswords ordered by level (3-94)
            CorrodedGreatblade();        // Level 3
            TitanClaymore();             // Level 12
            GaleforgedClaymore();        // Level 13
            VulcanWarblade();            // Level 17
            MercenarysFang();            // Level 23
            VanguardBattalionSword();    // Level 25
            UnmarkedGreatsword();        // Level 29
            WarbornBlade();              // Level 33
            ZephyrCutter();              // Level 39
            AbyssalBlade();              // Level 48
            ReapersFalx();               // Level 63
            MythriteClaymore();          // Level 65
            RegalVanguardBlade();        // Level 73
            TitanGreatsword();           // Level 76
            ZephyrWarblade();            // Level 89
            ExecutionersFaussar();       // Level 90
            CobraWarblade();             // Level 94

            return _builder.Build();
        }

        private void CorrodedGreatblade()
        {
            _builder.Create(RecipeType.CorrodedGreatblade, SkillType.Weaponcraft)
                .Level(3)
                .Category(RecipeCategoryType.GreatSword)
                .NormalItem("corrod_grtblad", 1)
                .Component("aurion_ingot", 4)
                .Component("beast_hide", 2)
                .Component("flux_compound", 1);
        }

        private void TitanClaymore()
        {
            _builder.Create(RecipeType.TitanClaymore, SkillType.Weaponcraft)
                .Level(12)
                .Category(RecipeCategoryType.GreatSword)
                .NormalItem("titan_claymr", 1)
                .HQItem("titan_claymr_p1", 1)
                .Component("aurion_ingot", 4)
                .Component("beast_hide", 2)
                .Component("flux_compound", 1);
        }

        private void GaleforgedClaymore()
        {
            _builder.Create(RecipeType.GaleforgedClaymore, SkillType.Weaponcraft)
                .Level(13)
                .Category(RecipeCategoryType.GreatSword)
                .NormalItem("galef_claymr", 1)
                .HQItem("galef_claymr_p1", 1)
                .Component("aurion_ingot", 4)
                .Component("beast_hide", 2)
                .Component("flux_compound", 1);
        }

        private void VulcanWarblade()
        {
            _builder.Create(RecipeType.VulcanWarblade, SkillType.Weaponcraft)
                .Level(17)
                .Category(RecipeCategoryType.GreatSword)
                .NormalItem("vulcan_warblad", 1)
                .Component("ferrite_core", 4)
                .Component("circuit_matrix", 2)
                .Component("power_cell", 1)
                .Component("plasma_conduit", 1);
        }

        private void MercenarysFang()
        {
            _builder.Create(RecipeType.MercenarysFang, SkillType.Weaponcraft)
                .Level(23)
                .Category(RecipeCategoryType.GreatSword)
                .NormalItem("mercen_fang", 1)
                .Component("ferrite_core", 4)
                .Component("circuit_matrix", 2)
                .Component("power_cell", 1);
        }

        private void VanguardBattalionSword()
        {
            _builder.Create(RecipeType.VanguardBattalionSword, SkillType.Weaponcraft)
                .Level(25)
                .Category(RecipeCategoryType.GreatSword)
                .NormalItem("vang_battswrd", 1)
                .Component("ferrite_core", 4)
                .Component("circuit_matrix", 2)
                .Component("power_cell", 1);
        }

        private void UnmarkedGreatsword()
        {
            _builder.Create(RecipeType.UnmarkedGreatsword, SkillType.Weaponcraft)
                .Level(29)
                .Category(RecipeCategoryType.GreatSword)
                .NormalItem("unmrk_greatswd", 1)
                .Component("brass_sheet", 4)
                .Component("mythrite_frag", 2)
                .Component("enhance_serum", 1);
        }

        private void WarbornBlade()
        {
            _builder.Create(RecipeType.WarbornBlade, SkillType.Weaponcraft)
                .Level(33)
                .Category(RecipeCategoryType.GreatSword)
                .NormalItem("warborn_blade", 1)
                .HQItem("warborn_blade_p1", 1)
                .Component("brass_sheet", 4)
                .Component("mythrite_frag", 2)
                .Component("enhance_serum", 1);
        }

        private void ZephyrCutter()
        {
            _builder.Create(RecipeType.ZephyrCutter, SkillType.Weaponcraft)
                .Level(39)
                .Category(RecipeCategoryType.GreatSword)
                .NormalItem("zephyr_cutter", 1)
                .HQItem("dominion_wrbl", 1)
                .Component("brass_sheet", 4)
                .Component("mythrite_frag", 2)
                .Component("enhance_serum", 1);
        }

        private void AbyssalBlade()
        {
            _builder.Create(RecipeType.AbyssalBlade, SkillType.Weaponcraft)
                .Level(48)
                .Category(RecipeCategoryType.GreatSword)
                .NormalItem("abyssal_bld", 1)
                .Component("mythrite_frag", 5)
                .Component("psi_crystal", 2)
                .Component("biosteel_comp", 1);
        }

        private void ReapersFalx()
        {
            _builder.Create(RecipeType.ReapersFalx, SkillType.Weaponcraft)
                .Level(63)
                .Category(RecipeCategoryType.GreatSword)
                .NormalItem("reaper_falx", 1)
                .HQItem("reaper_falx_p1", 1)
                .Component("mythrite_frag", 5)
                .Component("psi_crystal", 2)
                .Component("biosteel_comp", 1);
        }

        private void MythriteClaymore()
        {
            _builder.Create(RecipeType.MythriteClaymore, SkillType.Weaponcraft)
                .Level(65)
                .Category(RecipeCategoryType.GreatSword)
                .NormalItem("mythrite_clymr", 1)
                .Component("mythrite_frag", 5)
                .Component("psi_crystal", 2)
                .Component("biosteel_comp", 1);
        }

        private void RegalVanguardBlade()
        {
            _builder.Create(RecipeType.RegalVanguardBlade, SkillType.Weaponcraft)
                .Level(73)
                .Category(RecipeCategoryType.GreatSword)
                .NormalItem("regal_vangbl", 1)
                .HQItem("regal_vangbl_p1", 1)
                .UltraItem("regal_vangbl_p2", 1)
                .Component("mythrite_frag", 5)
                .Component("psi_crystal", 2)
                .Component("biosteel_comp", 1);
        }

        private void TitanGreatsword()
        {
            _builder.Create(RecipeType.TitanGreatsword, SkillType.Weaponcraft)
                .Level(76)
                .Category(RecipeCategoryType.GreatSword)
                .NormalItem("titan_grtswd", 1)
                .HQItem("titan_grtswd_p1", 1)
                .Component("titan_plate", 5)
                .Component("quantum_proc", 2)
                .Component("quantmyst_core", 1);
        }

        private void ZephyrWarblade()
        {
            _builder.Create(RecipeType.ZephyrWarblade, SkillType.Weaponcraft)
                .Level(89)
                .Category(RecipeCategoryType.GreatSword)
                .NormalItem("zephyr_wrbl", 1)
                .HQItem("zephyr_wrbl_p1", 1)
                .Component("titan_plate", 5)
                .Component("quantum_proc", 2)
                .Component("quantmyst_core", 1);
        }

        private void ExecutionersFaussar()
        {
            _builder.Create(RecipeType.ExecutionersFaussar, SkillType.Weaponcraft)
                .Level(90)
                .Category(RecipeCategoryType.GreatSword)
                .NormalItem("exec_faussar", 1)
                .HQItem("exec_faussr_p1", 1)
                .Component("titan_plate", 5)
                .Component("quantum_proc", 2)
                .Component("quantmyst_core", 1);
        }

        private void CobraWarblade()
        {
            _builder.Create(RecipeType.CobraWarblade, SkillType.Weaponcraft)
                .Level(94)
                .Category(RecipeCategoryType.GreatSword)
                .NormalItem("cobra_wrbl", 1)
                .Component("titan_plate", 5)
                .Component("quantum_proc", 2)
                .Component("quantmyst_core", 1);
        }
    }
}