using System.Collections.Generic;
using Anvil.Services;
using XM.Progression.Skill;

namespace XM.Plugin.Craft.RecipeDefinition.Weaponcraft
{
    [ServiceBinding(typeof(IRecipeListDefinition))]
    internal class StaffRecipes : IRecipeListDefinition
    {
        private readonly RecipeBuilder _builder = new();

        public Dictionary<RecipeType, RecipeDetail> BuildRecipes()
        {
            // Staffs ordered by level (4-100)
            AshenWarstaff();             // Level 4
            AshenPolearm();              // Level 14
            VerdantHollyStaff();         // Level 23
            VerdantHollyPolearm();       // Level 33
            ElmwoodWarstaff();           // Level 46
            SpikedMaul();                // Level 52
            ElmwoodPolearm();            // Level 62
            LeviathanStaff();            // Level 64
            OakwoodWarstaff();           // Level 82
            PassaddhiMysticStaff();      // Level 84
            OakwoodPolearm();            // Level 86
            QiResonanceStaff();          // Level 88
            MusketeersPolearm();         // Level 95
            TundraWalrusStaff();         // Level 100

            return _builder.Build();
        }

        private void AshenWarstaff()
        {
            _builder.Create(RecipeType.AshenWarstaff, SkillType.Weaponcraft)
                .Level(4)
                .Category(RecipeCategoryType.Staff)
                .NormalItem("ashen_wstaff", 1)
                .HQItem("ashen_wstf_p1", 1)
                .Component("aurion_ingot", 3)
                .Component("beast_hide", 1)
                .Component("ether_crystal", 1);
        }

        private void AshenPolearm()
        {
            _builder.Create(RecipeType.AshenPolearm, SkillType.Weaponcraft)
                .Level(14)
                .Category(RecipeCategoryType.Staff)
                .NormalItem("ashen_polearm", 1)
                .HQItem("ashen_plrm_p1", 1)
                .Component("aurion_ingot", 3)
                .Component("beast_hide", 1)
                .Component("ether_crystal", 1);
        }

        private void VerdantHollyStaff()
        {
            _builder.Create(RecipeType.VerdantHollyStaff, SkillType.Weaponcraft)
                .Level(23)
                .Category(RecipeCategoryType.Staff)
                .NormalItem("verdant_hstf", 1)
                .HQItem("verdant_hstf_p1", 1)
                .Component("ferrite_core", 3)
                .Component("circuit_matrix", 1)
                .Component("ether_crystal", 1);
        }

        private void VerdantHollyPolearm()
        {
            _builder.Create(RecipeType.VerdantHollyPolearm, SkillType.Weaponcraft)
                .Level(33)
                .Category(RecipeCategoryType.Staff)
                .NormalItem("verdant_hpole", 1)
                .HQItem("verdant_hpole_p1", 1)
                .Component("brass_sheet", 3)
                .Component("mythrite_frag", 2)
                .Component("ether_crystal", 1);
        }

        private void ElmwoodWarstaff()
        {
            _builder.Create(RecipeType.ElmwoodWarstaff, SkillType.Weaponcraft)
                .Level(46)
                .Category(RecipeCategoryType.Staff)
                .NormalItem("elmwood_wstf", 1)
                .HQItem("elmwood_wstf_p1", 1)
                .Component("living_wood", 4)
                .Component("mythrite_frag", 2)
                .Component("ether_crystal", 1);
        }

        private void SpikedMaul()
        {
            _builder.Create(RecipeType.SpikedMaul, SkillType.Weaponcraft)
                .Level(52)
                .Category(RecipeCategoryType.Staff)
                .NormalItem("spiked_maul", 1)
                .HQItem("spiked_maul_p1", 1)
                .Component("mythrite_frag", 4)
                .Component("psi_crystal", 2)
                .Component("ether_crystal", 1);
        }

        private void ElmwoodPolearm()
        {
            _builder.Create(RecipeType.ElmwoodPolearm, SkillType.Weaponcraft)
                .Level(62)
                .Category(RecipeCategoryType.Staff)
                .NormalItem("elmwood_plrm", 1)
                .HQItem("elmwood_plrm_p1", 1)
                .Component("living_wood", 4)
                .Component("mythrite_frag", 2)
                .Component("ether_crystal", 1);
        }

        private void LeviathanStaff()
        {
            _builder.Create(RecipeType.LeviathanStaff, SkillType.Weaponcraft)
                .Level(64)
                .Category(RecipeCategoryType.Staff)
                .NormalItem("leviathan_stf", 1)
                .HQItem("leviathan_stf_p1", 1)
                .Component("mythrite_frag", 4)
                .Component("psi_crystal", 2)
                .Component("ether_crystal", 1);
        }

        private void OakwoodWarstaff()
        {
            _builder.Create(RecipeType.OakwoodWarstaff, SkillType.Weaponcraft)
                .Level(82)
                .Category(RecipeCategoryType.Staff)
                .NormalItem("oakwood_wstf", 1)
                .HQItem("oakwood_wstf_p1", 1)
                .Component("living_wood", 5)
                .Component("titan_plate", 2)
                .Component("ether_crystal", 2);
        }

        private void PassaddhiMysticStaff()
        {
            _builder.Create(RecipeType.PassaddhiMysticStaff, SkillType.Weaponcraft)
                .Level(84)
                .Category(RecipeCategoryType.Staff)
                .NormalItem("passaddh_myst", 1)
                .HQItem("passaddh_myst_p1", 1)
                .Component("titan_plate", 4)
                .Component("quantum_proc", 2)
                .Component("ether_crystal", 2);
        }

        private void OakwoodPolearm()
        {
            _builder.Create(RecipeType.OakwoodPolearm, SkillType.Weaponcraft)
                .Level(86)
                .Category(RecipeCategoryType.Staff)
                .NormalItem("oakwood_plrm", 1)
                .HQItem("oakwood_plrm_p1", 1)
                .Component("living_wood", 5)
                .Component("titan_plate", 2)
                .Component("ether_crystal", 2);
        }

        private void QiResonanceStaff()
        {
            _builder.Create(RecipeType.QiResonanceStaff, SkillType.Weaponcraft)
                .Level(88)
                .Category(RecipeCategoryType.Staff)
                .NormalItem("qi_reson_stf", 1)
                .HQItem("qi_reson_stf_p1", 1)
                .Component("titan_plate", 4)
                .Component("quantum_proc", 2)
                .Component("ether_crystal", 2);
        }

        private void MusketeersPolearm()
        {
            _builder.Create(RecipeType.MusketeersPolearm, SkillType.Weaponcraft)
                .Level(95)
                .Category(RecipeCategoryType.Staff)
                .NormalItem("musketeer_plrm", 1)
                .Component("titan_plate", 4)
                .Component("quantum_proc", 2)
                .Component("ether_crystal", 2);
        }

        private void TundraWalrusStaff()
        {
            _builder.Create(RecipeType.TundraWalrusStaff, SkillType.Weaponcraft)
                .Level(100)
                .Category(RecipeCategoryType.Staff)
                .NormalItem("tundra_wal_stf", 1)
                .Component("titan_plate", 4)
                .Component("quantum_proc", 2)
                .Component("ether_crystal", 2);
        }
    }
}