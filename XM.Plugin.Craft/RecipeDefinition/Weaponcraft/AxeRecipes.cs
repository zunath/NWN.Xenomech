using System.Collections.Generic;
using Anvil.Services;
using XM.Progression.Skill;

namespace XM.Plugin.Craft.RecipeDefinition.Weaponcraft
{
    [ServiceBinding(typeof(IRecipeListDefinition))]
    internal class AxeRecipes : IRecipeListDefinition
    {
        private readonly RecipeBuilder _builder = new();

        public Dictionary<RecipeType, RecipeDetail> BuildRecipes()
        {
            // Axes ordered by level (7-99)
            AurionAlloyAxe();            // Level 7
            BrassstrikeAxe();            // Level 19
            BonecleaverAxe();            // Level 28
            BoneforgedPick();            // Level 38
            TitanBattleaxe();            // Level 42
            RaidersTomahawk();           // Level 53
            WarbornPick();               // Level 63
            MythriteAxe();               // Level 74
            WarcasterAxe();              // Level 85
            VeldtCleaver();              // Level 87
            ReaversTabar();              // Level 92
            MythritePick();              // Level 99

            return _builder.Build();
        }

        private void AurionAlloyAxe()
        {
            _builder.Create(RecipeType.AurionAlloyAxe, SkillType.Weaponcraft)
                .Level(7)
                .Category(RecipeCategoryType.Axe)
                .NormalItem("aurion_axe", 1)
                .HQItem("aurion_axe_p1", 1)
                .Component("aurion_ingot", 3)
                .Component("beast_hide", 1)
                .Component("flux_compound", 1);
        }

        private void BrassstrikeAxe()
        {
            _builder.Create(RecipeType.BrassstrikeAxe, SkillType.Weaponcraft)
                .Level(19)
                .Category(RecipeCategoryType.Axe)
                .NormalItem("brass_axestrk", 1)
                .HQItem("brass_axestrk_p1", 1)
                .Component("ferrite_core", 3)
                .Component("circuit_matrix", 1)
                .Component("power_cell", 1);
        }

        private void BonecleaverAxe()
        {
            _builder.Create(RecipeType.BonecleaverAxe, SkillType.Weaponcraft)
                .Level(28)
                .Category(RecipeCategoryType.Axe)
                .NormalItem("bonecleav_axe", 1)
                .HQItem("bonecleav_axe_p1", 1)
                .Component("brass_sheet", 3)
                .Component("mythrite_frag", 2)
                .Component("enhance_serum", 1);
        }

        private void BoneforgedPick()
        {
            _builder.Create(RecipeType.BoneforgedPick, SkillType.Weaponcraft)
                .Level(38)
                .Category(RecipeCategoryType.Axe)
                .NormalItem("boneforg_pick", 1)
                .HQItem("boneforg_pick_p1", 1)
                .Component("brass_sheet", 3)
                .Component("mythrite_frag", 2)
                .Component("enhance_serum", 1);
        }

        private void TitanBattleaxe()
        {
            _builder.Create(RecipeType.TitanBattleaxe, SkillType.Weaponcraft)
                .Level(42)
                .Category(RecipeCategoryType.Axe)
                .NormalItem("titan_battleax", 1)
                .HQItem("titan_battlax_p1", 1)
                .Component("brass_sheet", 3)
                .Component("mythrite_frag", 2)
                .Component("enhance_serum", 1);
        }

        private void RaidersTomahawk()
        {
            _builder.Create(RecipeType.RaidersTomahawk, SkillType.Weaponcraft)
                .Level(53)
                .Category(RecipeCategoryType.Axe)
                .NormalItem("raider_tomhwk", 1)
                .HQItem("raider_tomhwk_p1", 1)
                .Component("mythrite_frag", 4)
                .Component("psi_crystal", 2)
                .Component("biosteel_comp", 1);
        }

        private void WarbornPick()
        {
            _builder.Create(RecipeType.WarbornPick, SkillType.Weaponcraft)
                .Level(63)
                .Category(RecipeCategoryType.Axe)
                .NormalItem("warborn_pick", 1)
                .HQItem("warborn_pick_p1", 1)
                .Component("mythrite_frag", 4)
                .Component("psi_crystal", 2)
                .Component("biosteel_comp", 1);
        }

        private void MythriteAxe()
        {
            _builder.Create(RecipeType.MythriteAxe, SkillType.Weaponcraft)
                .Level(74)
                .Category(RecipeCategoryType.Axe)
                .NormalItem("mythrite_axe", 1)
                .HQItem("mythrite_axe_p1", 1)
                .Component("mythrite_frag", 4)
                .Component("psi_crystal", 2)
                .Component("biosteel_comp", 1);
        }

        private void WarcasterAxe()
        {
            _builder.Create(RecipeType.WarcasterAxe, SkillType.Weaponcraft)
                .Level(85)
                .Category(RecipeCategoryType.Axe)
                .NormalItem("warcast_axe", 1)
                .HQItem("warcast_axe_p1", 1)
                .UltraItem("warcast_axe_p2", 1)
                .Component("titan_plate", 4)
                .Component("quantum_proc", 2)
                .Component("quantmyst_core", 1)
                .Component("power_cell", 1);
        }

        private void VeldtCleaver()
        {
            _builder.Create(RecipeType.VeldtCleaver, SkillType.Weaponcraft)
                .Level(87)
                .Category(RecipeCategoryType.Axe)
                .NormalItem("veldt_clvr", 1)
                .HQItem("veldt_clvr_p1", 1)
                .Component("titan_plate", 4)
                .Component("quantum_proc", 2)
                .Component("quantmyst_core", 1);
        }

        private void ReaversTabar()
        {
            _builder.Create(RecipeType.ReaversTabar, SkillType.Weaponcraft)
                .Level(92)
                .Category(RecipeCategoryType.Axe)
                .NormalItem("reaver_tabar", 1)
                .HQItem("reaver_tabr_p1", 1)
                .Component("titan_plate", 4)
                .Component("quantum_proc", 2)
                .Component("quantmyst_core", 1);
        }

        private void MythritePick()
        {
            _builder.Create(RecipeType.MythritePick, SkillType.Weaponcraft)
                .Level(99)
                .Category(RecipeCategoryType.Axe)
                .NormalItem("mythrite_pick", 1)
                .HQItem("mythrite_pck_p1", 1)
                .Component("titan_plate", 4)
                .Component("quantum_proc", 2)
                .Component("quantmyst_core", 1);
        }
    }
}