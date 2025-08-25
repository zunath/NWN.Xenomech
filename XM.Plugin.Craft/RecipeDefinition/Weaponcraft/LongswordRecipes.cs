using System.Collections.Generic;
using Anvil.Services;
using XM.Progression.Skill;

namespace XM.Plugin.Craft.RecipeDefinition.Weaponcraft
{
    [ServiceBinding(typeof(IRecipeListDefinition))]
    internal class LongswordRecipes : IRecipeListDefinition
    {
        private readonly RecipeBuilder _builder = new();

        public Dictionary<RecipeType, RecipeDetail> BuildRecipes()
        {
            // Longswords ordered by level (2-98)
            AurionAlloyBlade();         // Level 2
            ResonanceBlade();           // Level 2
            VanguardCutter();           // Level 5
            ZenithianArcblade();        // Level 10
            SpathaTekEdge();            // Level 14
            HivefangSpatha();           // Level 16
            BrassArcblade();            // Level 18
            BilbronEdge();              // Level 20
            SparkfangEdge();            // Level 21
            EtherScimitar();            // Level 22
            FerriteSword();             // Level 26
            ArcbladeLongsword();        // Level 28
            DegenTek();                 // Level 32
            PulseTuck();                // Level 34
            TitanBroadsword();          // Level 36
            EtherFleuret();             // Level 40
            SteelKiljBlade();           // Level 42
            MythriteDegen();            // Level 50
            SanctifiedEdge();           // Level 44
            MythriteBlade();            // Level 54
            SparkfangDegen();           // Level 58
            RiftTulwar();               // Level 64
            AscendantBlade();           // Level 68
            InfernalDegen();            // Level 74
            CrescentShotel();           // Level 78
            SanctifiedDegen();          // Level 82
            NoviceDuelistsTuck();       // Level 84
            CombatCastersTalon();       // Level 86
            ArcFalchion();              // Level 88
            InfernalEdge();             // Level 92
            VanguardKnightsSword();     // Level 96
            MusketeersEdge();           // Level 98

            return _builder.Build();
        }

        private void AurionAlloyBlade()
        {
            _builder.Create(RecipeType.AurionAlloyBlade, SkillType.Weaponcraft)
                .Level(2)
                .Category(RecipeCategoryType.Longsword)
                .NormalItem("aurion_blade", 1)
                .HQItem("aurion_blade_p1", 1)
                .Component("aurion_ingot", 3)
                .Component("beast_hide", 1)
                .Component("flux_compound", 1);
        }

        private void ResonanceBlade()
        {
            _builder.Create(RecipeType.ResonanceBlade, SkillType.Weaponcraft)
                .Level(2)
                .Category(RecipeCategoryType.Longsword)
                .NormalItem("reson_blade", 1)
                .HQItem("reson_blade_p1", 1)
                .Component("aurion_ingot", 3)
                .Component("beast_hide", 1)
                .Component("flux_compound", 1);
        }

        private void VanguardCutter()
        {
            _builder.Create(RecipeType.VanguardCutter, SkillType.Weaponcraft)
                .Level(5)
                .Category(RecipeCategoryType.Longsword)
                .NormalItem("vanguard_cutr", 1)
                .HQItem("vanguard_cutr_p1", 1)
                .Component("aurion_ingot", 3)
                .Component("beast_hide", 1)
                .Component("flux_compound", 1);
        }

        private void ZenithianArcblade()
        {
            _builder.Create(RecipeType.ZenithianArcblade, SkillType.Weaponcraft)
                .Level(10)
                .Category(RecipeCategoryType.Longsword)
                .NormalItem("zenith_arcbl", 1)
                .HQItem("zenith_arcbl_p1", 1)
                .Component("aurion_ingot", 3)
                .Component("beast_hide", 1)
                .Component("flux_compound", 1)
                .Component("power_cell", 1);
        }

        private void SpathaTekEdge()
        {
            _builder.Create(RecipeType.SpathaTekEdge, SkillType.Weaponcraft)
                .Level(14)
                .Category(RecipeCategoryType.Longsword)
                .NormalItem("spathatek_edg", 1)
                .HQItem("spathatek_edg_p1", 1)
                .Component("aurion_ingot", 3)
                .Component("beast_hide", 1)
                .Component("flux_compound", 1);
        }

