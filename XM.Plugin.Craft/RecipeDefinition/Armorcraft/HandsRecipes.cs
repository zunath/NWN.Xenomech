using System.Collections.Generic;
using Anvil.Services;
using XM.Progression.Skill;

namespace XM.Plugin.Craft.RecipeDefinition.Armorcraft
{
    [ServiceBinding(typeof(IRecipeListDefinition))]
    internal class HandsRecipes : IRecipeListDefinition
    {
        private readonly RecipeBuilder _builder = new();

        public Dictionary<RecipeType, RecipeDetail> BuildRecipes()
        {
            // Hands Armor ordered by level (8-100)
            ReinforcedCuffs();                      // Level 8
            AurionAlloyMittens();                   // Level 9
            LeatherCombatGloves();                  // Level 11
            BattleforgedTekko();                    // Level 14
            WarforgedMitts();                       // Level 15
            TitanScaleGauntlets();                  // Level 17
            BrassguardMittens();                    // Level 19
            EtherwovenCuffs();                      // Level 23
            CombatGloves();                         // Level 24
            BoneforgedMittens();                    // Level 29
            LizardscaleCombatGloves();              // Level 35
            CottonWovenTekko();                     // Level 35
            ChitinPlatedMittens();                  // Level 39
            SanctifiedWhiteMitts();                 // Level 41
            CottonCombatGloves();                   // Level 48
            TitanChainMittens();                    // Level 51
            DevoteesSanctifiedMitts();              // Level 53
            BrassguardFingerGauntlets();            // Level 57
            NoctshadowGloves();                     // Level 57
            WoolenCombatCuffs();                    // Level 58
            EisenwroughtGauntlets();                // Level 58
            CombatReinforcedMittens();              // Level 59
            EarthforgedTekko();                     // Level 60
            OraclesMitts();                         // Level 61
            ReinforcedStuddedGloves();              // Level 61
            IroncladMittens();                      // Level 69
            SilverguardMittens();                   // Level 70
            EtherwovenMitts();                      // Level 71
            IroncladFingerGauntlets();              // Level 71
            CombatBracers();                        // Level 74
            LeatherboundGloves();                   // Level 74
            RegalVelvetCuffs();                     // Level 76
            TitanGauntlets();                       // Level 79
            WarcastersMitts();                      // Level 81
            IronMusketeersBattleGauntlets();        // Level 81
            SilverPlatedBangles();                  // Level 83
            ChitinPlatedMittens2();                 // Level 88
            InsulatedMufflers();                    // Level 92
            TideshellBangles();                     // Level 92
            RaptorstrikeGloves();                   // Level 93
            SteelguardFingerGauntlets();            // Level 93
            WoolenCombatBracers();                  // Level 96
            MythriteGauntlets();                    // Level 97
            ShinobiShadowTekko();                   // Level 97
            ObsidianMitts();                        // Level 97
            ObsidianCrowBracers();                  // Level 97
            TacticianMagiciansWarCuffs();           // Level 100

            return _builder.Build();
        }

        private void ReinforcedCuffs()
        {
            _builder.Create(RecipeType.ReinforcedCuffs, SkillType.Armorcraft)
                .Level(8)
                .Category(RecipeCategoryType.Hands)
                .NormalItem("reinforc_cuff", 1)
                .HQItem("reinforc_cuff_p1", 1)
                .Component("aurion_ingot", 2)
                .Component("beast_hide", 1)
                .Component("flux_compound", 1);
        }

        private void AurionAlloyMittens()
        {
            _builder.Create(RecipeType.AurionAlloyMittens, SkillType.Armorcraft)
                .Level(9)
                .Category(RecipeCategoryType.Hands)
                .NormalItem("aurion_mitts", 1)
                .HQItem("aurion_mitts_p1", 1)
                .Component("servo_motor", 1)
                .Component("aurion_ingot", 1);
        }

