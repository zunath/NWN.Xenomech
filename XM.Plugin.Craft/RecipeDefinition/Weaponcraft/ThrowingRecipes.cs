using System.Collections.Generic;
using Anvil.Services;
using XM.Progression.Skill;

namespace XM.Plugin.Craft.RecipeDefinition.Weaponcraft
{
    [ServiceBinding(typeof(IRecipeListDefinition))]
    internal class ThrowingRecipes : IRecipeListDefinition
    {
        private readonly RecipeBuilder _builder = new();

        public Dictionary<RecipeType, RecipeDetail> BuildRecipes()
        {
            // Throwing weapons ordered by level (4-99)
            CoarseShuriken();            // Level 4
            ArcShuriken();               // Level 7
            VanguardShuriken();          // Level 35
            WingShuriken();              // Level 43
            EtherShuriken();             // Level 55
            LongstrikeShuriken();        // Level 67
            WarcastersShuriken();        // Level 81
            NoviceDuelistsShuriken();    // Level 85
            YagudoCryoShuriken();        // Level 91
            CometFangShuriken();         // Level 99

            return _builder.Build();
        }

        private void CoarseShuriken()
        {
            _builder.Create(RecipeType.CoarseShuriken, SkillType.Weaponcraft)
                .Level(4)
                .Category(RecipeCategoryType.Throwing)
                .NormalItem("coarse_shurikn", 25)
                .Component("aurion_ingot", 2)
                .Component("beast_hide", 1)
                .Component("flux_compound", 1);
        }

        private void ArcShuriken()
        {
            _builder.Create(RecipeType.ArcShuriken, SkillType.Weaponcraft)
                .Level(7)
                .Category(RecipeCategoryType.Throwing)
                .NormalItem("arc_shuriken", 25)
                .HQItem("arc_shuriken_p1", 25)
                .Component("aurion_ingot", 2)
                .Component("beast_hide", 1)
                .Component("flux_compound", 1)
                .Component("power_cell", 1);
        }

        private void VanguardShuriken()
        {
            _builder.Create(RecipeType.VanguardShuriken, SkillType.Weaponcraft)
                .Level(35)
                .Category(RecipeCategoryType.Throwing)
                .NormalItem("vang_shuriken", 25)
                .Component("brass_sheet", 2)
                .Component("mythrite_frag", 1)
                .Component("enhance_serum", 1);
        }

        private void WingShuriken()
        {
            _builder.Create(RecipeType.WingShuriken, SkillType.Weaponcraft)
                .Level(43)
                .Category(RecipeCategoryType.Throwing)
                .NormalItem("wing_shuriken", 25)
                .HQItem("wing_shurkn_p1", 25)
                .Component("brass_sheet", 2)
                .Component("mythrite_frag", 1)
                .Component("enhance_serum", 1);
        }

        private void EtherShuriken()
        {
            _builder.Create(RecipeType.EtherShuriken, SkillType.Weaponcraft)
                .Level(55)
                .Category(RecipeCategoryType.Throwing)
                .NormalItem("ether_shrkn", 25)
                .HQItem("ether_shrkn_p1", 25)
                .Component("mythrite_frag", 3)
                .Component("psi_crystal", 1)
                .Component("biosteel_comp", 1)
                .Component("ether_crystal", 1);
        }

        private void LongstrikeShuriken()
        {
            _builder.Create(RecipeType.LongstrikeShuriken, SkillType.Weaponcraft)
                .Level(67)
                .Category(RecipeCategoryType.Throwing)
                .NormalItem("longstrk_shrkn", 25)
                .Component("mythrite_frag", 3)
                .Component("psi_crystal", 1)
                .Component("biosteel_comp", 1);
        }

        private void WarcastersShuriken()
        {
            _builder.Create(RecipeType.WarcastersShuriken, SkillType.Weaponcraft)
                .Level(81)
                .Category(RecipeCategoryType.Throwing)
                .NormalItem("warcast_shrkn", 25)
                .HQItem("warcast_shrkn_p1", 25)
                .UltraItem("warcast_shrkn_p2", 25)
                .Component("titan_plate", 3)
                .Component("quantum_proc", 1)
                .Component("quantmyst_core", 1)
                .Component("power_cell", 1);
        }

        private void NoviceDuelistsShuriken()
        {
            _builder.Create(RecipeType.NoviceDuelistsShuriken, SkillType.Weaponcraft)
                .Level(85)
                .Category(RecipeCategoryType.Throwing)
                .NormalItem("nov_duel_shrkn", 25)
                .HQItem("nov_duel_shrkn_p1", 25)
                .UltraItem("nov_duel_shrkn_p2", 25)
                .Component("titan_plate", 3)
                .Component("quantum_proc", 1)
                .Component("quantmyst_core", 1);
        }

        private void YagudoCryoShuriken()
        {
            _builder.Create(RecipeType.YagudoCryoShuriken, SkillType.Weaponcraft)
                .Level(91)
                .Category(RecipeCategoryType.Throwing)
                .NormalItem("yagudo_cryo_shr", 25)
                .Component("titan_plate", 3)
                .Component("quantum_proc", 1)
                .Component("quantmyst_core", 1);
        }

        private void CometFangShuriken()
        {
            _builder.Create(RecipeType.CometFangShuriken, SkillType.Weaponcraft)
                .Level(99)
                .Category(RecipeCategoryType.Throwing)
                .NormalItem("cometfang_shrkn", 25)
                .Component("titan_plate", 3)
                .Component("quantum_proc", 1)
                .Component("quantmyst_core", 1);
        }
    }
}