        private void HivefangSpatha()
        {
            _builder.Create(RecipeType.HivefangSpatha, SkillType.Weaponcraft)
                .Level(16)
                .Category(RecipeCategoryType.Longsword)
                .NormalItem("hivefang_spth", 1)
                .HQItem("hivefang_spth_p1", 1)
                .Component("ferrite_core", 3)
                .Component("circuit_matrix", 1)
                .Component("power_cell", 1);
        }

        private void BrassArcblade()
        {
            _builder.Create(RecipeType.BrassArcblade, SkillType.Weaponcraft)
                .Level(18)
                .Category(RecipeCategoryType.Longsword)
                .NormalItem("brass_arcbl", 1)
                .HQItem("brass_arcbl_p1", 1)
                .Component("ferrite_core", 3)
                .Component("circuit_matrix", 1)
                .Component("power_cell", 1)
                .Component("amp_crystal", 1);
        }

        private void BilbronEdge()
        {
            _builder.Create(RecipeType.BilbronEdge, SkillType.Weaponcraft)
                .Level(20)
                .Category(RecipeCategoryType.Longsword)
                .NormalItem("bilbron_edge", 1)
                .HQItem("bilbron_edge_p1", 1)
                .Component("ferrite_core", 3)
                .Component("circuit_matrix", 1)
                .Component("power_cell", 1);
        }

        private void SparkfangEdge()
        {
            _builder.Create(RecipeType.SparkfangEdge, SkillType.Weaponcraft)
                .Level(21)
                .Category(RecipeCategoryType.Longsword)
                .NormalItem("sparkfang_edg", 1)
                .HQItem("sparkfang_edg_p1", 1)
                .Component("ferrite_core", 3)
                .Component("circuit_matrix", 1)
                .Component("power_cell", 1)
                .Component("amp_crystal", 1);
        }

        private void EtherScimitar()
        {
            _builder.Create(RecipeType.EtherScimitar, SkillType.Weaponcraft)
                .Level(22)
                .Category(RecipeCategoryType.Longsword)
                .NormalItem("ether_scimita", 1)
                .HQItem("ether_scimta_p1", 1)
                .Component("ferrite_core", 3)
                .Component("circuit_matrix", 1)
                .Component("power_cell", 1)
                .Component("ether_crystal", 1);
        }

        private void FerriteSword()
        {
            _builder.Create(RecipeType.FerriteSword, SkillType.Weaponcraft)
                .Level(26)
                .Category(RecipeCategoryType.Longsword)
                .NormalItem("ferrite_sword", 1)
                .HQItem("ferrite_sword_p1", 1)
                .Component("brass_sheet", 3)
                .Component("mythrite_frag", 2)
                .Component("enhance_serum", 1);
        }

        private void ArcbladeLongsword()
        {
            _builder.Create(RecipeType.ArcbladeLongsword, SkillType.Weaponcraft)
                .Level(28)
                .Category(RecipeCategoryType.Longsword)
                .NormalItem("arcblad_lngsw", 1)
                .HQItem("arcblad_lngsw_p1", 1)
                .Component("brass_sheet", 3)
                .Component("mythrite_frag", 2)
                .Component("enhance_serum", 1)
                .Component("power_cell", 1);
        }

        private void DegenTek()
        {
            _builder.Create(RecipeType.DegenTek, SkillType.Weaponcraft)
                .Level(32)
                .Category(RecipeCategoryType.Longsword)
                .NormalItem("degen_tek", 1)
                .HQItem("degen_tek_p1", 1)
                .Component("brass_sheet", 3)
                .Component("mythrite_frag", 2)
                .Component("enhance_serum", 1);
        }

        private void PulseTuck()
        {
            _builder.Create(RecipeType.PulseTuck, SkillType.Weaponcraft)
                .Level(34)
                .Category(RecipeCategoryType.Longsword)
                .NormalItem("pulse_tuck", 1)
                .HQItem("pulse_tuck_p1", 1)
                .Component("brass_sheet", 3)
                .Component("mythrite_frag", 2)
                .Component("enhance_serum", 1);
        }

