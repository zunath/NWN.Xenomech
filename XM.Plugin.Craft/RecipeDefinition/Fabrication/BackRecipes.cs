using System.Collections.Generic;
using Anvil.Services;
using XM.Progression.Skill;

namespace XM.Plugin.Craft.RecipeDefinition.Fabrication
{
    [ServiceBinding(typeof(IRecipeListDefinition))]
    internal class BackRecipes : IRecipeListDefinition
    {
        private readonly RecipeBuilder _builder = new();

        public Dictionary<RecipeType, RecipeDetail> BuildRecipes()
        {
            // Back recipes ordered by level (5-100)
            HarestrideMantle();                         // Level 5
            EtherwovenCape();                           // Level 13
            WayfarersMantle();                          // Level 25
            DhalmelhideMantle();                        // Level 36
            LizardscaleMantle();                        // Level 37
            CottonWovenCape();                          // Level 38
            NomadsTrailMantle();                        // Level 46
            BonzesEtherCape();                          // Level 50
            PrecisionMantle();                          // Level 51
            DirewolfMantle();                           // Level 56
            MidnightShadowCape();                       // Level 62
            ObsidianCloak();                            // Level 67
            RamguardMantle();                           // Level 73
            CavaliersWarMantle();                       // Level 73
            CrimsonDrape();                             // Level 83
            AuroraSkyMantle();                          // Level 88
            ShadowJaguarMantle();                       // Level 94
            RaptorstrikeMantle();                       // Level 99
            ArcaneVeilMantle();                         // Level 99
            TitanWarMantle();                           // Level 100
            SharpshootersMantle();                      // Level 100

            return _builder.Build();
        }

        private void HarestrideMantle()
        {
            _builder.Create(RecipeType.HarestrideMantle, SkillType.Fabrication)
                .Level(5)
                .Category(RecipeCategoryType.Back)
                .NormalItem("harestride_mnt", 1)
                .HQItem("harestride_p1", 1)
                .Component("living_wood", 2)
                .Component("ether_crystal", 1)
                .Component("flux_compound", 1);
        }

        private void EtherwovenCape()
        {
            _builder.Create(RecipeType.EtherwovenCape, SkillType.Fabrication)
                .Level(13)
                .Category(RecipeCategoryType.Back)
                .NormalItem("etherwvn_cape", 1)
                .HQItem("etherwvn_cape_p1", 1)
                .Component("techno_fiber", 2)
                .Component("ether_crystal", 1)
                .Component("flux_compound", 1);
        }

        private void WayfarersMantle()
        {
            _builder.Create(RecipeType.WayfarersMantle, SkillType.Fabrication)
                .Level(25)
                .Category(RecipeCategoryType.Back)
                .NormalItem("wayfarer_mantle", 1)
                .Component("living_wood", 3)
                .Component("ferrite_core", 2)
                .Component("circuit_matrix", 1)
                .Component("enhance_serum", 1);
        }

        private void DhalmelhideMantle()
        {
            _builder.Create(RecipeType.DhalmelhideMantle, SkillType.Fabrication)
                .Level(36)
                .Category(RecipeCategoryType.Back)
                .NormalItem("dhalmel_mantle", 1)
                .HQItem("dhalmel_mant_p1", 1)
                .Component("beast_hide", 4)
                .Component("brass_sheet", 2)
                .Component("amp_crystal", 1);
        }

        private void LizardscaleMantle()
        {
            _builder.Create(RecipeType.LizardscaleMantle, SkillType.Fabrication)
                .Level(37)
                .Category(RecipeCategoryType.Back)
                .NormalItem("lizardsc_mantle", 1)
                .HQItem("lizardsc_mant_p1", 1)
                .Component("beast_hide", 3)
                .Component("brass_sheet", 2)
                .Component("amp_crystal", 1);
        }

        private void CottonWovenCape()
        {
            _builder.Create(RecipeType.CottonWovenCape, SkillType.Fabrication)
                .Level(38)
                .Category(RecipeCategoryType.Back)
                .NormalItem("cottonwv_cape", 1)
                .HQItem("cottonwv_cape_p1", 1)
                .Component("techno_fiber", 3)
                .Component("brass_sheet", 1)
                .Component("amp_crystal", 1);
        }

        private void NomadsTrailMantle()
        {
            _builder.Create(RecipeType.NomadsTrailMantle, SkillType.Fabrication)
                .Level(46)
                .Category(RecipeCategoryType.Back)
                .NormalItem("nomad_trail_mnt", 1)
                .HQItem("nomad_trail_p1", 1)
                .Component("beast_hide", 4)
                .Component("mythrite_frag", 2)
                .Component("psi_crystal", 1);
        }

        private void BonzesEtherCape()
        {
            _builder.Create(RecipeType.BonzesEtherCape, SkillType.Fabrication)
                .Level(50)
                .Category(RecipeCategoryType.Back)
                .NormalItem("bonze_ether_cape", 1)
                .HQItem("bonze_ether_p1", 1)
                .Component("techno_fiber", 3)
                .Component("mythrite_frag", 2)
                .Component("psi_crystal", 2);
        }

        private void PrecisionMantle()
        {
            _builder.Create(RecipeType.PrecisionMantle, SkillType.Fabrication)
                .Level(51)
                .Category(RecipeCategoryType.Back)
                .NormalItem("precision_mantle", 1)
                .HQItem("precision_mant_p1", 1)
                .Component("beast_hide", 3)
                .Component("mythrite_frag", 2)
                .Component("psi_crystal", 1)
                .Component("purify_filter", 1);
        }

