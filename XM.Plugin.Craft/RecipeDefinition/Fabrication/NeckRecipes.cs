using System.Collections.Generic;
using Anvil.Services;
using XM.Progression.Skill;

namespace XM.Plugin.Craft.RecipeDefinition.Fabrication
{
    [ServiceBinding(typeof(IRecipeListDefinition))]
    internal class NeckRecipes : IRecipeListDefinition
    {
        private readonly RecipeBuilder _builder = new();

        public Dictionary<RecipeType, RecipeDetail> BuildRecipes()
        {
            // Neck recipes ordered by level (9-100)
            LeatherboundGorget();                       // Level 9
            PlumeCollar();                              // Level 11
            JudicatorsBadge();                          // Level 15
            VerdantScarf();                             // Level 17
            TitanScaleGorget();                         // Level 21
            RangersPendant();                           // Level 27
            AvianWhistle();                             // Level 32
            FocusedResonanceCollar();                   // Level 35
            ObsidianSilkNeckerchief();                  // Level 38
            ChitinPlatedGorget();                       // Level 39
            VerdantGorget();                            // Level 40
            PredatorFangNecklace();                     // Level 44
            TigerfangStole();                           // Level 45
            WovenHempGorget();                          // Level 47
            BeastmastersWhistle();                      // Level 49
            TitanChainGorget();                         // Level 50
            SanctifiedPhial();                          // Level 50
            DirewolfGorget();                           // Level 57
            AegisShieldPendant();                       // Level 72
            MedievalWarCollar();                        // Level 73
            ReinforcedGorget();                         // Level 80
            MohbwaWardenScarf();                        // Level 81
            AzureGorget();                              // Level 91
            WarlordsNodowa();                           // Level 99
            MoonlitArcTorque();                         // Level 99
            MythriteGorget();                           // Level 100
            RaptorBeakNecklace();                       // Level 100
            IntellectResonanceTorque();                 // Level 100

            return _builder.Build();
        }

        private void LeatherboundGorget()
        {
            _builder.Create(RecipeType.LeatherboundGorget, SkillType.Fabrication)
                .Level(9)
                .Category(RecipeCategoryType.Neck)
                .NormalItem("leatherb_gorget", 1)
                .HQItem("leatherb_gorg_p1", 1)
                .Component("beast_hide", 2)
                .Component("ether_crystal", 1)
                .Component("bond_agent", 1);
        }

        private void PlumeCollar()
        {
            _builder.Create(RecipeType.PlumeCollar, SkillType.Fabrication)
                .Level(11)
                .Category(RecipeCategoryType.Neck)
                .NormalItem("plume_collar", 1)
                .HQItem("plume_collar_p1", 1)
                .Component("sinew_strand", 1)
                .Component("ether_crystal", 1)
                .Component("bond_agent", 1);
        }

        private void JudicatorsBadge()
        {
            _builder.Create(RecipeType.JudicatorsBadge, SkillType.Fabrication)
                .Level(15)
                .Category(RecipeCategoryType.Neck)
                .NormalItem("judicator_badge", 1)
                .Component("ether_crystal", 2)
                .Component("spirit_ess", 1)
                .Component("flux_compound", 1);
        }

        private void VerdantScarf()
        {
            _builder.Create(RecipeType.VerdantScarf, SkillType.Fabrication)
                .Level(17)
                .Category(RecipeCategoryType.Neck)
                .NormalItem("verdant_scarf", 1)
                .Component("living_wood", 2)
                .Component("ferrite_core", 1)
                .Component("enhance_serum", 1);
        }

        private void TitanScaleGorget()
        {
            _builder.Create(RecipeType.TitanScaleGorget, SkillType.Fabrication)
                .Level(21)
                .Category(RecipeCategoryType.Neck)
                .NormalItem("titan_scale_gor", 1)
                .Component("ferrite_core", 2)
                .Component("circuit_matrix", 1)
                .Component("enhance_serum", 1);
        }

        private void RangersPendant()
        {
            _builder.Create(RecipeType.RangersPendant, SkillType.Fabrication)
                .Level(27)
                .Category(RecipeCategoryType.Neck)
                .NormalItem("ranger_pendant", 1)
                .Component("beast_hide", 2)
                .Component("amp_crystal", 1)
                .Component("purify_filter", 1);
        }

        private void AvianWhistle()
        {
            _builder.Create(RecipeType.AvianWhistle, SkillType.Fabrication)
                .Level(32)
                .Category(RecipeCategoryType.Neck)
                .NormalItem("avian_whistle", 1)
                .Component("sinew_strand", 2)
                .Component("amp_crystal", 1)
                .Component("purify_filter", 1);
        }

