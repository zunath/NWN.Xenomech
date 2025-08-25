using System.Collections.Generic;
using Anvil.Services;
using XM.Progression.Skill;

namespace XM.Plugin.Craft.RecipeDefinition.Armorcraft
{
    [ServiceBinding(typeof(IRecipeListDefinition))]
    internal class BodyRecipes : IRecipeListDefinition
    {
        private readonly RecipeBuilder _builder = new();

        public Dictionary<RecipeType, RecipeDetail> BuildRecipes()
        {
            // Body Armor ordered by level (2-100)
            AurionAlloyHarness();                   // Level 2
            EtherwovenRobe();                       // Level 3
            LeatherCombatVest();                    // Level 12
            WarwovenKenpogi();                      // Level 16
            ReinforcedTunic();                      // Level 18
            BrassguardHarness();                    // Level 20
            ReinforcedDoublet();                    // Level 22
            TitanScaleMail();                       // Level 23
            EtherwovenLinenRobe();                  // Level 23
            LizardscaleJerkin();                    // Level 34
            BoneforgedHarness();                    // Level 35
            CottonWovenDogi();                      // Level 37
            ObsidianTunic();                        // Level 38
            ChitinPlatedHarness();                  // Level 42
            TitanChainmail();                       // Level 47
            CottonfieldDoublet();                   // Level 49
            BrassguardScaleMail();                  // Level 52
            WarWovenWoolRobe();                     // Level 56
            Eisenbreastplate();                     // Level 56
            OraclesTunic();                         // Level 57
            NoctshadowDoublet();                    // Level 57
            EarthforgedGi();                        // Level 58
            ReinforcedStuddedVest();                // Level 62
            WarcastersCloak();                      // Level 65
            EtherwovenDoublet();                    // Level 66
            BishopsSanctifiedRobe();                // Level 67
            PaddedCombatArmor();                    // Level 69
            FortifiedGambison();                    // Level 69
            RegalVelvetRobe();                      // Level 73
            SilverguardMail();                      // Level 74
            IroncladScaleMail();                    // Level 74
            BattleHardenedCuirass();                // Level 77
            WarcastersCloak2();                     // Level 79
            IronMusketeersWarGambison();            // Level 79
            RegalSquiresChainmail();                // Level 80
            RegalSquiresEtherRobe();                // Level 81
            TitanBreastplate();                     // Level 82
            DivineAegisBreastplate();               // Level 82
            WoolenCombatDoublet();                  // Level 87
            ReinforcedBrigandine();                 // Level 87
            BandedWarMail();                        // Level 89
            ChitinPlatedHarness2();                 // Level 90
            WoolenWarGambison();                    // Level 95
            MythriteBreastplate();                  // Level 95
            ObsidianCrowJupon();                    // Level 97
            SanctifiedWhiteCloak();                 // Level 98
            DinohideJerkin();                       // Level 99
            SteelguardScaleMail();                  // Level 99
            ShinobiShadowGi();                      // Level 100

            return _builder.Build();
        }

        private void AurionAlloyHarness()
        {
            _builder.Create(RecipeType.AurionAlloyHarness, SkillType.Armorcraft)
                .Level(2)
                .Category(RecipeCategoryType.Body)
                .NormalItem("aurion_harness", 1)
                .HQItem("aurion_harnes_p1", 1)
                .Component("aurion_ingot", 4)
                .Component("techno_fiber", 2)
                .Component("flux_compound", 1);
        }

        private void EtherwovenRobe()
        {
            _builder.Create(RecipeType.EtherwovenRobe, SkillType.Armorcraft)
                .Level(3)
                .Category(RecipeCategoryType.Body)
                .NormalItem("etherwvn_robe", 1)
                .HQItem("etherwvn_robe_p1", 1)
                .Component("ether_crystal", 2)
                .Component("techno_fiber", 3)
                .Component("spirit_ess", 1)
                .Component("bond_agent", 1);
        }

        private void LeatherCombatVest()
        {
            _builder.Create(RecipeType.LeatherCombatVest, SkillType.Armorcraft)
                .Level(12)
                .Category(RecipeCategoryType.Body)
                .NormalItem("leather_comvst", 1)
                .HQItem("leather_comvst_p1", 1)
                .Component("sinew_strand", 2)
                .Component("aurion_ingot", 2)
                .Component("beast_hide", 2);
        }

