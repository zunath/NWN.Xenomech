using System.Collections.Generic;
using Anvil.Services;
using XM.Progression.Skill;

namespace XM.Plugin.Craft.RecipeDefinition.Weaponcraft
{
    [ServiceBinding(typeof(IRecipeListDefinition))]
    internal class ClawRecipes : IRecipeListDefinition
    {
        private readonly RecipeBuilder _builder = new();

        public Dictionary<RecipeType, RecipeDetail> BuildRecipes()
        {
            // Claws ordered by level (2-98)
            PantherClaws();              // Level 2
            EtherCesti();                // Level 2
            AurionAlloyKnuckles();       // Level 10
            BrassstrikeKnuckles();       // Level 18
            TropicStrikers();            // Level 20
            BrassfangClaws();            // Level 22
            HydrofangClaws();            // Level 23
            PrecisionClaws();            // Level 28
            ZephyrClaws();               // Level 30
            ShadowfangClaws();           // Level 39
            AlloyKnuckles();             // Level 40
            PredatorClaws();             // Level 48
            RavagerClaws();              // Level 56
            HydrofangTalons();           // Level 60
            TyroWarKatars();             // Level 62
            WarKatars();                 // Level 66
            ToxinfangClaws();            // Level 67
            ToxinfangTalons();           // Level 71
            MythriteKnuckles();          // Level 74
            EtherwovenStraps();          // Level 79
            MythriteClaws();             // Level 82
            ArcstrikeAdargas();          // Level 85
            PredatorPatas();             // Level 93
            TacticianMagiciansTalons();  // Level 98

            return _builder.Build();
        }

        private void PantherClaws()
        {
            _builder.Create(RecipeType.PantherClaws, SkillType.Weaponcraft)
                .Level(2)
                .Category(RecipeCategoryType.HandToHand)
                .NormalItem("panther_claws", 1)
                .HQItem("panther_claws_p1", 1)
                .Component("chitin_plate", 3)
                .Component("sinew_strand", 2)
                .Component("bond_agent", 1);
        }

        private void EtherCesti()
        {
            _builder.Create(RecipeType.EtherCesti, SkillType.Weaponcraft)
                .Level(2)
                .Category(RecipeCategoryType.HandToHand)
                .NormalItem("ether_cesti", 1)
                .HQItem("ether_cesti_p1", 1)
                .Component("chitin_plate", 3)
                .Component("sinew_strand", 2)
                .Component("bond_agent", 1)
                .Component("ether_crystal", 1);
        }

        private void AurionAlloyKnuckles()
        {
            _builder.Create(RecipeType.AurionAlloyKnuckles, SkillType.Weaponcraft)
                .Level(10)
                .Category(RecipeCategoryType.HandToHand)
                .NormalItem("aurion_knkls", 1)
                .HQItem("aurion_knkls_p1", 1)
                .Component("chitin_plate", 3)
                .Component("sinew_strand", 2)
                .Component("bond_agent", 1);
        }

        private void BrassstrikeKnuckles()
        {
            _builder.Create(RecipeType.BrassstrikeKnuckles, SkillType.Weaponcraft)
                .Level(18)
                .Category(RecipeCategoryType.HandToHand)
                .NormalItem("brass_knkls", 1)
                .HQItem("brass_knkls_p1", 1)
                .Component("chitin_plate", 3)
                .Component("techno_fiber", 2)
                .Component("power_cell", 1);
        }

        private void TropicStrikers()
        {
            _builder.Create(RecipeType.TropicStrikers, SkillType.Weaponcraft)
                .Level(20)
                .Category(RecipeCategoryType.HandToHand)
                .NormalItem("tropic_strikr", 1)
                .HQItem("tropic_strkr_p1", 1)
                .Component("chitin_plate", 3)
                .Component("techno_fiber", 2)
                .Component("power_cell", 1);
        }

        private void BrassfangClaws()
        {
            _builder.Create(RecipeType.BrassfangClaws, SkillType.Weaponcraft)
                .Level(22)
                .Category(RecipeCategoryType.HandToHand)
                .NormalItem("brassfang_cls", 1)
                .HQItem("brassfang_cls_p1", 1)
                .Component("chitin_plate", 3)
                .Component("techno_fiber", 2)
                .Component("power_cell", 1);
        }

