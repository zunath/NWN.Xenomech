using System.Collections.Generic;
using Anvil.Services;
using XM.Progression.Skill;

namespace XM.Plugin.Craft.RecipeDefinition.Armorcraft
{
    [ServiceBinding(typeof(IRecipeListDefinition))]
    internal class FeetRecipes : IRecipeListDefinition
    {
        private readonly RecipeBuilder _builder = new();

        public Dictionary<RecipeType, RecipeDetail> BuildRecipes()
        {
            // Feet Armor ordered by level (4-100)
            AshenStrideClogs();                     // Level 4
            AurionAlloyLeggings();                  // Level 7
            LeatherCombatHighboots();               // Level 11
            WarwovenKyahan();                       // Level 13
            EtherTouchedSoleas();                   // Level 16
            ReinforcedGaiters();                    // Level 19
            BrassguardLeggings();                   // Level 20
            TitanScaleGreaves();                    // Level 23
            VerdantHollyClogs();                    // Level 27
            LizardscaleLedelsens();                 // Level 32
            BoneforgedLeggings();                   // Level 33
            CottonWovenKyahan();                    // Level 33
            MettleforgedLeggings();                 // Level 35
            ChitinPlatedLeggings();                 // Level 39
            ArcaneWeaversSandals();                 // Level 42
            WingstrideBoots();                      // Level 48
            CottonWovenGaiters();                   // Level 49
            TitanWarGreaves();                      // Level 49
            BrassguardGreaves();                    // Level 55
            EarthforgedKyahan();                    // Level 55
            ZephyrSoleas();                         // Level 56
            EisensteelBoots();                      // Level 57
            OraclesPumps();                         // Level 58
            NoctshadowGaiters();                    // Level 58
            ReinforcedStuddedBoots();               // Level 58
            CombatLeggings();                       // Level 67
            EtherwovenShoes();                      // Level 69
            InsulatedSocks();                       // Level 70
            SilverguardGreaves();                   // Level 73
            LeatherboundHighboots();                // Level 73
            IroncladGreaves();                      // Level 75
            ObsidianSabots();                       // Level 76
            IronMusketeersBattleSabatons();         // Level 77
            PlateforgedLeggings();                  // Level 80
            WarcastersShoes();                      // Level 82
            InfernalSabots();                       // Level 85
            ChitinPlatedLeggings2();                // Level 87
            WarbornSollerets();                     // Level 91
            RaptorstrikeLedelsens();                // Level 94
            SteelguardGreaves();                    // Level 96
            MythriteLeggings();                     // Level 97
            ObsidianCrowGaiters();                  // Level 97
            WoolenInsulatedSocks();                 // Level 98
            ReinforcedMoccasins();                  // Level 98
            ShinobiShadowKyahan();                  // Level 100
            TacticianMagiciansWarPigaches();        // Level 100

            return _builder.Build();
        }

        private void AshenStrideClogs()
        {
            _builder.Create(RecipeType.AshenStrideClogs, SkillType.Armorcraft)
                .Level(4)
                .Category(RecipeCategoryType.Feet)
                .NormalItem("ashenstrd_clg", 1)
                .HQItem("ashenstrd_clg_p1", 1)
                .Component("living_wood", 2)
                .Component("beast_hide", 1)
                .Component("flux_compound", 1);
        }

        private void AurionAlloyLeggings()
        {
            _builder.Create(RecipeType.AurionAlloyLeggings, SkillType.Armorcraft)
                .Level(7)
                .Category(RecipeCategoryType.Feet)
                .NormalItem("aurion_leggins", 1)
                .HQItem("aurion_leggins_p1", 1)
                .Component("aurion_ingot", 3)
                .Component("beast_hide", 2)
                .Component("bond_agent", 1);
        }