        private void WarwovenKenpogi()
        {
            _builder.Create(RecipeType.WarwovenKenpogi, SkillType.Armorcraft)
                .Level(16)
                .Category(RecipeCategoryType.Body)
                .NormalItem("warwvn_kenpgi", 1)
                .HQItem("warwvn_kenpgi_p1", 1)
                .Component("techno_fiber", 2)
                .Component("ferrite_core", 2)
                .Component("circuit_matrix", 2);
        }

        private void ReinforcedTunic()
        {
            _builder.Create(RecipeType.ReinforcedTunic, SkillType.Armorcraft)
                .Level(18)
                .Category(RecipeCategoryType.Body)
                .NormalItem("reinforc_tunc", 1)
                .HQItem("reinforc_tunc_p1", 1)
                .Component("techno_fiber", 2)
                .Component("ferrite_core", 2)
                .Component("circuit_matrix", 2);
        }

        private void BrassguardHarness()
        {
            _builder.Create(RecipeType.BrassguardHarness, SkillType.Armorcraft)
                .Level(20)
                .Category(RecipeCategoryType.Body)
                .NormalItem("brassg_harness", 1)
                .HQItem("brassg_harness_p1", 1)
                .Component("living_wood", 3)
                .Component("ferrite_core", 2)
                .Component("circuit_matrix", 2);
        }

        private void ReinforcedDoublet()
        {
            _builder.Create(RecipeType.ReinforcedDoublet, SkillType.Armorcraft)
                .Level(22)
                .Category(RecipeCategoryType.Body)
                .NormalItem("reinforc_dblt", 1)
                .HQItem("reinforc_dblt_p1", 1)
                .Component("techno_fiber", 2)
                .Component("ferrite_core", 2)
                .Component("circuit_matrix", 2);
        }

        private void TitanScaleMail()
        {
            _builder.Create(RecipeType.TitanScaleMail, SkillType.Armorcraft)
                .Level(23)
                .Category(RecipeCategoryType.Body)
                .NormalItem("titan_scale_ml", 1)
                .HQItem("titan_scale_ml_p1", 1)
                .Component("ferrite_core", 3)
                .Component("circuit_matrix", 2)
                .Component("enhance_serum", 1);
        }

        private void EtherwovenLinenRobe()
        {
            _builder.Create(RecipeType.EtherwovenLinenRobe, SkillType.Armorcraft)
                .Level(23)
                .Category(RecipeCategoryType.Body)
                .NormalItem("etherwvn_lrobe", 1)
                .HQItem("etherwvn_lrobe_p1", 1)
                .Component("techno_fiber", 2)
                .Component("ferrite_core", 2)
                .Component("circuit_matrix", 2);
        }

        private void LizardscaleJerkin()
        {
            _builder.Create(RecipeType.LizardscaleJerkin, SkillType.Armorcraft)
                .Level(34)
                .Category(RecipeCategoryType.Body)
                .NormalItem("lizardsc_jerkn", 1)
                .HQItem("lizardsc_jerkn_p1", 1)
                .Component("techno_fiber", 2)
                .Component("ferrite_core", 2)
                .Component("biosteel_comp", 2);
        }

        private void BoneforgedHarness()
        {
            _builder.Create(RecipeType.BoneforgedHarness, SkillType.Armorcraft)
                .Level(35)
                .Category(RecipeCategoryType.Body)
                .NormalItem("bonefrg_harness", 1)
                .HQItem("bonefrg_harness_p1", 1)
                .Component("ferrite_core", 3)
                .Component("biosteel_comp", 2)
                .Component("neural_inter", 1);
        }

        private void CottonWovenDogi()
        {
            _builder.Create(RecipeType.CottonWovenDogi, SkillType.Armorcraft)
                .Level(37)
                .Category(RecipeCategoryType.Body)
                .NormalItem("cottonwv_dogi", 1)
                .HQItem("cottonwv_dogi_p1", 1)
                .Component("techno_fiber", 2)
                .Component("ferrite_core", 2)
                .Component("biosteel_comp", 2);
        }

        private void ObsidianTunic()
        {
            _builder.Create(RecipeType.ObsidianTunic, SkillType.Armorcraft)
                .Level(38)
                .Category(RecipeCategoryType.Body)
                .NormalItem("obsidian_tunic", 1)
                .HQItem("obsidian_tunic_p1", 1)
                .Component("techno_fiber", 2)
                .Component("ferrite_core", 2)
                .Component("biosteel_comp", 2);
        }

