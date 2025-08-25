using System.Collections.Generic;
using Anvil.Services;
using XM.Progression.Skill;

namespace XM.Plugin.Craft.RecipeDefinition.Fabrication
{
    [ServiceBinding(typeof(IRecipeListDefinition))]
    internal class WaistRecipes : IRecipeListDefinition
    {
        private readonly RecipeBuilder _builder = new();

        public Dictionary<RecipeType, RecipeDetail> BuildRecipes()
        {
            // Waist recipes ordered by level (3-97)
            BloodforgedStone();                         // Level 3
            WovenHekoObi();                             // Level 13
            LeatherboundBelt();                         // Level 17
            TitanPlateBelt();                           // Level 21
            LizardscaleBelt();                          // Level 31
            WarriorsWarBelt();                          // Level 32
            ArcaneInfusedBelt();                        // Level 35
            MohbwaWardenSash();                         // Level 39
            SilverthreadObi();                          // Level 40
            ReinforcedChainBelt();                      // Level 50
            ForceboundBelt();                           // Level 53
            GriotsResonanceBelt();                      // Level 55
            ShamansEtherBelt();                         // Level 57
            HojutsuCombatBelt();                        // Level 64
            AuricGoldObi();                             // Level 66
            SilverguardBelt();                          // Level 67
            ReinforcedCorsette();                       // Level 80
            TacticalWaistbelt();                        // Level 80
            QiqirnScoutsSash();                         // Level 82
            WarforgedSwordbelt();                       // Level 86
            LifewardenBelt();                           // Level 97

            return _builder.Build();
        }

        private void BloodforgedStone()
        {
            _builder.Create(RecipeType.BloodforgedStone, SkillType.Fabrication)
                .Level(3)
                .Category(RecipeCategoryType.Waist)
                .NormalItem("bloodforged_stn", 1)
                .HQItem("bloodforged_p1", 1)
                .Component("aurion_ingot", 2)
                .Component("bond_agent", 1)
                .Component("beast_hide", 1);
        }

        private void WovenHekoObi()
        {
            _builder.Create(RecipeType.WovenHekoObi, SkillType.Fabrication)
                .Level(13)
                .Category(RecipeCategoryType.Waist)
                .NormalItem("woven_heko_obi", 1)
                .HQItem("woven_hekoobi_p1", 1)
                .Component("beast_hide", 2)
                .Component("sinew_strand", 1)
                .Component("bond_agent", 1);
        }

        private void LeatherboundBelt()
        {
            _builder.Create(RecipeType.LeatherboundBelt, SkillType.Fabrication)
                .Level(17)
                .Category(RecipeCategoryType.Waist)
                .NormalItem("leatherb_belt", 1)
                .HQItem("leatherb_belt_p1", 1)
                .Component("beast_hide", 3)
                .Component("ferrite_core", 1)
                .Component("circuit_matrix", 1);
        }

        private void TitanPlateBelt()
        {
            _builder.Create(RecipeType.TitanPlateBelt, SkillType.Fabrication)
                .Level(21)
                .Category(RecipeCategoryType.Waist)
                .NormalItem("titan_plate_blt", 1)
                .Component("ferrite_core", 3)
                .Component("circuit_matrix", 2)
                .Component("enhance_serum", 1);
        }

        private void LizardscaleBelt()
        {
            _builder.Create(RecipeType.LizardscaleBelt, SkillType.Fabrication)
                .Level(31)
                .Category(RecipeCategoryType.Waist)
                .NormalItem("lizardsc_belt", 1)
                .HQItem("lizardsc_belt_p1", 1)
                .Component("beast_hide", 3)
                .Component("brass_sheet", 2)
                .Component("amp_crystal", 1);
        }

        private void WarriorsWarBelt()
        {
            _builder.Create(RecipeType.WarriorsWarBelt, SkillType.Fabrication)
                .Level(32)
                .Category(RecipeCategoryType.Waist)
                .NormalItem("warrior_war_blt", 1)
                .HQItem("warrior_warblt_p1", 1)
                .Component("beast_hide", 3)
                .Component("brass_sheet", 1)
                .Component("amp_crystal", 1)
                .Component("purify_filter", 1);
        }

        private void ArcaneInfusedBelt()
        {
            _builder.Create(RecipeType.ArcaneInfusedBelt, SkillType.Fabrication)
                .Level(35)
                .Category(RecipeCategoryType.Waist)
                .NormalItem("arcane_inf_belt", 1)
                .HQItem("arcane_infblt_p1", 1)
                .Component("techno_fiber", 2)
                .Component("brass_sheet", 2)
                .Component("amp_crystal", 1)
                .Component("purify_filter", 1);
        }

        private void MohbwaWardenSash()
        {
            _builder.Create(RecipeType.MohbwaWardenSash, SkillType.Fabrication)
                .Level(39)
                .Category(RecipeCategoryType.Waist)
                .NormalItem("mohbwa_ward_sas", 1)
                .HQItem("mohbwa_wards_p1", 1)
                .Component("beast_hide", 3)
                .Component("brass_sheet", 1)
                .Component("amp_crystal", 1);
        }

        private void SilverthreadObi()
        {
            _builder.Create(RecipeType.SilverthreadObi, SkillType.Fabrication)
                .Level(40)
                .Category(RecipeCategoryType.Waist)
                .NormalItem("silvert_obi", 1)
                .HQItem("silvert_obi_p1", 1)
                .Component("techno_fiber", 3)
                .Component("brass_sheet", 2)
                .Component("amp_crystal", 1);
        }

