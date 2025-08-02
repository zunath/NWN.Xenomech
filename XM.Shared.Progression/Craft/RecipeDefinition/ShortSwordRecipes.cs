using System.Collections.Generic;
using Anvil.Services;
using XM.Progression.Skill;

namespace XM.Progression.Craft.RecipeDefinition
{
    [ServiceBinding(typeof(IRecipeListDefinition))]
    internal class ShortSwordRecipes : IRecipeListDefinition
    {
        private readonly RecipeBuilder _builder = new();

        public Dictionary<RecipeType, RecipeDetail> BuildRecipes()
        {
            BuildShortSwordRecipes();
            return _builder.Build();
        }

        private void BuildShortSwordRecipes()
        {
            // Level 3
            _builder.Create(RecipeType.PhantomFang, SkillType.Weaponcraft)
                .Level(3)
                .Category(RecipeCategoryType.ShortSword)
                .NormalItem("phantom_fang", 1)
                .HQItem("phantom_fang_p1", 1);

            // Level 15
            _builder.Create(RecipeType.Stormpiercer, SkillType.Weaponcraft)
                .Level(15)
                .Category(RecipeCategoryType.ShortSword)
                .NormalItem("stormpiercer", 1)
                .HQItem("stormpiercer_p1", 1);

            // Level 25
            _builder.Create(RecipeType.GaleFang, SkillType.Weaponcraft)
                .Level(25)
                .Category(RecipeCategoryType.ShortSword)
                .NormalItem("gale_fang", 1)
                .HQItem("gale_fang_p1", 1);

            // Level 26
            _builder.Create(RecipeType.ShadowfangBlade, SkillType.Weaponcraft)
                .Level(26)
                .Category(RecipeCategoryType.ShortSword)
                .NormalItem("shdwfang_bld", 1)
                .HQItem("shdwfang_bld_p1", 1);

            // Level 34
            _builder.Create(RecipeType.ObsidianFang, SkillType.Weaponcraft)
                .Level(34)
                .Category(RecipeCategoryType.ShortSword)
                .NormalItem("obsidian_fang", 1)
                .HQItem("obsidian_fang_p1", 1);

            // Level 45
            _builder.Create(RecipeType.SkylarkEdge, SkillType.Weaponcraft)
                .Level(45)
                .Category(RecipeCategoryType.ShortSword)
                .NormalItem("skylark_edge", 1)
                .HQItem("skylark_edge_p1", 1);

            // Level 54
            _builder.Create(RecipeType.VanguardFang, SkillType.Weaponcraft)
                .Level(54)
                .Category(RecipeCategoryType.ShortSword)
                .NormalItem("vang_fang", 1)
                .HQItem("vang_fang_p1", 1);

            // Level 65
            _builder.Create(RecipeType.SwiftclawSword, SkillType.Weaponcraft)
                .Level(65)
                .Category(RecipeCategoryType.ShortSword)
                .NormalItem("swiftclaw_swd", 1)
                .HQItem("swiftclaw_swd_p1", 1);

            // Level 66
            _builder.Create(RecipeType.TempestFang, SkillType.Weaponcraft)
                .Level(66)
                .Category(RecipeCategoryType.ShortSword)
                .NormalItem("tempest_fang", 1)
                .HQItem("tempest_fang_p1", 1);

            // Level 71
            _builder.Create(RecipeType.FractureFang, SkillType.Weaponcraft)
                .Level(71)
                .Category(RecipeCategoryType.ShortSword)
                .NormalItem("fracture_fang", 1)
                .HQItem("fracture_fang_p1", 1);

            // Level 82
            _builder.Create(RecipeType.TrainingBlade, SkillType.Weaponcraft)
                .Level(82)
                .Category(RecipeCategoryType.ShortSword)
                .NormalItem("train_bld", 1)
                .HQItem("train_bld_p1", 1);

            // Level 88
            _builder.Create(RecipeType.Cherryblade, SkillType.Weaponcraft)
                .Level(88)
                .Category(RecipeCategoryType.ShortSword)
                .NormalItem("cherryblade", 1)
                .HQItem("cherryblade_p1", 1);

            // Level 90
            _builder.Create(RecipeType.TwilightFang, SkillType.Weaponcraft)
                .Level(90)
                .Category(RecipeCategoryType.ShortSword)
                .NormalItem("twilight_fang", 1)
                .HQItem("twilight_fng_p1", 1);

            // Level 92
            _builder.Create(RecipeType.CrimsonWing, SkillType.Weaponcraft)
                .Level(92)
                .Category(RecipeCategoryType.ShortSword)
                .NormalItem("crimson_wing", 1)
                .HQItem("crimson_wing_p1", 1);

            // Level 96
            _builder.Create(RecipeType.TwinstrikeBlade, SkillType.Weaponcraft)
                .Level(96)
                .Category(RecipeCategoryType.ShortSword)
                .NormalItem("twinstrk_bld", 1)
                .HQItem("twinstrk_bld_p1", 1);

            // Level 100
            _builder.Create(RecipeType.Nyxblade, SkillType.Weaponcraft)
                .Level(100)
                .Category(RecipeCategoryType.ShortSword)
                .NormalItem("nyxblade", 1);
        }
    }
} 