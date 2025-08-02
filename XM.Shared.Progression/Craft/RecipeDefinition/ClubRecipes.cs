using System.Collections.Generic;
using Anvil.Services;
using XM.Progression.Skill;

namespace XM.Progression.Craft.RecipeDefinition
{
    [ServiceBinding(typeof(IRecipeListDefinition))]
    internal class ClubRecipes : IRecipeListDefinition
    {
        private readonly RecipeBuilder _builder = new();

        public Dictionary<RecipeType, RecipeDetail> BuildRecipes()
        {
            BuildClubRecipes();
            return _builder.Build();
        }

        private void BuildClubRecipes()
        {
            // Level 2
            _builder.Create(RecipeType.AshStriker, SkillType.Weaponcraft)
                .Level(2)
                .Category(RecipeCategoryType.Club)
                .NormalItem("ash_striker", 1)
                .HQItem("ash_striker_p1", 1);

            // Level 3
            _builder.Create(RecipeType.MapleConduit, SkillType.Weaponcraft)
                .Level(3)
                .Category(RecipeCategoryType.Club)
                .NormalItem("maple_conduit", 1)
                .HQItem("maple_conduit_p1", 1);

            // Level 5
            _builder.Create(RecipeType.AurionAlloyMace, SkillType.Weaponcraft)
                .Level(5)
                .Category(RecipeCategoryType.Club)
                .NormalItem("aurion_mace", 1)
                .HQItem("aurion_mace_p1", 1);

            // Level 6
            _builder.Create(RecipeType.AurionAlloyHammer, SkillType.Weaponcraft)
                .Level(6)
                .Category(RecipeCategoryType.Club)
                .NormalItem("aurion_hammer", 1)
                .HQItem("aurion_hammer_p1", 1);

            // Level 11
            _builder.Create(RecipeType.AurionAlloyRod, SkillType.Weaponcraft)
                .Level(11)
                .Category(RecipeCategoryType.Club)
                .NormalItem("aurion_rod", 1)
                .HQItem("aurion_rod_p1", 1);

            // Level 19
            _builder.Create(RecipeType.WillowConduit, SkillType.Weaponcraft)
                .Level(19)
                .Category(RecipeCategoryType.Club)
                .NormalItem("willow_condui", 1)
                .HQItem("willow_condui_p1", 1);

            // Level 23
            _builder.Create(RecipeType.BrassforceRod, SkillType.Weaponcraft)
                .Level(23)
                .Category(RecipeCategoryType.Club)
                .NormalItem("brassforce_rd", 1)
                .HQItem("brassforce_rd_p1", 1);

            // Level 24
            _builder.Create(RecipeType.BrassstrikeHammer, SkillType.Weaponcraft)
                .Level(24)
                .Category(RecipeCategoryType.Club)
                .NormalItem("brass_hamstrk", 1)
                .HQItem("brass_hamstrk_p1", 1);

            // Level 36
            _builder.Create(RecipeType.YewConduit, SkillType.Weaponcraft)
                .Level(36)
                .Category(RecipeCategoryType.Club)
                .NormalItem("yew_conduit", 1)
                .HQItem("yew_conduit_p1", 1);

            // Level 37
            _builder.Create(RecipeType.TitanMace, SkillType.Weaponcraft)
                .Level(37)
                .Category(RecipeCategoryType.Club)
                .NormalItem("titan_mace", 1)
                .HQItem("titan_mace_p1", 1);

            // Level 40
            _builder.Create(RecipeType.WarbringerHammer, SkillType.Weaponcraft)
                .Level(40)
                .Category(RecipeCategoryType.Club)
                .NormalItem("warbrng_hamm", 1)
                .HQItem("warbrng_hamm_p1", 1);

            // Level 41
            _builder.Create(RecipeType.ArcaneRod, SkillType.Weaponcraft)
                .Level(41)
                .Category(RecipeCategoryType.Club)
                .NormalItem("arcane_rod", 1)
                .HQItem("arcane_rod_p1", 1);

            // Level 48
            _builder.Create(RecipeType.EremitesEtherWand, SkillType.Weaponcraft)
                .Level(48)
                .Category(RecipeCategoryType.Club)
                .NormalItem("erem_etherwnd", 1)
                .HQItem("erem_etherwnd_p1", 1);

            // Level 49
            _builder.Create(RecipeType.BonecrusherCudgel, SkillType.Weaponcraft)
                .Level(49)
                .Category(RecipeCategoryType.Club)
                .NormalItem("bonecrusher_cdgl", 1)
                .HQItem("bonecrusher_cdgl_p1", 1);

            // Level 52
            _builder.Create(RecipeType.SpikedMaul, SkillType.Weaponcraft)
                .Level(52)
                .Category(RecipeCategoryType.Club)
                .NormalItem("spiked_maul", 1)
                .HQItem("spiked_maul_p1", 1);

            // Level 62
            _builder.Create(RecipeType.TitanMaul, SkillType.Weaponcraft)
                .Level(62)
                .Category(RecipeCategoryType.Club)
                .NormalItem("titan_maul", 1)
                .HQItem("titan_maul_p1", 1);

            // Level 68
            _builder.Create(RecipeType.MythriteRod, SkillType.Weaponcraft)
                .Level(68)
                .Category(RecipeCategoryType.Club)
                .NormalItem("mythrite_rod", 1)
                .HQItem("mythrite_rod_p1", 1);

            // Level 70
            _builder.Create(RecipeType.MythriteMace, SkillType.Weaponcraft)
                .Level(70)
                .Category(RecipeCategoryType.Club)
                .NormalItem("mythrite_mac", 1)
                .HQItem("mythrite_mac_p1", 1);

            // Level 72
            _builder.Create(RecipeType.OakfangCudgel, SkillType.Weaponcraft)
                .Level(72)
                .Category(RecipeCategoryType.Club)
                .NormalItem("oakfang_cdgl", 1)
                .HQItem("oakfang_cdgl_p1", 1);

            // Level 77
            _builder.Create(RecipeType.HallowedMaul, SkillType.Weaponcraft)
                .Level(77)
                .Category(RecipeCategoryType.Club)
                .NormalItem("hallowed_maul", 1)
                .HQItem("hallowed_maul_p1", 1);

            // Level 82
            _builder.Create(RecipeType.TitanClub, SkillType.Weaponcraft)
                .Level(82)
                .Category(RecipeCategoryType.Club)
                .NormalItem("titan_club", 1)
                .HQItem("titan_club_p1", 1);

            // Level 86
            _builder.Create(RecipeType.BoneforgedRod, SkillType.Weaponcraft)
                .Level(86)
                .Category(RecipeCategoryType.Club)
                .NormalItem("boneforged_rd", 1)
                .HQItem("boneforged_rd_p1", 1);

            // Level 88
            _builder.Create(RecipeType.HallowedMace, SkillType.Weaponcraft)
                .Level(88)
                .Category(RecipeCategoryType.Club)
                .NormalItem("hallowed_mac", 1)
                .HQItem("hallowed_mac_p1", 1);

            // Level 97
            _builder.Create(RecipeType.TacticianMagiciansConduit, SkillType.Weaponcraft)
                .Level(97)
                .Category(RecipeCategoryType.Club)
                .NormalItem("tact_mag_cndt", 1);
        }
    }
} 