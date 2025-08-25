using System.Collections.Generic;
using Anvil.Services;
using XM.Progression.Skill;

namespace XM.Plugin.Craft.RecipeDefinition.Weaponcraft
{
    [ServiceBinding(typeof(IRecipeListDefinition))]
    internal class GreatAxeRecipes : IRecipeListDefinition
    {
        private readonly RecipeBuilder _builder = new();

        public Dictionary<RecipeType, RecipeDetail> BuildRecipes()
        {
            // Great Axes ordered by level (9-100)
            TraineeWaraxe();             // Level 9
            RazorwingAxe();              // Level 11
            InfernalCleaver();           // Level 15
            TitanGreataxe();             // Level 24
            HydroCleaver();              // Level 26
            MothfangAxe();               // Level 37
            VanguardSplitter();          // Level 48
            VerdantSlayer();             // Level 51
            CenturionsCleaver();         // Level 57
            TwinfangAxe();               // Level 61
            ReapersVoulge();             // Level 69
            TitanWaraxe();               // Level 72
            KhetenWarblade();            // Level 83
            JuggernautMothAxe();         // Level 89
            TewhaReaver();               // Level 91
            LeucosReapersVoulge();       // Level 100

            return _builder.Build();
        }

        private void TraineeWaraxe()
        {
            _builder.Create(RecipeType.TraineeWaraxe, SkillType.Weaponcraft)
                .Level(9)
                .Category(RecipeCategoryType.GreatAxe)
                .NormalItem("trainee_waraxe", 1)
                .Component("aurion_ingot", 4)
                .Component("beast_hide", 2)
                .Component("flux_compound", 1);
        }

        private void RazorwingAxe()
        {
            _builder.Create(RecipeType.RazorwingAxe, SkillType.Weaponcraft)
                .Level(11)
                .Category(RecipeCategoryType.GreatAxe)
                .NormalItem("razorw_axe", 1)
                .HQItem("razorw_axe_p1", 1)
                .Component("aurion_ingot", 4)
                .Component("beast_hide", 2)
                .Component("flux_compound", 1);
        }

        private void InfernalCleaver()
        {
            _builder.Create(RecipeType.InfernalCleaver, SkillType.Weaponcraft)
                .Level(15)
                .Category(RecipeCategoryType.GreatAxe)
                .NormalItem("infernal_cleavr", 1)
                .HQItem("hellfire_clvr", 1)
                .Component("aurion_ingot", 4)
                .Component("beast_hide", 2)
                .Component("flux_compound", 1)
                .Component("plasma_conduit", 1);
        }

        private void TitanGreataxe()
        {
            _builder.Create(RecipeType.TitanGreataxe, SkillType.Weaponcraft)
                .Level(24)
                .Category(RecipeCategoryType.GreatAxe)
                .NormalItem("titan_greatax", 1)
                .HQItem("titan_greatax_p1", 1)
                .Component("ferrite_core", 4)
                .Component("circuit_matrix", 2)
                .Component("power_cell", 1);
        }

        private void HydroCleaver()
        {
            _builder.Create(RecipeType.HydroCleaver, SkillType.Weaponcraft)
                .Level(26)
                .Category(RecipeCategoryType.GreatAxe)
                .NormalItem("hydro_cleavr", 1)
                .HQItem("hydro_cleavr_p1", 1)
                .Component("brass_sheet", 4)
                .Component("mythrite_frag", 2)
                .Component("enhance_serum", 1);
        }

        private void MothfangAxe()
        {
            _builder.Create(RecipeType.MothfangAxe, SkillType.Weaponcraft)
                .Level(37)
                .Category(RecipeCategoryType.GreatAxe)
                .NormalItem("mothfang_axe", 1)
                .Component("brass_sheet", 4)
                .Component("mythrite_frag", 2)
                .Component("enhance_serum", 1);
        }

        private void VanguardSplitter()
        {
            _builder.Create(RecipeType.VanguardSplitter, SkillType.Weaponcraft)
                .Level(48)
                .Category(RecipeCategoryType.GreatAxe)
                .NormalItem("vang_splittr", 1)
                .Component("mythrite_frag", 5)
                .Component("psi_crystal", 2)
                .Component("biosteel_comp", 1);
        }