        private void DirewolfMantle()
        {
            _builder.Create(RecipeType.DirewolfMantle, SkillType.Fabrication)
                .Level(56)
                .Category(RecipeCategoryType.Back)
                .NormalItem("direwolf_mantle", 1)
                .HQItem("direwolf_mant_p1", 1)
                .Component("beast_hide", 4)
                .Component("mythrite_frag", 2)
                .Component("psi_crystal", 1);
        }

        private void MidnightShadowCape()
        {
            _builder.Create(RecipeType.MidnightShadowCape, SkillType.Fabrication)
                .Level(62)
                .Category(RecipeCategoryType.Back)
                .NormalItem("midnight_shdw_cp", 1)
                .HQItem("midnight_shdw_p1", 1)
                .Component("techno_fiber", 4)
                .Component("mythrite_frag", 3)
                .Component("psi_crystal", 1);
        }

        private void ObsidianCloak()
        {
            _builder.Create(RecipeType.ObsidianCloak, SkillType.Fabrication)
                .Level(67)
                .Category(RecipeCategoryType.Back)
                .NormalItem("obsidian_cloak", 1)
                .HQItem("obsidian_cloak_p1", 1)
                .Component("techno_fiber", 4)
                .Component("mythrite_frag", 3)
                .Component("psi_crystal", 2);
        }

        private void RamguardMantle()
        {
            _builder.Create(RecipeType.RamguardMantle, SkillType.Fabrication)
                .Level(73)
                .Category(RecipeCategoryType.Back)
                .NormalItem("ramguard_mantle", 1)
                .HQItem("ramguard_mant_p1", 1)
                .Component("beast_hide", 4)
                .Component("mythrite_frag", 3)
                .Component("psi_crystal", 1);
        }

        private void CavaliersWarMantle()
        {
            _builder.Create(RecipeType.CavaliersWarMantle, SkillType.Fabrication)
                .Level(73)
                .Category(RecipeCategoryType.Back)
                .NormalItem("cavalier_war_mnt", 1)
                .HQItem("cavalier_warm_p1", 1)
                .Component("beast_hide", 4)
                .Component("mythrite_frag", 2)
                .Component("psi_crystal", 2);
        }

        private void CrimsonDrape()
        {
            _builder.Create(RecipeType.CrimsonDrape, SkillType.Fabrication)
                .Level(83)
                .Category(RecipeCategoryType.Back)
                .NormalItem("crimson_drape", 1)
                .HQItem("crimson_drape_p1", 1)
                .Component("techno_fiber", 4)
                .Component("titan_plate", 3)
                .Component("quantum_proc", 2);
        }

        private void AuroraSkyMantle()
        {
            _builder.Create(RecipeType.AuroraSkyMantle, SkillType.Fabrication)
                .Level(88)
                .Category(RecipeCategoryType.Back)
                .NormalItem("aurora_sky_mantl", 1)
                .HQItem("aurora_sky_p1", 1)
                .Component("techno_fiber", 5)
                .Component("titan_plate", 3)
                .Component("quantum_proc", 2)
                .Component("sync_core", 1);
        }

        private void ShadowJaguarMantle()
        {
            _builder.Create(RecipeType.ShadowJaguarMantle, SkillType.Fabrication)
                .Level(94)
                .Category(RecipeCategoryType.Back)
                .NormalItem("shadow_jag_mnt", 1)
                .HQItem("shadow_jag_p1", 1)
                .Component("beast_hide", 5)
                .Component("titan_plate", 3)
                .Component("quantum_proc", 2)
                .Component("harmonic_alloy", 1);
        }

        private void RaptorstrikeMantle()
        {
            _builder.Create(RecipeType.RaptorstrikeMantle, SkillType.Fabrication)
                .Level(99)
                .Category(RecipeCategoryType.Back)
                .NormalItem("raptorstrike_mnt", 1)
                .HQItem("raptorstrike_p1", 1)
                .Component("beast_hide", 5)
                .Component("titan_plate", 4)
                .Component("quantum_proc", 2)
                .Component("sync_core", 1);
        }

        private void ArcaneVeilMantle()
        {
            _builder.Create(RecipeType.ArcaneVeilMantle, SkillType.Fabrication)
                .Level(99)
                .Category(RecipeCategoryType.Back)
                .NormalItem("arcane_veil_mnt", 1)
                .Component("techno_fiber", 5)
                .Component("titan_plate", 3)
                .Component("quantum_proc", 3)
                .Component("harmonic_alloy", 1);
        }

        private void TitanWarMantle()
        {
            _builder.Create(RecipeType.TitanWarMantle, SkillType.Fabrication)
                .Level(100)
                .Category(RecipeCategoryType.Back)
                .NormalItem("titan_war_mantle", 1)
                .Component("titan_plate", 5)
                .Component("quantum_proc", 4)
                .Component("nano_enchant", 2)
                .Component("harmonic_alloy", 2)
                .Component("sync_core", 1);
        }

        private void SharpshootersMantle()
        {
            _builder.Create(RecipeType.SharpshootersMantle, SkillType.Fabrication)
                .Level(100)
                .Category(RecipeCategoryType.Back)
                .NormalItem("sharpshooter_mnt", 1)
                .Component("techno_fiber", 5)
                .Component("titan_plate", 4)
                .Component("quantum_proc", 3)
                .Component("nano_enchant", 1)
                .Component("harmonic_alloy", 1);
        }
    }
}