        private void ChitinPlatedHarness()
        {
            _builder.Create(RecipeType.ChitinPlatedHarness, SkillType.Armorcraft)
                .Level(42)
                .Category(RecipeCategoryType.Body)
                .NormalItem("chitin_mt_hrns", 1)
                .HQItem("chitin_mt_hrns_p1", 1)
                .Component("chitin_plate", 2)
                .Component("ferrite_core", 2)
                .Component("biosteel_comp", 2)
                .Component("neural_inter", 1);
        }

        private void TitanChainmail()
        {
            _builder.Create(RecipeType.TitanChainmail, SkillType.Armorcraft)
                .Level(47)
                .Category(RecipeCategoryType.Body)
                .NormalItem("titan_chainml", 1)
                .HQItem("titan_chainml_p1", 1)
                .Component("mythrite_frag", 3)
                .Component("psi_crystal", 2)
                .Component("biosteel_comp", 1)
                .Component("purify_filter", 1);
        }

        private void CottonfieldDoublet()
        {
            _builder.Create(RecipeType.CottonfieldDoublet, SkillType.Armorcraft)
                .Level(49)
                .Category(RecipeCategoryType.Body)
                .NormalItem("cottonfld_dblt", 1)
                .HQItem("cottonfld_dblt_p1", 1)
                .Component("techno_fiber", 2)
                .Component("mythrite_frag", 2)
                .Component("psi_crystal", 2)
                .Component("biosteel_comp", 1);
        }

        private void BrassguardScaleMail()
        {
            _builder.Create(RecipeType.BrassguardScaleMail, SkillType.Armorcraft)
                .Level(52)
                .Category(RecipeCategoryType.Body)
                .NormalItem("brassg_scl_ml", 1)
                .HQItem("brassg_scl_ml_p1", 1)
                .Component("living_wood", 4)
                .Component("mythrite_frag", 2)
                .Component("psi_crystal", 2)
                .Component("biosteel_comp", 1);
        }

        private void WarWovenWoolRobe()
        {
            _builder.Create(RecipeType.WarWovenWoolRobe, SkillType.Armorcraft)
                .Level(56)
                .Category(RecipeCategoryType.Body)
                .NormalItem("warwvn_woolrb", 1)
                .HQItem("warwvn_woolrb_p1", 1)
                .Component("techno_fiber", 2)
                .Component("mythrite_frag", 2)
                .Component("psi_crystal", 2)
                .Component("biosteel_comp", 1);
        }

        private void Eisenbreastplate()
        {
            _builder.Create(RecipeType.Eisenbreastplate, SkillType.Armorcraft)
                .Level(56)
                .Category(RecipeCategoryType.Body)
                .NormalItem("eisen_brestplt", 1)
                .HQItem("eisen_brestplt_p1", 1)
                .Component("mythrite_frag", 4)
                .Component("psi_crystal", 2)
                .Component("biosteel_comp", 1)
                .Component("purify_filter", 1);
        }

        private void OraclesTunic()
        {
            _builder.Create(RecipeType.OraclesTunic, SkillType.Armorcraft)
                .Level(57)
                .Category(RecipeCategoryType.Body)
                .NormalItem("oracle_tunic", 1)
                .HQItem("oracle_tunic_p1", 1)
                .Component("techno_fiber", 2)
                .Component("mythrite_frag", 2)
                .Component("psi_crystal", 2)
                .Component("biosteel_comp", 1);
        }

        private void NoctshadowDoublet()
        {
            _builder.Create(RecipeType.NoctshadowDoublet, SkillType.Armorcraft)
                .Level(57)
                .Category(RecipeCategoryType.Body)
                .NormalItem("noctshdw_dblt", 1)
                .HQItem("noctshdw_dblt_p1", 1)
                .Component("techno_fiber", 2)
                .Component("mythrite_frag", 2)
                .Component("psi_crystal", 2)
                .Component("biosteel_comp", 1);
        }

        private void EarthforgedGi()
        {
            _builder.Create(RecipeType.EarthforgedGi, SkillType.Armorcraft)
                .Level(58)
                .Category(RecipeCategoryType.Body)
                .NormalItem("earthfrg_gi", 1)
                .HQItem("earthfrg_gi_p1", 1)
                .Component("mythrite_frag", 4)
                .Component("psi_crystal", 2)
                .Component("biosteel_comp", 1)
                .Component("purify_filter", 1);
        }