        private void ReinforcedChainBelt()
        {
            _builder.Create(RecipeType.ReinforcedChainBelt, SkillType.Fabrication)
                .Level(50)
                .Category(RecipeCategoryType.Waist)
                .NormalItem("reinforce_ch_blt", 1)
                .HQItem("reinforce_chb_p1", 1)
                .Component("mythrite_frag", 4)
                .Component("psi_crystal", 1)
                .Component("purify_filter", 1);
        }

        private void ForceboundBelt()
        {
            _builder.Create(RecipeType.ForceboundBelt, SkillType.Fabrication)
                .Level(53)
                .Category(RecipeCategoryType.Waist)
                .NormalItem("forcebound_belt", 1)
                .Component("mythrite_frag", 3)
                .Component("psi_crystal", 2)
                .Component("purify_filter", 1);
        }

        private void GriotsResonanceBelt()
        {
            _builder.Create(RecipeType.GriotsResonanceBelt, SkillType.Fabrication)
                .Level(55)
                .Category(RecipeCategoryType.Waist)
                .NormalItem("griot_reso_belt", 1)
                .HQItem("griot_reso_p1", 1)
                .Component("mythrite_frag", 3)
                .Component("psi_crystal", 2)
                .Component("spirit_ess", 1)
                .Component("purify_filter", 1);
        }

        private void ShamansEtherBelt()
        {
            _builder.Create(RecipeType.ShamansEtherBelt, SkillType.Fabrication)
                .Level(57)
                .Category(RecipeCategoryType.Waist)
                .NormalItem("shaman_ether_blt", 1)
                .HQItem("shaman_ether_p1", 1)
                .Component("mythrite_frag", 3)
                .Component("psi_crystal", 2)
                .Component("spirit_ess", 2);
        }

        private void HojutsuCombatBelt()
        {
            _builder.Create(RecipeType.HojutsuCombatBelt, SkillType.Fabrication)
                .Level(64)
                .Category(RecipeCategoryType.Waist)
                .NormalItem("hojutsu_com_belt", 1)
                .HQItem("hojutsu_combl_p1", 1)
                .Component("beast_hide", 4)
                .Component("mythrite_frag", 2)
                .Component("psi_crystal", 1)
                .Component("purify_filter", 1);
        }

        private void AuricGoldObi()
        {
            _builder.Create(RecipeType.AuricGoldObi, SkillType.Fabrication)
                .Level(66)
                .Category(RecipeCategoryType.Waist)
                .NormalItem("auric_gold_obi", 1)
                .HQItem("auric_goldobi_p1", 1)
                .Component("mythrite_frag", 4)
                .Component("psi_crystal", 2)
                .Component("spirit_ess", 1);
        }

        private void SilverguardBelt()
        {
            _builder.Create(RecipeType.SilverguardBelt, SkillType.Fabrication)
                .Level(67)
                .Category(RecipeCategoryType.Waist)
                .NormalItem("silverg_belt", 1)
                .HQItem("silverg_belt_p1", 1)
                .Component("mythrite_frag", 4)
                .Component("psi_crystal", 1)
                .Component("purify_filter", 2);
        }

        private void ReinforcedCorsette()
        {
            _builder.Create(RecipeType.ReinforcedCorsette, SkillType.Fabrication)
                .Level(80)
                .Category(RecipeCategoryType.Waist)
                .NormalItem("reinforce_corset", 1)
                .HQItem("reinforce_cors_p1", 1)
                .Component("titan_plate", 3)
                .Component("quantum_proc", 1)
                .Component("nano_enchant", 1)
                .Component("harmonic_alloy", 1);
        }

        private void TacticalWaistbelt()
        {
            _builder.Create(RecipeType.TacticalWaistbelt, SkillType.Fabrication)
                .Level(80)
                .Category(RecipeCategoryType.Waist)
                .NormalItem("tactic_waistbelt", 1)
                .HQItem("tactic_waist_p1", 1)
                .Component("titan_plate", 3)
                .Component("quantum_proc", 2)
                .Component("sync_core", 1);
        }

        private void QiqirnScoutsSash()
        {
            _builder.Create(RecipeType.QiqirnScoutsSash, SkillType.Fabrication)
                .Level(82)
                .Category(RecipeCategoryType.Waist)
                .NormalItem("qiqirn_scout_sas", 1)
                .HQItem("qiqirn_scout_p1", 1)
                .Component("beast_hide", 4)
                .Component("titan_plate", 2)
                .Component("quantum_proc", 1)
                .Component("sync_core", 1);
        }

        private void WarforgedSwordbelt()
        {
            _builder.Create(RecipeType.WarforgedSwordbelt, SkillType.Fabrication)
                .Level(86)
                .Category(RecipeCategoryType.Waist)
                .NormalItem("warforged_swdbel", 1)
                .HQItem("warforged_swd_p1", 1)
                .Component("titan_plate", 4)
                .Component("quantum_proc", 2)
                .Component("harmonic_alloy", 2);
        }

        private void LifewardenBelt()
        {
            _builder.Create(RecipeType.LifewardenBelt, SkillType.Fabrication)
                .Level(97)
                .Category(RecipeCategoryType.Waist)
                .NormalItem("lifewarden_belt", 1)
                .Component("titan_plate", 4)
                .Component("quantum_proc", 3)
                .Component("harmonic_alloy", 2);
        }
    }
}