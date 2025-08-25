using System.Collections.Generic;
using Anvil.Services;
using XM.Progression.Skill;

namespace XM.Plugin.Craft.RecipeDefinition.Weaponcraft
{
    [ServiceBinding(typeof(IRecipeListDefinition))]
    internal class DaggerRecipes : IRecipeListDefinition
    {
        private readonly RecipeBuilder _builder = new();

        public Dictionary<RecipeType, RecipeDetail> BuildRecipes()
        {
            // Daggers ordered by level (1-97)
            AurionAlloyDagger();        // Level 1
            AurionAlloyKnife();         // Level 3
            Shadowpiercer();            // Level 8
            PhantomKnife();             // Level 12
            ArcDagger();                // Level 16
            BrassfangDagger();          // Level 19
            WhisperfangDagger();        // Level 21
            ArcKnife();                 // Level 23
            SparkfangDagger();          // Level 27
            VenomfangBaselard();        // Level 31
            EtherfangKukri();           // Level 41
            ToxinFang();                // Level 43
            ToxinKnife();               // Level 45
            MythriteDagger();           // Level 46
            RangingFang();              // Level 47
            ToxinKukri();               // Level 49
            EtherfangKris();            // Level 59 (Etherfang Kris)
            StrikerKnife();             // Level 62 (Striker's Knife)
            RoguesJambiya();            // Level 63
            MythriteKnife();            // Level 70
            MythriteKukri();            // Level 75
            SparkfangBaselard();        // Level 80
            WarcasterDagger();          // Level 87
            BonefangKnife();            // Level 93
            CorsairsFang();             // Level 97

            return _builder.Build();
        }

        private void AurionAlloyDagger()
        {
            _builder.Create(RecipeType.AurionAlloyDagger, SkillType.Weaponcraft)
                .Level(1)
                .Category(RecipeCategoryType.Dagger)
                .NormalItem("aurion_daggr", 1)
                .HQItem("aurion_daggr_p1", 1)
                .Component("aurion_ingot", 2)
                .Component("beast_hide", 1)
                .Component("flux_compound", 1);
        }

        private void AurionAlloyKnife()
        {
            _builder.Create(RecipeType.AurionAlloyKnife, SkillType.Weaponcraft)
                .Level(3)
                .Category(RecipeCategoryType.Dagger)
                .NormalItem("aurion_knife", 1)
                .HQItem("aurion_knife_p1", 1)
                .Component("aurion_ingot", 2)
                .Component("beast_hide", 1)
                .Component("flux_compound", 1);
        }

        private void Shadowpiercer()
        {
            _builder.Create(RecipeType.Shadowpiercer, SkillType.Weaponcraft)
                .Level(8)
                .Category(RecipeCategoryType.Dagger)
                .NormalItem("shadowpiercer", 1)
                .HQItem("shadowpiercer_p1", 1)
                .Component("aurion_ingot", 2)
                .Component("beast_hide", 1)
                .Component("flux_compound", 1)
                .Component("void_shard", 1);
        }

        private void PhantomKnife()
        {
            _builder.Create(RecipeType.PhantomKnife, SkillType.Weaponcraft)
                .Level(12)
                .Category(RecipeCategoryType.Dagger)
                .NormalItem("phantom_knife", 1)
                .HQItem("phantom_knf_p1", 1)
                .Component("aurion_ingot", 2)
                .Component("beast_hide", 1)
                .Component("flux_compound", 1)
                .Component("void_shard", 1);
        }

        private void ArcDagger()
        {
            _builder.Create(RecipeType.ArcDagger, SkillType.Weaponcraft)
                .Level(16)
                .Category(RecipeCategoryType.Dagger)
                .NormalItem("arc_dagger", 1)
                .HQItem("arc_dagger_p1", 1)
                .Component("ferrite_core", 2)
                .Component("circuit_matrix", 1)
                .Component("power_cell", 1)
                .Component("amp_crystal", 1);
        }

        private void BrassfangDagger()
        {
            _builder.Create(RecipeType.BrassfangDagger, SkillType.Weaponcraft)
                .Level(19)
                .Category(RecipeCategoryType.Dagger)
                .NormalItem("brassfang_dgr", 1)
                .HQItem("brassfang_dgr_p1", 1)
                .Component("ferrite_core", 2)
                .Component("circuit_matrix", 1)
                .Component("power_cell", 1);
        }