        private void ReinforcedStuddedVest()
        {
            _builder.Create(RecipeType.ReinforcedStuddedVest, SkillType.Armorcraft)
                .Level(62)
                .Category(RecipeCategoryType.Body)
                .NormalItem("rein_stud_vst", 1)
                .HQItem("rein_stud_vst_p1", 1)
                .Component("mythrite_frag", 4)
                .Component("psi_crystal", 2)
                .Component("biosteel_comp", 1)
                .Component("purify_filter", 1);
        }

        private void WarcastersCloak()
        {
            _builder.Create(RecipeType.WarcastersCloak, SkillType.Armorcraft)
                .Level(65)
                .Category(RecipeCategoryType.Body)
                .NormalItem("warcast_cloak", 1)
                .HQItem("warcast_cloak_p1", 1)
                .Component("mythrite_frag", 4)
                .Component("psi_crystal", 2)
                .Component("biosteel_comp", 1)
                .Component("purify_filter", 1);
        }

        private void EtherwovenDoublet()
        {
            _builder.Create(RecipeType.EtherwovenDoublet, SkillType.Armorcraft)
                .Level(66)
                .Category(RecipeCategoryType.Body)
                .NormalItem("etherwvn_dblt", 1)
                .HQItem("etherwvn_dblt_p1", 1)
                .Component("techno_fiber", 2)
                .Component("mythrite_frag", 2)
                .Component("psi_crystal", 2)
                .Component("biosteel_comp", 1);
        }

        private void BishopsSanctifiedRobe()
        {
            _builder.Create(RecipeType.BishopsSanctifiedRobe, SkillType.Armorcraft)
                .Level(67)
                .Category(RecipeCategoryType.Body)
                .NormalItem("bishop_sanctrb", 1)
                .HQItem("bishop_sanctrb_p1", 1)
                .Component("techno_fiber", 2)
                .Component("mythrite_frag", 2)
                .Component("psi_crystal", 2)
                .Component("biosteel_comp", 1);
        }

        private void PaddedCombatArmor()
        {
            _builder.Create(RecipeType.PaddedCombatArmor, SkillType.Armorcraft)
                .Level(69)
                .Category(RecipeCategoryType.Body)
                .NormalItem("padded_com_amr", 1)
                .HQItem("padded_com_amr_p1", 1)
                .Component("circuit_matrix", 2)
                .Component("mythrite_frag", 2)
                .Component("psi_crystal", 2)
                .Component("biosteel_comp", 1);
        }

        private void FortifiedGambison()
        {
            _builder.Create(RecipeType.FortifiedGambison, SkillType.Armorcraft)
                .Level(69)
                .Category(RecipeCategoryType.Body)
                .NormalItem("fortified_gmbs", 1)
                .HQItem("fortified_gmbs_p1", 1)
                .Component("mythrite_frag", 4)
                .Component("psi_crystal", 2)
                .Component("biosteel_comp", 1)
                .Component("purify_filter", 1);
        }

        private void RegalVelvetRobe()
        {
            _builder.Create(RecipeType.RegalVelvetRobe, SkillType.Armorcraft)
                .Level(73)
                .Category(RecipeCategoryType.Body)
                .NormalItem("regal_velvetrb", 1)
                .HQItem("regal_velvetrb_p1", 1)
                .Component("techno_fiber", 2)
                .Component("mythrite_frag", 2)
                .Component("psi_crystal", 2)
                .Component("biosteel_comp", 1);
        }

        private void SilverguardMail()
        {
            _builder.Create(RecipeType.SilverguardMail, SkillType.Armorcraft)
                .Level(74)
                .Category(RecipeCategoryType.Body)
                .NormalItem("silverg_mail", 1)
                .HQItem("silverg_mail_p1", 1)
                .Component("living_wood", 4)
                .Component("mythrite_frag", 2)
                .Component("psi_crystal", 2)
                .Component("biosteel_comp", 1);
        }

        private void IroncladScaleMail()
        {
            _builder.Create(RecipeType.IroncladScaleMail, SkillType.Armorcraft)
                .Level(74)
                .Category(RecipeCategoryType.Body)
                .NormalItem("ironclad_sclml", 1)
                .HQItem("ironclad_sclml_p1", 1)
                .Component("mythrite_frag", 4)
                .Component("psi_crystal", 2)
                .Component("biosteel_comp", 1)
                .Component("purify_filter", 1);
        }