        private void LeatherCombatHighboots()
        {
            _builder.Create(RecipeType.LeatherCombatHighboots, SkillType.Armorcraft)
                .Level(11)
                .Category(RecipeCategoryType.Feet)
                .NormalItem("leather_comhbt", 1)
                .HQItem("leather_comhbt_p1", 1)
                .Component("sinew_strand", 1)
                .Component("aurion_ingot", 1);
        }

        private void WarwovenKyahan()
        {
            _builder.Create(RecipeType.WarwovenKyahan, SkillType.Armorcraft)
                .Level(13)
                .Category(RecipeCategoryType.Feet)
                .NormalItem("warwvn_kyahan", 1)
                .HQItem("warwvn_kyahan_p1", 1)
                .Component("aurion_ingot", 2)
                .Component("beast_hide", 1);
        }

        private void EtherTouchedSoleas()
        {
            _builder.Create(RecipeType.EtherTouchedSoleas, SkillType.Armorcraft)
                .Level(16)
                .Category(RecipeCategoryType.Feet)
                .NormalItem("ethertch_sleas", 1)
                .HQItem("ethertch_sleas_p1", 1)
                .Component("ether_crystal", 1)
                .Component("ferrite_core", 1);
        }

        private void ReinforcedGaiters()
        {
            _builder.Create(RecipeType.ReinforcedGaiters, SkillType.Armorcraft)
                .Level(19)
                .Category(RecipeCategoryType.Feet)
                .NormalItem("reinforc_gait", 1)
                .HQItem("reinforc_gait_p1", 1)
                .Component("ferrite_core", 2)
                .Component("circuit_matrix", 1);
        }

        private void BrassguardLeggings()
        {
            _builder.Create(RecipeType.BrassguardLeggings, SkillType.Armorcraft)
                .Level(20)
                .Category(RecipeCategoryType.Feet)
                .NormalItem("brassg_legg", 1)
                .HQItem("brassg_legg_p1", 1)
                .Component("living_wood", 2)
                .Component("ferrite_core", 1);
        }

        private void TitanScaleGreaves()
        {
            _builder.Create(RecipeType.TitanScaleGreaves, SkillType.Armorcraft)
                .Level(23)
                .Category(RecipeCategoryType.Feet)
                .NormalItem("titan_scale_gvs", 1)
                .HQItem("titan_scale_gvs_p1", 1)
                .Component("ferrite_core", 2)
                .Component("circuit_matrix", 1);
        }

        private void VerdantHollyClogs()
        {
            _builder.Create(RecipeType.VerdantHollyClogs, SkillType.Armorcraft)
                .Level(27)
                .Category(RecipeCategoryType.Feet)
                .NormalItem("verdholly_clgs", 1)
                .HQItem("verdholly_clgs_p1", 1)
                .Component("living_wood", 2)
                .Component("ferrite_core", 1);
        }

        private void LizardscaleLedelsens()
        {
            _builder.Create(RecipeType.LizardscaleLedelsens, SkillType.Armorcraft)
                .Level(32)
                .Category(RecipeCategoryType.Feet)
                .NormalItem("lizardsc_ledls", 1)
                .HQItem("lizardsc_ledls_p1", 1)
                .Component("ferrite_core", 2)
                .Component("biosteel_comp", 1)
                .Component("neural_inter", 1);
        }

        private void BoneforgedLeggings()
        {
            _builder.Create(RecipeType.BoneforgedLeggings, SkillType.Armorcraft)
                .Level(33)
                .Category(RecipeCategoryType.Feet)
                .NormalItem("bonefrg_legg", 1)
                .HQItem("bonefrg_legg_p1", 1)
                .Component("ferrite_core", 2)
                .Component("biosteel_comp", 1)
                .Component("neural_inter", 1);
        }

        private void CottonWovenKyahan()
        {
            _builder.Create(RecipeType.CottonWovenKyahan, SkillType.Armorcraft)
                .Level(33)
                .Category(RecipeCategoryType.Feet)
                .NormalItem("cottonwv_kyhan", 1)
                .HQItem("cottonwv_kyhan_p1", 1)
                .Component("ferrite_core", 2)
                .Component("biosteel_comp", 1)
                .Component("neural_inter", 1);
        }