        private void LeatherCombatGloves()
        {
            _builder.Create(RecipeType.LeatherCombatGloves, SkillType.Armorcraft)
                .Level(11)
                .Category(RecipeCategoryType.Hands)
                .NormalItem("leather_comglv", 1)
                .HQItem("leather_comglv_p1", 1)
                .Component("sinew_strand", 1)
                .Component("aurion_ingot", 1);
        }

        private void BattleforgedTekko()
        {
            _builder.Create(RecipeType.BattleforgedTekko, SkillType.Armorcraft)
                .Level(14)
                .Category(RecipeCategoryType.Hands)
                .NormalItem("battlefrg_tkko", 1)
                .HQItem("battlefrg_tkko_p1", 1)
                .Component("servo_motor", 1)
                .Component("aurion_ingot", 1);
        }

        private void WarforgedMitts()
        {
            _builder.Create(RecipeType.WarforgedMitts, SkillType.Armorcraft)
                .Level(15)
                .Category(RecipeCategoryType.Hands)
                .NormalItem("warforged_mitts", 1)
                .HQItem("warforged_mitts_p1", 1)
                .Component("servo_motor", 1)
                .Component("aurion_ingot", 1);
        }

        private void TitanScaleGauntlets()
        {
            _builder.Create(RecipeType.TitanScaleGauntlets, SkillType.Armorcraft)
                .Level(17)
                .Category(RecipeCategoryType.Hands)
                .NormalItem("titan_gauntlts", 1)
                .HQItem("titan_gauntlts_p1", 1)
                .Component("servo_motor", 1)
                .Component("ferrite_core", 1);
        }

        private void BrassguardMittens()
        {
            _builder.Create(RecipeType.BrassguardMittens, SkillType.Armorcraft)
                .Level(19)
                .Category(RecipeCategoryType.Hands)
                .NormalItem("brassg_mitts", 1)
                .HQItem("brassg_mitts_p1", 1)
                .Component("living_wood", 2)
                .Component("ferrite_core", 1);
        }

        private void EtherwovenCuffs()
        {
            _builder.Create(RecipeType.EtherwovenCuffs, SkillType.Armorcraft)
                .Level(23)
                .Category(RecipeCategoryType.Hands)
                .NormalItem("etherwvn_cuffs", 1)
                .HQItem("etherwvn_cuffs_p1", 1)
                .Component("ether_crystal", 1)
                .Component("ferrite_core", 1);
        }

        private void CombatGloves()
        {
            _builder.Create(RecipeType.CombatGloves, SkillType.Armorcraft)
                .Level(24)
                .Category(RecipeCategoryType.Hands)
                .NormalItem("combat_gloves", 1)
                .HQItem("combat_gloves_p1", 1)
                .Component("circuit_matrix", 1)
                .Component("ferrite_core", 1);
        }

        private void BoneforgedMittens()
        {
            _builder.Create(RecipeType.BoneforgedMittens, SkillType.Armorcraft)
                .Level(29)
                .Category(RecipeCategoryType.Hands)
                .NormalItem("bonefrg_mitts", 1)
                .HQItem("bonefrg_mitts_p1", 1)
                .Component("servo_motor", 1)
                .Component("ferrite_core", 1);
        }

        private void LizardscaleCombatGloves()
        {
            _builder.Create(RecipeType.LizardscaleCombatGloves, SkillType.Armorcraft)
                .Level(35)
                .Category(RecipeCategoryType.Hands)
                .NormalItem("lizardsc_comglv", 1)
                .HQItem("lizardsc_comglv_p1", 1)
                .Component("circuit_matrix", 1)
                .Component("ferrite_core", 1)
                .Component("biosteel_comp", 1);
        }

        private void CottonWovenTekko()
        {
            _builder.Create(RecipeType.CottonWovenTekko, SkillType.Armorcraft)
                .Level(35)
                .Category(RecipeCategoryType.Hands)
                .NormalItem("cottonwv_tekko", 1)
                .HQItem("cottonwv_tekko_p1", 1)
                .Component("servo_motor", 1)
                .Component("ferrite_core", 1)
                .Component("biosteel_comp", 1);
        }

