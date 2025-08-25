using System.Collections.Generic;
using Anvil.Services;
using XM.Progression.Skill;

namespace XM.Plugin.Craft.RecipeDefinition.Armorcraft
{
    [ServiceBinding(typeof(IRecipeListDefinition))]
    internal class HeadRecipes : IRecipeListDefinition
    {
        private readonly RecipeBuilder _builder = new();

        public Dictionary<RecipeType, RecipeDetail> BuildRecipes()
        {
            // Head Armor ordered by level (1-100)
            AurionAlloyCap();                       // Level 1
            LeatherTacticalBandana();               // Level 23
            CombatHachimaki();                      // Level 24
            WayfarersHat();                         // Level 27
            LegionnairesHelm();                     // Level 39
            TacticalHeadgear();                     // Level 42
            VanguardFaceguard();                    // Level 43
            MonksWargear();                         // Level 44
            BrassguardCap();                        // Level 48
            RegalFootmansBandana();                 // Level 49
            SagesEtherCirclet();                    // Level 49
            IroncladMask();                         // Level 50
            FederationCombatWrap();                 // Level 53
            PrecisionCombatBandana();               // Level 53
            ReflexHeadband();                       // Level 54
            WoolenTacticalHat();                    // Level 55
            ScholarsCirclet();                      // Level 57
            BrassguardMask();                       // Level 57
            KampfschallerWarHelm();                 // Level 57
            CenturionsWarVisor();                   // Level 57
            LegionnairesEtherCirclet();             // Level 57
            MercenaryCaptainsWargear();             // Level 57
            HoshikazuWarwrap();                     // Level 58
            EisenwarHelm();                         // Level 58
            ReinforcedBandana();                    // Level 59
            BoneforgedMask();                       // Level 60
            EruditesMindband();                     // Level 61
            EarthwovenHachimaki();                  // Level 61
            NoctshadeBeret();                       // Level 61
            CottonWeaveHeadband();                  // Level 62
            RegalSquiresHelm();                     // Level 62
            LizardscaleHelm();                      // Level 65
            CorrodedWarCap();                       // Level 67
            PaddedCombatCap();                      // Level 68
            CottonCombatWrap();                     // Level 69
            FortifiedGuardCap();                    // Level 70
            UnmarkedCombatCap();                    // Level 72
            GarrisonWarSallet();                    // Level 74
            CrimsonWarCap();                        // Level 74
            SilveredMask();                         // Level 74
            IroncladVisor();                        // Level 74
            IronMusketeersBattleArmet();            // Level 77
            LeatherboundBandana();                  // Level 79
            WarbornSallet();                        // Level 82
            MercenarysWarwrap();                    // Level 85
            ChitinPlatedMask();                     // Level 85
            ValkyrianMask();                        // Level 86
            ChitinPlatedMask2();                    // Level 87
            WalkureMask();                          // Level 89
            BandedWarhelm();                        // Level 89
            CorsairsTricorne();                     // Level 91
            WoolenWarCap();                         // Level 93
            CottonfieldHeadgear();                  // Level 94
            RaptorstrikeHelm();                     // Level 95
            MythriteSallet();                       // Level 96
            ShinobiShadowguard();                   // Level 97
            SteelguardVisor();                      // Level 98
            ObsidianCrowBeret();                    // Level 100
            TacticiansArcaneHat();                  // Level 100
            ShockguardMask();                       // Level 100

            return _builder.Build();
        }

        private void AurionAlloyCap()
        {
            _builder.Create(RecipeType.AurionAlloyCap, SkillType.Armorcraft)
                .Level(1)
                .Category(RecipeCategoryType.Head)
                .NormalItem("aurion_cap", 1)
                .HQItem("aurion_cap_p1", 1)
                .Component("aurion_ingot", 2)
                .Component("beast_hide", 1)
                .Component("bond_agent", 1);
        }