        private void MettleforgedLeggings()
        {
            _builder.Create(RecipeType.MettleforgedLeggings, SkillType.Armorcraft)
                .Level(35)
                .Category(RecipeCategoryType.Feet)
                .NormalItem("mettlefrg_legg", 1)
                .HQItem("mettlefrg_legg_p1", 1)
                .Component("ferrite_core", 2)
                .Component("biosteel_comp", 1)
                .Component("neural_inter", 1);
        }

        private void ChitinPlatedLeggings()
        {
            _builder.Create(RecipeType.ChitinPlatedLeggings, SkillType.Armorcraft)
                .Level(39)
                .Category(RecipeCategoryType.Feet)
                .NormalItem("chitin_mt_legg", 1)
                .HQItem("chitin_mt_legg_p1", 1)
                .Component("chitin_plate", 1)
                .Component("ferrite_core", 1)
                .Component("biosteel_comp", 1);
        }

        private void ArcaneWeaversSandals()
        {
            _builder.Create(RecipeType.ArcaneWeaversSandals, SkillType.Armorcraft)
                .Level(42)
                .Category(RecipeCategoryType.Feet)
                .NormalItem("arcane_sandals", 1)
                .HQItem("arcane_sandals_p1", 1)
                .Component("ether_crystal", 1)
                .Component("ferrite_core", 1)
                .Component("biosteel_comp", 1);
        }

        private void WingstrideBoots()
        {
            _builder.Create(RecipeType.WingstrideBoots, SkillType.Armorcraft)
                .Level(48)
                .Category(RecipeCategoryType.Feet)
                .NormalItem("wingstride_bts", 1)
                .HQItem("wingstride_bts_p1", 1)
                .Component("mythrite_frag", 3)
                .Component("psi_crystal", 1)
                .Component("biosteel_comp", 1);
        }

        private void CottonWovenGaiters()
        {
            _builder.Create(RecipeType.CottonWovenGaiters, SkillType.Armorcraft)
                .Level(49)
                .Category(RecipeCategoryType.Feet)
                .NormalItem("cottonwv_gaits", 1)
                .HQItem("cottonwv_gaits_p1", 1)
                .Component("mythrite_frag", 3)
                .Component("psi_crystal", 1)
                .Component("biosteel_comp", 1);
        }

        private void TitanWarGreaves()
        {
            _builder.Create(RecipeType.TitanWarGreaves, SkillType.Armorcraft)
                .Level(49)
                .Category(RecipeCategoryType.Feet)
                .NormalItem("titan_war_gvs", 1)
                .HQItem("titan_war_gvs_p1", 1)
                .Component("mythrite_frag", 3)
                .Component("psi_crystal", 1)
                .Component("biosteel_comp", 1);
        }

        private void BrassguardGreaves()
        {
            _builder.Create(RecipeType.BrassguardGreaves, SkillType.Armorcraft)
                .Level(55)
                .Category(RecipeCategoryType.Feet)
                .NormalItem("brassg_greavs", 1)
                .HQItem("brassg_greavs_p1", 1)
                .Component("living_wood", 3)
                .Component("mythrite_frag", 1)
                .Component("psi_crystal", 1);
        }

        private void EarthforgedKyahan()
        {
            _builder.Create(RecipeType.EarthforgedKyahan, SkillType.Armorcraft)
                .Level(55)
                .Category(RecipeCategoryType.Feet)
                .NormalItem("earthfrg_kyhan", 1)
                .HQItem("earthfrg_kyhan_p1", 1)
                .Component("mythrite_frag", 3)
                .Component("psi_crystal", 1)
                .Component("biosteel_comp", 1);
        }