        private void HydrofangClaws()
        {
            _builder.Create(RecipeType.HydrofangClaws, SkillType.Weaponcraft)
                .Level(23)
                .Category(RecipeCategoryType.HandToHand)
                .NormalItem("hydrofang_cls", 1)
                .HQItem("hydrofang_cls_p1", 1)
                .Component("chitin_plate", 3)
                .Component("techno_fiber", 2)
                .Component("power_cell", 1);
        }

        private void PrecisionClaws()
        {
            _builder.Create(RecipeType.PrecisionClaws, SkillType.Weaponcraft)
                .Level(28)
                .Category(RecipeCategoryType.HandToHand)
                .NormalItem("precis_claws", 1)
                .Component("chitin_plate", 4)
                .Component("sinew_strand", 2)
                .Component("mythrite_frag", 1);
        }

        private void ZephyrClaws()
        {
            _builder.Create(RecipeType.ZephyrClaws, SkillType.Weaponcraft)
                .Level(30)
                .Category(RecipeCategoryType.HandToHand)
                .NormalItem("zephyr_claws", 1)
                .Component("chitin_plate", 4)
                .Component("sinew_strand", 2)
                .Component("mythrite_frag", 1);
        }

        private void ShadowfangClaws()
        {
            _builder.Create(RecipeType.ShadowfangClaws, SkillType.Weaponcraft)
                .Level(39)
                .Category(RecipeCategoryType.HandToHand)
                .NormalItem("shdwfang_cls", 1)
                .HQItem("shdwfang_cls_p1", 1)
                .Component("chitin_plate", 4)
                .Component("sinew_strand", 2)
                .Component("mythrite_frag", 1)
                .Component("void_shard", 1);
        }

        private void AlloyKnuckles()
        {
            _builder.Create(RecipeType.AlloyKnuckles, SkillType.Weaponcraft)
                .Level(40)
                .Category(RecipeCategoryType.HandToHand)
                .NormalItem("alloy_knkls", 1)
                .HQItem("alloy_knkls_p1", 1)
                .Component("chitin_plate", 4)
                .Component("sinew_strand", 2)
                .Component("mythrite_frag", 1);
        }

        private void PredatorClaws()
        {
            _builder.Create(RecipeType.PredatorClaws, SkillType.Weaponcraft)
                .Level(48)
                .Category(RecipeCategoryType.HandToHand)
                .NormalItem("predator_cls", 1)
                .HQItem("predator_cls_p1", 1)
                .Component("chitin_plate", 4)
                .Component("biosteel_comp", 2)
                .Component("psi_crystal", 1);
        }

        private void RavagerClaws()
        {
            _builder.Create(RecipeType.RavagerClaws, SkillType.Weaponcraft)
                .Level(56)
                .Category(RecipeCategoryType.HandToHand)
                .NormalItem("ravager_cls", 1)
                .HQItem("ravager_cls_p1", 1)
                .Component("chitin_plate", 4)
                .Component("biosteel_comp", 2)
                .Component("psi_crystal", 1);
        }

        private void HydrofangTalons()
        {
            _builder.Create(RecipeType.HydrofangTalons, SkillType.Weaponcraft)
                .Level(60)
                .Category(RecipeCategoryType.HandToHand)
                .NormalItem("hydrofang_tln", 1)
                .HQItem("hydrofang_tln_p1", 1)
                .Component("chitin_plate", 4)
                .Component("biosteel_comp", 2)
                .Component("psi_crystal", 1);
        }

        private void TyroWarKatars()
        {
            _builder.Create(RecipeType.TyroWarKatars, SkillType.Weaponcraft)
                .Level(62)
                .Category(RecipeCategoryType.HandToHand)
                .NormalItem("tyro_wrkatr", 1)
                .HQItem("tyro_wrkatr_p1", 1)
                .Component("chitin_plate", 4)
                .Component("biosteel_comp", 2)
                .Component("psi_crystal", 1);
        }