        private void TitanBroadsword()
        {
            _builder.Create(RecipeType.TitanBroadsword, SkillType.Weaponcraft)
                .Level(36)
                .Category(RecipeCategoryType.Longsword)
                .NormalItem("titan_broadsw", 1)
                .HQItem("titan_broadsw_p1", 1)
                .Component("brass_sheet", 3)
                .Component("mythrite_frag", 2)
                .Component("enhance_serum", 1);
        }

        private void EtherFleuret()
        {
            _builder.Create(RecipeType.EtherFleuret, SkillType.Weaponcraft)
                .Level(40)
                .Category(RecipeCategoryType.Longsword)
                .NormalItem("ether_fleuret", 1)
                .HQItem("ether_fleurt_p1", 1)
                .Component("brass_sheet", 3)
                .Component("mythrite_frag", 2)
                .Component("enhance_serum", 1)
                .Component("ether_crystal", 1);
        }

        private void SteelKiljBlade()
        {
            _builder.Create(RecipeType.SteelKiljBlade, SkillType.Weaponcraft)
                .Level(42)
                .Category(RecipeCategoryType.Longsword)
                .NormalItem("steel_kiljbl", 1)
                .HQItem("steel_kiljbl_p1", 1)
                .Component("brass_sheet", 3)
                .Component("mythrite_frag", 2)
                .Component("enhance_serum", 1);
        }

        private void SanctifiedEdge()
        {
            _builder.Create(RecipeType.SanctifiedEdge, SkillType.Weaponcraft)
                .Level(44)
                .Category(RecipeCategoryType.Longsword)
                .NormalItem("sanct_edge", 1)
                .HQItem("sanct_edge_p1", 1)
                .Component("brass_sheet", 3)
                .Component("mythrite_frag", 2)
                .Component("enhance_serum", 1);
        }

        private void MythriteDegen()
        {
            _builder.Create(RecipeType.MythriteDegen, SkillType.Weaponcraft)
                .Level(50)
                .Category(RecipeCategoryType.Longsword)
                .NormalItem("mythrite_dgn", 1)
                .HQItem("mythrite_dgn_p1", 1)
                .Component("mythrite_frag", 4)
                .Component("psi_crystal", 2)
                .Component("biosteel_comp", 1);
        }

        private void MythriteBlade()
        {
            _builder.Create(RecipeType.MythriteBlade, SkillType.Weaponcraft)
                .Level(54)
                .Category(RecipeCategoryType.Longsword)
                .NormalItem("mythrite_bld", 1)
                .HQItem("mythrite_bld_p1", 1)
                .Component("mythrite_frag", 4)
                .Component("psi_crystal", 2)
                .Component("biosteel_comp", 1);
        }

        private void SparkfangDegen()
        {
            _builder.Create(RecipeType.SparkfangDegen, SkillType.Weaponcraft)
                .Level(58)
                .Category(RecipeCategoryType.Longsword)
                .NormalItem("sparkfang_dgn", 1)
                .HQItem("sparkfang_dgn_p1", 1)
                .Component("mythrite_frag", 4)
                .Component("psi_crystal", 2)
                .Component("biosteel_comp", 1)
                .Component("power_cell", 1);
        }

        private void RiftTulwar()
        {
            _builder.Create(RecipeType.RiftTulwar, SkillType.Weaponcraft)
                .Level(64)
                .Category(RecipeCategoryType.Longsword)
                .NormalItem("rift_tulwar", 1)
                .HQItem("rift_tulwar_p1", 1)
                .Component("mythrite_frag", 4)
                .Component("psi_crystal", 2)
                .Component("biosteel_comp", 1);
        }

        private void AscendantBlade()
        {
            _builder.Create(RecipeType.AscendantBlade, SkillType.Weaponcraft)
                .Level(68)
                .Category(RecipeCategoryType.Longsword)
                .NormalItem("ascend_bld", 1)
                .HQItem("ascend_bld_p1", 1)
                .Component("mythrite_frag", 4)
                .Component("psi_crystal", 2)
                .Component("biosteel_comp", 1);
        }