        private void ZephyrSoleas()
        {
            _builder.Create(RecipeType.ZephyrSoleas, SkillType.Armorcraft)
                .Level(56)
                .Category(RecipeCategoryType.Feet)
                .NormalItem("zephyr_soleas", 1)
                .HQItem("zephyr_soleas_p1", 1)
                .Component("mythrite_frag", 3)
                .Component("psi_crystal", 1)
                .Component("biosteel_comp", 1);
        }

        private void EisensteelBoots()
        {
            _builder.Create(RecipeType.EisensteelBoots, SkillType.Armorcraft)
                .Level(57)
                .Category(RecipeCategoryType.Feet)
                .NormalItem("eisensteel_bts", 1)
                .HQItem("eisensteel_bts_p1", 1)
                .Component("mythrite_frag", 3)
                .Component("psi_crystal", 1)
                .Component("biosteel_comp", 1);
        }

        private void OraclesPumps()
        {
            _builder.Create(RecipeType.OraclesPumps, SkillType.Armorcraft)
                .Level(58)
                .Category(RecipeCategoryType.Feet)
                .NormalItem("oracle_pumps", 1)
                .HQItem("oracle_pumps_p1", 1)
                .Component("ether_crystal", 1)
                .Component("mythrite_frag", 1)
                .Component("psi_crystal", 1);
        }

        private void NoctshadowGaiters()
        {
            _builder.Create(RecipeType.NoctshadowGaiters, SkillType.Armorcraft)
                .Level(58)
                .Category(RecipeCategoryType.Feet)
                .NormalItem("noctshdw_gaits", 1)
                .HQItem("noctshdw_gaits_p1", 1)
                .Component("mythrite_frag", 3)
                .Component("psi_crystal", 1)
                .Component("biosteel_comp", 1);
        }

        private void ReinforcedStuddedBoots()
        {
            _builder.Create(RecipeType.ReinforcedStuddedBoots, SkillType.Armorcraft)
                .Level(58)
                .Category(RecipeCategoryType.Feet)
                .NormalItem("rein_stud_bts", 1)
                .HQItem("rein_stud_bts_p1", 1)
                .Component("mythrite_frag", 3)
                .Component("psi_crystal", 1)
                .Component("biosteel_comp", 1);
        }

        private void CombatLeggings()
        {
            _builder.Create(RecipeType.CombatLeggings, SkillType.Armorcraft)
                .Level(67)
                .Category(RecipeCategoryType.Feet)
                .NormalItem("combat_leggins", 1)
                .HQItem("combat_leggins_p1", 1)
                .Component("circuit_matrix", 1)
                .Component("mythrite_frag", 1)
                .Component("psi_crystal", 1);
        }

        private void EtherwovenShoes()
        {
            _builder.Create(RecipeType.EtherwovenShoes, SkillType.Armorcraft)
                .Level(69)
                .Category(RecipeCategoryType.Feet)
                .NormalItem("etherwvn_shoes", 1)
                .HQItem("etherwvn_shoes_p1", 1)
                .Component("ether_crystal", 1)
                .Component("mythrite_frag", 1)
                .Component("psi_crystal", 1);
        }

        private void InsulatedSocks()
        {
            _builder.Create(RecipeType.InsulatedSocks, SkillType.Armorcraft)
                .Level(70)
                .Category(RecipeCategoryType.Feet)
                .NormalItem("insulated_scks", 1)
                .HQItem("insulated_scks_p1", 1)
                .Component("mythrite_frag", 3)
                .Component("psi_crystal", 1)
                .Component("biosteel_comp", 1);
        }

        private void SilverguardGreaves()
        {
            _builder.Create(RecipeType.SilverguardGreaves, SkillType.Armorcraft)
                .Level(73)
                .Category(RecipeCategoryType.Feet)
                .NormalItem("silverg_grvs", 1)
                .HQItem("silverg_grvs_p1", 1)
                .Component("living_wood", 3)
                .Component("mythrite_frag", 1)
                .Component("psi_crystal", 1);
        }

