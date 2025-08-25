using System.Collections.Generic;
using Anvil.Services;
using XM.Progression.Skill;

namespace XM.Plugin.Craft.RecipeDefinition.Weaponcraft
{
    [ServiceBinding(typeof(IRecipeListDefinition))]
    internal class ClubRecipes : IRecipeListDefinition
    {
        private readonly RecipeBuilder _builder = new();

        public Dictionary<RecipeType, RecipeDetail> BuildRecipes()
        {
            // Clubs ordered by level (2-97)
            AshStriker();                      // Level 2
            MapleConduit();                    // Level 3
            AurionAlloyMace();                 // Level 5
            AurionAlloyHammer();               // Level 6
            AurionAlloyRod();                  // Level 11
            WillowConduit();                   // Level 19
            BrassforceRod();                   // Level 23
            BrassstrikeHammer();               // Level 24
            YewConduit();                      // Level 36
            TitanMace();                       // Level 37
            WarbringerHammer();                // Level 40
            ArcaneRod();                       // Level 41
            EremitesEtherWand();               // Level 48
            BonecrusherCudgel();               // Level 49
            TitanMaul();                       // Level 62
            MythriteRod();                     // Level 68
            MythriteMace();                    // Level 70
            OakfangCudgel();                   // Level 72
            HallowedMaul();                    // Level 77
            TitanClub();                       // Level 82
            BoneforgedRod();                   // Level 86
            HallowedMace();                    // Level 88
            RosethornConduit();                // Level 94
            TacticianMagiciansConduit();       // Level 97

            return _builder.Build();
        }

        private void AshStriker()
        {
            _builder.Create(RecipeType.AshStriker, SkillType.Weaponcraft)
                .Level(2)
                .Category(RecipeCategoryType.Club)
                .NormalItem("ash_striker", 1)
                .HQItem("ash_striker_p1", 1)
                .Component("aurion_ingot", 3)
                .Component("beast_hide", 1)
                .Component("flux_compound", 1);
        }

        private void MapleConduit()
        {
            _builder.Create(RecipeType.MapleConduit, SkillType.Weaponcraft)
                .Level(3)
                .Category(RecipeCategoryType.Club)
                .NormalItem("maple_conduit", 1)
                .HQItem("maple_conduit_p1", 1)
                .Component("living_wood", 3)
                .Component("ether_crystal", 1)
                .Component("flux_compound", 1);
        }

        private void AurionAlloyMace()
        {
            _builder.Create(RecipeType.AurionAlloyMace, SkillType.Weaponcraft)
                .Level(5)
                .Category(RecipeCategoryType.Club)
                .NormalItem("aurion_mace", 1)
                .HQItem("aurion_mace_p1", 1)
                .Component("aurion_ingot", 3)
                .Component("beast_hide", 1)
                .Component("flux_compound", 1);
        }

        private void AurionAlloyHammer()
        {
            _builder.Create(RecipeType.AurionAlloyHammer, SkillType.Weaponcraft)
                .Level(6)
                .Category(RecipeCategoryType.Club)
                .NormalItem("aurion_hammer", 1)
                .HQItem("aurion_hammer_p1", 1)
                .Component("aurion_ingot", 3)
                .Component("beast_hide", 1)
                .Component("flux_compound", 1);
        }

        private void AurionAlloyRod()
        {
            _builder.Create(RecipeType.AurionAlloyRod, SkillType.Weaponcraft)
                .Level(11)
                .Category(RecipeCategoryType.Club)
                .NormalItem("aurion_rod", 1)
                .HQItem("aurion_rod_p1", 1)
                .Component("aurion_ingot", 3)
                .Component("beast_hide", 1)
                .Component("flux_compound", 1);
        }

        private void WillowConduit()
        {
            _builder.Create(RecipeType.WillowConduit, SkillType.Weaponcraft)
                .Level(19)
                .Category(RecipeCategoryType.Club)
                .NormalItem("willow_condui", 1)
                .HQItem("willow_condui_p1", 1)
                .Component("living_wood", 3)
                .Component("ferrite_core", 1)
                .Component("ether_crystal", 1);
        }