        private void BattleHardenedCuirass()
        {
            _builder.Create(RecipeType.BattleHardenedCuirass, SkillType.Armorcraft)
                .Level(77)
                .Category(RecipeCategoryType.Body)
                .NormalItem("battlhard_cuirs", 1)
                .HQItem("battlhard_cuirs_p1", 1)
                .Component("titan_plate", 4)
                .Component("crystal_scale", 2)
                .Component("quantum_proc", 1)
                .Component("harmonic_alloy", 1);
        }

        private void WarcastersCloak2()
        {
            _builder.Create(RecipeType.WarcastersCloak2, SkillType.Armorcraft)
                .Level(79)
                .Category(RecipeCategoryType.Body)
                .NormalItem("warcast_cloak2", 1)
                .HQItem("warcast_cloak_p1", 1)
                .UltraItem("warcast_cloak_p2", 1)
                .Component("titan_plate", 4)
                .Component("crystal_scale", 2)
                .Component("quantum_proc", 1)
                .Component("harmonic_alloy", 1);
        }

        private void IronMusketeersWarGambison()
        {
            _builder.Create(RecipeType.IronMusketeersWarGambison, SkillType.Armorcraft)
                .Level(79)
                .Category(RecipeCategoryType.Body)
                .NormalItem("ironmus_wargmb", 1)
                .HQItem("ironmus_wargmb_p1", 1)
                .UltraItem("ironmus_wargmb_p2", 1)
                .Component("titan_plate", 4)
                .Component("crystal_scale", 2)
                .Component("quantum_proc", 1)
                .Component("harmonic_alloy", 1);
        }

        private void RegalSquiresChainmail()
        {
            _builder.Create(RecipeType.RegalSquiresChainmail, SkillType.Armorcraft)
                .Level(80)
                .Category(RecipeCategoryType.Body)
                .NormalItem("regal_squireml", 1)
                .HQItem("regal_squireml_p1", 1)
                .UltraItem("regal_sq_chain_p2", 1)
                .Component("titan_plate", 4)
                .Component("crystal_scale", 2)
                .Component("quantum_proc", 1)
                .Component("harmonic_alloy", 1);
        }

        private void RegalSquiresEtherRobe()
        {
            _builder.Create(RecipeType.RegalSquiresEtherRobe, SkillType.Armorcraft)
                .Level(81)
                .Category(RecipeCategoryType.Body)
                .NormalItem("regal_sq_ethrb", 1)
                .HQItem("regal_sq_ethrb_p1", 1)
                .UltraItem("regal_sq_ether_p2", 1)
                .Component("techno_fiber", 2)
                .Component("titan_plate", 2)
                .Component("crystal_scale", 2)
                .Component("quantum_proc", 1);
        }

        private void TitanBreastplate()
        {
            _builder.Create(RecipeType.TitanBreastplate, SkillType.Armorcraft)
                .Level(82)
                .Category(RecipeCategoryType.Body)
                .NormalItem("titan_breastpl", 1)
                .HQItem("titan_breastpl_p1", 1)
                .Component("titan_plate", 4)
                .Component("crystal_scale", 2)
                .Component("quantum_proc", 1)
                .Component("harmonic_alloy", 1);
        }

        private void DivineAegisBreastplate()
        {
            _builder.Create(RecipeType.DivineAegisBreastplate, SkillType.Armorcraft)
                .Level(82)
                .Category(RecipeCategoryType.Body)
                .NormalItem("divineaeg_brst", 1)
                .HQItem("divineaeg_brst_p1", 1)
                .Component("living_wood", 4)
                .Component("titan_plate", 2)
                .Component("crystal_scale", 2)
                .Component("quantum_proc", 1);
        }

        private void WoolenCombatDoublet()
        {
            _builder.Create(RecipeType.WoolenCombatDoublet, SkillType.Armorcraft)
                .Level(87)
                .Category(RecipeCategoryType.Body)
                .NormalItem("wool_combat_dbl", 1)
                .HQItem("wool_combat_dbl_p1", 1)
                .Component("techno_fiber", 2)
                .Component("titan_plate", 2)
                .Component("crystal_scale", 2)
                .Component("quantum_proc", 1);
        }

        private void ReinforcedBrigandine()
        {
            _builder.Create(RecipeType.ReinforcedBrigandine, SkillType.Armorcraft)
                .Level(87)
                .Category(RecipeCategoryType.Body)
                .NormalItem("rein_brigan", 1)
                .HQItem("rein_brigan_p1", 1)
                .Component("titan_plate", 4)
                .Component("crystal_scale", 2)
                .Component("quantum_proc", 1)
                .Component("harmonic_alloy", 1);
        }