        private void WhisperfangDagger()
        {
            _builder.Create(RecipeType.WhisperfangDagger, SkillType.Weaponcraft)
                .Level(21)
                .Category(RecipeCategoryType.Dagger)
                .NormalItem("whisper_dgr", 1)
                .HQItem("whisper_dgr_p1", 1)
                .Component("ferrite_core", 2)
                .Component("circuit_matrix", 1)
                .Component("power_cell", 1);
        }

        private void ArcKnife()
        {
            _builder.Create(RecipeType.ArcKnife, SkillType.Weaponcraft)
                .Level(23)
                .Category(RecipeCategoryType.Dagger)
                .NormalItem("arc_knife", 1)
                .HQItem("arc_knife_p1", 1)
                .Component("ferrite_core", 2)
                .Component("circuit_matrix", 1)
                .Component("power_cell", 1)
                .Component("amp_crystal", 1);
        }

        private void SparkfangDagger()
        {
            _builder.Create(RecipeType.SparkfangDagger, SkillType.Weaponcraft)
                .Level(27)
                .Category(RecipeCategoryType.Dagger)
                .NormalItem("spark_dagger", 1)
                .HQItem("spark_dagger_p1", 1)
                .Component("brass_sheet", 2)
                .Component("mythrite_frag", 1)
                .Component("enhance_serum", 1)
                .Component("power_cell", 1);
        }

        private void VenomfangBaselard()
        {
            _builder.Create(RecipeType.VenomfangBaselard, SkillType.Weaponcraft)
                .Level(31)
                .Category(RecipeCategoryType.Dagger)
                .NormalItem("venom_baselrd", 1)
                .HQItem("venom_baselrd_p1", 1)
                .Component("brass_sheet", 2)
                .Component("mythrite_frag", 1)
                .Component("enhance_serum", 1)
                .Component("venom_sac", 1);
        }

        private void EtherfangKukri()
        {
            _builder.Create(RecipeType.EtherfangKukri, SkillType.Weaponcraft)
                .Level(41)
                .Category(RecipeCategoryType.Dagger)
                .NormalItem("etherfang_kkr", 1)
                .HQItem("etherfang_kkr_p1", 1)
                .Component("brass_sheet", 2)
                .Component("mythrite_frag", 1)
                .Component("enhance_serum", 1)
                .Component("ether_crystal", 1);
        }

        private void ToxinFang()
        {
            _builder.Create(RecipeType.ToxinFang, SkillType.Weaponcraft)
                .Level(43)
                .Category(RecipeCategoryType.Dagger)
                .NormalItem("toxin_fang", 1)
                .HQItem("toxin_fang_p1", 1)
                .Component("brass_sheet", 2)
                .Component("mythrite_frag", 1)
                .Component("enhance_serum", 1)
                .Component("venom_sac", 1);
        }

        private void ToxinKnife()
        {
            _builder.Create(RecipeType.ToxinKnife, SkillType.Weaponcraft)
                .Level(45)
                .Category(RecipeCategoryType.Dagger)
                .NormalItem("toxin_knife", 1)
                .HQItem("toxin_knife_p1", 1)
                .Component("brass_sheet", 2)
                .Component("mythrite_frag", 1)
                .Component("enhance_serum", 1)
                .Component("venom_sac", 1);
        }

        private void MythriteDagger()
        {
            _builder.Create(RecipeType.MythriteDagger, SkillType.Weaponcraft)
                .Level(46)
                .Category(RecipeCategoryType.Dagger)
                .NormalItem("mythrite_dagr", 1)
                .HQItem("mythrite_dgr_p1", 1)
                .Component("mythrite_frag", 3)
                .Component("psi_crystal", 1)
                .Component("biosteel_comp", 1);
        }

        private void RangingFang()
        {
            _builder.Create(RecipeType.RangingFang, SkillType.Weaponcraft)
                .Level(47)
                .Category(RecipeCategoryType.Dagger)
                .NormalItem("ranging_fang", 1)
                .HQItem("ranging_fang_p1", 1)
                .Component("mythrite_frag", 3)
                .Component("psi_crystal", 1)
                .Component("biosteel_comp", 1);
        }

