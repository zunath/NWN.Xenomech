using System.Collections.Generic;
using Anvil.Services;
using XM.Progression.Skill;

namespace XM.Plugin.Craft.RecipeDefinition.Synthesis
{
    [ServiceBinding(typeof(IRecipeListDefinition))]
    internal class HeadRecipes : IRecipeListDefinition
    {
        private readonly RecipeBuilder _builder = new();

        public Dictionary<RecipeType, RecipeDetail> BuildRecipes()
        {
            // Synthesis head accessories ordered by level (1-97)
            // All mystical head accessories using premium mystical components
            EtherTouchedCirclet();              // Level 1
            MysticHairpin();                    // Level 12
            ArcaneBand();                       // Level 18
            CrystalCirclet();                   // Level 26
            ResonanceHeadband();                // Level 34
            ElementalCrown();                   // Level 42
            MythriteTiara();                    // Level 50
            PsiCrystalCirclet();                // Level 58
            VoidShadowDiadem();                 // Level 66
            QuantumForgedCrown();               // Level 74
            TemporalFluxCirclet();              // Level 82
            NanoEnchantedTiara();               // Level 90
            EthertechMasterDiadem();            // Level 97

            return _builder.Build();
        }

        private void EtherTouchedCirclet()
        {
            _builder.Create(RecipeType.EtherTouchedCirclet, SkillType.Synthesis)
                .Level(1)
                .Category(RecipeCategoryType.Head)
                .NormalItem("ether_touch_circlet", 1)
                .HQItem("ether_touch_circlet_hq", 1)
                .Component("arcane_dust", 1)
                .Component("ether_crystal", 1);
        }

        private void MysticHairpin()
        {
            _builder.Create(RecipeType.MysticHairpin, SkillType.Synthesis)
                .Level(12)
                .Category(RecipeCategoryType.Head)
                .NormalItem("mystic_hairpin", 1)
                .HQItem("mystic_hairpin_hq", 1)
                .Component("arcane_dust", 2)
                .Component("ether_crystal", 1)
                .Component("brass_sheet", 1);
        }

        private void ArcaneBand()
        {
            _builder.Create(RecipeType.ArcaneBand, SkillType.Synthesis)
                .Level(18)
                .Category(RecipeCategoryType.Head)
                .NormalItem("arcane_band", 1)
                .HQItem("arcane_band_hq", 1)
                .Component("arcane_dust", 2)
                .Component("ether_crystal", 2)
                .Component("bond_agent", 1);
        }

        private void CrystalCirclet()
        {
            _builder.Create(RecipeType.CrystalCirclet, SkillType.Synthesis)
                .Level(26)
                .Category(RecipeCategoryType.Head)
                .NormalItem("crystal_circlet", 1)
                .HQItem("crystal_circlet_hq", 1)
                .Component("arcane_dust", 3)
                .Component("ferrite_core", 1)
                .Component("resonance_ston", 1);
        }

        private void ResonanceHeadband()
        {
            _builder.Create(RecipeType.ResonanceHeadband, SkillType.Synthesis)
                .Level(34)
                .Category(RecipeCategoryType.Head)
                .NormalItem("resonance_headband", 1)
                .HQItem("resonance_headband_hq", 1)
                .Component("arcane_dust", 4)
                .Component("ferrite_core", 2)
                .Component("spirit_ess", 1)
                .Component("enhance_serum", 1);
        }

        private void ElementalCrown()
        {
            _builder.Create(RecipeType.ElementalCrown, SkillType.Synthesis)
                .Level(42)
                .Category(RecipeCategoryType.Head)
                .NormalItem("elemental_crown", 1)
                .HQItem("elemental_crown_hq", 1)
                .Component("arcane_dust", 5)
                .Component("ferrite_core", 2)
                .Component("resonance_ston", 2)
                .Component("spirit_ess", 1);
        }

        private void MythriteTiara()
        {
            _builder.Create(RecipeType.MythriteTiara, SkillType.Synthesis)
                .Level(50)
                .Category(RecipeCategoryType.Head)
                .NormalItem("mythrite_tiara", 1)
                .HQItem("mythrite_tiara_hq", 1)
                .Component("arcane_dust", 6)
                .Component("mythrite_frag", 2)
                .Component("spirit_ess", 2)
                .Component("enhance_serum", 1);
        }

        private void PsiCrystalCirclet()
        {
            _builder.Create(RecipeType.PsiCrystalCirclet, SkillType.Synthesis)
                .Level(58)
                .Category(RecipeCategoryType.Head)
                .NormalItem("psi_crystal_circlet", 1)
                .HQItem("psi_crystal_circlet_hq", 1)
                .Component("arcane_dust", 8)
                .Component("mythrite_frag", 2)
                .Component("psi_crystal", 2)
                .Component("spirit_ess", 1);
        }

        private void VoidShadowDiadem()
        {
            _builder.Create(RecipeType.VoidShadowDiadem, SkillType.Synthesis)
                .Level(66)
                .Category(RecipeCategoryType.Head)
                .NormalItem("void_shadow_diadem", 1)
                .HQItem("void_shadow_diadem_hq", 1)
                .Component("arcane_dust", 10)
                .Component("mythrite_frag", 3)
                .Component("psi_crystal", 2)
                .Component("void_shard", 1);
        }

        private void QuantumForgedCrown()
        {
            _builder.Create(RecipeType.QuantumForgedCrown, SkillType.Synthesis)
                .Level(74)
                .Category(RecipeCategoryType.Head)
                .NormalItem("quantum_forged_crown", 1)
                .HQItem("quantum_forged_crown_hq", 1)
                .Component("arcane_dust", 12)
                .Component("psi_crystal", 3)
                .Component("void_shard", 2)
                .Component("quantmyst_core", 1);
        }

        private void TemporalFluxCirclet()
        {
            _builder.Create(RecipeType.TemporalFluxCirclet, SkillType.Synthesis)
                .Level(82)
                .Category(RecipeCategoryType.Head)
                .NormalItem("temporal_flux_circlet", 1)
                .HQItem("temporal_flux_circlet_hq", 1)
                .Component("arcane_dust", 15)
                .Component("quantmyst_core", 2)
                .Component("temporal_flux", 1)
                .Component("quantum_steel", 1);
        }

        private void NanoEnchantedTiara()
        {
            _builder.Create(RecipeType.NanoEnchantedTiara, SkillType.Synthesis)
                .Level(90)
                .Category(RecipeCategoryType.Head)
                .NormalItem("nano_enchanted_tiara", 1)
                .HQItem("nano_enchanted_tiara_hq", 1)
                .Component("arcane_dust", 18)
                .Component("quantmyst_core", 2)
                .Component("temporal_flux", 2)
                .Component("nano_enchant", 2);
        }

        private void EthertechMasterDiadem()
        {
            _builder.Create(RecipeType.EthertechMasterDiadem, SkillType.Synthesis)
                .Level(97)
                .Category(RecipeCategoryType.Head)
                .NormalItem("ethertech_master_diadem", 1)
                .Component("arcane_dust", 25)
                .Component("quantmyst_core", 3)
                .Component("temporal_flux", 2)
                .Component("ethertech_int", 1)
                .Component("nano_enchant", 2);
        }
    }
}