        private void BandedWarMail()
        {
            _builder.Create(RecipeType.BandedWarMail, SkillType.Armorcraft)
                .Level(89)
                .Category(RecipeCategoryType.Body)
                .NormalItem("banded_warmail", 1)
                .HQItem("banded_warmil_p1", 1)
                .Component("titan_plate", 4)
                .Component("crystal_scale", 2)
                .Component("quantum_proc", 1)
                .Component("harmonic_alloy", 1);
        }

        private void ChitinPlatedHarness2()
        {
            _builder.Create(RecipeType.ChitinPlatedHarness2, SkillType.Armorcraft)
                .Level(90)
                .Category(RecipeCategoryType.Body)
                .NormalItem("chitin_hrns2", 1)
                .HQItem("chitin_hrns2_p1", 1)
                .Component("chitin_plate", 2)
                .Component("titan_plate", 2)
                .Component("crystal_scale", 2)
                .Component("quantum_proc", 1);
        }

        private void WoolenWarGambison()
        {
            _builder.Create(RecipeType.WoolenWarGambison, SkillType.Armorcraft)
                .Level(95)
                .Category(RecipeCategoryType.Body)
                .NormalItem("wool_war_gmbs", 1)
                .HQItem("wool_war_gmbs_p1", 1)
                .Component("titan_plate", 4)
                .Component("crystal_scale", 2)
                .Component("quantum_proc", 1)
                .Component("harmonic_alloy", 1);
        }

        private void MythriteBreastplate()
        {
            _builder.Create(RecipeType.MythriteBreastplate, SkillType.Armorcraft)
                .Level(95)
                .Category(RecipeCategoryType.Body)
                .NormalItem("mythrite_breastp", 1)
                .HQItem("mythrite_breastp_p1", 1)
                .Component("titan_plate", 4)
                .Component("crystal_scale", 2)
                .Component("quantum_proc", 1)
                .Component("harmonic_alloy", 1);
        }

        private void ObsidianCrowJupon()
        {
            _builder.Create(RecipeType.ObsidianCrowJupon, SkillType.Armorcraft)
                .Level(97)
                .Category(RecipeCategoryType.Body)
                .NormalItem("obsidian_crow_ju", 1)
                .HQItem("obsidian_crow_ju_p1", 1)
                .Component("titan_plate", 4)
                .Component("crystal_scale", 2)
                .Component("quantum_proc", 1)
                .Component("harmonic_alloy", 1);
        }

        private void SanctifiedWhiteCloak()
        {
            _builder.Create(RecipeType.SanctifiedWhiteCloak, SkillType.Armorcraft)
                .Level(98)
                .Category(RecipeCategoryType.Body)
                .NormalItem("sanct_white_clk", 1)
                .HQItem("sanct_white_clk_p1", 1)
                .Component("ether_crystal", 2)
                .Component("titan_plate", 2)
                .Component("crystal_scale", 2)
                .Component("quantum_proc", 1);
        }

        private void DinohideJerkin()
        {
            _builder.Create(RecipeType.DinohideJerkin, SkillType.Armorcraft)
                .Level(99)
                .Category(RecipeCategoryType.Body)
                .NormalItem("dinohide_jerkn", 1)
                .HQItem("dinohide_jerkn_p1", 1)
                .Component("techno_fiber", 2)
                .Component("titan_plate", 2)
                .Component("crystal_scale", 2)
                .Component("quantum_proc", 1);
        }

        private void SteelguardScaleMail()
        {
            _builder.Create(RecipeType.SteelguardScaleMail, SkillType.Armorcraft)
                .Level(99)
                .Category(RecipeCategoryType.Body)
                .NormalItem("steelg_scl_ml", 1)
                .HQItem("steelg_scl_ml_p1", 1)
                .Component("living_wood", 4)
                .Component("titan_plate", 2)
                .Component("crystal_scale", 2)
                .Component("quantum_proc", 1);
        }

        private void ShinobiShadowGi()
        {
            _builder.Create(RecipeType.ShinobiShadowGi, SkillType.Armorcraft)
                .Level(100)
                .Category(RecipeCategoryType.Body)
                .NormalItem("shinobi_shdwgi", 1)
                .HQItem("shinobi_shdwgi_p1", 1)
                .Component("titan_plate", 4)
                .Component("crystal_scale", 2)
                .Component("quantum_proc", 1)
                .Component("harmonic_alloy", 1);
        }
    }
}