        private void LeatherboundHighboots()
        {
            _builder.Create(RecipeType.LeatherboundHighboots, SkillType.Armorcraft)
                .Level(73)
                .Category(RecipeCategoryType.Feet)
                .NormalItem("leatherb_hboot", 1)
                .HQItem("leatherb_hboot_p1", 1)
                .Component("sinew_strand", 1)
                .Component("mythrite_frag", 1)
                .Component("psi_crystal", 1);
        }

        private void IroncladGreaves()
        {
            _builder.Create(RecipeType.IroncladGreaves, SkillType.Armorcraft)
                .Level(75)
                .Category(RecipeCategoryType.Feet)
                .NormalItem("ironclad_grevs", 1)
                .HQItem("ironclad_grevs_p1", 1)
                .Component("mythrite_frag", 3)
                .Component("psi_crystal", 1)
                .Component("biosteel_comp", 1);
        }

        private void ObsidianSabots()
        {
            _builder.Create(RecipeType.ObsidianSabots, SkillType.Armorcraft)
                .Level(76)
                .Category(RecipeCategoryType.Feet)
                .NormalItem("obsidian_sabots", 1)
                .HQItem("obsidian_sabots_p1", 1)
                .Component("titan_plate", 3)
                .Component("crystal_scale", 1)
                .Component("quantum_proc", 1);
        }

        private void IronMusketeersBattleSabatons()
        {
            _builder.Create(RecipeType.IronMusketeersBattleSabatons, SkillType.Armorcraft)
                .Level(77)
                .Category(RecipeCategoryType.Feet)
                .NormalItem("ironmus_battsbt", 1)
                .HQItem("ironmus_battsbt_p1", 1)
                .UltraItem("ironmus_battsb_p2", 1)
                .Component("titan_plate", 3)
                .Component("crystal_scale", 1)
                .Component("quantum_proc", 1);
        }

        private void PlateforgedLeggings()
        {
            _builder.Create(RecipeType.PlateforgedLeggings, SkillType.Armorcraft)
                .Level(80)
                .Category(RecipeCategoryType.Feet)
                .NormalItem("platefrg_legg", 1)
                .HQItem("platefrg_legg_p1", 1)
                .Component("titan_plate", 3)
                .Component("crystal_scale", 1)
                .Component("quantum_proc", 1);
        }

        private void WarcastersShoes()
        {
            _builder.Create(RecipeType.WarcastersShoes, SkillType.Armorcraft)
                .Level(82)
                .Category(RecipeCategoryType.Feet)
                .NormalItem("warcast_shoes", 1)
                .HQItem("warcast_shoes_p1", 1)
                .UltraItem("warcast_shoes_p2", 1)
                .Component("titan_plate", 3)
                .Component("crystal_scale", 1)
                .Component("quantum_proc", 1);
        }

        private void InfernalSabots()
        {
            _builder.Create(RecipeType.InfernalSabots, SkillType.Armorcraft)
                .Level(85)
                .Category(RecipeCategoryType.Feet)
                .NormalItem("infernal_sabots", 1)
                .HQItem("infernal_sabots_p1", 1)
                .Component("titan_plate", 3)
                .Component("crystal_scale", 1)
                .Component("quantum_proc", 1);
        }

        private void ChitinPlatedLeggings2()
        {
            _builder.Create(RecipeType.ChitinPlatedLeggings2, SkillType.Armorcraft)
                .Level(87)
                .Category(RecipeCategoryType.Feet)
                .NormalItem("chitin_leggins2", 1)
                .HQItem("chitin_leggins2_p1", 1)
                .Component("chitin_plate", 1)
                .Component("titan_plate", 1)
                .Component("crystal_scale", 1);
        }