        private void ChitinPlatedMittens()
        {
            _builder.Create(RecipeType.ChitinPlatedMittens, SkillType.Armorcraft)
                .Level(39)
                .Category(RecipeCategoryType.Hands)
                .NormalItem("chitin_mt_mitt", 1)
                .HQItem("chitin_mt_mitt_p1", 1)
                .Component("servo_motor", 1)
                .Component("ferrite_core", 1)
                .Component("biosteel_comp", 1);
        }

        private void SanctifiedWhiteMitts()
        {
            _builder.Create(RecipeType.SanctifiedWhiteMitts, SkillType.Armorcraft)
                .Level(41)
                .Category(RecipeCategoryType.Hands)
                .NormalItem("sanct_white_mt", 1)
                .HQItem("sanct_white_mt_p1", 1)
                .Component("ether_crystal", 1)
                .Component("ferrite_core", 1)
                .Component("biosteel_comp", 1);
        }

        private void CottonCombatGloves()
        {
            _builder.Create(RecipeType.CottonCombatGloves, SkillType.Armorcraft)
                .Level(48)
                .Category(RecipeCategoryType.Hands)
                .NormalItem("cotton_comglv", 1)
                .HQItem("cotton_comglv_p1", 1)
                .Component("circuit_matrix", 1)
                .Component("mythrite_frag", 1)
                .Component("psi_crystal", 1);
        }

        private void TitanChainMittens()
        {
            _builder.Create(RecipeType.TitanChainMittens, SkillType.Armorcraft)
                .Level(51)
                .Category(RecipeCategoryType.Hands)
                .NormalItem("titan_chainmt", 1)
                .HQItem("titan_chainmt_p1", 1)
                .Component("servo_motor", 1)
                .Component("mythrite_frag", 1)
                .Component("psi_crystal", 1);
        }

        private void DevoteesSanctifiedMitts()
        {
            _builder.Create(RecipeType.DevoteesSanctifiedMitts, SkillType.Armorcraft)
                .Level(53)
                .Category(RecipeCategoryType.Hands)
                .NormalItem("devot_sanct_mt", 1)
                .HQItem("devot_sanct_mt_p1", 1)
                .Component("ether_crystal", 1)
                .Component("mythrite_frag", 1)
                .Component("psi_crystal", 1);
        }

        private void BrassguardFingerGauntlets()
        {
            _builder.Create(RecipeType.BrassguardFingerGauntlets, SkillType.Armorcraft)
                .Level(57)
                .Category(RecipeCategoryType.Hands)
                .NormalItem("brassg_fingerga", 1)
                .HQItem("brassg_fingga_p1", 1)
                .Component("living_wood", 3)
                .Component("mythrite_frag", 1)
                .Component("psi_crystal", 1);
        }

        private void NoctshadowGloves()
        {
            _builder.Create(RecipeType.NoctshadowGloves, SkillType.Armorcraft)
                .Level(57)
                .Category(RecipeCategoryType.Hands)
                .NormalItem("noctshdw_glovs", 1)
                .HQItem("noctshdw_glovs_p1", 1)
                .Component("mythrite_frag", 3)
                .Component("psi_crystal", 1)
                .Component("biosteel_comp", 1);
        }

        private void WoolenCombatCuffs()
        {
            _builder.Create(RecipeType.WoolenCombatCuffs, SkillType.Armorcraft)
                .Level(58)
                .Category(RecipeCategoryType.Hands)
                .NormalItem("wool_combatcf", 1)
                .HQItem("wool_combatcf_p1", 1)
                .Component("servo_motor", 1)
                .Component("mythrite_frag", 1)
                .Component("psi_crystal", 1);
        }

        private void EisenwroughtGauntlets()
        {
            _builder.Create(RecipeType.EisenwroughtGauntlets, SkillType.Armorcraft)
                .Level(58)
                .Category(RecipeCategoryType.Hands)
                .NormalItem("eisenw_gauntlt", 1)
                .HQItem("eisenw_gauntlt_p1", 1)
                .Component("servo_motor", 1)
                .Component("mythrite_frag", 1)
                .Component("psi_crystal", 1);
        }