        private void LeatherTacticalBandana()
        {
            _builder.Create(RecipeType.LeatherTacticalBandana, SkillType.Armorcraft)
                .Level(23)
                .Category(RecipeCategoryType.Head)
                .NormalItem("leather_tacbnd", 1)
                .HQItem("leather_tacbnd_p1", 1)
                .Component("techno_fiber", 1)
                .Component("ferrite_core", 1);
        }

        private void CombatHachimaki()
        {
            _builder.Create(RecipeType.CombatHachimaki, SkillType.Armorcraft)
                .Level(24)
                .Category(RecipeCategoryType.Head)
                .NormalItem("combat_hachmk", 1)
                .HQItem("combat_hachmk_p1", 1)
                .Component("circuit_matrix", 1)
                .Component("ferrite_core", 1);
        }

        private void WayfarersHat()
        {
            _builder.Create(RecipeType.WayfarersHat, SkillType.Armorcraft)
                .Level(27)
                .Category(RecipeCategoryType.Head)
                .NormalItem("wayfarer_hat", 1)
                .Component("neural_inter", 1)
                .Component("ferrite_core", 1);
        }

        private void LegionnairesHelm()
        {
            _builder.Create(RecipeType.LegionnairesHelm, SkillType.Armorcraft)
                .Level(39)
                .Category(RecipeCategoryType.Head)
                .NormalItem("legion_helm", 1)
                .Component("neural_inter", 1)
                .Component("ferrite_core", 1)
                .Component("biosteel_comp", 1);
        }

        private void TacticalHeadgear()
        {
            _builder.Create(RecipeType.TacticalHeadgear, SkillType.Armorcraft)
                .Level(42)
                .Category(RecipeCategoryType.Head)
                .NormalItem("tacticl_headgr", 1)
                .HQItem("tacticl_headgr_p1", 1)
                .Component("circuit_matrix", 1)
                .Component("ferrite_core", 1)
                .Component("biosteel_comp", 1);
        }

        private void VanguardFaceguard()
        {
            _builder.Create(RecipeType.VanguardFaceguard, SkillType.Armorcraft)
                .Level(43)
                .Category(RecipeCategoryType.Head)
                .NormalItem("vang_facegrd", 1)
                .HQItem("vang_facegrd_p1", 1)
                .Component("living_wood", 2)
                .Component("ferrite_core", 1)
                .Component("biosteel_comp", 1);
        }

        private void MonksWargear()
        {
            _builder.Create(RecipeType.MonksWargear, SkillType.Armorcraft)
                .Level(44)
                .Category(RecipeCategoryType.Head)
                .NormalItem("monks_wargear", 1)
                .Component("ferrite_core", 2)
                .Component("biosteel_comp", 1)
                .Component("neural_inter", 1);
        }

        private void BrassguardCap()
        {
            _builder.Create(RecipeType.BrassguardCap, SkillType.Armorcraft)
                .Level(48)
                .Category(RecipeCategoryType.Head)
                .NormalItem("brassg_cap", 1)
                .HQItem("brassg_cap_p1", 1)
                .Component("living_wood", 2)
                .Component("mythrite_frag", 1)
                .Component("psi_crystal", 1);
        }

        private void RegalFootmansBandana()
        {
            _builder.Create(RecipeType.RegalFootmansBandana, SkillType.Armorcraft)
                .Level(49)
                .Category(RecipeCategoryType.Head)
                .NormalItem("regal_footbnd", 1)
                .Component("techno_fiber", 1)
                .Component("mythrite_frag", 1)
                .Component("psi_crystal", 1);
        }

        private void SagesEtherCirclet()
        {
            _builder.Create(RecipeType.SagesEtherCirclet, SkillType.Armorcraft)
                .Level(49)
                .Category(RecipeCategoryType.Head)
                .NormalItem("sages_ethercrc", 1)
                .Component("ether_crystal", 1)
                .Component("mythrite_frag", 1)
                .Component("psi_crystal", 1);
        }

        private void IroncladMask()
        {
            _builder.Create(RecipeType.IroncladMask, SkillType.Armorcraft)
                .Level(50)
                .Category(RecipeCategoryType.Head)
                .NormalItem("ironclad_mask", 1)
                .HQItem("ironclad_mask_p1", 1)
                .Component("neural_inter", 1)
                .Component("mythrite_frag", 1)
                .Component("psi_crystal", 1);
        }