        private void BrassforceRod()
        {
            _builder.Create(RecipeType.BrassforceRod, SkillType.Weaponcraft)
                .Level(23)
                .Category(RecipeCategoryType.Club)
                .NormalItem("brassforce_rd", 1)
                .HQItem("brassforce_rd_p1", 1)
                .Component("ferrite_core", 3)
                .Component("circuit_matrix", 2)
                .Component("power_cell", 1);
        }

        private void BrassstrikeHammer()
        {
            _builder.Create(RecipeType.BrassstrikeHammer, SkillType.Weaponcraft)
                .Level(24)
                .Category(RecipeCategoryType.Club)
                .NormalItem("brass_hamstrk", 1)
                .HQItem("brass_hamstrk_p1", 1)
                .Component("ferrite_core", 3)
                .Component("circuit_matrix", 2)
                .Component("power_cell", 1);
        }

        private void YewConduit()
        {
            _builder.Create(RecipeType.YewConduit, SkillType.Weaponcraft)
                .Level(36)
                .Category(RecipeCategoryType.Club)
                .NormalItem("yew_conduit", 1)
                .HQItem("yew_conduit_p1", 1)
                .Component("living_wood", 4)
                .Component("brass_sheet", 2)
                .Component("ether_crystal", 1);
        }

        private void TitanMace()
        {
            _builder.Create(RecipeType.TitanMace, SkillType.Weaponcraft)
                .Level(37)
                .Category(RecipeCategoryType.Club)
                .NormalItem("titan_mace", 1)
                .HQItem("titan_mace_p1", 1)
                .Component("brass_sheet", 3)
                .Component("mythrite_frag", 2)
                .Component("enhance_serum", 1);
        }

        private void WarbringerHammer()
        {
            _builder.Create(RecipeType.WarbringerHammer, SkillType.Weaponcraft)
                .Level(40)
                .Category(RecipeCategoryType.Club)
                .NormalItem("warbrng_hamm", 1)
                .HQItem("warbrng_hamm_p1", 1)
                .Component("brass_sheet", 3)
                .Component("mythrite_frag", 2)
                .Component("enhance_serum", 1);
        }

        private void ArcaneRod()
        {
            _builder.Create(RecipeType.ArcaneRod, SkillType.Weaponcraft)
                .Level(41)
                .Category(RecipeCategoryType.Club)
                .NormalItem("arcane_rod", 1)
                .HQItem("arcane_rod_p1", 1)
                .Component("brass_sheet", 3)
                .Component("mythrite_frag", 2)
                .Component("enhance_serum", 1)
                .Component("power_cell", 1);
        }

        private void EremitesEtherWand()
        {
            _builder.Create(RecipeType.EremitesEtherWand, SkillType.Weaponcraft)
                .Level(48)
                .Category(RecipeCategoryType.Club)
                .NormalItem("erem_etherwnd", 1)
                .HQItem("erem_etherwnd_p1", 1)
                .Component("mythrite_frag", 4)
                .Component("psi_crystal", 2)
                .Component("purify_filter", 1)
                .Component("ether_crystal", 1);
        }

        private void BonecrusherCudgel()
        {
            _builder.Create(RecipeType.BonecrusherCudgel, SkillType.Weaponcraft)
                .Level(49)
                .Category(RecipeCategoryType.Club)
                .NormalItem("bonecrusher_cdgl", 1)
                .HQItem("bonecrusher_cdgl_p1", 1)
                .Component("mythrite_frag", 4)
                .Component("psi_crystal", 2)
                .Component("purify_filter", 1);
        }

        private void TitanMaul()
        {
            _builder.Create(RecipeType.TitanMaul, SkillType.Weaponcraft)
                .Level(62)
                .Category(RecipeCategoryType.Club)
                .NormalItem("titan_maul", 1)
                .HQItem("titan_maul_p1", 1)
                .Component("mythrite_frag", 4)
                .Component("psi_crystal", 2)
                .Component("purify_filter", 1);
        }