        private void CombatReinforcedMittens()
        {
            _builder.Create(RecipeType.CombatReinforcedMittens, SkillType.Armorcraft)
                .Level(59)
                .Category(RecipeCategoryType.Hands)
                .NormalItem("combat_rein_mt", 1)
                .HQItem("combat_rein_mt_p1", 1)
                .Component("servo_motor", 1)
                .Component("mythrite_frag", 1)
                .Component("psi_crystal", 1);
        }

        private void EarthforgedTekko()
        {
            _builder.Create(RecipeType.EarthforgedTekko, SkillType.Armorcraft)
                .Level(60)
                .Category(RecipeCategoryType.Hands)
                .NormalItem("earthfrg_tekko", 1)
                .HQItem("earthfrg_tekko_p1", 1)
                .Component("servo_motor", 1)
                .Component("mythrite_frag", 1)
                .Component("psi_crystal", 1);
        }

        private void OraclesMitts()
        {
            _builder.Create(RecipeType.OraclesMitts, SkillType.Armorcraft)
                .Level(61)
                .Category(RecipeCategoryType.Hands)
                .NormalItem("oracle_mitts", 1)
                .HQItem("oracle_mitts_p1", 1)
                .Component("ether_crystal", 1)
                .Component("mythrite_frag", 1)
                .Component("psi_crystal", 1);
        }

        private void ReinforcedStuddedGloves()
        {
            _builder.Create(RecipeType.ReinforcedStuddedGloves, SkillType.Armorcraft)
                .Level(61)
                .Category(RecipeCategoryType.Hands)
                .NormalItem("rein_stud_glv", 1)
                .HQItem("rein_stud_glv_p1", 1)
                .Component("mythrite_frag", 3)
                .Component("psi_crystal", 1)
                .Component("biosteel_comp", 1);
        }

        private void IroncladMittens()
        {
            _builder.Create(RecipeType.IroncladMittens, SkillType.Armorcraft)
                .Level(69)
                .Category(RecipeCategoryType.Hands)
                .NormalItem("ironclad_mitts", 1)
                .HQItem("ironclad_mitts_p1", 1)
                .Component("servo_motor", 1)
                .Component("mythrite_frag", 1)
                .Component("psi_crystal", 1);
        }

        private void SilverguardMittens()
        {
            _builder.Create(RecipeType.SilverguardMittens, SkillType.Armorcraft)
                .Level(70)
                .Category(RecipeCategoryType.Hands)
                .NormalItem("silverg_mitts", 1)
                .HQItem("silverg_mitts_p1", 1)
                .Component("living_wood", 3)
                .Component("mythrite_frag", 1)
                .Component("psi_crystal", 1);
        }

        private void EtherwovenMitts()
        {
            _builder.Create(RecipeType.EtherwovenMitts, SkillType.Armorcraft)
                .Level(71)
                .Category(RecipeCategoryType.Hands)
                .NormalItem("etherwvn_mitts", 1)
                .HQItem("etherwvn_mitts_p1", 1)
                .Component("ether_crystal", 1)
                .Component("mythrite_frag", 1)
                .Component("psi_crystal", 1);
        }

        private void IroncladFingerGauntlets()
        {
            _builder.Create(RecipeType.IroncladFingerGauntlets, SkillType.Armorcraft)
                .Level(71)
                .Category(RecipeCategoryType.Hands)
                .NormalItem("ironclad_fingga", 1)
                .HQItem("ironclad_fingga_p1", 1)
                .Component("servo_motor", 1)
                .Component("mythrite_frag", 1)
                .Component("psi_crystal", 1);
        }