        private void FederationCombatWrap()
        {
            _builder.Create(RecipeType.FederationCombatWrap, SkillType.Armorcraft)
                .Level(53)
                .Category(RecipeCategoryType.Head)
                .NormalItem("fed_combat_wrap", 1)
                .Component("techno_fiber", 1)
                .Component("mythrite_frag", 1)
                .Component("psi_crystal", 1);
        }

        private void PrecisionCombatBandana()
        {
            _builder.Create(RecipeType.PrecisionCombatBandana, SkillType.Armorcraft)
                .Level(53)
                .Category(RecipeCategoryType.Head)
                .NormalItem("prec_com_bndna", 1)
                .Component("techno_fiber", 1)
                .Component("mythrite_frag", 1)
                .Component("psi_crystal", 1);
        }

        private void ReflexHeadband()
        {
            _builder.Create(RecipeType.ReflexHeadband, SkillType.Armorcraft)
                .Level(54)
                .Category(RecipeCategoryType.Head)
                .NormalItem("reflex_headbnd", 1)
                .Component("mythrite_frag", 2)
                .Component("psi_crystal", 1)
                .Component("biosteel_comp", 1);
        }

        private void WoolenTacticalHat()
        {
            _builder.Create(RecipeType.WoolenTacticalHat, SkillType.Armorcraft)
                .Level(55)
                .Category(RecipeCategoryType.Head)
                .NormalItem("wool_tactc_hat", 1)
                .HQItem("wool_tactc_hat_p1", 1)
                .Component("circuit_matrix", 1)
                .Component("mythrite_frag", 1)
                .Component("psi_crystal", 1);
        }

        private void ScholarsCirclet()
        {
            _builder.Create(RecipeType.ScholarsCirclet, SkillType.Armorcraft)
                .Level(57)
                .Category(RecipeCategoryType.Head)
                .NormalItem("scholar_circlt", 1)
                .Component("neural_inter", 1)
                .Component("mythrite_frag", 1)
                .Component("psi_crystal", 1);
        }

        private void BrassguardMask()
        {
            _builder.Create(RecipeType.BrassguardMask, SkillType.Armorcraft)
                .Level(57)
                .Category(RecipeCategoryType.Head)
                .NormalItem("brassg_mask", 1)
                .HQItem("brassg_mask_p1", 1)
                .Component("living_wood", 2)
                .Component("mythrite_frag", 1)
                .Component("psi_crystal", 1);
        }

        private void KampfschallerWarHelm()
        {
            _builder.Create(RecipeType.KampfschallerWarHelm, SkillType.Armorcraft)
                .Level(57)
                .Category(RecipeCategoryType.Head)
                .NormalItem("kampfs_warhlm", 1)
                .Component("neural_inter", 1)
                .Component("mythrite_frag", 1)
                .Component("psi_crystal", 1);
        }

        private void CenturionsWarVisor()
        {
            _builder.Create(RecipeType.CenturionsWarVisor, SkillType.Armorcraft)
                .Level(57)
                .Category(RecipeCategoryType.Head)
                .NormalItem("centur_warvsr", 1)
                .Component("neural_inter", 1)
                .Component("mythrite_frag", 1)
                .Component("psi_crystal", 1);
        }

        private void LegionnairesEtherCirclet()
        {
            _builder.Create(RecipeType.LegionnairesEtherCirclet, SkillType.Armorcraft)
                .Level(57)
                .Category(RecipeCategoryType.Head)
                .NormalItem("legion_ethercl", 1)
                .Component("ether_crystal", 1)
                .Component("mythrite_frag", 1)
                .Component("psi_crystal", 1);
        }

        private void MercenaryCaptainsWargear()
        {
            _builder.Create(RecipeType.MercenaryCaptainsWargear, SkillType.Armorcraft)
                .Level(57)
                .Category(RecipeCategoryType.Head)
                .NormalItem("mercapt_wargear", 1)
                .Component("neural_inter", 1)
                .Component("mythrite_frag", 1)
                .Component("psi_crystal", 1);
        }

