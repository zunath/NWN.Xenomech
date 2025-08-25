using System.Collections.Generic;
using Anvil.Services;
using XM.Progression.Skill;

namespace XM.Plugin.Craft.RecipeDefinition.Weaponcraft
{
    [ServiceBinding(typeof(IRecipeListDefinition))]
    internal class ShortSwordRecipes : IRecipeListDefinition
    {
        private readonly RecipeBuilder _builder = new();

        public Dictionary<RecipeType, RecipeDetail> BuildRecipes()
        {
            // Short Swords ordered by level (3-100)
            PhantomFang();               // Level 3
            Stormpiercer();              // Level 15
            GaleFang();                  // Level 25
            ShadowfangBlade();           // Level 26
            ObsidianFang();              // Level 34
            VanguardFang();              // Level 54
            SkylarkEdge();               // Level 45
            SwiftclawSword();            // Level 65
            TempestFang();               // Level 66
            FractureFang();              // Level 71
            TrainingBlade();             // Level 82
            Cherryblade();               // Level 88
            TwilightFang();              // Level 90
            CrimsonWing();               // Level 92
            TwinstrikeBlade();           // Level 96
            Nyxblade();                  // Level 100

            return _builder.Build();
        }

        private void PhantomFang()
        {
            _builder.Create(RecipeType.PhantomFang, SkillType.Weaponcraft)
                .Level(3)
                .Category(RecipeCategoryType.ShortSword)
                .NormalItem("phantom_fang", 1)
                .HQItem("phantom_fang_p1", 1)
                .Component("aurion_ingot", 3)
                .Component("beast_hide", 1)
                .Component("flux_compound", 1)
                .Component("void_shard", 1);
        }

        private void Stormpiercer()
        {
            _builder.Create(RecipeType.Stormpiercer, SkillType.Weaponcraft)
                .Level(15)
                .Category(RecipeCategoryType.ShortSword)
                .NormalItem("stormpiercer", 1)
                .HQItem("stormpiercer_p1", 1)
                .Component("aurion_ingot", 3)
                .Component("beast_hide", 1)
                .Component("flux_compound", 1);
        }

        private void GaleFang()
        {
            _builder.Create(RecipeType.GaleFang, SkillType.Weaponcraft)
                .Level(25)
                .Category(RecipeCategoryType.ShortSword)
                .NormalItem("gale_fang", 1)
                .HQItem("gale_fang_p1", 1)
                .Component("ferrite_core", 3)
                .Component("circuit_matrix", 1)
                .Component("power_cell", 1);
        }

        private void ShadowfangBlade()
        {
            _builder.Create(RecipeType.ShadowfangBlade, SkillType.Weaponcraft)
                .Level(26)
                .Category(RecipeCategoryType.ShortSword)
                .NormalItem("shdwfang_bld", 1)
                .HQItem("shdwfang_bld_p1", 1)
                .Component("brass_sheet", 3)
                .Component("mythrite_frag", 2)
                .Component("enhance_serum", 1)
                .Component("void_shard", 1);
        }

        private void ObsidianFang()
        {
            _builder.Create(RecipeType.ObsidianFang, SkillType.Weaponcraft)
                .Level(34)
                .Category(RecipeCategoryType.ShortSword)
                .NormalItem("obsidian_fang", 1)
                .HQItem("obsidian_fang_p1", 1)
                .Component("brass_sheet", 3)
                .Component("mythrite_frag", 2)
                .Component("enhance_serum", 1);
        }

        private void SkylarkEdge()
        {
            _builder.Create(RecipeType.SkylarkEdge, SkillType.Weaponcraft)
                .Level(45)
                .Category(RecipeCategoryType.ShortSword)
                .NormalItem("skylark_edge", 1)
                .HQItem("skylark_edge_p1", 1)
                .Component("brass_sheet", 3)
                .Component("mythrite_frag", 2)
                .Component("enhance_serum", 1);
        }