        private void FocusedResonanceCollar()
        {
            _builder.Create(RecipeType.FocusedResonanceCollar, SkillType.Fabrication)
                .Level(35)
                .Category(RecipeCategoryType.Neck)
                .NormalItem("focus_reso_colr", 1)
                .HQItem("focus_reso_p1", 1)
                .Component("beast_hide", 2)
                .Component("amp_crystal", 2)
                .Component("purify_filter", 1);
        }

        private void ObsidianSilkNeckerchief()
        {
            _builder.Create(RecipeType.ObsidianSilkNeckerchief, SkillType.Fabrication)
                .Level(38)
                .Category(RecipeCategoryType.Neck)
                .NormalItem("obsidian_silk_nc", 1)
                .Component("techno_fiber", 2)
                .Component("amp_crystal", 1)
                .Component("purify_filter", 1);
        }

        private void ChitinPlatedGorget()
        {
            _builder.Create(RecipeType.ChitinPlatedGorget, SkillType.Fabrication)
                .Level(39)
                .Category(RecipeCategoryType.Neck)
                .NormalItem("chitin_gorget", 1)
                .Component("brass_sheet", 2)
                .Component("amp_crystal", 1)
                .Component("purify_filter", 1);
        }

        private void VerdantGorget()
        {
            _builder.Create(RecipeType.VerdantGorget, SkillType.Fabrication)
                .Level(40)
                .Category(RecipeCategoryType.Neck)
                .NormalItem("verdant_gorget", 1)
                .Component("living_wood", 2)
                .Component("amp_crystal", 1)
                .Component("purify_filter", 1);
        }

        private void PredatorFangNecklace()
        {
            _builder.Create(RecipeType.PredatorFangNecklace, SkillType.Fabrication)
                .Level(44)
                .Category(RecipeCategoryType.Neck)
                .NormalItem("predator_fnk_nec", 1)
                .HQItem("pred_fang_nec_p1", 1)
                .Component("sinew_strand", 2)
                .Component("amp_crystal", 2)
                .Component("purify_filter", 1);
        }

        private void TigerfangStole()
        {
            _builder.Create(RecipeType.TigerfangStole, SkillType.Fabrication)
                .Level(45)
                .Category(RecipeCategoryType.Neck)
                .NormalItem("tigerfang_stole", 1)
                .Component("sinew_strand", 3)
                .Component("amp_crystal", 1)
                .Component("purify_filter", 1);
        }

        private void WovenHempGorget()
        {
            _builder.Create(RecipeType.WovenHempGorget, SkillType.Fabrication)
                .Level(47)
                .Category(RecipeCategoryType.Neck)
                .NormalItem("woven_hemp_gor", 1)
                .HQItem("woven_hempgr_p1", 1)
                .Component("techno_fiber", 2)
                .Component("mythrite_frag", 1)
                .Component("psi_crystal", 1)
                .Component("purify_filter", 1);
        }

        private void BeastmastersWhistle()
        {
            _builder.Create(RecipeType.BeastmastersWhistle, SkillType.Fabrication)
                .Level(49)
                .Category(RecipeCategoryType.Neck)
                .NormalItem("beast_whistle", 1)
                .Component("sinew_strand", 2)
                .Component("psi_crystal", 1)
                .Component("spirit_ess", 1);
        }

        private void TitanChainGorget()
        {
            _builder.Create(RecipeType.TitanChainGorget, SkillType.Fabrication)
                .Level(50)
                .Category(RecipeCategoryType.Neck)
                .NormalItem("titan_chain_gor", 1)
                .HQItem("titan_chain_p1", 1)
                .Component("mythrite_frag", 3)
                .Component("psi_crystal", 1)
                .Component("purify_filter", 1);
        }

        private void SanctifiedPhial()
        {
            _builder.Create(RecipeType.SanctifiedPhial, SkillType.Fabrication)
                .Level(50)
                .Category(RecipeCategoryType.Neck)
                .NormalItem("sanctified_phial", 1)
                .Component("mythrite_frag", 2)
                .Component("psi_crystal", 1)
                .Component("spirit_ess", 2);
        }