        private void HoshikazuWarwrap()
        {
            _builder.Create(RecipeType.HoshikazuWarwrap, SkillType.Armorcraft)
                .Level(58)
                .Category(RecipeCategoryType.Head)
                .NormalItem("hoshikazu_wrap", 1)
                .Component("techno_fiber", 1)
                .Component("mythrite_frag", 1)
                .Component("psi_crystal", 1);
        }

        private void EisenwarHelm()
        {
            _builder.Create(RecipeType.EisenwarHelm, SkillType.Armorcraft)
                .Level(58)
                .Category(RecipeCategoryType.Head)
                .NormalItem("eisenwar_helm", 1)
                .Component("neural_inter", 1)
                .Component("mythrite_frag", 1)
                .Component("psi_crystal", 1);
        }

        private void ReinforcedBandana()
        {
            _builder.Create(RecipeType.ReinforcedBandana, SkillType.Armorcraft)
                .Level(59)
                .Category(RecipeCategoryType.Head)
                .NormalItem("reinforced_bndn", 1)
                .HQItem("reinforced_bndn_p1", 1)
                .Component("techno_fiber", 1)
                .Component("mythrite_frag", 1)
                .Component("psi_crystal", 1);
        }

        private void BoneforgedMask()
        {
            _builder.Create(RecipeType.BoneforgedMask, SkillType.Armorcraft)
                .Level(60)
                .Category(RecipeCategoryType.Head)
                .NormalItem("bonefrg_mask", 1)
                .HQItem("bonefrg_mask_p1", 1)
                .Component("neural_inter", 1)
                .Component("mythrite_frag", 1)
                .Component("psi_crystal", 1);
        }

        private void EruditesMindband()
        {
            _builder.Create(RecipeType.EruditesMindband, SkillType.Armorcraft)
                .Level(61)
                .Category(RecipeCategoryType.Head)
                .NormalItem("erudite_mindbd", 1)
                .Component("neural_inter", 1)
                .Component("mythrite_frag", 1)
                .Component("psi_crystal", 1);
        }

        private void EarthwovenHachimaki()
        {
            _builder.Create(RecipeType.EarthwovenHachimaki, SkillType.Armorcraft)
                .Level(61)
                .Category(RecipeCategoryType.Head)
                .NormalItem("earthwvn_hachm", 1)
                .HQItem("earthwvn_hachm_p1", 1)
                .Component("mythrite_frag", 2)
                .Component("psi_crystal", 1)
                .Component("biosteel_comp", 1);
        }

        private void NoctshadeBeret()
        {
            _builder.Create(RecipeType.NoctshadeBeret, SkillType.Armorcraft)
                .Level(61)
                .Category(RecipeCategoryType.Head)
                .NormalItem("noctshade_beret", 1)
                .HQItem("noctshade_beret_p1", 1)
                .Component("mythrite_frag", 2)
                .Component("psi_crystal", 1)
                .Component("biosteel_comp", 1);
        }

        private void CottonWeaveHeadband()
        {
            _builder.Create(RecipeType.CottonWeaveHeadband, SkillType.Armorcraft)
                .Level(62)
                .Category(RecipeCategoryType.Head)
                .NormalItem("cottonwv_hdbnd", 1)
                .Component("mythrite_frag", 2)
                .Component("psi_crystal", 1)
                .Component("biosteel_comp", 1);
        }

        private void RegalSquiresHelm()
        {
            _builder.Create(RecipeType.RegalSquiresHelm, SkillType.Armorcraft)
                .Level(62)
                .Category(RecipeCategoryType.Head)
                .NormalItem("regal_squirehlm", 1)
                .Component("neural_inter", 1)
                .Component("mythrite_frag", 1)
                .Component("psi_crystal", 1);
        }

