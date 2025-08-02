using System.Collections.Generic;
using Anvil.Services;
using XM.Progression.Skill;

namespace XM.Progression.Craft.RecipeDefinition
{
    [ServiceBinding(typeof(IRecipeListDefinition))]
    internal class DaggerRecipes : IRecipeListDefinition
    {
        private readonly RecipeBuilder _builder = new();

        public Dictionary<RecipeType, RecipeDetail> BuildRecipes()
        {
            BuildDaggerRecipes();
            return _builder.Build();
        }

        private void BuildDaggerRecipes()
        {
            // Level 1
            _builder.Create(RecipeType.AurionAlloyDagger, SkillType.Weaponcraft)
                .Level(1)
                .Category(RecipeCategoryType.Dagger)
                .NormalItem("aurion_daggr", 1)
                .HQItem("aurion_daggr_p1", 1);

            // Level 3
            _builder.Create(RecipeType.AurionAlloyKnife, SkillType.Weaponcraft)
                .Level(3)
                .Category(RecipeCategoryType.Dagger)
                .NormalItem("aurion_knife", 1)
                .HQItem("aurion_knife_p1", 1);

            // Level 8
            _builder.Create(RecipeType.Shadowpiercer, SkillType.Weaponcraft)
                .Level(8)
                .Category(RecipeCategoryType.Dagger)
                .NormalItem("shadowpiercer", 1)
                .HQItem("shadowpiercer_p1", 1);

            // Level 12
            _builder.Create(RecipeType.PhantomKnife, SkillType.Weaponcraft)
                .Level(12)
                .Category(RecipeCategoryType.Dagger)
                .NormalItem("phantom_knife", 1)
                .HQItem("phantom_knf_p1", 1);

            // Level 16
            _builder.Create(RecipeType.ArcDagger, SkillType.Weaponcraft)
                .Level(16)
                .Category(RecipeCategoryType.Dagger)
                .NormalItem("arc_dagger", 1)
                .HQItem("arc_dagger_p1", 1);

            // Level 19
            _builder.Create(RecipeType.BrassfangDagger, SkillType.Weaponcraft)
                .Level(19)
                .Category(RecipeCategoryType.Dagger)
                .NormalItem("brassfang_dgr", 1)
                .HQItem("brassfang_dgr_p1", 1);

            // Level 21
            _builder.Create(RecipeType.WhisperfangDagger, SkillType.Weaponcraft)
                .Level(21)
                .Category(RecipeCategoryType.Dagger)
                .NormalItem("whisper_dgr", 1)
                .HQItem("whisper_dgr_p1", 1);

            // Level 23
            _builder.Create(RecipeType.ArcKnife, SkillType.Weaponcraft)
                .Level(23)
                .Category(RecipeCategoryType.Dagger)
                .NormalItem("arc_knife", 1)
                .HQItem("arc_knife_p1", 1);

            // Level 27
            _builder.Create(RecipeType.SparkfangDagger, SkillType.Weaponcraft)
                .Level(27)
                .Category(RecipeCategoryType.Dagger)
                .NormalItem("spark_dagger", 1)
                .HQItem("spark_dagger_p1", 1);

            // Level 31
            _builder.Create(RecipeType.VenomfangBaselard, SkillType.Weaponcraft)
                .Level(31)
                .Category(RecipeCategoryType.Dagger)
                .NormalItem("venom_baselrd", 1)
                .HQItem("venom_baselrd_p1", 1);

            // Level 41
            _builder.Create(RecipeType.EtherfangKukri, SkillType.Weaponcraft)
                .Level(41)
                .Category(RecipeCategoryType.Dagger)
                .NormalItem("etherfang_kkr", 1)
                .HQItem("etherfang_kkr_p1", 1);

            // Level 43
            _builder.Create(RecipeType.ToxinFang, SkillType.Weaponcraft)
                .Level(43)
                .Category(RecipeCategoryType.Dagger)
                .NormalItem("toxin_fang", 1)
                .HQItem("toxin_fang_p1", 1);

            // Level 45
            _builder.Create(RecipeType.ToxinKnife, SkillType.Weaponcraft)
                .Level(45)
                .Category(RecipeCategoryType.Dagger)
                .NormalItem("toxin_knife", 1)
                .HQItem("toxin_knife_p1", 1);

            // Level 46
            _builder.Create(RecipeType.MythriteDagger, SkillType.Weaponcraft)
                .Level(46)
                .Category(RecipeCategoryType.Dagger)
                .NormalItem("mythrite_dagr", 1)
                .HQItem("mythrite_dgr_p1", 1);

            // Level 47
            _builder.Create(RecipeType.RangingFang, SkillType.Weaponcraft)
                .Level(47)
                .Category(RecipeCategoryType.Dagger)
                .NormalItem("ranging_fang", 1)
                .HQItem("ranging_fang_p1", 1);

            // Level 49
            _builder.Create(RecipeType.ToxinKukri, SkillType.Weaponcraft)
                .Level(49)
                .Category(RecipeCategoryType.Dagger)
                .NormalItem("toxin_kkr", 1)
                .HQItem("toxin_kkr_p1", 1);

            // Level 59
            _builder.Create(RecipeType.EtherfangKris, SkillType.Weaponcraft)
                .Level(59)
                .Category(RecipeCategoryType.Dagger)
                .NormalItem("etherfang_krs", 1)
                .HQItem("etherfang_krs_p1", 1);

            // Level 62
            _builder.Create(RecipeType.StrikersKnife, SkillType.Weaponcraft)
                .Level(62)
                .Category(RecipeCategoryType.Dagger)
                .NormalItem("striker_knf", 1)
                .HQItem("striker_knf_p1", 1);

            // Level 63
            _builder.Create(RecipeType.RoguesJambiya, SkillType.Weaponcraft)
                .Level(63)
                .Category(RecipeCategoryType.Dagger)
                .NormalItem("rogue_jambya", 1)
                .HQItem("rogue_jambya_p1", 1);

            // Level 70
            _builder.Create(RecipeType.MythriteKnife, SkillType.Weaponcraft)
                .Level(70)
                .Category(RecipeCategoryType.Dagger)
                .NormalItem("mythrite_knf", 1)
                .HQItem("mythrite_knf_p1", 1);

            // Level 75
            _builder.Create(RecipeType.MythriteKukri, SkillType.Weaponcraft)
                .Level(75)
                .Category(RecipeCategoryType.Dagger)
                .NormalItem("mythrite_kkr", 1)
                .HQItem("mythrite_kkr_p1", 1);

            // Level 80
            _builder.Create(RecipeType.SparkfangBaselard, SkillType.Weaponcraft)
                .Level(80)
                .Category(RecipeCategoryType.Dagger)
                .NormalItem("sparkfang_basl", 1)
                .HQItem("sparkfang_basl_p1", 1);

            // Level 82
            _builder.Create(RecipeType.SanctifiedDegen, SkillType.Weaponcraft)
                .Level(82)
                .Category(RecipeCategoryType.Dagger)
                .NormalItem("sanct_dgn", 1)
                .HQItem("sanct_dgn_p1", 1);

            // Level 87
            _builder.Create(RecipeType.WarcasterDagger, SkillType.Weaponcraft)
                .Level(87)
                .Category(RecipeCategoryType.Dagger)
                .NormalItem("warcast_dgr", 1)
                .HQItem("warcast_dgr_p1", 1)
                .UltraItem("warcast_dgr_p2", 1);

            // Level 93
            _builder.Create(RecipeType.BonefangKnife, SkillType.Weaponcraft)
                .Level(93)
                .Category(RecipeCategoryType.Dagger)
                .NormalItem("bonefang_knf", 1)
                .HQItem("bonefang_knf_p1", 1);

            // Level 97
            _builder.Create(RecipeType.CorsairsFang, SkillType.Weaponcraft)
                .Level(97)
                .Category(RecipeCategoryType.Dagger)
                .NormalItem("corsair_fang", 1);
        }
    }
} 