        private void CombatBracers()
        {
            _builder.Create(RecipeType.CombatBracers, SkillType.Armorcraft)
                .Level(74)
                .Category(RecipeCategoryType.Hands)
                .NormalItem("combat_bracers", 1)
                .HQItem("combat_bracers_p1", 1)
                .Component("servo_motor", 1)
                .Component("mythrite_frag", 1)
                .Component("psi_crystal", 1);
        }

        private void LeatherboundGloves()
        {
            _builder.Create(RecipeType.LeatherboundGloves, SkillType.Armorcraft)
                .Level(74)
                .Category(RecipeCategoryType.Hands)
                .NormalItem("leatherb_glovs", 1)
                .HQItem("leatherb_glovs_p1", 1)
                .Component("sinew_strand", 1)
                .Component("mythrite_frag", 1)
                .Component("psi_crystal", 1);
        }

        private void RegalVelvetCuffs()
        {
            _builder.Create(RecipeType.RegalVelvetCuffs, SkillType.Armorcraft)
                .Level(76)
                .Category(RecipeCategoryType.Hands)
                .NormalItem("regal_velvetcf", 1)
                .HQItem("regal_velvetcf_p1", 1)
                .Component("servo_motor", 1)
                .Component("titan_plate", 1)
                .Component("crystal_scale", 1);
        }

        private void TitanGauntlets()
        {
            _builder.Create(RecipeType.TitanGauntlets, SkillType.Armorcraft)
                .Level(79)
                .Category(RecipeCategoryType.Hands)
                .NormalItem("titan_gauntlts2", 1)
                .HQItem("titan_gauntlts_p1", 1)
                .Component("servo_motor", 1)
                .Component("titan_plate", 1)
                .Component("crystal_scale", 1);
        }

        private void WarcastersMitts()
        {
            _builder.Create(RecipeType.WarcastersMitts, SkillType.Armorcraft)
                .Level(81)
                .Category(RecipeCategoryType.Hands)
                .NormalItem("warcast_mitts", 1)
                .HQItem("warcast_mitts_p1", 1)
                .UltraItem("warcast_mitts_p2", 1)
                .Component("servo_motor", 1)
                .Component("titan_plate", 1)
                .Component("crystal_scale", 1);
        }

        private void IronMusketeersBattleGauntlets()
        {
            _builder.Create(RecipeType.IronMusketeersBattleGauntlets, SkillType.Armorcraft)
                .Level(81)
                .Category(RecipeCategoryType.Hands)
                .NormalItem("ironmus_battgnt", 1)
                .HQItem("ironmus_battgnt_p1", 1)
                .UltraItem("ironmus_battgn_p2", 1)
                .Component("servo_motor", 1)
                .Component("titan_plate", 1)
                .Component("crystal_scale", 1);
        }

        private void SilverPlatedBangles()
        {
            _builder.Create(RecipeType.SilverPlatedBangles, SkillType.Armorcraft)
                .Level(83)
                .Category(RecipeCategoryType.Hands)
                .NormalItem("silverp_bngls", 1)
                .HQItem("silverp_bngls_p1", 1)
                .Component("servo_motor", 1)
                .Component("titan_plate", 1)
                .Component("crystal_scale", 1);
        }

        private void ChitinPlatedMittens2()
        {
            _builder.Create(RecipeType.ChitinPlatedMittens2, SkillType.Armorcraft)
                .Level(88)
                .Category(RecipeCategoryType.Hands)
                .NormalItem("chitin_mittens2", 1)
                .HQItem("chitin_mittens2_p1", 1)
                .Component("servo_motor", 1)
                .Component("titan_plate", 1)
                .Component("crystal_scale", 1);
        }

        private void InsulatedMufflers()
        {
            _builder.Create(RecipeType.InsulatedMufflers, SkillType.Armorcraft)
                .Level(92)
                .Category(RecipeCategoryType.Hands)
                .NormalItem("insulated_muff", 1)
                .HQItem("insulated_muff_p1", 1)
                .Component("servo_motor", 1)
                .Component("titan_plate", 1)
                .Component("crystal_scale", 1);
        }

