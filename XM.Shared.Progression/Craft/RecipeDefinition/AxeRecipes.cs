using System.Collections.Generic;
using Anvil.Services;
using XM.Progression.Skill;

namespace XM.Progression.Craft.RecipeDefinition
{
    [ServiceBinding(typeof(IRecipeListDefinition))]
    internal class AxeRecipes : IRecipeListDefinition
    {
        private readonly RecipeBuilder _builder = new();

        public Dictionary<RecipeType, RecipeDetail> BuildRecipes()
        {
            BuildAxeRecipes();
            return _builder.Build();
        }

        private void BuildAxeRecipes()
        {
            // Level 7
            _builder.Create(RecipeType.AurionAlloyAxe, SkillType.Weaponcraft)
                .Level(7)
                .Category(RecipeCategoryType.Axe)
                .NormalItem("aurion_axe", 1)
                .HQItem("aurion_axe_p1", 1);

            // Level 19
            _builder.Create(RecipeType.BrassstrikeAxe, SkillType.Weaponcraft)
                .Level(19)
                .Category(RecipeCategoryType.Axe)
                .NormalItem("brass_axestrk", 1)
                .HQItem("brass_axestrk_p1", 1);

            // Level 28
            _builder.Create(RecipeType.BonecleaverAxe, SkillType.Weaponcraft)
                .Level(28)
                .Category(RecipeCategoryType.Axe)
                .NormalItem("bonecleav_axe", 1)
                .HQItem("bonecleav_axe_p1", 1);

            // Level 38
            _builder.Create(RecipeType.BoneforgedPick, SkillType.Weaponcraft)
                .Level(38)
                .Category(RecipeCategoryType.Axe)
                .NormalItem("boneforg_pick", 1)
                .HQItem("boneforg_pick_p1", 1);

            // Level 42
            _builder.Create(RecipeType.TitanBattleaxe, SkillType.Weaponcraft)
                .Level(42)
                .Category(RecipeCategoryType.Axe)
                .NormalItem("titan_battleax", 1)
                .HQItem("titan_battlax_p1", 1);

            // Level 53
            _builder.Create(RecipeType.RaidersTomahawk, SkillType.Weaponcraft)
                .Level(53)
                .Category(RecipeCategoryType.Axe)
                .NormalItem("raider_tomhwk", 1)
                .HQItem("raider_tomhwk_p1", 1);

            // Level 63
            _builder.Create(RecipeType.WarbornPick, SkillType.Weaponcraft)
                .Level(63)
                .Category(RecipeCategoryType.Axe)
                .NormalItem("warborn_pick", 1)
                .HQItem("warborn_pick_p1", 1);

            // Level 74
            _builder.Create(RecipeType.MythriteAxe, SkillType.Weaponcraft)
                .Level(74)
                .Category(RecipeCategoryType.Axe)
                .NormalItem("mythrite_axe", 1)
                .HQItem("mythrite_axe_p1", 1);

            // Level 85
            _builder.Create(RecipeType.WarcastersAxe, SkillType.Weaponcraft)
                .Level(85)
                .Category(RecipeCategoryType.Axe)
                .NormalItem("warcast_axe", 1)
                .HQItem("warcast_axe_p1", 1)
                .UltraItem("warcast_axe_p2", 1);

            // Level 87
            _builder.Create(RecipeType.VeldtCleaver, SkillType.Weaponcraft)
                .Level(87)
                .Category(RecipeCategoryType.Axe)
                .NormalItem("veldt_clvr", 1)
                .HQItem("veldt_clvr_p1", 1);

            // Level 92
            _builder.Create(RecipeType.ReaversTabar, SkillType.Weaponcraft)
                .Level(92)
                .Category(RecipeCategoryType.Axe)
                .NormalItem("reaver_tabar", 1)
                .HQItem("reaver_tabr_p1", 1);

            // Level 99
            _builder.Create(RecipeType.MythritePick, SkillType.Weaponcraft)
                .Level(99)
                .Category(RecipeCategoryType.Axe)
                .NormalItem("mythrite_pick", 1)
                .HQItem("mythrite_pck_p1", 1);
        }
    }
} 