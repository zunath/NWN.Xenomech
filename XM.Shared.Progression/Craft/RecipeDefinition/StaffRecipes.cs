using System.Collections.Generic;
using Anvil.Services;
using XM.Progression.Skill;

namespace XM.Progression.Craft.RecipeDefinition
{
    [ServiceBinding(typeof(IRecipeListDefinition))]
    internal class StaffRecipes : IRecipeListDefinition
    {
        private readonly RecipeBuilder _builder = new();

        public Dictionary<RecipeType, RecipeDetail> BuildRecipes()
        {
            BuildStaffRecipes();
            return _builder.Build();
        }

        private void BuildStaffRecipes()
        {
            // Level 4
            _builder.Create(RecipeType.AshenWarstaff, SkillType.Weaponcraft)
                .Level(4)
                .Category(RecipeCategoryType.Staff)
                .NormalItem("ashen_wstaff", 1)
                .HQItem("ashen_wstf_p1", 1);

            // Level 14
            _builder.Create(RecipeType.AshenPolearm, SkillType.Weaponcraft)
                .Level(14)
                .Category(RecipeCategoryType.Staff)
                .NormalItem("ashen_polearm", 1)
                .HQItem("ashen_plrm_p1", 1);

            // Level 23
            _builder.Create(RecipeType.VerdantHollyStaff, SkillType.Weaponcraft)
                .Level(23)
                .Category(RecipeCategoryType.Staff)
                .NormalItem("verdant_hstf", 1)
                .HQItem("verdant_hstf_p1", 1);

            // Level 33
            _builder.Create(RecipeType.VerdantHollyPolearm, SkillType.Weaponcraft)
                .Level(33)
                .Category(RecipeCategoryType.Staff)
                .NormalItem("verdant_hpole", 1)
                .HQItem("verdant_hpole_p1", 1);

            // Level 46
            _builder.Create(RecipeType.ElmwoodWarstaff, SkillType.Weaponcraft)
                .Level(46)
                .Category(RecipeCategoryType.Staff)
                .NormalItem("elmwood_wstf", 1)
                .HQItem("elmwood_wstf_p1", 1);

            // Level 52
            _builder.Create(RecipeType.SpikedMaul, SkillType.Weaponcraft)
                .Level(52)
                .Category(RecipeCategoryType.Staff)
                .NormalItem("spiked_maul", 1)
                .HQItem("spiked_maul_p1", 1);

            // Level 62
            _builder.Create(RecipeType.ElmwoodPolearm, SkillType.Weaponcraft)
                .Level(62)
                .Category(RecipeCategoryType.Staff)
                .NormalItem("elmwood_plrm", 1)
                .HQItem("elmwood_plrm_p1", 1);

            // Level 64
            _builder.Create(RecipeType.LeviathanStaff, SkillType.Weaponcraft)
                .Level(64)
                .Category(RecipeCategoryType.Staff)
                .NormalItem("leviathan_stf", 1)
                .HQItem("leviathan_stf_p1", 1);

            // Level 82
            _builder.Create(RecipeType.OakwoodWarstaff, SkillType.Weaponcraft)
                .Level(82)
                .Category(RecipeCategoryType.Staff)
                .NormalItem("oakwood_wstf", 1)
                .HQItem("oakwood_wstf_p1", 1);

            // Level 84
            _builder.Create(RecipeType.PassaddhiMysticStaff, SkillType.Weaponcraft)
                .Level(84)
                .Category(RecipeCategoryType.Staff)
                .NormalItem("passaddh_myst", 1)
                .HQItem("passaddh_myst_p1", 1);

            // Level 86
            _builder.Create(RecipeType.OakwoodPolearm, SkillType.Weaponcraft)
                .Level(86)
                .Category(RecipeCategoryType.Staff)
                .NormalItem("oakwood_plrm", 1)
                .HQItem("oakwood_plrm_p1", 1);

            // Level 88
            _builder.Create(RecipeType.QiResonanceStaff, SkillType.Weaponcraft)
                .Level(88)
                .Category(RecipeCategoryType.Staff)
                .NormalItem("qi_reson_stf", 1)
                .HQItem("qi_reson_stf_p1", 1);

            // Level 94
            _builder.Create(RecipeType.RosethornConduit, SkillType.Weaponcraft)
                .Level(94)
                .Category(RecipeCategoryType.Staff)
                .NormalItem("rosethorn_cndt", 1)
                .HQItem("rosethorn_cndt_p1", 1);

            // Level 95
            _builder.Create(RecipeType.MusketeersPolearm, SkillType.Weaponcraft)
                .Level(95)
                .Category(RecipeCategoryType.Staff)
                .NormalItem("musketeer_plrm", 1);

            // Level 100
            _builder.Create(RecipeType.TundraWalrusStaff, SkillType.Weaponcraft)
                .Level(100)
                .Category(RecipeCategoryType.Staff)
                .NormalItem("tundra_wal_stf", 1);
        }
    }
} 