        private void MythriteRod()
        {
            _builder.Create(RecipeType.MythriteRod, SkillType.Weaponcraft)
                .Level(68)
                .Category(RecipeCategoryType.Club)
                .NormalItem("mythrite_rod", 1)
                .HQItem("mythrite_rod_p1", 1)
                .Component("mythrite_frag", 4)
                .Component("psi_crystal", 2)
                .Component("purify_filter", 1);
        }

        private void MythriteMace()
        {
            _builder.Create(RecipeType.MythriteMace, SkillType.Weaponcraft)
                .Level(70)
                .Category(RecipeCategoryType.Club)
                .NormalItem("mythrite_mac", 1)
                .HQItem("mythrite_mac_p1", 1)
                .Component("mythrite_frag", 4)
                .Component("psi_crystal", 2)
                .Component("purify_filter", 1);
        }

        private void OakfangCudgel()
        {
            _builder.Create(RecipeType.OakfangCudgel, SkillType.Weaponcraft)
                .Level(72)
                .Category(RecipeCategoryType.Club)
                .NormalItem("oakfang_cdgl", 1)
                .HQItem("oakfang_cdgl_p1", 1)
                .Component("mythrite_frag", 4)
                .Component("psi_crystal", 2)
                .Component("purify_filter", 1);
        }

        private void HallowedMaul()
        {
            _builder.Create(RecipeType.HallowedMaul, SkillType.Weaponcraft)
                .Level(77)
                .Category(RecipeCategoryType.Club)
                .NormalItem("hallowed_maul", 1)
                .HQItem("hallowed_maul_p1", 1)
                .Component("titan_plate", 4)
                .Component("quantum_proc", 2)
                .Component("sync_core", 1);
        }

        private void TitanClub()
        {
            _builder.Create(RecipeType.TitanClub, SkillType.Weaponcraft)
                .Level(82)
                .Category(RecipeCategoryType.Club)
                .NormalItem("titan_club", 1)
                .HQItem("titan_club_p1", 1)
                .Component("titan_plate", 4)
                .Component("quantum_proc", 2)
                .Component("sync_core", 1);
        }

        private void BoneforgedRod()
        {
            _builder.Create(RecipeType.BoneforgedRod, SkillType.Weaponcraft)
                .Level(86)
                .Category(RecipeCategoryType.Club)
                .NormalItem("boneforged_rd", 1)
                .HQItem("boneforged_rd_p1", 1)
                .Component("titan_plate", 4)
                .Component("quantum_proc", 2)
                .Component("sync_core", 1);
        }

        private void HallowedMace()
        {
            _builder.Create(RecipeType.HallowedMace, SkillType.Weaponcraft)
                .Level(88)
                .Category(RecipeCategoryType.Club)
                .NormalItem("hallowed_mac", 1)
                .HQItem("hallowed_mac_p1", 1)
                .Component("titan_plate", 4)
                .Component("quantum_proc", 2)
                .Component("sync_core", 1);
        }

        private void RosethornConduit()
        {
            _builder.Create(RecipeType.RosethornConduit, SkillType.Weaponcraft)
                .Level(94)
                .Category(RecipeCategoryType.Club)
                .NormalItem("rosethorn_cndt", 1)
                .HQItem("rosethorn_cndt_p1", 1)
                .Component("living_wood", 5)
                .Component("titan_plate", 2)
                .Component("ether_crystal", 2);
        }

        private void TacticianMagiciansConduit()
        {
            _builder.Create(RecipeType.TacticianMagiciansConduit, SkillType.Weaponcraft)
                .Level(97)
                .Category(RecipeCategoryType.Club)
                .NormalItem("tact_mag_cndt", 1)
                .Component("living_wood", 5)
                .Component("titan_plate", 2)
                .Component("ether_crystal", 2)
                .Component("spirit_ess", 1);
        }
    }
}