using System.Collections.Generic;
using Anvil.Services;
using XM.Progression.Skill;

namespace XM.Plugin.Craft.RecipeDefinition.Weaponcraft
{
    [ServiceBinding(typeof(IRecipeListDefinition))]
    internal class PolearmRecipes : IRecipeListDefinition
    {
        private readonly RecipeBuilder _builder = new();

        public Dictionary<RecipeType, RecipeDetail> BuildRecipes()
        {
            // Polearms ordered by level (7-100)
            StormHarpoon();              // Level 7
            AurionAlloySpear();          // Level 13
            BrassstrikeSpear();          // Level 33
            SparkfangSpear();            // Level 35
            VanguardLance();             // Level 45
            TitanSpear();                // Level 53
            SparkfangLance();            // Level 75
            TitanLance();                // Level 73
            WarbornHalberd();            // Level 77
            ObeliskWarLance();           // Level 79
            MythriteLance();             // Level 98
            RoyalKnightArmyWarLance();   // Level 100

            return _builder.Build();
        }

        private void StormHarpoon()
        {
            _builder.Create(RecipeType.StormHarpoon, SkillType.Weaponcraft)
                .Level(7)
                .Category(RecipeCategoryType.Polearm)
                .NormalItem("storm_harpoon", 1)
                .HQItem("storm_harpon_p1", 1)
                .Component("aurion_ingot", 3)
                .Component("beast_hide", 1)
                .Component("ether_crystal", 1);
        }

        private void AurionAlloySpear()
        {
            _builder.Create(RecipeType.AurionAlloySpear, SkillType.Weaponcraft)
                .Level(13)
                .Category(RecipeCategoryType.Polearm)
                .NormalItem("aurion_spear", 1)
                .HQItem("aurion_spear_p1", 1)
                .Component("aurion_ingot", 3)
                .Component("beast_hide", 1)
                .Component("ether_crystal", 1);
        }

        private void BrassstrikeSpear()
        {
            _builder.Create(RecipeType.BrassstrikeSpear, SkillType.Weaponcraft)
                .Level(33)
                .Category(RecipeCategoryType.Polearm)
                .NormalItem("brass_spearstk", 1)
                .HQItem("brass_spearstk_p1", 1)
                .Component("brass_sheet", 3)
                .Component("mythrite_frag", 2)
                .Component("ether_crystal", 1);
        }

        private void SparkfangSpear()
        {
            _builder.Create(RecipeType.SparkfangSpear, SkillType.Weaponcraft)
                .Level(35)
                .Category(RecipeCategoryType.Polearm)
                .NormalItem("spark_spear", 1)
                .HQItem("spark_spear_p1", 1)
                .Component("brass_sheet", 3)
                .Component("mythrite_frag", 2)
                .Component("ether_crystal", 1)
                .Component("power_cell", 1);
        }

        private void VanguardLance()
        {
            _builder.Create(RecipeType.VanguardLance, SkillType.Weaponcraft)
                .Level(45)
                .Category(RecipeCategoryType.Polearm)
                .NormalItem("vang_lance", 1)
                .Component("brass_sheet", 3)
                .Component("mythrite_frag", 2)
                .Component("ether_crystal", 1);
        }

        private void TitanSpear()
        {
            _builder.Create(RecipeType.TitanSpear, SkillType.Weaponcraft)
                .Level(53)
                .Category(RecipeCategoryType.Polearm)
                .NormalItem("titan_spear", 1)
                .HQItem("titan_spear_p1", 1)
                .Component("mythrite_frag", 4)
                .Component("psi_crystal", 2)
                .Component("ether_crystal", 1);
        }

        private void TitanLance()
        {
            _builder.Create(RecipeType.TitanLance, SkillType.Weaponcraft)
                .Level(73)
                .Category(RecipeCategoryType.Polearm)
                .NormalItem("titan_lance", 1)
                .HQItem("titan_lance_p1", 1)
                .Component("mythrite_frag", 4)
                .Component("psi_crystal", 2)
                .Component("ether_crystal", 1);
        }

        private void SparkfangLance()
        {
            _builder.Create(RecipeType.SparkfangLance, SkillType.Weaponcraft)
                .Level(75)
                .Category(RecipeCategoryType.Polearm)
                .NormalItem("sparkfang_lnc", 1)
                .HQItem("sparkfang_lnc_p1", 1)
                .Component("mythrite_frag", 4)
                .Component("psi_crystal", 2)
                .Component("ether_crystal", 1)
                .Component("power_cell", 1);
        }

        private void WarbornHalberd()
        {
            _builder.Create(RecipeType.WarbornHalberd, SkillType.Weaponcraft)
                .Level(77)
                .Category(RecipeCategoryType.Polearm)
                .NormalItem("warborn_hlbrd", 1)
                .HQItem("warborn_hlbrd_p1", 1)
                .Component("titan_plate", 4)
                .Component("quantum_proc", 2)
                .Component("ether_crystal", 2);
        }

        private void ObeliskWarLance()
        {
            _builder.Create(RecipeType.ObeliskWarLance, SkillType.Weaponcraft)
                .Level(79)
                .Category(RecipeCategoryType.Polearm)
                .NormalItem("obelisk_wlanc", 1)
                .HQItem("obelisk_wlanc_p1", 1)
                .Component("titan_plate", 4)
                .Component("quantum_proc", 2)
                .Component("ether_crystal", 2);
        }

        private void MythriteLance()
        {
            _builder.Create(RecipeType.MythriteLance, SkillType.Weaponcraft)
                .Level(98)
                .Category(RecipeCategoryType.Polearm)
                .NormalItem("mythrite_lnc", 1)
                .HQItem("mythrite_lnc_p1", 1)
                .Component("titan_plate", 4)
                .Component("quantum_proc", 2)
                .Component("ether_crystal", 2);
        }

        private void RoyalKnightArmyWarLance()
        {
            _builder.Create(RecipeType.RoyalKnightArmyWarLance, SkillType.Weaponcraft)
                .Level(100)
                .Category(RecipeCategoryType.Polearm)
                .NormalItem("roy_knight_wlc", 1)
                .Component("titan_plate", 4)
                .Component("quantum_proc", 2)
                .Component("ether_crystal", 2);
        }
    }
}