        private void LizardscaleHelm()
        {
            _builder.Create(RecipeType.LizardscaleHelm, SkillType.Armorcraft)
                .Level(65)
                .Category(RecipeCategoryType.Head)
                .NormalItem("lizardsc_helm", 1)
                .HQItem("lizardsc_helm_p1", 1)
                .Component("neural_inter", 1)
                .Component("mythrite_frag", 1)
                .Component("psi_crystal", 1);
        }

        private void CorrodedWarCap()
        {
            _builder.Create(RecipeType.CorrodedWarCap, SkillType.Armorcraft)
                .Level(67)
                .Category(RecipeCategoryType.Head)
                .NormalItem("corrod_war_cap", 1)
                .Component("neural_inter", 1)
                .Component("mythrite_frag", 1)
                .Component("psi_crystal", 1);
        }

        private void PaddedCombatCap()
        {
            _builder.Create(RecipeType.PaddedCombatCap, SkillType.Armorcraft)
                .Level(68)
                .Category(RecipeCategoryType.Head)
                .NormalItem("padded_com_cap", 1)
                .Component("circuit_matrix", 1)
                .Component("mythrite_frag", 1)
                .Component("psi_crystal", 1);
        }

        private void CottonCombatWrap()
        {
            _builder.Create(RecipeType.CottonCombatWrap, SkillType.Armorcraft)
                .Level(69)
                .Category(RecipeCategoryType.Head)
                .NormalItem("cotton_com_wrap", 1)
                .HQItem("cotton_com_wrap_p1", 1)
                .Component("techno_fiber", 1)
                .Component("mythrite_frag", 1)
                .Component("psi_crystal", 1);
        }

        private void FortifiedGuardCap()
        {
            _builder.Create(RecipeType.FortifiedGuardCap, SkillType.Armorcraft)
                .Level(70)
                .Category(RecipeCategoryType.Head)
                .NormalItem("fortified_grdc", 1)
                .Component("living_wood", 2)
                .Component("mythrite_frag", 1)
                .Component("psi_crystal", 1);
        }

        private void UnmarkedCombatCap()
        {
            _builder.Create(RecipeType.UnmarkedCombatCap, SkillType.Armorcraft)
                .Level(72)
                .Category(RecipeCategoryType.Head)
                .NormalItem("unmarked_com_cp", 1)
                .Component("circuit_matrix", 1)
                .Component("mythrite_frag", 1)
                .Component("psi_crystal", 1);
        }

        private void GarrisonWarSallet()
        {
            _builder.Create(RecipeType.GarrisonWarSallet, SkillType.Armorcraft)
                .Level(74)
                .Category(RecipeCategoryType.Head)
                .NormalItem("garrison_warsal", 1)
                .Component("mythrite_frag", 2)
                .Component("psi_crystal", 1)
                .Component("biosteel_comp", 1);
        }

        private void CrimsonWarCap()
        {
            _builder.Create(RecipeType.CrimsonWarCap, SkillType.Armorcraft)
                .Level(74)
                .Category(RecipeCategoryType.Head)
                .NormalItem("crimson_warcap", 1)
                .HQItem("crimson_warcap_p1", 1)
                .Component("neural_inter", 1)
                .Component("mythrite_frag", 1)
                .Component("psi_crystal", 1);
        }

        private void SilveredMask()
        {
            _builder.Create(RecipeType.SilveredMask, SkillType.Armorcraft)
                .Level(74)
                .Category(RecipeCategoryType.Head)
                .NormalItem("silvered_mask", 1)
                .HQItem("silvered_mask_p1", 1)
                .Component("neural_inter", 1)
                .Component("mythrite_frag", 1)
                .Component("psi_crystal", 1);
        }

        private void IroncladVisor()
        {
            _builder.Create(RecipeType.IroncladVisor, SkillType.Armorcraft)
                .Level(74)
                .Category(RecipeCategoryType.Head)
                .NormalItem("ironclad_visor", 1)
                .HQItem("ironclad_visor_p1", 1)
                .Component("neural_inter", 1)
                .Component("mythrite_frag", 1)
                .Component("psi_crystal", 1);
        }