        private void DirewolfGorget()
        {
            _builder.Create(RecipeType.DirewolfGorget, SkillType.Fabrication)
                .Level(57)
                .Category(RecipeCategoryType.Neck)
                .NormalItem("direwolf_gorget", 1)
                .HQItem("direwolf_gorg_p1", 1)
                .Component("sinew_strand", 3)
                .Component("mythrite_frag", 2)
                .Component("psi_crystal", 1);
        }

        private void AegisShieldPendant()
        {
            _builder.Create(RecipeType.AegisShieldPendant, SkillType.Fabrication)
                .Level(72)
                .Category(RecipeCategoryType.Neck)
                .NormalItem("aegis_shield_pnd", 1)
                .Component("mythrite_frag", 3)
                .Component("psi_crystal", 2)
                .Component("spirit_ess", 1);
        }

        private void MedievalWarCollar()
        {
            _builder.Create(RecipeType.MedievalWarCollar, SkillType.Fabrication)
                .Level(73)
                .Category(RecipeCategoryType.Neck)
                .NormalItem("medieval_war_col", 1)
                .Component("mythrite_frag", 3)
                .Component("psi_crystal", 1)
                .Component("purify_filter", 1);
        }

        private void ReinforcedGorget()
        {
            _builder.Create(RecipeType.ReinforcedGorget, SkillType.Fabrication)
                .Level(80)
                .Category(RecipeCategoryType.Neck)
                .NormalItem("reinforce_gorget", 1)
                .HQItem("reinforce_gorg_p1", 1)
                .Component("titan_plate", 2)
                .Component("quantum_proc", 1)
                .Component("harmonic_alloy", 1);
        }

        private void MohbwaWardenScarf()
        {
            _builder.Create(RecipeType.MohbwaWardenScarf, SkillType.Fabrication)
                .Level(81)
                .Category(RecipeCategoryType.Neck)
                .NormalItem("mohbwa_ward_scar", 1)
                .HQItem("mohbwa_wardsc_p1", 1)
                .Component("techno_fiber", 3)
                .Component("quantum_proc", 1)
                .Component("nano_enchant", 2);
        }

        private void AzureGorget()
        {
            _builder.Create(RecipeType.AzureGorget, SkillType.Fabrication)
                .Level(91)
                .Category(RecipeCategoryType.Neck)
                .NormalItem("azure_gorget", 1)
                .Component("titan_plate", 3)
                .Component("quantum_proc", 2)
                .Component("nano_enchant", 2);
        }

        private void WarlordsNodowa()
        {
            _builder.Create(RecipeType.WarlordsNodowa, SkillType.Fabrication)
                .Level(99)
                .Category(RecipeCategoryType.Neck)
                .NormalItem("warlord_nodowa", 1)
                .HQItem("warlord_nodo_p1", 1)
                .Component("titan_plate", 3)
                .Component("quantum_proc", 3)
                .Component("nano_enchant", 2)
                .Component("sync_core", 1);
        }

        private void MoonlitArcTorque()
        {
            _builder.Create(RecipeType.MoonlitArcTorque, SkillType.Fabrication)
                .Level(99)
                .Category(RecipeCategoryType.Neck)
                .NormalItem("moonlit_arc_torq", 1)
                .Component("titan_plate", 3)
                .Component("quantum_proc", 2)
                .Component("nano_enchant", 3);
        }

        private void MythriteGorget()
        {
            _builder.Create(RecipeType.MythriteGorget, SkillType.Fabrication)
                .Level(100)
                .Category(RecipeCategoryType.Neck)
                .NormalItem("mythrite_gorget", 1)
                .HQItem("mythrite_gorg_p1", 1)
                .Component("titan_plate", 4)
                .Component("quantum_proc", 3)
                .Component("nano_enchant", 2)
                .Component("harmonic_alloy", 1);
        }

        private void RaptorBeakNecklace()
        {
            _builder.Create(RecipeType.RaptorBeakNecklace, SkillType.Fabrication)
                .Level(100)
                .Category(RecipeCategoryType.Neck)
                .NormalItem("raptor_beak_neck", 1)
                .HQItem("raptor_beak_p1", 1)
                .Component("sinew_strand", 4)
                .Component("titan_plate", 3)
                .Component("quantum_proc", 3)
                .Component("sync_core", 1);
        }

        private void IntellectResonanceTorque()
        {
            _builder.Create(RecipeType.IntellectResonanceTorque, SkillType.Fabrication)
                .Level(100)
                .Category(RecipeCategoryType.Neck)
                .NormalItem("intellect_res_tq", 1)
                .Component("titan_plate", 4)
                .Component("quantum_proc", 4)
                .Component("nano_enchant", 3);
        }
    }
}