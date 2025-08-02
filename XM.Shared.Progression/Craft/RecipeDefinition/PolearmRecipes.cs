using System.Collections.Generic;
using Anvil.Services;
using XM.Progression.Skill;

namespace XM.Progression.Craft.RecipeDefinition
{
    [ServiceBinding(typeof(IRecipeListDefinition))]
    internal class PolearmRecipes : IRecipeListDefinition
    {
        private readonly RecipeBuilder _builder = new();

        public Dictionary<RecipeType, RecipeDetail> BuildRecipes()
        {
            BuildPolearmRecipes();
            return _builder.Build();
        }

        private void BuildPolearmRecipes()
        {
            // Level 7
            _builder.Create(RecipeType.StormHarpoon, SkillType.Weaponcraft)
                .Level(7)
                .Category(RecipeCategoryType.Polearm)
                .NormalItem("storm_harpoon", 1)
                .HQItem("storm_harpon_p1", 1);

            // Level 13
            _builder.Create(RecipeType.AurionAlloySpear, SkillType.Weaponcraft)
                .Level(13)
                .Category(RecipeCategoryType.Polearm)
                .NormalItem("aurion_spear", 1)
                .HQItem("aurion_spear_p1", 1);

            // Level 33
            _builder.Create(RecipeType.BrassstrikeSpear, SkillType.Weaponcraft)
                .Level(33)
                .Category(RecipeCategoryType.Polearm)
                .NormalItem("brass_spearstk", 1)
                .HQItem("brass_spearstk_p1", 1);

            // Level 35
            _builder.Create(RecipeType.SparkfangSpear, SkillType.Weaponcraft)
                .Level(35)
                .Category(RecipeCategoryType.Polearm)
                .NormalItem("spark_spear", 1)
                .HQItem("spark_spear_p1", 1);

            // Level 45
            _builder.Create(RecipeType.VanguardLance, SkillType.Weaponcraft)
                .Level(45)
                .Category(RecipeCategoryType.Polearm)
                .NormalItem("vang_lance", 1);

            // Level 53
            _builder.Create(RecipeType.TitanSpear, SkillType.Weaponcraft)
                .Level(53)
                .Category(RecipeCategoryType.Polearm)
                .NormalItem("titan_spear", 1)
                .HQItem("titan_spear_p1", 1);

            // Level 73
            _builder.Create(RecipeType.TitanLance, SkillType.Weaponcraft)
                .Level(73)
                .Category(RecipeCategoryType.Polearm)
                .NormalItem("titan_lance", 1)
                .HQItem("titan_lance_p1", 1);

            // Level 75
            _builder.Create(RecipeType.SparkfangLance, SkillType.Weaponcraft)
                .Level(75)
                .Category(RecipeCategoryType.Polearm)
                .NormalItem("sparkfang_lnc", 1)
                .HQItem("sparkfang_lnc_p1", 1);

            // Level 77
            _builder.Create(RecipeType.WarbornHalberd, SkillType.Weaponcraft)
                .Level(77)
                .Category(RecipeCategoryType.Polearm)
                .NormalItem("warborn_hlbrd", 1)
                .HQItem("warborn_hlbrd_p1", 1);

            // Level 79
            _builder.Create(RecipeType.ObeliskWarLance, SkillType.Weaponcraft)
                .Level(79)
                .Category(RecipeCategoryType.Polearm)
                .NormalItem("obelisk_wlanc", 1)
                .HQItem("obelisk_wlanc_p1", 1);

            // Level 98
            _builder.Create(RecipeType.MythriteLance, SkillType.Weaponcraft)
                .Level(98)
                .Category(RecipeCategoryType.Polearm)
                .NormalItem("mythrite_lnc", 1)
                .HQItem("mythrite_lnc_p1", 1);

            // Level 100
            _builder.Create(RecipeType.RoyalKnightArmyWarLance, SkillType.Weaponcraft)
                .Level(100)
                .Category(RecipeCategoryType.Polearm)
                .NormalItem("roy_knight_wlc", 1);
        }
    }
} 