        private void IronMusketeersBattleArmet()
        {
            _builder.Create(RecipeType.IronMusketeersBattleArmet, SkillType.Armorcraft)
                .Level(77)
                .Category(RecipeCategoryType.Head)
                .NormalItem("ironmus_battlrm", 1)
                .HQItem("ironmus_battlrm_p1", 1)
                .UltraItem("ironmus_battlr_p2", 1)
                .Component("titan_plate", 2)
                .Component("crystal_scale", 1)
                .Component("quantum_proc", 1);
        }

        private void LeatherboundBandana()
        {
            _builder.Create(RecipeType.LeatherboundBandana, SkillType.Armorcraft)
                .Level(79)
                .Category(RecipeCategoryType.Head)
                .NormalItem("leatherb_bndna", 1)
                .HQItem("leatherb_bndna_p1", 1)
                .Component("techno_fiber", 1)
                .Component("titan_plate", 1)
                .Component("crystal_scale", 1);
        }

        private void WarbornSallet()
        {
            _builder.Create(RecipeType.WarbornSallet, SkillType.Armorcraft)
                .Level(82)
                .Category(RecipeCategoryType.Head)
                .NormalItem("warborn_sallet", 1)
                .HQItem("warborn_sallet_p1", 1)
                .Component("titan_plate", 2)
                .Component("crystal_scale", 1)
                .Component("quantum_proc", 1);
        }

        private void MercenarysWarwrap()
        {
            _builder.Create(RecipeType.MercenarysWarwrap, SkillType.Armorcraft)
                .Level(85)
                .Category(RecipeCategoryType.Head)
                .NormalItem("merc_warwrap", 1)
                .Component("techno_fiber", 1)
                .Component("titan_plate", 1)
                .Component("crystal_scale", 1);
        }

        private void ChitinPlatedMask()
        {
            _builder.Create(RecipeType.ChitinPlatedMask, SkillType.Armorcraft)
                .Level(85)
                .Category(RecipeCategoryType.Head)
                .NormalItem("chitin_mask", 1)
                .HQItem("chitin_mask_p1", 1)
                .Component("chitin_plate", 1)
                .Component("titan_plate", 1)
                .Component("crystal_scale", 1);
        }

        private void ValkyrianMask()
        {
            _builder.Create(RecipeType.ValkyrianMask, SkillType.Armorcraft)
                .Level(86)
                .Category(RecipeCategoryType.Head)
                .NormalItem("valkyrian_mask", 1)
                .Component("neural_inter", 1)
                .Component("titan_plate", 1)
                .Component("crystal_scale", 1);
        }

        private void ChitinPlatedMask2()
        {
            _builder.Create(RecipeType.ChitinPlatedMask2, SkillType.Armorcraft)
                .Level(87)
                .Category(RecipeCategoryType.Head)
                .NormalItem("chitin_mask2", 1)
                .HQItem("chitin_mask2_p1", 1)
                .Component("chitin_plate", 1)
                .Component("titan_plate", 1)
                .Component("crystal_scale", 1);
        }

        private void WalkureMask()
        {
            _builder.Create(RecipeType.WalkureMask, SkillType.Armorcraft)
                .Level(89)
                .Category(RecipeCategoryType.Head)
                .NormalItem("walkure_mask", 1)
                .Component("neural_inter", 1)
                .Component("titan_plate", 1)
                .Component("crystal_scale", 1);
        }

        private void BandedWarhelm()
        {
            _builder.Create(RecipeType.BandedWarhelm, SkillType.Armorcraft)
                .Level(89)
                .Category(RecipeCategoryType.Head)
                .NormalItem("banded_warhelm", 1)
                .HQItem("banded_warhlm_p1", 1)
                .Component("neural_inter", 1)
                .Component("titan_plate", 1)
                .Component("crystal_scale", 1);
        }

        private void CorsairsTricorne()
        {
            _builder.Create(RecipeType.CorsairsTricorne, SkillType.Armorcraft)
                .Level(91)
                .Category(RecipeCategoryType.Head)
                .NormalItem("corsair_tricrn", 1)
                .HQItem("corsair_tricrn_p1", 1)
                .Component("titan_plate", 2)
                .Component("crystal_scale", 1)
                .Component("quantum_proc", 1);
        }

