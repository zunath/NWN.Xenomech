using System.Collections.Generic;
using Anvil.Services;
using XM.Progression.Skill;

namespace XM.Plugin.Craft.RecipeDefinition.Armorcraft
{
    [ServiceBinding(typeof(IRecipeListDefinition))]
    internal class ShieldRecipes : IRecipeListDefinition
    {
        private readonly RecipeBuilder _builder = new();

        public Dictionary<RecipeType, RecipeDetail> BuildRecipes()
        {
            // Shield Armor ordered by level (5-100)
            VerdantGuard();                         // Level 5
            AbyssalBarrier();                       // Level 6
            ShellbackAegis();                       // Level 14
            TimberguardShield();                    // Level 16
            SentinelAegis();                        // Level 18
            ElmguardShield();                       // Level 26
            TitanTarge();                           // Level 32
            TropicWard();                           // Level 34
            LanternAegis();                         // Level 38
            TideshellShield();                      // Level 48
            WarbornKiteShield();                    // Level 56
            OakheartShield();                       // Level 72
            SylphicAegis();                         // Level 78
            RegalSquiresGuard();                    // Level 80
            EmberplateHeater();                     // Level 86
            WarbornBuckler();                       // Level 90
            LeathercladGuard();                     // Level 98
            RoyalKnightArmyBulwark();               // Level 100

            return _builder.Build();
        }

        private void VerdantGuard()
        {
            _builder.Create(RecipeType.VerdantGuard, SkillType.Armorcraft)
                .Level(5)
                .Category(RecipeCategoryType.Shield)
                .NormalItem("verdant_guard", 1)
                .HQItem("verdant_guard_p1", 1)
                .Component("living_wood", 3)
                .Component("aurion_ingot", 2)
                .Component("beast_hide", 1)
                .Component("bond_agent", 1);
        }

        private void AbyssalBarrier()
        {
            _builder.Create(RecipeType.AbyssalBarrier, SkillType.Armorcraft)
                .Level(6)
                .Category(RecipeCategoryType.Shield)
                .NormalItem("abyssal_barr", 1)
                .Component("aurion_ingot", 2)
                .Component("beast_hide", 2)
                .Component("flux_compound", 1)
                .Component("bond_agent", 1);
        }

        private void ShellbackAegis()
        {
            _builder.Create(RecipeType.ShellbackAegis, SkillType.Armorcraft)
                .Level(14)
                .Category(RecipeCategoryType.Shield)
                .NormalItem("shellback_aegis", 1)
                .Component("living_wood", 3)
                .Component("aurion_ingot", 2)
                .Component("beast_hide", 2)
                .Component("flux_compound", 1);
        }

        private void TimberguardShield()
        {
            _builder.Create(RecipeType.TimberguardShield, SkillType.Armorcraft)
                .Level(16)
                .Category(RecipeCategoryType.Shield)
                .NormalItem("timbergrd_shld", 1)
                .HQItem("timbergrd_shld_p1", 1)
                .Component("living_wood", 3)
                .Component("ferrite_core", 2)
                .Component("circuit_matrix", 2)
                .Component("enhance_serum", 1);
        }

        private void SentinelAegis()
        {
            _builder.Create(RecipeType.SentinelAegis, SkillType.Armorcraft)
                .Level(18)
                .Category(RecipeCategoryType.Shield)
                .NormalItem("sentinel_aegis", 1)
                .HQItem("sentinel_aegis_p1", 1)
                .Component("living_wood", 3)
                .Component("ferrite_core", 2)
                .Component("circuit_matrix", 2)
                .Component("enhance_serum", 1);
        }

        private void ElmguardShield()
        {
            _builder.Create(RecipeType.ElmguardShield, SkillType.Armorcraft)
                .Level(26)
                .Category(RecipeCategoryType.Shield)
                .NormalItem("elmguard_shld", 1)
                .HQItem("elmguard_shld_p1", 1)
                .Component("living_wood", 3)
                .Component("ferrite_core", 2)
                .Component("biosteel_comp", 2)
                .Component("neural_inter", 1);
        }

        private void TitanTarge()
        {
            _builder.Create(RecipeType.TitanTarge, SkillType.Armorcraft)
                .Level(32)
                .Category(RecipeCategoryType.Shield)
                .NormalItem("titan_targe", 1)
                .HQItem("titan_targe_p1", 1)
                .Component("living_wood", 3)
                .Component("ferrite_core", 2)
                .Component("biosteel_comp", 2)
                .Component("neural_inter", 1);
        }

        private void TropicWard()
        {
            _builder.Create(RecipeType.TropicWard, SkillType.Armorcraft)
                .Level(34)
                .Category(RecipeCategoryType.Shield)
                .NormalItem("tropic_ward", 1)
                .Component("ferrite_core", 3)
                .Component("biosteel_comp", 2)
                .Component("neural_inter", 1)
                .Component("enhance_serum", 1);
        }