        private void WarKatars()
        {
            _builder.Create(RecipeType.WarKatars, SkillType.Weaponcraft)
                .Level(66)
                .Category(RecipeCategoryType.HandToHand)
                .NormalItem("war_katars", 1)
                .HQItem("war_katars_p1", 1)
                .Component("chitin_plate", 4)
                .Component("biosteel_comp", 2)
                .Component("psi_crystal", 1);
        }

        private void ToxinfangClaws()
        {
            _builder.Create(RecipeType.ToxinfangClaws, SkillType.Weaponcraft)
                .Level(67)
                .Category(RecipeCategoryType.HandToHand)
                .NormalItem("toxin_cls", 1)
                .HQItem("toxin_cls_p1", 1)
                .Component("chitin_plate", 4)
                .Component("biosteel_comp", 2)
                .Component("psi_crystal", 1)
                .Component("venom_sac", 1);
        }

        private void ToxinfangTalons()
        {
            _builder.Create(RecipeType.ToxinfangTalons, SkillType.Weaponcraft)
                .Level(71)
                .Category(RecipeCategoryType.HandToHand)
                .NormalItem("toxin_tlns", 1)
                .HQItem("toxin_tlns_p1", 1)
                .Component("chitin_plate", 4)
                .Component("biosteel_comp", 2)
                .Component("psi_crystal", 1)
                .Component("venom_sac", 1);
        }

        private void MythriteKnuckles()
        {
            _builder.Create(RecipeType.MythriteKnuckles, SkillType.Weaponcraft)
                .Level(74)
                .Category(RecipeCategoryType.HandToHand)
                .NormalItem("mythrite_knkls", 1)
                .HQItem("mythrite_knkls_p1", 1)
                .Component("chitin_plate", 4)
                .Component("biosteel_comp", 2)
                .Component("psi_crystal", 1);
        }

        private void EtherwovenStraps()
        {
            _builder.Create(RecipeType.EtherwovenStraps, SkillType.Weaponcraft)
                .Level(79)
                .Category(RecipeCategoryType.HandToHand)
                .NormalItem("etherwoven_stp", 1)
                .HQItem("etherwoven_stp_p1", 1)
                .Component("chitin_plate", 5)
                .Component("biosteel_comp", 3)
                .Component("quantum_proc", 1)
                .Component("ether_crystal", 1);
        }

        private void MythriteClaws()
        {
            _builder.Create(RecipeType.MythriteClaws, SkillType.Weaponcraft)
                .Level(82)
                .Category(RecipeCategoryType.HandToHand)
                .NormalItem("mythrite_cls", 1)
                .HQItem("mythrite_cls_p1", 1)
                .Component("chitin_plate", 5)
                .Component("biosteel_comp", 3)
                .Component("quantum_proc", 1);
        }

        private void ArcstrikeAdargas()
        {
            _builder.Create(RecipeType.ArcstrikeAdargas, SkillType.Weaponcraft)
                .Level(85)
                .Category(RecipeCategoryType.HandToHand)
                .NormalItem("arcstrk_adgs", 1)
                .HQItem("arcstrk_adgs_p1", 1)
                .Component("chitin_plate", 5)
                .Component("biosteel_comp", 3)
                .Component("quantum_proc", 1)
                .Component("power_cell", 1);
        }

        private void PredatorPatas()
        {
            _builder.Create(RecipeType.PredatorPatas, SkillType.Weaponcraft)
                .Level(93)
                .Category(RecipeCategoryType.HandToHand)
                .NormalItem("predator_pts", 1)
                .HQItem("predator_pts_p1", 1)
                .Component("chitin_plate", 5)
                .Component("biosteel_comp", 3)
                .Component("quantum_proc", 1);
        }

        private void TacticianMagiciansTalons()
        {
            _builder.Create(RecipeType.TacticianMagiciansTalons, SkillType.Weaponcraft)
                .Level(98)
                .Category(RecipeCategoryType.HandToHand)
                .NormalItem("tact_mag_tlns", 1)
                .Component("chitin_plate", 5)
                .Component("biosteel_comp", 3)
                .Component("quantum_proc", 1)
                .Component("ether_crystal", 1);
        }
    }
}