        private void VanguardFang()
        {
            _builder.Create(RecipeType.VanguardFang, SkillType.Weaponcraft)
                .Level(54)
                .Category(RecipeCategoryType.ShortSword)
                .NormalItem("vang_fang", 1)
                .HQItem("vang_fang_p1", 1)
                .Component("mythrite_frag", 4)
                .Component("psi_crystal", 2)
                .Component("biosteel_comp", 1);
        }

        private void SwiftclawSword()
        {
            _builder.Create(RecipeType.SwiftclawSword, SkillType.Weaponcraft)
                .Level(65)
                .Category(RecipeCategoryType.ShortSword)
                .NormalItem("swiftclaw_swd", 1)
                .HQItem("swiftclaw_swd_p1", 1)
                .Component("mythrite_frag", 4)
                .Component("psi_crystal", 2)
                .Component("biosteel_comp", 1);
        }

        private void TempestFang()
        {
            _builder.Create(RecipeType.TempestFang, SkillType.Weaponcraft)
                .Level(66)
                .Category(RecipeCategoryType.ShortSword)
                .NormalItem("tempest_fang", 1)
                .HQItem("tempest_fang_p1", 1)
                .Component("mythrite_frag", 4)
                .Component("psi_crystal", 2)
                .Component("biosteel_comp", 1);
        }

        private void FractureFang()
        {
            _builder.Create(RecipeType.FractureFang, SkillType.Weaponcraft)
                .Level(71)
                .Category(RecipeCategoryType.ShortSword)
                .NormalItem("fracture_fang", 1)
                .HQItem("fracture_fang_p1", 1)
                .Component("mythrite_frag", 4)
                .Component("psi_crystal", 2)
                .Component("biosteel_comp", 1);
        }

        private void TrainingBlade()
        {
            _builder.Create(RecipeType.TrainingBlade, SkillType.Weaponcraft)
                .Level(82)
                .Category(RecipeCategoryType.ShortSword)
                .NormalItem("train_bld", 1)
                .HQItem("train_bld_p1", 1)
                .Component("titan_plate", 4)
                .Component("quantum_proc", 2)
                .Component("quantmyst_core", 1);
        }

        private void Cherryblade()
        {
            _builder.Create(RecipeType.Cherryblade, SkillType.Weaponcraft)
                .Level(88)
                .Category(RecipeCategoryType.ShortSword)
                .NormalItem("cherryblade", 1)
                .HQItem("cherryblade_p1", 1)
                .Component("titan_plate", 4)
                .Component("quantum_proc", 2)
                .Component("quantmyst_core", 1);
        }

        private void TwilightFang()
        {
            _builder.Create(RecipeType.TwilightFang, SkillType.Weaponcraft)
                .Level(90)
                .Category(RecipeCategoryType.ShortSword)
                .NormalItem("twilight_fang", 1)
                .HQItem("twilight_fng_p1", 1)
                .Component("titan_plate", 4)
                .Component("quantum_proc", 2)
                .Component("quantmyst_core", 1);
        }

        private void CrimsonWing()
        {
            _builder.Create(RecipeType.CrimsonWing, SkillType.Weaponcraft)
                .Level(92)
                .Category(RecipeCategoryType.ShortSword)
                .NormalItem("crimson_wing", 1)
                .HQItem("crimson_wing_p1", 1)
                .Component("titan_plate", 4)
                .Component("quantum_proc", 2)
                .Component("quantmyst_core", 1);
        }

        private void TwinstrikeBlade()
        {
            _builder.Create(RecipeType.TwinstrikeBlade, SkillType.Weaponcraft)
                .Level(96)
                .Category(RecipeCategoryType.ShortSword)
                .NormalItem("twinstrk_bld", 1)
                .HQItem("twinstrk_bld_p1", 1)
                .Component("titan_plate", 4)
                .Component("quantum_proc", 2)
                .Component("quantmyst_core", 1);
        }

        private void Nyxblade()
        {
            _builder.Create(RecipeType.Nyxblade, SkillType.Weaponcraft)
                .Level(100)
                .Category(RecipeCategoryType.ShortSword)
                .NormalItem("nyxblade", 1)
                .Component("titan_plate", 4)
                .Component("quantum_proc", 2)
                .Component("quantmyst_core", 1);
        }
    }
}