        private void LanternAegis()
        {
            _builder.Create(RecipeType.LanternAegis, SkillType.Armorcraft)
                .Level(38)
                .Category(RecipeCategoryType.Shield)
                .NormalItem("lantern_aegis", 1)
                .Component("living_wood", 3)
                .Component("ferrite_core", 2)
                .Component("biosteel_comp", 2)
                .Component("neural_inter", 1);
        }

        private void TideshellShield()
        {
            _builder.Create(RecipeType.TideshellShield, SkillType.Armorcraft)
                .Level(48)
                .Category(RecipeCategoryType.Shield)
                .NormalItem("tideshell_shld", 1)
                .HQItem("tideshell_shld_p1", 1)
                .Component("living_wood", 3)
                .Component("mythrite_frag", 2)
                .Component("psi_crystal", 2)
                .Component("biosteel_comp", 1);
        }

        private void WarbornKiteShield()
        {
            _builder.Create(RecipeType.WarbornKiteShield, SkillType.Armorcraft)
                .Level(56)
                .Category(RecipeCategoryType.Shield)
                .NormalItem("warborn_kite", 1)
                .HQItem("warborn_kite_p1", 1)
                .Component("mythrite_frag", 3)
                .Component("psi_crystal", 2)
                .Component("biosteel_comp", 1)
                .Component("purify_filter", 1);
        }

        private void OakheartShield()
        {
            _builder.Create(RecipeType.OakheartShield, SkillType.Armorcraft)
                .Level(72)
                .Category(RecipeCategoryType.Shield)
                .NormalItem("oakheart_shld", 1)
                .HQItem("oakheart_shld_p1", 1)
                .Component("living_wood", 3)
                .Component("mythrite_frag", 2)
                .Component("psi_crystal", 2)
                .Component("biosteel_comp", 1);
        }

        private void SylphicAegis()
        {
            _builder.Create(RecipeType.SylphicAegis, SkillType.Armorcraft)
                .Level(78)
                .Category(RecipeCategoryType.Shield)
                .NormalItem("sylphic_aegis", 1)
                .Component("living_wood", 3)
                .Component("titan_plate", 2)
                .Component("crystal_scale", 2)
                .Component("quantum_proc", 1);
        }

        private void RegalSquiresGuard()
        {
            _builder.Create(RecipeType.RegalSquiresGuard, SkillType.Armorcraft)
                .Level(80)
                .Category(RecipeCategoryType.Shield)
                .NormalItem("regal_squiregrd", 1)
                .HQItem("regal_squiregrd_p1", 1)
                .UltraItem("regal_sq_guard_p2", 1)
                .Component("living_wood", 3)
                .Component("titan_plate", 2)
                .Component("crystal_scale", 2)
                .Component("quantum_proc", 1);
        }

        private void EmberplateHeater()
        {
            _builder.Create(RecipeType.EmberplateHeater, SkillType.Armorcraft)
                .Level(86)
                .Category(RecipeCategoryType.Shield)
                .NormalItem("emberplate_heat", 1)
                .HQItem("emberplate_heat_p1", 1)
                .Component("living_wood", 3)
                .Component("titan_plate", 2)
                .Component("crystal_scale", 2)
                .Component("quantum_proc", 1);
        }

        private void WarbornBuckler()
        {
            _builder.Create(RecipeType.WarbornBuckler, SkillType.Armorcraft)
                .Level(90)
                .Category(RecipeCategoryType.Shield)
                .NormalItem("warborn_buckler", 1)
                .HQItem("warborn_bucklr_p1", 1)
                .Component("living_wood", 3)
                .Component("titan_plate", 2)
                .Component("crystal_scale", 2)
                .Component("quantum_proc", 1);
        }

        private void LeathercladGuard()
        {
            _builder.Create(RecipeType.LeathercladGuard, SkillType.Armorcraft)
                .Level(98)
                .Category(RecipeCategoryType.Shield)
                .NormalItem("leatherclad_grd", 1)
                .HQItem("leatherclad_grd_p1", 1)
                .Component("living_wood", 3)
                .Component("titan_plate", 2)
                .Component("crystal_scale", 2)
                .Component("quantum_proc", 1);
        }

        private void RoyalKnightArmyBulwark()
        {
            _builder.Create(RecipeType.RoyalKnightArmyBulwark, SkillType.Armorcraft)
                .Level(100)
                .Category(RecipeCategoryType.Shield)
                .NormalItem("roy_knight_blwk", 1)
                .Component("living_wood", 3)
                .Component("titan_plate", 2)
                .Component("crystal_scale", 2)
                .Component("quantum_proc", 1);
        }
    }
}