        private void InfernalDegen()
        {
            _builder.Create(RecipeType.InfernalDegen, SkillType.Weaponcraft)
                .Level(74)
                .Category(RecipeCategoryType.Longsword)
                .NormalItem("infernal_dgn", 1)
                .HQItem("infernal_dgn_p1", 1)
                .Component("mythrite_frag", 4)
                .Component("psi_crystal", 2)
                .Component("biosteel_comp", 1)
                .Component("plasma_conduit", 1);
        }

        private void CrescentShotel()
        {
            _builder.Create(RecipeType.CrescentShotel, SkillType.Weaponcraft)
                .Level(78)
                .Category(RecipeCategoryType.Longsword)
                .NormalItem("crescent_shtl", 1)
                .HQItem("crescent_shtl_p1", 1)
                .Component("titan_plate", 4)
                .Component("quantum_proc", 2)
                .Component("quantmyst_core", 1);
        }

        private void SanctifiedDegen()
        {
            _builder.Create(RecipeType.SanctifiedDegen, SkillType.Weaponcraft)
                .Level(82)
                .Category(RecipeCategoryType.Longsword)
                .NormalItem("sanct_dgn", 1)
                .HQItem("sanct_dgn_p1", 1)
                .Component("titan_plate", 4)
                .Component("quantum_proc", 2)
                .Component("quantmyst_core", 1);
        }

        private void NoviceDuelistsTuck()
        {
            _builder.Create(RecipeType.NoviceDuelistsTuck, SkillType.Weaponcraft)
                .Level(84)
                .Category(RecipeCategoryType.Longsword)
                .NormalItem("nov_duel_tuck", 1)
                .HQItem("nov_duel_tuck_p1", 1)
                .UltraItem("nov_duel_tuck_p2", 1)
                .Component("titan_plate", 4)
                .Component("quantum_proc", 2)
                .Component("quantmyst_core", 1);
        }

        private void CombatCastersTalon()
        {
            _builder.Create(RecipeType.CombatCastersTalon, SkillType.Weaponcraft)
                .Level(86)
                .Category(RecipeCategoryType.Longsword)
                .NormalItem("combatcast_tln", 1)
                .HQItem("combatcast_tln_p1", 1)
                .UltraItem("combatcast_tln_p2", 1)
                .Component("titan_plate", 4)
                .Component("quantum_proc", 2)
                .Component("quantmyst_core", 1);
        }

        private void ArcFalchion()
        {
            _builder.Create(RecipeType.ArcFalchion, SkillType.Weaponcraft)
                .Level(88)
                .Category(RecipeCategoryType.Longsword)
                .NormalItem("arc_falchn", 1)
                .HQItem("arc_falchn_p1", 1)
                .Component("titan_plate", 4)
                .Component("quantum_proc", 2)
                .Component("quantmyst_core", 1)
                .Component("power_cell", 1);
        }

        private void InfernalEdge()
        {
            _builder.Create(RecipeType.InfernalEdge, SkillType.Weaponcraft)
                .Level(92)
                .Category(RecipeCategoryType.Longsword)
                .NormalItem("infernal_edg", 1)
                .HQItem("infernal_edg_p1", 1)
                .Component("titan_plate", 4)
                .Component("quantum_proc", 2)
                .Component("quantmyst_core", 1)
                .Component("plasma_conduit", 1);
        }

        private void VanguardKnightsSword()
        {
            _builder.Create(RecipeType.VanguardKnightsSword, SkillType.Weaponcraft)
                .Level(96)
                .Category(RecipeCategoryType.Longsword)
                .NormalItem("vang_knight_swd", 1)
                .HQItem("vang_knight_swd_p1", 1)
                .Component("titan_plate", 4)
                .Component("quantum_proc", 2)
                .Component("quantmyst_core", 1);
        }

        private void MusketeersEdge()
        {
            _builder.Create(RecipeType.MusketeersEdge, SkillType.Weaponcraft)
                .Level(98)
                .Category(RecipeCategoryType.Longsword)
                .NormalItem("musketeer_edg", 1)
                .Component("titan_plate", 4)
                .Component("quantum_proc", 2)
                .Component("quantmyst_core", 1);
        }
    }
}