        private void WarbornSollerets()
        {
            _builder.Create(RecipeType.WarbornSollerets, SkillType.Armorcraft)
                .Level(91)
                .Category(RecipeCategoryType.Feet)
                .NormalItem("warborn_sollret", 1)
                .HQItem("warborn_sollret_p1", 1)
                .Component("titan_plate", 3)
                .Component("crystal_scale", 1)
                .Component("quantum_proc", 1);
        }

        private void RaptorstrikeLedelsens()
        {
            _builder.Create(RecipeType.RaptorstrikeLedelsens, SkillType.Armorcraft)
                .Level(94)
                .Category(RecipeCategoryType.Feet)
                .NormalItem("raptor_ledelsns", 1)
                .HQItem("raptor_ledelsns_p1", 1)
                .Component("titan_plate", 3)
                .Component("crystal_scale", 1)
                .Component("quantum_proc", 1);
        }

        private void SteelguardGreaves()
        {
            _builder.Create(RecipeType.SteelguardGreaves, SkillType.Armorcraft)
                .Level(96)
                .Category(RecipeCategoryType.Feet)
                .NormalItem("steelg_grevs", 1)
                .HQItem("steelg_grevs_p1", 1)
                .Component("living_wood", 3)
                .Component("titan_plate", 1)
                .Component("crystal_scale", 1);
        }

        private void MythriteLeggings()
        {
            _builder.Create(RecipeType.MythriteLeggings, SkillType.Armorcraft)
                .Level(97)
                .Category(RecipeCategoryType.Feet)
                .NormalItem("mythrite_leggins", 1)
                .HQItem("mythrite_leggins_p1", 1)
                .Component("titan_plate", 3)
                .Component("crystal_scale", 1)
                .Component("quantum_proc", 1);
        }

        private void ObsidianCrowGaiters()
        {
            _builder.Create(RecipeType.ObsidianCrowGaiters, SkillType.Armorcraft)
                .Level(97)
                .Category(RecipeCategoryType.Feet)
                .NormalItem("obsidian_crow_ga", 1)
                .HQItem("obsidian_crow_ga_p1", 1)
                .Component("titan_plate", 3)
                .Component("crystal_scale", 1)
                .Component("quantum_proc", 1);
        }

        private void WoolenInsulatedSocks()
        {
            _builder.Create(RecipeType.WoolenInsulatedSocks, SkillType.Armorcraft)
                .Level(98)
                .Category(RecipeCategoryType.Feet)
                .NormalItem("wool_insul_scks", 1)
                .HQItem("wool_insul_scks_p1", 1)
                .Component("titan_plate", 3)
                .Component("crystal_scale", 1)
                .Component("quantum_proc", 1);
        }

        private void ReinforcedMoccasins()
        {
            _builder.Create(RecipeType.ReinforcedMoccasins, SkillType.Armorcraft)
                .Level(98)
                .Category(RecipeCategoryType.Feet)
                .NormalItem("rein_moccasins", 1)
                .HQItem("rein_moccasins_p1", 1)
                .Component("titan_plate", 3)
                .Component("crystal_scale", 1)
                .Component("quantum_proc", 1);
        }

        private void ShinobiShadowKyahan()
        {
            _builder.Create(RecipeType.ShinobiShadowKyahan, SkillType.Armorcraft)
                .Level(100)
                .Category(RecipeCategoryType.Feet)
                .NormalItem("shinobi_shdwky", 1)
                .HQItem("shinobi_shdwky_p1", 1)
                .Component("titan_plate", 3)
                .Component("crystal_scale", 1)
                .Component("quantum_proc", 1);
        }

        private void TacticianMagiciansWarPigaches()
        {
            _builder.Create(RecipeType.TacticianMagiciansWarPigaches, SkillType.Armorcraft)
                .Level(100)
                .Category(RecipeCategoryType.Feet)
                .NormalItem("tact_mag_warpga", 1)
                .Component("titan_plate", 3)
                .Component("crystal_scale", 1)
                .Component("quantum_proc", 1);
        }
    }
}