        private void VerdantSlayer()
        {
            _builder.Create(RecipeType.VerdantSlayer, SkillType.Weaponcraft)
                .Level(51)
                .Category(RecipeCategoryType.GreatAxe)
                .NormalItem("verdant_slyr", 1)
                .Component("mythrite_frag", 5)
                .Component("psi_crystal", 2)
                .Component("biosteel_comp", 1);
        }

        private void CenturionsCleaver()
        {
            _builder.Create(RecipeType.CenturionsCleaver, SkillType.Weaponcraft)
                .Level(57)
                .Category(RecipeCategoryType.GreatAxe)
                .NormalItem("centur_clvr", 1)
                .Component("mythrite_frag", 5)
                .Component("psi_crystal", 2)
                .Component("biosteel_comp", 1);
        }

        private void TwinfangAxe()
        {
            _builder.Create(RecipeType.TwinfangAxe, SkillType.Weaponcraft)
                .Level(61)
                .Category(RecipeCategoryType.GreatAxe)
                .NormalItem("twinfang_axe", 1)
                .Component("mythrite_frag", 5)
                .Component("psi_crystal", 2)
                .Component("biosteel_comp", 1);
        }

        private void ReapersVoulge()
        {
            _builder.Create(RecipeType.ReapersVoulge, SkillType.Weaponcraft)
                .Level(69)
                .Category(RecipeCategoryType.GreatAxe)
                .NormalItem("reaper_voulg", 1)
                .HQItem("reaper_voulg_p1", 1)
                .Component("mythrite_frag", 5)
                .Component("psi_crystal", 2)
                .Component("biosteel_comp", 1);
        }

        private void TitanWaraxe()
        {
            _builder.Create(RecipeType.TitanWaraxe, SkillType.Weaponcraft)
                .Level(72)
                .Category(RecipeCategoryType.GreatAxe)
                .NormalItem("titan_waraxe", 1)
                .HQItem("titan_waraxe_p1", 1)
                .Component("mythrite_frag", 5)
                .Component("psi_crystal", 2)
                .Component("biosteel_comp", 1);
        }

        private void KhetenWarblade()
        {
            _builder.Create(RecipeType.KhetenWarblade, SkillType.Weaponcraft)
                .Level(83)
                .Category(RecipeCategoryType.GreatAxe)
                .NormalItem("kheten_wrbl", 1)
                .HQItem("kheten_wrbl_p1", 1)
                .Component("titan_plate", 5)
                .Component("quantum_proc", 2)
                .Component("quantmyst_core", 1);
        }

        private void JuggernautMothAxe()
        {
            _builder.Create(RecipeType.JuggernautMothAxe, SkillType.Weaponcraft)
                .Level(89)
                .Category(RecipeCategoryType.GreatAxe)
                .NormalItem("jugger_mothaxe", 1)
                .Component("titan_plate", 5)
                .Component("quantum_proc", 2)
                .Component("quantmyst_core", 1);
        }

        private void TewhaReaver()
        {
            _builder.Create(RecipeType.TewhaReaver, SkillType.Weaponcraft)
                .Level(91)
                .Category(RecipeCategoryType.GreatAxe)
                .NormalItem("tewha_reaver", 1)
                .HQItem("tewha_reavr_p1", 1)
                .Component("titan_plate", 5)
                .Component("quantum_proc", 2)
                .Component("quantmyst_core", 1);
        }

        private void LeucosReapersVoulge()
        {
            _builder.Create(RecipeType.LeucosReapersVoulge, SkillType.Weaponcraft)
                .Level(100)
                .Category(RecipeCategoryType.GreatAxe)
                .NormalItem("leucos_reap_vlg", 1)
                .HQItem("leucos_reap_vlg_p1", 1)
                .Component("titan_plate", 5)
                .Component("quantum_proc", 2)
                .Component("quantmyst_core", 1);
        }
    }
}