        private void WoolenWarCap()
        {
            _builder.Create(RecipeType.WoolenWarCap, SkillType.Armorcraft)
                .Level(93)
                .Category(RecipeCategoryType.Head)
                .NormalItem("wool_war_cap", 1)
                .HQItem("wool_war_cap_p1", 1)
                .Component("neural_inter", 1)
                .Component("titan_plate", 1)
                .Component("crystal_scale", 1);
        }

        private void CottonfieldHeadgear()
        {
            _builder.Create(RecipeType.CottonfieldHeadgear, SkillType.Armorcraft)
                .Level(94)
                .Category(RecipeCategoryType.Head)
                .NormalItem("cottonfld_headg", 1)
                .HQItem("cottonfld_headg_p1", 1)
                .Component("neural_inter", 1)
                .Component("titan_plate", 1)
                .Component("crystal_scale", 1);
        }

        private void RaptorstrikeHelm()
        {
            _builder.Create(RecipeType.RaptorstrikeHelm, SkillType.Armorcraft)
                .Level(95)
                .Category(RecipeCategoryType.Head)
                .NormalItem("raptor_helm", 1)
                .HQItem("raptor_helm_p1", 1)
                .Component("neural_inter", 1)
                .Component("titan_plate", 1)
                .Component("crystal_scale", 1);
        }

        private void MythriteSallet()
        {
            _builder.Create(RecipeType.MythriteSallet, SkillType.Armorcraft)
                .Level(96)
                .Category(RecipeCategoryType.Head)
                .NormalItem("mythrite_sallet", 1)
                .HQItem("mythrite_sallet_p1", 1)
                .Component("titan_plate", 2)
                .Component("crystal_scale", 1)
                .Component("quantum_proc", 1);
        }

        private void ShinobiShadowguard()
        {
            _builder.Create(RecipeType.ShinobiShadowguard, SkillType.Armorcraft)
                .Level(97)
                .Category(RecipeCategoryType.Head)
                .NormalItem("shinobi_shdwgd", 1)
                .HQItem("shinobi_shdwgd_p1", 1)
                .Component("living_wood", 2)
                .Component("titan_plate", 1)
                .Component("crystal_scale", 1);
        }

        private void SteelguardVisor()
        {
            _builder.Create(RecipeType.SteelguardVisor, SkillType.Armorcraft)
                .Level(98)
                .Category(RecipeCategoryType.Head)
                .NormalItem("steelg_visor", 1)
                .HQItem("steelg_visor_p1", 1)
                .Component("living_wood", 2)
                .Component("titan_plate", 1)
                .Component("crystal_scale", 1);
        }

        private void ObsidianCrowBeret()
        {
            _builder.Create(RecipeType.ObsidianCrowBeret, SkillType.Armorcraft)
                .Level(100)
                .Category(RecipeCategoryType.Head)
                .NormalItem("obsidian_crow_be", 1)
                .HQItem("obsidian_crow_be_p1", 1)
                .Component("titan_plate", 2)
                .Component("crystal_scale", 1)
                .Component("quantum_proc", 1);
        }

        private void TacticiansArcaneHat()
        {
            _builder.Create(RecipeType.TacticiansArcaneHat, SkillType.Armorcraft)
                .Level(100)
                .Category(RecipeCategoryType.Head)
                .NormalItem("tact_arcane_hat", 1)
                .Component("ether_crystal", 1)
                .Component("titan_plate", 1)
                .Component("crystal_scale", 1);
        }

        private void ShockguardMask()
        {
            _builder.Create(RecipeType.ShockguardMask, SkillType.Armorcraft)
                .Level(100)
                .Category(RecipeCategoryType.Head)
                .NormalItem("shockguard_mask", 1)
                .Component("living_wood", 2)
                .Component("titan_plate", 1)
                .Component("crystal_scale", 1);
        }
    }
}