        private void TideshellBangles()
        {
            _builder.Create(RecipeType.TideshellBangles, SkillType.Armorcraft)
                .Level(92)
                .Category(RecipeCategoryType.Hands)
                .NormalItem("tideshell_bngls", 1)
                .HQItem("tideshell_bngls_p1", 1)
                .Component("servo_motor", 1)
                .Component("titan_plate", 1)
                .Component("crystal_scale", 1);
        }

        private void RaptorstrikeGloves()
        {
            _builder.Create(RecipeType.RaptorstrikeGloves, SkillType.Armorcraft)
                .Level(93)
                .Category(RecipeCategoryType.Hands)
                .NormalItem("raptor_gloves", 1)
                .HQItem("raptor_gloves_p1", 1)
                .Component("titan_plate", 3)
                .Component("crystal_scale", 1)
                .Component("quantum_proc", 1);
        }

        private void SteelguardFingerGauntlets()
        {
            _builder.Create(RecipeType.SteelguardFingerGauntlets, SkillType.Armorcraft)
                .Level(93)
                .Category(RecipeCategoryType.Hands)
                .NormalItem("steelg_fingga", 1)
                .HQItem("steelg_fingga_p1", 1)
                .Component("living_wood", 3)
                .Component("titan_plate", 1)
                .Component("crystal_scale", 1);
        }

        private void WoolenCombatBracers()
        {
            _builder.Create(RecipeType.WoolenCombatBracers, SkillType.Armorcraft)
                .Level(96)
                .Category(RecipeCategoryType.Hands)
                .NormalItem("wool_combat_brc", 1)
                .HQItem("wool_combat_brc_p1", 1)
                .Component("servo_motor", 1)
                .Component("titan_plate", 1)
                .Component("crystal_scale", 1);
        }

        private void MythriteGauntlets()
        {
            _builder.Create(RecipeType.MythriteGauntlets, SkillType.Armorcraft)
                .Level(97)
                .Category(RecipeCategoryType.Hands)
                .NormalItem("mythrite_gauntl", 1)
                .HQItem("mythrite_gauntl_p1", 1)
                .Component("servo_motor", 1)
                .Component("titan_plate", 1)
                .Component("crystal_scale", 1);
        }

        private void ShinobiShadowTekko()
        {
            _builder.Create(RecipeType.ShinobiShadowTekko, SkillType.Armorcraft)
                .Level(97)
                .Category(RecipeCategoryType.Hands)
                .NormalItem("shinobi_shdwtk", 1)
                .HQItem("shinobi_shdwtk_p1", 1)
                .Component("servo_motor", 1)
                .Component("titan_plate", 1)
                .Component("crystal_scale", 1);
        }

        private void ObsidianMitts()
        {
            _builder.Create(RecipeType.ObsidianMitts, SkillType.Armorcraft)
                .Level(97)
                .Category(RecipeCategoryType.Hands)
                .NormalItem("obsidian_mitts", 1)
                .HQItem("obsidian_mitts_p1", 1)
                .Component("servo_motor", 1)
                .Component("titan_plate", 1)
                .Component("crystal_scale", 1);
        }

        private void ObsidianCrowBracers()
        {
            _builder.Create(RecipeType.ObsidianCrowBracers, SkillType.Armorcraft)
                .Level(97)
                .Category(RecipeCategoryType.Hands)
                .NormalItem("obsidian_crow_br", 1)
                .HQItem("obsidian_crow_br_p1", 1)
                .Component("servo_motor", 1)
                .Component("titan_plate", 1)
                .Component("crystal_scale", 1);
        }

        private void TacticianMagiciansWarCuffs()
        {
            _builder.Create(RecipeType.TacticianMagiciansWarCuffs, SkillType.Armorcraft)
                .Level(100)
                .Category(RecipeCategoryType.Hands)
                .NormalItem("tact_mag_warcf", 1)
                .Component("servo_motor", 1)
                .Component("titan_plate", 1)
                .Component("crystal_scale", 1);
        }
    }
}