        private void ToxinKukri()
        {
            _builder.Create(RecipeType.ToxinKukri, SkillType.Weaponcraft)
                .Level(49)
                .Category(RecipeCategoryType.Dagger)
                .NormalItem("toxin_kkr", 1)
                .HQItem("toxin_kkr_p1", 1)
                .Component("mythrite_frag", 3)
                .Component("psi_crystal", 1)
                .Component("biosteel_comp", 1)
                .Component("venom_sac", 1);
        }

        private void EtherfangKris()
        {
            _builder.Create(RecipeType.EtherfangKris, SkillType.Weaponcraft)
                .Level(59)
                .Category(RecipeCategoryType.Dagger)
                .NormalItem("etherfang_krs", 1)
                .HQItem("etherfang_krs_p1", 1)
                .Component("mythrite_frag", 3)
                .Component("psi_crystal", 1)
                .Component("biosteel_comp", 1)
                .Component("ether_crystal", 1);
        }

        private void StrikerKnife()
        {
            _builder.Create(RecipeType.StrikerKnife, SkillType.Weaponcraft)
                .Level(62)
                .Category(RecipeCategoryType.Dagger)
                .NormalItem("striker_knf", 1)
                .HQItem("striker_knf_p1", 1)
                .Component("mythrite_frag", 3)
                .Component("psi_crystal", 1)
                .Component("biosteel_comp", 1);
        }

        private void RoguesJambiya()
        {
            _builder.Create(RecipeType.RoguesJambiya, SkillType.Weaponcraft)
                .Level(63)
                .Category(RecipeCategoryType.Dagger)
                .NormalItem("rogue_jambya", 1)
                .HQItem("rogue_jambya_p1", 1)
                .Component("mythrite_frag", 3)
                .Component("psi_crystal", 1)
                .Component("biosteel_comp", 1);
        }

        private void MythriteKnife()
        {
            _builder.Create(RecipeType.MythriteKnife, SkillType.Weaponcraft)
                .Level(70)
                .Category(RecipeCategoryType.Dagger)
                .NormalItem("mythrite_knf", 1)
                .HQItem("mythrite_knf_p1", 1)
                .Component("mythrite_frag", 3)
                .Component("psi_crystal", 1)
                .Component("biosteel_comp", 1);
        }

        private void MythriteKukri()
        {
            _builder.Create(RecipeType.MythriteKukri, SkillType.Weaponcraft)
                .Level(75)
                .Category(RecipeCategoryType.Dagger)
                .NormalItem("mythrite_kkr", 1)
                .HQItem("mythrite_kkr_p1", 1)
                .Component("mythrite_frag", 3)
                .Component("psi_crystal", 1)
                .Component("biosteel_comp", 1);
        }

        private void SparkfangBaselard()
        {
            _builder.Create(RecipeType.SparkfangBaselard, SkillType.Weaponcraft)
                .Level(80)
                .Category(RecipeCategoryType.Dagger)
                .NormalItem("sparkfang_basl", 1)
                .HQItem("sparkfang_basl_p1", 1)
                .Component("titan_plate", 3)
                .Component("quantum_proc", 1)
                .Component("quantmyst_core", 1)
                .Component("power_cell", 1);
        }

        private void WarcasterDagger()
        {
            _builder.Create(RecipeType.WarcasterDagger, SkillType.Weaponcraft)
                .Level(87)
                .Category(RecipeCategoryType.Dagger)
                .NormalItem("warcast_dgr", 1)
                .HQItem("warcast_dgr_p1", 1)
                .UltraItem("warcast_dgr_p2", 1)
                .Component("titan_plate", 3)
                .Component("quantum_proc", 1)
                .Component("quantmyst_core", 1)
                .Component("power_cell", 1);
        }

        private void BonefangKnife()
        {
            _builder.Create(RecipeType.BonefangKnife, SkillType.Weaponcraft)
                .Level(93)
                .Category(RecipeCategoryType.Dagger)
                .NormalItem("bonefang_knf", 1)
                .HQItem("bonefang_knf_p1", 1)
                .Component("titan_plate", 3)
                .Component("quantum_proc", 1)
                .Component("quantmyst_core", 1);
        }

        private void CorsairsFang()
        {
            _builder.Create(RecipeType.CorsairsFang, SkillType.Weaponcraft)
                .Level(97)
                .Category(RecipeCategoryType.Dagger)
                .NormalItem("corsair_fang", 1)
                .Component("titan_plate", 3)
                .Component("quantum_proc", 1)
                .Component("quantmyst_core", 1);
        }
    }
}