using System.Collections.Generic;
using Anvil.Services;
using XM.Progression.Skill;

namespace XM.Plugin.Craft.RecipeDefinition.Engineering
{
    [ServiceBinding(typeof(IRecipeListDefinition))]
    internal class PistolRecipes : IRecipeListDefinition
    {
        private readonly RecipeBuilder _builder = new();

        public Dictionary<RecipeType, RecipeDetail> BuildRecipes()
        {
            // Pistols ordered by level (3-97)
            ArcstrikePistol();          // Level 3
            LegionnairesRepeater();     // Level 21
            VanguardRepeater();         // Level 24
            BastionRepeater();          // Level 31
            ZamburakAuto();            // Level 61
            RikonodoStriker();         // Level 73
            HuntersFang();             // Level 80
            TellsMarksman();           // Level 88
            TrackersSidearm();         // Level 91
            EtherboltArbalest();       // Level 97

            return _builder.Build();
        }

        private void ArcstrikePistol()
        {
            _builder.Create(RecipeType.ArcstrikePistol, SkillType.Engineering)
                .Level(3)
                .Category(RecipeCategoryType.Pistol)
                .NormalItem("arcstrike_pistol", 1)
                .HQItem("arcstrike_pistol", 1)
                .Component("brass_sheet", 2)
                .Component("circuit_matrix", 1)
                .Component("power_cell", 1)
                .Component("bond_agent", 1);
        }

        private void LegionnairesRepeater()
        {
            _builder.Create(RecipeType.LegionnairesRepeater, SkillType.Engineering)
                .Level(21)
                .Category(RecipeCategoryType.Pistol)
                .NormalItem("legionnaires_rep", 1)
                .Component("ferrite_core", 2)
                .Component("power_cell", 1)
                .Component("servo_motor", 1)
                .Component("enhance_serum", 1);
        }

        private void VanguardRepeater()
        {
            _builder.Create(RecipeType.VanguardRepeater, SkillType.Engineering)
                .Level(24)
                .Category(RecipeCategoryType.Pistol)
                .NormalItem("vanguard_repeate", 1)
                .HQItem("vanguard_repeate", 1)
                .Component("ferrite_core", 2)
                .Component("power_cell", 2)
                .Component("circuit_matrix", 1)
                .Component("enhance_serum", 1);
        }

        private void BastionRepeater()
        {
            _builder.Create(RecipeType.BastionRepeater, SkillType.Engineering)
                .Level(31)
                .Category(RecipeCategoryType.Pistol)
                .NormalItem("bastion_repeater", 1)
                .HQItem("republic_handcan", 1)  // Special case: HQ is "Republic Handcannon"
                .Component("darksteel_bar", 2)
                .Component("neural_inter", 1)
                .Component("power_cell", 2)
                .Component("techno_fiber", 1);
        }

        private void ZamburakAuto()
        {
            _builder.Create(RecipeType.ZamburakAuto, SkillType.Engineering)
                .Level(61)
                .Category(RecipeCategoryType.Pistol)
                .NormalItem("zamburak_auto", 1)
                .HQItem("zamburak_auto", 1)
                .Component("titan_plate", 2)
                .Component("quantum_proc", 1)
                .Component("phase_crystal", 2)
                .Component("biosteel_comp", 1);
        }

        private void RikonodoStriker()
        {
            _builder.Create(RecipeType.RikonodoStriker, SkillType.Engineering)
                .Level(73)
                .Category(RecipeCategoryType.Pistol)
                .NormalItem("rikonodo_striker", 1)
                .Component("titan_plate", 2)
                .Component("quantum_proc", 2)
                .Component("phase_crystal", 1)
                .Component("biosteel_comp", 1)
                .Component("ethertech_int", 1);
        }

        private void HuntersFang()
        {
            _builder.Create(RecipeType.HuntersFang, SkillType.Engineering)
                .Level(80)
                .Category(RecipeCategoryType.Pistol)
                .NormalItem("hunters_fang", 1)
                .Component("quantum_steel", 2)
                .Component("temporal_flux", 1)
                .Component("biosteel_comp", 1)
                .Component("ethertech_int", 1)
                .Component("nano_enchant", 1);
        }

        private void TellsMarksman()
        {
            _builder.Create(RecipeType.TellsMarksman, SkillType.Engineering)
                .Level(88)
                .Category(RecipeCategoryType.Pistol)
                .NormalItem("tells_marksman", 1)
                .Component("quantum_steel", 2)
                .Component("temporal_flux", 1)
                .Component("biosteel_comp", 1)
                .Component("ethertech_int", 2)
                .Component("nano_enchant", 1);
        }

        private void TrackersSidearm()
        {
            _builder.Create(RecipeType.TrackersSidearm, SkillType.Engineering)
                .Level(91)
                .Category(RecipeCategoryType.Pistol)
                .NormalItem("trackers_sidearm", 1)
                .HQItem("trackers_sidearm", 1)
                .Component("quantum_steel", 2)
                .Component("temporal_flux", 1)
                .Component("biosteel_comp", 1)
                .Component("ethertech_int", 1)
                .Component("nano_enchant", 2);
        }

        private void EtherboltArbalest()
        {
            _builder.Create(RecipeType.EtherboltArbalest, SkillType.Engineering)
                .Level(97)
                .Category(RecipeCategoryType.Pistol)
                .NormalItem("etherbolt_arbale", 1)
                .HQItem("etherbolt_arbale", 1)
                .Component("quantum_steel", 2)
                .Component("temporal_flux", 2)
                .Component("biosteel_comp", 1)
                .Component("ethertech_int", 2)
                .Component("nano_enchant", 1)
                .Component("sync_core", 1);
        }
    }
}