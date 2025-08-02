using System.Collections.Generic;
using Anvil.Services;
using XM.Progression.Skill;

namespace XM.Progression.Craft.RecipeDefinition
{
    [ServiceBinding(typeof(IRecipeListDefinition))]
    internal class LongswordRecipes : IRecipeListDefinition
    {
        private readonly RecipeBuilder _builder = new();

        public Dictionary<RecipeType, RecipeDetail> BuildRecipes()
        {
            BuildLongswordRecipes();
            return _builder.Build();
        }

        private void BuildLongswordRecipes()
        {
            // Level 2
            _builder.Create(RecipeType.AurionAlloyBlade, SkillType.Weaponcraft)
                .Level(2)
                .Category(RecipeCategoryType.Longsword)
                .NormalItem("aurion_blade", 1)
                .HQItem("aurion_blade_p1", 1);

            _builder.Create(RecipeType.ResonanceBlade, SkillType.Weaponcraft)
                .Level(2)
                .Category(RecipeCategoryType.Longsword)
                .NormalItem("reson_blade", 1)
                .HQItem("reson_blade_p1", 1);

            // Level 5
            _builder.Create(RecipeType.VanguardCutter, SkillType.Weaponcraft)
                .Level(5)
                .Category(RecipeCategoryType.Longsword)
                .NormalItem("vanguard_cutr", 1)
                .HQItem("vanguard_cutr_p1", 1);

            // Level 10
            _builder.Create(RecipeType.ZenithianArcblade, SkillType.Weaponcraft)
                .Level(10)
                .Category(RecipeCategoryType.Longsword)
                .NormalItem("zenith_arcbl", 1)
                .HQItem("zenith_arcbl_p1", 1);

            // Level 14
            _builder.Create(RecipeType.SpathaTekEdge, SkillType.Weaponcraft)
                .Level(14)
                .Category(RecipeCategoryType.Longsword)
                .NormalItem("spathatek_edg", 1)
                .HQItem("spathatek_edg_p1", 1);

            // Level 16
            _builder.Create(RecipeType.HivefangSpatha, SkillType.Weaponcraft)
                .Level(16)
                .Category(RecipeCategoryType.Longsword)
                .NormalItem("hivefang_spth", 1)
                .HQItem("hivefang_spth_p1", 1);

            // Level 18
            _builder.Create(RecipeType.BrassArcblade, SkillType.Weaponcraft)
                .Level(18)
                .Category(RecipeCategoryType.Longsword)
                .NormalItem("brass_arcbl", 1)
                .HQItem("brass_arcbl_p1", 1);

            // Level 20
            _builder.Create(RecipeType.BilbronEdge, SkillType.Weaponcraft)
                .Level(20)
                .Category(RecipeCategoryType.Longsword)
                .NormalItem("bilbron_edge", 1)
                .HQItem("bilbron_edge_p1", 1);

            // Level 21
            _builder.Create(RecipeType.SparkfangEdge, SkillType.Weaponcraft)
                .Level(21)
                .Category(RecipeCategoryType.Longsword)
                .NormalItem("sparkfang_edg", 1)
                .HQItem("sparkfang_edg_p1", 1);

            // Level 22
            _builder.Create(RecipeType.EtherScimitar, SkillType.Weaponcraft)
                .Level(22)
                .Category(RecipeCategoryType.Longsword)
                .NormalItem("ether_scimita", 1)
                .HQItem("ether_scimta_p1", 1);

            // Level 26
            _builder.Create(RecipeType.FerriteSword, SkillType.Weaponcraft)
                .Level(26)
                .Category(RecipeCategoryType.Longsword)
                .NormalItem("ferrite_sword", 1)
                .HQItem("ferrite_sword_p1", 1);

            // Level 28
            _builder.Create(RecipeType.ArcbladeLongsword, SkillType.Weaponcraft)
                .Level(28)
                .Category(RecipeCategoryType.Longsword)
                .NormalItem("arcblad_lngsw", 1)
                .HQItem("arcblad_lngsw_p1", 1);

            // Level 32
            _builder.Create(RecipeType.DegenTek, SkillType.Weaponcraft)
                .Level(32)
                .Category(RecipeCategoryType.Longsword)
                .NormalItem("degen_tek", 1)
                .HQItem("degen_tek_p1", 1);

            // Level 34
            _builder.Create(RecipeType.PulseTuck, SkillType.Weaponcraft)
                .Level(34)
                .Category(RecipeCategoryType.Longsword)
                .NormalItem("pulse_tuck", 1)
                .HQItem("pulse_tuck_p1", 1);

            // Level 36
            _builder.Create(RecipeType.TitanBroadsword, SkillType.Weaponcraft)
                .Level(36)
                .Category(RecipeCategoryType.Longsword)
                .NormalItem("titan_broadsw", 1)
                .HQItem("titan_broadsw_p1", 1);

            // Level 40
            _builder.Create(RecipeType.EtherFleuret, SkillType.Weaponcraft)
                .Level(40)
                .Category(RecipeCategoryType.Longsword)
                .NormalItem("ether_fleuret", 1)
                .HQItem("ether_fleurt_p1", 1);

            // Level 42
            _builder.Create(RecipeType.SteelKiljBlade, SkillType.Weaponcraft)
                .Level(42)
                .Category(RecipeCategoryType.Longsword)
                .NormalItem("steel_kiljbl", 1)
                .HQItem("steel_kiljbl_p1", 1);

            // Level 44
            _builder.Create(RecipeType.SanctifiedEdge, SkillType.Weaponcraft)
                .Level(44)
                .Category(RecipeCategoryType.Longsword)
                .NormalItem("sanct_edge", 1)
                .HQItem("sanct_edge_p1", 1);

            // Level 50
            _builder.Create(RecipeType.MythriteDegen, SkillType.Weaponcraft)
                .Level(50)
                .Category(RecipeCategoryType.Longsword)
                .NormalItem("mythrite_dgn", 1)
                .HQItem("mythrite_dgn_p1", 1);

            // Level 54
            _builder.Create(RecipeType.MythriteBlade, SkillType.Weaponcraft)
                .Level(54)
                .Category(RecipeCategoryType.Longsword)
                .NormalItem("mythrite_bld", 1)
                .HQItem("mythrite_bld_p1", 1);

            // Level 58
            _builder.Create(RecipeType.SparkfangDegen, SkillType.Weaponcraft)
                .Level(58)
                .Category(RecipeCategoryType.Longsword)
                .NormalItem("sparkfang_dgn", 1)
                .HQItem("sparkfang_dgn_p1", 1);

            // Level 64
            _builder.Create(RecipeType.RiftTulwar, SkillType.Weaponcraft)
                .Level(64)
                .Category(RecipeCategoryType.Longsword)
                .NormalItem("rift_tulwar", 1)
                .HQItem("rift_tulwar_p1", 1);

            // Level 68
            _builder.Create(RecipeType.AscendantBlade, SkillType.Weaponcraft)
                .Level(68)
                .Category(RecipeCategoryType.Longsword)
                .NormalItem("ascend_bld", 1)
                .HQItem("ascend_bld_p1", 1);

            // Level 74
            _builder.Create(RecipeType.InfernalDegen, SkillType.Weaponcraft)
                .Level(74)
                .Category(RecipeCategoryType.Longsword)
                .NormalItem("infernal_dgn", 1)
                .HQItem("infernal_dgn_p1", 1);

            // Level 78
            _builder.Create(RecipeType.CrescentShotel, SkillType.Weaponcraft)
                .Level(78)
                .Category(RecipeCategoryType.Longsword)
                .NormalItem("crescent_shtl", 1)
                .HQItem("crescent_shtl_p1", 1);

            // Level 84
            _builder.Create(RecipeType.NoviceDuelistsTuck, SkillType.Weaponcraft)
                .Level(84)
                .Category(RecipeCategoryType.Longsword)
                .NormalItem("nov_duel_tuck", 1)
                .HQItem("nov_duel_tuck_p1", 1)
                .UltraItem("nov_duel_tuck_p2", 1);

            // Level 86
            _builder.Create(RecipeType.CombatCastersTalon, SkillType.Weaponcraft)
                .Level(86)
                .Category(RecipeCategoryType.Longsword)
                .NormalItem("combatcast_tln", 1)
                .HQItem("combatcast_tln_p1", 1)
                .UltraItem("combatcast_tln_p2", 1);

            // Level 88
            _builder.Create(RecipeType.ArcFalchion, SkillType.Weaponcraft)
                .Level(88)
                .Category(RecipeCategoryType.Longsword)
                .NormalItem("arc_falchn", 1)
                .HQItem("arc_falchn_p1", 1);

            // Level 92
            _builder.Create(RecipeType.InfernalEdge, SkillType.Weaponcraft)
                .Level(92)
                .Category(RecipeCategoryType.Longsword)
                .NormalItem("infernal_edg", 1)
                .HQItem("infernal_edg_p1", 1);

            // Level 96
            _builder.Create(RecipeType.VanguardKnightsSword, SkillType.Weaponcraft)
                .Level(96)
                .Category(RecipeCategoryType.Longsword)
                .NormalItem("vang_knight_swd", 1)
                .HQItem("vang_knight_swd_p1", 1);

            // Level 98
            _builder.Create(RecipeType.MusketeersEdge, SkillType.Weaponcraft)
                .Level(98)
                .Category(RecipeCategoryType.Longsword)
                .NormalItem("musketeer_edg", 1);
        }
    }
} 