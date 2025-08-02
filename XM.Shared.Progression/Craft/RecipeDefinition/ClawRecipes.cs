using System.Collections.Generic;
using Anvil.Services;
using XM.Progression.Skill;

namespace XM.Progression.Craft.RecipeDefinition
{
    [ServiceBinding(typeof(IRecipeListDefinition))]
    internal class ClawRecipes : IRecipeListDefinition
    {
        private readonly RecipeBuilder _builder = new();

        public Dictionary<RecipeType, RecipeDetail> BuildRecipes()
        {
            BuildClawRecipes();
            return _builder.Build();
        }

        private void BuildClawRecipes()
        {
            // Level 2
            _builder.Create(RecipeType.PantherClaws, SkillType.Weaponcraft)
                .Level(2)
                .Category(RecipeCategoryType.Claw)
                .NormalItem("panther_claws", 1)
                .HQItem("panther_claws_p1", 1);

            _builder.Create(RecipeType.EtherCesti, SkillType.Weaponcraft)
                .Level(2)
                .Category(RecipeCategoryType.Claw)
                .NormalItem("ether_cesti", 1)
                .HQItem("ether_cesti_p1", 1);

            // Level 10
            _builder.Create(RecipeType.AurionAlloyKnuckles, SkillType.Weaponcraft)
                .Level(10)
                .Category(RecipeCategoryType.Claw)
                .NormalItem("aurion_knkls", 1)
                .HQItem("aurion_knkls_p1", 1);

            // Level 18
            _builder.Create(RecipeType.BrassstrikeKnuckles, SkillType.Weaponcraft)
                .Level(18)
                .Category(RecipeCategoryType.Claw)
                .NormalItem("brass_knkls", 1)
                .HQItem("brass_knkls_p1", 1);

            // Level 20
            _builder.Create(RecipeType.TropicStrikers, SkillType.Weaponcraft)
                .Level(20)
                .Category(RecipeCategoryType.Claw)
                .NormalItem("tropic_strikr", 1)
                .HQItem("tropic_strkr_p1", 1);

            // Level 22
            _builder.Create(RecipeType.BrassfangClaws, SkillType.Weaponcraft)
                .Level(22)
                .Category(RecipeCategoryType.Claw)
                .NormalItem("brassfang_cls", 1)
                .HQItem("brassfang_cls_p1", 1);

            // Level 23
            _builder.Create(RecipeType.HydrofangClaws, SkillType.Weaponcraft)
                .Level(23)
                .Category(RecipeCategoryType.Claw)
                .NormalItem("hydrofang_cls", 1)
                .HQItem("hydrofang_cls_p1", 1);

            // Level 28
            _builder.Create(RecipeType.PrecisionClaws, SkillType.Weaponcraft)
                .Level(28)
                .Category(RecipeCategoryType.Claw)
                .NormalItem("precis_claws", 1);

            // Level 30
            _builder.Create(RecipeType.ZephyrClaws, SkillType.Weaponcraft)
                .Level(30)
                .Category(RecipeCategoryType.Claw)
                .NormalItem("zephyr_claws", 1);

            // Level 39
            _builder.Create(RecipeType.ShadowfangClaws, SkillType.Weaponcraft)
                .Level(39)
                .Category(RecipeCategoryType.Claw)
                .NormalItem("shdwfang_cls", 1)
                .HQItem("shdwfang_cls_p1", 1);

            // Level 40
            _builder.Create(RecipeType.AlloyKnuckles, SkillType.Weaponcraft)
                .Level(40)
                .Category(RecipeCategoryType.Claw)
                .NormalItem("alloy_knkls", 1)
                .HQItem("alloy_knkls_p1", 1);

            // Level 48
            _builder.Create(RecipeType.PredatorClaws, SkillType.Weaponcraft)
                .Level(48)
                .Category(RecipeCategoryType.Claw)
                .NormalItem("predator_cls", 1)
                .HQItem("predator_cls_p1", 1);

            // Level 54
            _builder.Create(RecipeType.ToxinCesti, SkillType.Weaponcraft)
                .Level(54)
                .Category(RecipeCategoryType.Claw)
                .NormalItem("toxin_csti", 1)
                .HQItem("toxin_csti_p1", 1);

            // Level 56
            _builder.Create(RecipeType.RavagerClaws, SkillType.Weaponcraft)
                .Level(56)
                .Category(RecipeCategoryType.Claw)
                .NormalItem("ravager_cls", 1)
                .HQItem("ravager_cls_p1", 1);

            // Level 60
            _builder.Create(RecipeType.HydrofangTalons, SkillType.Weaponcraft)
                .Level(60)
                .Category(RecipeCategoryType.Claw)
                .NormalItem("hydrofang_tln", 1)
                .HQItem("hydrofang_tln_p1", 1);

            // Level 62
            _builder.Create(RecipeType.TyroWarKatars, SkillType.Weaponcraft)
                .Level(62)
                .Category(RecipeCategoryType.Claw)
                .NormalItem("tyro_wrkatr", 1)
                .HQItem("tyro_wrkatr_p1", 1);

            // Level 66
            _builder.Create(RecipeType.WarKatars, SkillType.Weaponcraft)
                .Level(66)
                .Category(RecipeCategoryType.Claw)
                .NormalItem("war_katars", 1)
                .HQItem("war_katars_p1", 1);

            // Level 67
            _builder.Create(RecipeType.ToxinfangClaws, SkillType.Weaponcraft)
                .Level(67)
                .Category(RecipeCategoryType.Claw)
                .NormalItem("toxin_cls", 1)
                .HQItem("toxin_cls_p1", 1);

            // Level 71
            _builder.Create(RecipeType.ToxinfangTalons, SkillType.Weaponcraft)
                .Level(71)
                .Category(RecipeCategoryType.Claw)
                .NormalItem("toxin_tlns", 1)
                .HQItem("toxin_tlns_p1", 1);

            // Level 74
            _builder.Create(RecipeType.MythriteKnuckles, SkillType.Weaponcraft)
                .Level(74)
                .Category(RecipeCategoryType.Claw)
                .NormalItem("mythrite_knkls", 1)
                .HQItem("mythrite_knkls_p1", 1);

            // Level 75
            _builder.Create(RecipeType.ToxinWarKatars, SkillType.Weaponcraft)
                .Level(75)
                .Category(RecipeCategoryType.Claw)
                .NormalItem("toxin_wrkatr", 1)
                .HQItem("toxin_wrkatr_p1", 1);

            // Level 79
            _builder.Create(RecipeType.EtherwovenStraps, SkillType.Weaponcraft)
                .Level(79)
                .Category(RecipeCategoryType.Claw)
                .NormalItem("etherwoven_stp", 1)
                .HQItem("etherwoven_stp_p1", 1);

            // Level 82
            _builder.Create(RecipeType.MythriteClaws, SkillType.Weaponcraft)
                .Level(82)
                .Category(RecipeCategoryType.Claw)
                .NormalItem("mythrite_cls", 1)
                .HQItem("mythrite_cls_p1", 1);

            // Level 85
            _builder.Create(RecipeType.ArcstrikeAdargas, SkillType.Weaponcraft)
                .Level(85)
                .Category(RecipeCategoryType.Claw)
                .NormalItem("arcstrk_adgs", 1)
                .HQItem("arcstrk_adgs_p1", 1);

            // Level 93
            _builder.Create(RecipeType.PredatorPatas, SkillType.Weaponcraft)
                .Level(93)
                .Category(RecipeCategoryType.Claw)
                .NormalItem("predator_pts", 1)
                .HQItem("predator_pts_p1", 1);

            // Level 98
            _builder.Create(RecipeType.TacticianMagiciansTalons, SkillType.Weaponcraft)
                .Level(98)
                .Category(RecipeCategoryType.Claw)
                .NormalItem("tact_mag_tlns", 1);
        }
    }
} 